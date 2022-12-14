using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSystem;
using ItemSystem;

public class MonsterCombat : CharacterCombat
{
    public new float Attack(CharacterStats target, Skill skill)
    {
        float dmgTot;
        if (skill.DmgType == EnumRef.DamageType.Physical)
            dmgTot = skill.DmgAmount + (skill.DmgAmount * this.Stats.STR.Value);
        else if (skill.DmgType == EnumRef.DamageType.Magical)
            dmgTot = skill.DmgAmount + (skill.DmgAmount * this.Stats.INT.Value);
        else
            dmgTot = skill.DmgAmount;
        return dmgTot;
    }
}
