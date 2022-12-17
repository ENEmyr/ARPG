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
        public Stat MaxHP; 
        public Stat MaxMP; 
        public Stat PhysicalResist;
        public Stat MagicalResist;
        public Stat STR;
        public Stat INT;
        public List<Stat> AbnormalStat;
        public List<Skill> Skills;
        public IDictionary<Consumable, float> TemporalAffect = new Dictionary<Consumable, float>();
        public bool IsDead = false;

        public virtual void Awake() { /* Init Stats */ }

        public virtual void Start() {
            InvokeRepeating("passiveAffect", 1f, 1f); // repeat the method every 1s, 1s delay
        }

        public virtual void Die(EnumRef.DamageType dmgType)
        {
            this.IsDead = true;
            this.TemporalAffect = new Dictionary<Consumable, float>();
            Debug.Log(this.Name + "is dying.");
        }

        public void Update()
        {
            if (!this.IsDead)
            {
                if (this.HP < this.MaxHP.Value)
                {
                    this.HP += Time.deltaTime;
                    this.HP = Mathf.Clamp(this.HP, 0, this.MaxHP.Value);
                }
                if (this.MP < this.MaxMP.Value)
                {
                    this.MP += Time.deltaTime + ((this.STR.Value + this.INT.Value) * .1f) * Time.deltaTime; // 10% of STR+INT will be use to increase MP restoration rate/second
                    this.MP = Mathf.Clamp(this.MP, 0, this.MaxMP.Value);
                }
                //passiveAffect();
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
            {
                if (!this.TemporalAffect.ContainsKey(item))
                    this.TemporalAffect.Add(item, Time.time+item.AffectTime);
                else
                {
                    if (Time.time <= this.TemporalAffect[item])
                        this.TemporalAffect[item] += item.AffectTime;
                    else
                        this.TemporalAffect[item] = Time.time + item.AffectTime;
                }
            }
            switch (item.BuffType)
            {
                case EnumRef.BuffType.P_HP:
                    this.HP += item.AmountValue;
                    this.HP = Mathf.Clamp(this.HP, 0, this.MaxHP.Value);
                    break;
                case EnumRef.BuffType.P_MP:
                    this.MP += item.AmountValue;
                    this.MP = Mathf.Clamp(this.MP, 0, this.MaxMP.Value);
                    break;
                 case EnumRef.BuffType.P_HPMax:
                    if (!ignoreAffectTime)
                    {
                        float MaxHP = Mathf.Clamp(this.MaxHP.Value + item.AmountValue, this.MaxHP.Value + item.AmountValue, float.MaxValue);
                        this.MaxHP.AddModifier(item.Name, MaxHP);
                    }
                    break;
                 case EnumRef.BuffType.P_MPMax:
                    if (!ignoreAffectTime)
                    {
                        float MaxMP = Mathf.Clamp(this.MaxMP.Value + item.AmountValue, this.MaxMP.Value + item.AmountValue, float.MaxValue);
                        this.MaxMP.AddModifier(item.Name, MaxMP);
                    }
                    break;
                // TODO: implement other BuffType
                default:
                    Debug.Log("The BuffType is not currently supported");
                    break;
            }
        }

        private void passiveAffect()
        {
            if (this.IsDead) return;
            foreach (var e in this.TemporalAffect)
            {
                if (Time.time > e.Value)
                {
                    switch (e.Key.BuffType)
                    {
                        case EnumRef.BuffType.P_HP:
                            break;
                        case EnumRef.BuffType.P_MP:
                            break;
                        // TODO: implement other BuffType
                        case EnumRef.BuffType.P_HPMax:
                            this.MaxHP.RemoveModifier(e.Key.Name);
                            break;
                        case EnumRef.BuffType.P_MPMax:
                            this.MaxMP.RemoveModifier(e.Key.Name);
                            break;
                        default:
                            Debug.Log("The BuffType is not currently supported");
                            break;
                    }
                }
                else
                    Affect(e.Key, true);
            }
        }
    }
}
