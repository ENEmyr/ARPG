using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemSystem;

namespace CharacterSystem
{
    [RequireComponent(typeof(PlayerStats))]
    [RequireComponent(typeof(PlayerCombat))]
    [RequireComponent(typeof(CharacterProgression))]
    public class Player : MonoBehaviour
    {
        #region Singleton
        public static Player s_Instance;
        void Awake()
        {
            if (s_Instance != null)
            {
                Debug.LogWarning("More than one instance of Player found!");
                return;
            }
            s_Instance = this;
        }
        private Player() { }
        #endregion

        public PlayerStats Stats;
        public PlayerCombat Combat;
        public CharacterProgression Progression;
        public GameManager GameState;

        [SerializeField]
        private bool debug = false;
        public Transform firePoint {get; protected set;}

        public void Start()
        {
            this.Progression.OnLevelUp += this.updateStatsOnLevelUp;
        }

        public void Update()
        {
            if (PlaceObjectOnPlane.key || this.debug)
            {
                Equipment mainHand = EquipmentManager.s_Instance.GetEquipment(EnumRef.EquipmentSlot.MainHand);
                if (mainHand != null)
                {
                    this.firePoint = GameObject.Find(mainHand.Prefab.name + "(Clone)").transform.Find("FirePoint").GetComponent<Transform>();
                    if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
                        Attack();
                }
                else
                    Debug.Log("Didn't have weapon on main hand.");

            }
        }

        public void Attack()
        {
            Debug.Log("Firing!");
            if (EquipmentManager.s_Instance.GetEquipment(EnumRef.EquipmentSlot.MainHand) != null)
            {
                Equipment weapon = EquipmentManager.s_Instance.GetEquipment(EnumRef.EquipmentSlot.MainHand);
                Ray raycast = new Ray();
                RaycastHit raycastHit;
                Skill sk = EquipmentManager.s_Instance.GetEquipment(EnumRef.EquipmentSlot.MainHand).EquipmentSkill;
                if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
                {
                    raycast.origin = transform.position;
                    raycast.direction = transform.forward;
                    Physics.Raycast(raycast, out raycastHit);
                    if (raycastHit.collider == null)
                        this.Combat.Attack(null, sk);
                    else
                    {
                        if (raycastHit.collider.gameObject.name == sk.Renderer.name + "(Clone)")
                            this.Combat.Attack(null, sk);
                        else
                            this.Combat.Attack(raycastHit.collider.gameObject.GetComponent<Monster>().Stats, sk);
                    }
                }
            }
        }

        public void Respawn()
        {
            this.Stats.HP = this.Stats.MaxHP.Value;
            this.Stats.MP = this.Stats.MaxMP.Value;
            this.Stats.IsDead = false;
        }

        private void OnTriggerEnter(Collider obj)
        {
            SkillDamage skill = obj.gameObject.GetComponent<SkillDamage>();
            this.Stats.ReceiveDamage(skill.Amount, skill.Sk.DmgType);
        }

        private void updateStatsOnLevelUp()
        {
            this.Stats.MaxHP.RemoveModifier("levelUp");
            this.Stats.MaxMP.RemoveModifier("levelUp");
            this.Stats.STR.RemoveModifier("levelUp");
            this.Stats.INT.RemoveModifier("levelUp");
            this.Stats.PhysicalResist.RemoveModifier("levelUp");
            this.Stats.MagicalResist.RemoveModifier("levelUp");

            this.Stats.MaxHP.AddModifier("levelUp", this.Progression.MaxHPModifier);
            this.Stats.MaxMP.AddModifier("levelUp", this.Progression.MaxMPModifier);
            this.Stats.STR.AddModifier("levelUp", this.Progression.STRModifier);
            this.Stats.INT.AddModifier("levelUp", this.Progression.INTModifier);
            this.Stats.PhysicalResist.AddModifier("levelUp", this.Progression.PhysicalResistModifier);
            this.Stats.MagicalResist.AddModifier("levelUp", this.Progression.MagicalResistModifier);
        }
    }
}
