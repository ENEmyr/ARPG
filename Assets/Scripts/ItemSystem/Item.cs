using UnityEngine;

namespace ItemSystem
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
    public class Item : ScriptableObject
    {
        public string Name = "New Item";
        public string Description = "";
        public Sprite Icon = null;
        public EnumRef.ItemCategory Category;

        public virtual void Use()
        {
            Debug.Log("Using " + this.Name);
        }

        protected void RemoveFromInventory()
        {
            Inventory.s_Instance.Remove(this);
        }
    }
}
