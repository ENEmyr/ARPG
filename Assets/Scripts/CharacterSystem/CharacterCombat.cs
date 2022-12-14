using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemSystem;

namespace CharacterSystem
{
    [RequireComponent(typeof(CharacterStats))]
    public class CharacterCombat : MonoBehaviour
    {
        public event System.Action OnAttack;
        public CharacterStats Stats;
        public float AttackSpeed = 1f;
        public float AttackDelay = .6f;
        public float AttackRadius = 1f;
        public float AttackChance = 0f;
        public IDictionary<string, float> attackCooldown = new Dictionary<string, float>(); // Skills cooldown

        void Start()
        {
            this.Stats = GetComponent<CharacterStats>();
        }

        void Update()
        {

        }

        public float Attack(CharacterStats target, Skill skill)
        {
            float dmgTot;
            if (!this.attackCooldown.ContainsKey(skill.Name))
                this.attackCooldown.Add(skill.Name, 0f);
            if (Time.time > this.attackCooldown[skill.Name])
            {
                if (this.Stats.MP < skill.MPUse)
                {
                    Debug.Log("MP is not enough to use skill");
                    return 0;
                }
                if (skill.DmgType == EnumRef.DamageType.Physical)
                {
                    // skill cooldown time depends on base attack speed and 10% of STR/INT of Character
                    this.attackCooldown[skill.Name] = Time.time + (this.AttackSpeed / (skill.Cooldown - (skill.Cooldown * this.Stats.STR.Value * .1f)));
                    dmgTot = skill.DmgAmount + (skill.DmgAmount * this.Stats.STR.Value);
                }
                else if (skill.DmgType == EnumRef.DamageType.Magical)
                {
                    this.attackCooldown[skill.Name] = Time.time + (this.AttackSpeed / (skill.Cooldown - (skill.Cooldown * this.Stats.INT.Value * .1f)));
                    dmgTot = skill.DmgAmount + (skill.DmgAmount * this.Stats.INT.Value);
                }
                else
                    dmgTot = skill.DmgAmount;
                this.Stats.MP -= skill.MPUse;
                StartCoroutine(doDamage(dmgTot, skill, target, this.AttackDelay));
                if (this.OnAttack != null)
                    this.OnAttack();
                return dmgTot;
            }
            return 0;
        }

        private IEnumerator doDamage(
            float dmg,
            Skill sk,
            CharacterStats target,
            float delay
            )
        {
            Debug.Log("Start attack coroutine");
            GameObject spawned = Instantiate(sk.Renderer, Player.s_Instance.firePoint.position, Player.s_Instance.firePoint.rotation);
            yield return new WaitForSeconds(delay);
            if (target != null)
            {
                Debug.Log(transform.name + " do damage " + dmg + " units to " + target.Name);
                target.ReceiveDamage(dmg, sk.DmgType);
            }
            else Destroy(spawned);
        }
    }
}
