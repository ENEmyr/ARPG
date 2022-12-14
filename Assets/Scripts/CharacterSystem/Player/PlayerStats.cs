using UnityEngine;

namespace CharacterSystem
{
    public class PlayerStats : CharacterStats
    {
        public override void Awake()
        {
            this.HP = this.MaxHP;
            this.MP = this.MaxMP;
        }

        public override void Die(EnumRef.DamageType dmgType)
        {
            base.Die(dmgType);
            UIManager ui = GameObject.Find("GameManager").GetComponent<UIManager>();
            ui.ShowGameOverScreen();
        }
    }
}
