using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSystem;

namespace ItemSystem
{
    public class EquipmentManager : MonoBehaviour
    {
        #region Singleton
        public static EquipmentManager s_Instance;
        void Awake()
        {
            if (s_Instance != null)
            {
                Debug.LogWarning("More than one instance of EquipmentManager found!");
                return;
            }
            s_Instance = this;
        }
        private EquipmentManager() { }
        #endregion

        public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
        public OnEquipmentChanged OnEquipmentChangedCallback;
        [SerializeField]
        private Equipment[] defaultEquip;
        [SerializeField]
        private Equipment[] currEquip;
        [SerializeField]
        private GameObject[] currGameObjects;

        void Start()
        {
            int nSlot = System.Enum.GetNames(typeof(EnumRef.EquipmentSlot)).Length;
            this.currEquip = new Equipment[nSlot];
            this.currGameObjects = new GameObject[nSlot];
            equipDefault();
        }
        public void Equip(Equipment e)
        {
            int position = (int)e.SlotType;
            if (this.currEquip[position] != null)
            {
                Equipment oldItem = UnEquip(position);
                if (OnEquipmentChangedCallback != null) // swap item
                    OnEquipmentChangedCallback.Invoke(e, oldItem);
            }
            else
            {
                if (OnEquipmentChangedCallback != null) // equip item
                    OnEquipmentChangedCallback.Invoke(e, null);
            }
            Debug.Log("Equip " + e.Name);
            this.currEquip[position] = e;
            this.currGameObjects[position] = Instantiate(e.Prefab, Player.s_Instance.gameObject.transform);
        }
        public Equipment UnEquip(int position)
        {
            Equipment e = this.currEquip[position];
            Destroy(this.currGameObjects[position]);
            this.currEquip[position] = null;
            this.currGameObjects[position] = null;
            Inventory.s_Instance.Add(e);
            if (OnEquipmentChangedCallback != null) // unequip item
                OnEquipmentChangedCallback.Invoke(null, e);
            Debug.Log("UnEquip " + e.Name);
            return e;
        }
        public Equipment GetEquipment(EnumRef.EquipmentSlot slot)
        {
            return this.currEquip[(int)slot];
        }
        private void equipDefault()
        {
            foreach (Equipment e in this.defaultEquip)
            {
                Equip(e);
            }
        }
    }
}
