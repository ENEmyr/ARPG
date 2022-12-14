using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using CharacterSystem;

public class AtkBtn : MonoBehaviour
{
    private Button atkBtn;
    //private Player player;

    // Start is called before the first frame update
    void Start()
    {
        atkBtn = GameObject.Find(gameObject.name).GetComponent<Button>();
        atkBtn.onClick.AddListener(Player.s_Instance.Attack);
        //player = GameObject.Find("Player").GetComponent<Player>();
        //atkBtn.onClick.AddListener(player.Attack);
    }

    void TestSpawnItem()
    {
        ItemSystem.Equipment mainHandWeapon = ItemSystem.EquipmentManager.s_Instance.GetEquipment(EnumRef.EquipmentSlot.MainHand);
        ItemSystem.Skill skill = (ItemSystem.Skill)mainHandWeapon.EquipmentSkill;
        GameObject item = skill.Renderer;
        GameObject spawnedSkill = Instantiate(item, new Vector3(39, -25, 20), Quaternion.Euler(0, 0, -45)) as GameObject;
        SkillDamage spawnedSkillScript = spawnedSkill.AddComponent<SkillDamage>();
        spawnedSkillScript.Amount = 10;
        spawnedSkillScript.Sk = skill;
    }
}
