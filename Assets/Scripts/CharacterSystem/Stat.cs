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
        private IDictionary<string, float> modifier = new Dictionary<string, float>();
        public float Value
        {
            get
            {
                if (this.modifier.Count > 0)
                    return this.baseValue + this.modifier.Values.Sum();
                else 
                    return this.baseValue;
            }
            private set { }
        }

        public void AddModifier(string k, float m)
        {
            if (!modifier.ContainsKey(k))
                this.modifier.Add(k, m);
            else
                this.modifier[k] += m;
        }

        public void RemoveModifier(string k)
        {
            this.modifier.Remove(k);
        }
    }
}
