using System;
using UnityEngine;
using UnityEngine.UI;
using CharacterSystem;
using TMPro;

public class CharacterTag : MonoBehaviour
{
    private CharacterStats stats;
    private CharacterProgression progression;
    private Image hBRepresentative;
    private TMP_Text t_Name;
    // Start is called before the first frame update
    void Start()
    {
        this.stats = this.transform.parent.gameObject.GetComponent<CharacterStats>();
        this.progression = this.transform.parent.gameObject.GetComponent<CharacterProgression>();
        this.hBRepresentative = this.transform.Find("Canvas").transform.Find("HealthBar").transform.Find("HBRepresentative").GetComponent<Image>();
        this.t_Name = this.transform.Find("Canvas").transform.Find("T_name").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        t_Name.text = String.Format("< Level {0} >\n{1}", this.progression.Level, this.stats.Name);
        this.hBRepresentative.fillAmount = this.stats.HP / this.stats.MaxHP.Value;
    }
}
