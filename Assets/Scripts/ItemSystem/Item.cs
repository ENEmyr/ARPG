using UnityEngine;

namespace ItemSystem
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
    public class Item : ScriptableObject
    {
        public string Name = "New Item";
        public string Description = "";
        public Sprite Icon = null;
        public int StackSize = 1;
        public EnumRef.ItemCategory Category;

        public virtual void Use()
        {
            Debug.Log("Using " + this.Name);
        }

        protected void RemoveFromInventory()
        {
            this.StackSize--;
            if (this.StackSize == 0)
                Inventory.s_Instance.Remove(this);
        }
    }
}
