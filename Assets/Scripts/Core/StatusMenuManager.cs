using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ItemSystem;
using CharacterSystem;

public class StatusMenuManager : MonoBehaviour
{
    [SerializeField]
    private Image EXPBarRepresentative;
    [SerializeField]
    private TMP_Text T_EB, T_PlayerNameStatus, T_Statuses;

    private Player playerInstance;
    private CharacterStats playerStats;
    private CharacterProgression playerProgress;
    private Equipment currWeapon;
    // Start is called before the first frame update
    void Start()
    {
        this.playerInstance = Player.s_Instance;
        this.playerStats = this.playerInstance.Stats;
        this.playerProgress = this.playerInstance.Progression;
    }

    // Update is called once per frame
    void Update()
    {
        int totExp = Mathf.Clamp(this.playerProgress.PEXP + this.playerProgress.MEXP, 0, this.playerProgress.EXPCap);
        float expPercentage = (float)totExp / (float)this.playerProgress.EXPCap;
        this.currWeapon = EquipmentManager.s_Instance.GetEquipment(EnumRef.EquipmentSlot.MainHand);
        T_PlayerNameStatus.text = String.Format("{0} : Lvl.{1}", this.playerStats.Name, this.playerProgress.Level);
        T_EB.text = String.Format("EXP :  {0}/{1}", this.playerProgress.PEXP + this.playerProgress.MEXP, this.playerProgress.EXPCap);
        EXPBarRepresentative.fillAmount = expPercentage;
        T_Statuses.text = String.Format(
            "HP: {0}/{1}\tMP: {2}/{3}\nPEXP: {4}\tMEXP: {5}\nSTR: {6}\tINT: {7}\nPhysical Resistance: {8}\nMagical Resistance: {9}\nCurrent Weapon: {10}\nWeapon Resistance Modifier: {11}\nWeapon Damage Modifier: {12}\nWeapon Radius Modifier: {13}\nWeapon Skill: {14}",
            (int)this.playerStats.HP, this.playerStats.MaxHP.Value,
            (int)this.playerStats.MP, this.playerStats.MaxMP.Value,
            this.playerProgress.PEXP, this.playerProgress.MEXP,
            this.playerStats.STR.Value, this.playerStats.INT.Value,
            this.playerStats.PhysicalResist.Value, this.playerStats.MagicalResist.Value,
            this.currWeapon.Name, this.currWeapon.ResistModifier,
            this.currWeapon.DamageModifier, this.currWeapon.RadiusModifier,
            this.currWeapon.EquipmentSkill.Name
        );
    }
}
