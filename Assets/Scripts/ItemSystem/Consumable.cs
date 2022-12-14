using UnityEngine;
using CharacterSystem;

namespace ItemSystem
{
    [CreateAssetMenu(fileName = "New Consumable", menuName = "Items/Consumable")]
    public class Consumable : Item
    {
        public float AffectTime = 0f;
        public float AmountValue = 0f;
        public EnumRef.BuffType BuffType;

        public override void Use()
        {
            // find Player by getcomponent then use Affect method in PlayerStats class 
            base.Use();
            Player.s_Instance.Stats.Affect(this);
            RemoveFromInventory();
        }
    }
}
