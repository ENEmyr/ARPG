using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSystem;

public class MonsterStats : CharacterStats
{
    public int EXPGain = 0;
    public override void Die(EnumRef.DamageType dmgType)
    {
        this.IsDead = true;
        Player.s_Instance.Progression.ReceiveEXP(this.EXPGain, dmgType);
        Destroy(this.gameObject);
        Debug.Log(this.Name + "is dying.");
    }
}
