using UnityEngine;
using UnityEngine.UI;
using CharacterSystem;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private CharacterStats stats;
    private Image healthBar, manaBar;

    void Start()
    {
        healthBar = gameObject.transform.Find("HealthBar")
                    .transform.Find("HBRepresentative").GetComponent<Image>();
        manaBar = gameObject.transform.Find("ManaBar")
                    .transform.Find("MNRepresentative").GetComponent<Image>();
    }

    void Update()
    {
        healthBar.fillAmount = stats.HP / stats.MaxHP.Value;
        manaBar.fillAmount = stats.MP / stats.MaxMP.Value;
    }
}
