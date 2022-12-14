using UnityEngine;

namespace ItemSystem
{
    [CreateAssetMenu(fileName = "New Skill", menuName = "Items/Skill")]
    public class Skill : Item
    {
        public EnumRef.DamageType DmgType;
        public float DmgAmount;
        public float Cooldown;
        public float MPUse;
        public GameObject Renderer;

        public override void Use()
        {
            base.Use();
        }
    }
}
