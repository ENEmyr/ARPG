using System.Collections.Generic;
using UnityEngine;
using ItemSystem;

namespace CharacterSystem
{
    public class CharacterStats : MonoBehaviour
    {
        public string Name;
        public float HP;
        public float MP;
        public float MaxHP;
        public float MaxMP;
        public Stat PhysicalResist;
        public Stat MagicalResist;
        public Stat STR;
        public Stat INT;
        public List<Stat> AbnormalStat;
        public List<Skill> Skills;
        public List<Consumable> temporalAffect = new List<Consumable>();
        public bool IsDead = false;

        public virtual void Awake() { /* Init Stats */ }

        public virtual void Start() { }

        public virtual void Die(EnumRef.DamageType dmgType)
        {
            this.IsDead = true;
            this.temporalAffect = new List<Consumable>();
            Debug.Log(this.Name + "is dying.");
        }

        public void Update()
        {
            if (!this.IsDead)
            {
                if (this.HP < this.MaxHP)
                {
                    this.HP += Time.deltaTime;
                    this.HP = Mathf.Clamp(this.HP, 0, this.MaxHP);
                }
                if (this.MP < this.MaxMP)
                {
                    this.MP += Time.deltaTime + ((this.STR.Value + this.INT.Value) * .1f) * Time.deltaTime; // 10% of STR+INT will be use to increase MP restoration rate/second
                    this.MP = Mathf.Clamp(this.MP, 0, this.MaxMP);
                }
                foreach (Consumable item in this.temporalAffect)
                {
                    item.AffectTime -= Time.deltaTime;
                }
                passiveAffect();
            }
        }

        public float ReceiveDamage(float dmg, EnumRef.DamageType dmgType)
        {
            float remainDmg;
            if (dmgType == EnumRef.DamageType.Physical || dmgType == EnumRef.DamageType.Environment)
                remainDmg = dmg - (PhysicalResist.Value * dmg);
            else if (dmgType == EnumRef.DamageType.Magical)
                remainDmg = dmg - (MagicalResist.Value * dmg);
            else
                remainDmg = dmg;
            remainDmg = Mathf.Clamp(remainDmg, 0, float.MaxValue);
            this.HP -= remainDmg;
            Debug.Log(transform.name + " takes " + remainDmg + " damage.");
            if (this.HP <= 0)
                Die(dmgType);
            return this.HP;
        }

        public void Affect(Consumable item, bool ignoreAffectTime = false)
        {
            if (item.AffectTime != 0f && !ignoreAffectTime)
                temporalAffect.Add(item);
            switch (item.BuffType)
            {
                case EnumRef.BuffType.P_HP:
                    this.HP += item.AmountValue;
                    this.HP = Mathf.Clamp(this.HP, 0, this.MaxHP);
                    break;
                case EnumRef.BuffType.P_MP:
                    this.MP += item.AmountValue;
                    this.MP = Mathf.Clamp(this.MP, 0, this.MaxMP);
                    break;
                // TODO: implement other BuffType
                // case EnumRef.BuffType.P_HPMax:
                //     this.MaxHP = Mathf.Clamp(this.MaxHP + item.AmountValue, this.MaxHP + item.AmountValue, float.MaxValue);
                //     break;
                // case EnumRef.BuffType.P_MPMax:
                //     this.MaxMP = Mathf.Clamp(this.MaxMP + item.AmountValue, this.MaxMP + item.AmountValue, float.MaxValue);
                //     break;
                default:
                    Debug.Log("The BuffType is not currently supported");
                    break;
            }
        }

        private void passiveAffect()
        {
            List<Consumable> destroyList = new List<Consumable>();
            foreach (Consumable item in this.temporalAffect)
            {
                if (item.AffectTime <= 0)
                    destroyList.Add(item);
                Affect(item, true);
            }
            foreach (Consumable item in destroyList)
                this.temporalAffect.Remove(item);
        }
    }
}
