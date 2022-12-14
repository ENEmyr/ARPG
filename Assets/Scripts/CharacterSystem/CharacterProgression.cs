using System.Collections.Generic;
using UnityEngine;

namespace CharacterSystem
{
    public class CharacterProgression : MonoBehaviour
    {
        public event System.Action OnLevelUp;
        public EnumRef.CharacterType CharType = EnumRef.CharacterType.Player;
        public int Level = 0;
        public int PEXP { get; protected set; }
        public int MEXP { get; protected set; }
        public int EXPCap { get; protected set; }
        public float MaxHPModifier { get; protected set; }
        public float MaxMPModifier { get; protected set; }
        public float PhysicalResistModifier { get; protected set; }
        public float MagicalResistModifier { get; protected set; }
        public float STRModifier { get; protected set; }
        public float INTModifier { get; protected set; }
        [SerializeField]
        protected IDictionary<EnumRef.CharacterType, float> CharacterTypeMultiplier;
        [SerializeField]
        protected float ResistModifierPerLevel;
        [SerializeField]
        private static int s_statusPointPerLevel = 2;

        public virtual void Awake()
        {
            this.CharacterTypeMultiplier = new Dictionary<EnumRef.CharacterType, float>() {
              {EnumRef.CharacterType.Player, 1f},
              {EnumRef.CharacterType.Mob, 2f},
              {EnumRef.CharacterType.Boss, 5f},
              {EnumRef.CharacterType.Npc, 1.5f},
            };
            this.ResistModifierPerLevel = .01f;
            initLevel();
        }

        public virtual void Start() { }

        public void ReceiveEXP(int amount, EnumRef.DamageType dmgType)
        {
            if (dmgType == EnumRef.DamageType.Magical)
                this.MEXP += amount;
            else if (dmgType == EnumRef.DamageType.Physical)
                this.PEXP += amount;
            else
                return;

            if (this.PEXP + this.MEXP >= this.EXPCap)
                levelUp();
        }

        private int calculateEXPCapForLevel(int level)
        {
            return Mathf.RoundToInt((4 * Mathf.Pow(level, 3)) / 5);
        }

        private void levelUp()
        {
            float multiplier = this.CharacterTypeMultiplier[this.CharType];
            this.Level += 1;
            this.MaxHPModifier += 10 * multiplier;
            this.MaxMPModifier += 10 * multiplier;
            this.PhysicalResistModifier += this.ResistModifierPerLevel * multiplier;
            this.MagicalResistModifier += this.ResistModifierPerLevel * multiplier;
            this.STRModifier = s_statusPointPerLevel * (this.MEXP / this.EXPCap) * multiplier; // STR/INT will increase in proportion to the EXP gained for each DamageType
            this.INTModifier = s_statusPointPerLevel * (this.PEXP / this.EXPCap) * multiplier;
            this.EXPCap = calculateEXPCapForLevel(this.Level);
            this.MEXP = 0;
            this.PEXP = 0;
            if (this.OnLevelUp != null)
                this.OnLevelUp();
        }

        protected void initLevel()
        {
            this.PEXP = 1;
            this.MEXP = 1;
            this.EXPCap = 1;
            levelUp();
            this.MaxHPModifier *= this.Level - 1;
            this.MaxMPModifier *= this.Level - 1;
            this.PhysicalResistModifier *= this.Level - 1;
            this.MagicalResistModifier *= this.Level - 1;
            this.STRModifier = this.Level * this.CharacterTypeMultiplier[this.CharType];
            this.INTModifier = this.Level * this.CharacterTypeMultiplier[this.CharType];
            if (this.OnLevelUp != null)
                this.OnLevelUp();
        }
    }
}
