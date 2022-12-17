using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{
    public class Inventory : MonoBehaviour
    {
        #region Singleton
        public static Inventory s_Instance;
        void Awake()
        {
            if (s_Instance != null)
            {
                Debug.LogWarning("More than one instance of Inventory found!");
                return;
            }
            s_Instance = this;
        }
        private Inventory() { }
        #endregion

        public delegate void OnItemChanged();
        public OnItemChanged OnItemChangedCallback;
        public System.Action OnItemIndexChanged;
        [SerializeField]
        private List<Item> items = new List<Item>();
        [SerializeField]
        private int space = 20;
        private int listIndex = 0;

        public bool Add(Item item)
        {
            if (this.items.Count >= this.space)
            {
                Debug.Log("Inventory slot is full");
                return false;
            }
            if (this.items.Contains(item))
            {
                int itemIndex = this.items.IndexOf(item);
                this.items[itemIndex].StackSize++;
            } else
                this.items.Add(item);
            if (this.OnItemChangedCallback != null)
                this.OnItemChangedCallback.Invoke();
            if (this.OnItemIndexChanged != null) 
                this.OnItemIndexChanged();
            return true;
        }
        public bool Remove(Item item)
        {
            int itemIndex = this.items.IndexOf(item);
            if (itemIndex != -1)
            {
                if (this.items[itemIndex].StackSize > 0) this.items[itemIndex].StackSize--;
                else
                {
                    this.items.Remove(item);
                    if (itemIndex == this.listIndex)
                    {
                        this.listIndex = this.listIndex == 0 ? this.listIndex : this.listIndex - 1;
                        if (this.OnItemIndexChanged != null)
                            this.OnItemIndexChanged();
                    }
                    if (this.OnItemChangedCallback != null)
                        this.OnItemChangedCallback.Invoke();
                }
                return true;
            }
            else
                return false;
        }

        public Item GetItem(int idx)
        {
            if (this.items.Count == 0)
                return null;
            return this.items[idx];
        }

        public Item NextItem()
        {
            if (this.items.Count == 0)
                return null;
            this.listIndex++;
            this.listIndex %= this.items.Count;
            if (this.OnItemIndexChanged != null) 
                this.OnItemIndexChanged();

            return this.items[this.listIndex];
        }

        public Item PrevItem()
        {
            if (this.items.Count == 0)
                return null;
            this.listIndex--;
            if (this.listIndex < 0)
                this.listIndex = this.items.Count - 1;
            if (this.OnItemIndexChanged != null) 
                this.OnItemIndexChanged();

            return this.items[this.listIndex];
        }

        public Item CurrentItem()
        {
            if (this.items.Count == 0)
                return null;
            return this.items[this.listIndex];
        }
    }
}
