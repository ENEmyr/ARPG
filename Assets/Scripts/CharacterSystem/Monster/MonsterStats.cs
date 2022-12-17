using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSystem;

public class MonsterStats : CharacterStats
{
    public int EXPGain = 0;

    public override void Awake()
    {
        this.HP = this.MaxHP.Value;
        this.MP = this.MaxMP.Value;
    }
    public override void Die(EnumRef.DamageType dmgType)
    {
        Player.s_Instance.Progression.ReceiveEXP(this.EXPGain, dmgType);
        Destroy(this.gameObject);
        base.Die(dmgType);
    }
}
