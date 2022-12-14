using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CharacterSystem
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField]
        private float baseValue;
        private List<float> modifier = new List<float>();
        public float Value
        {
            get
            {
                if (this.modifier.Count > 0)
                    return this.baseValue + this.modifier.Aggregate((acc, x) => acc + x);
                else 
                    return this.baseValue;
            }
            private set { }
        }

        public void AddModifier(float m)
        {
            this.modifier.Add(m);
        }

        public void RemoveModifier(float m)
        {
            this.modifier.Remove(m);
        }
    }
}
