using UnityEngine;

namespace ItemSystem
{
    [CreateAssetMenu(fileName = "New Equipment", menuName = "Items/Equipment")]
    public class Equipment : Item
    {
        public EnumRef.EquipmentSlot SlotType;
        public GameObject Prefab;
        public float ResistModifier;
        public float DamageModifier;
        public float RadiusModifier;
        public Skill EquipmentSkill;

        public override void Use()
        {
            EquipmentManager.s_Instance.Equip(this);
            RemoveFromInventory();
            base.Use();
        }
    }
}
