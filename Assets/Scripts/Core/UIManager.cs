using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using CharacterSystem;
using ItemSystem;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform mainUI, gameOverUI, statusMenuUI;
    [SerializeField]
    private TMP_Text mainMenuPlayerName;

    private bool isStatusMenuOpen = false;

    void Start()
    {
        mainMenuPlayerName.text = String.Format("{0}", Player.s_Instance.Stats.Name);

    }

    public void OpenStatusMenu()
    {
        if (this.isStatusMenuOpen)
            CloseStatusMenu();
        else
        {
            this.isStatusMenuOpen = true;
            statusMenuUI.DOAnchorPos(Vector2.zero, .25f);
            for (int i = 0; i < GameObject.Find("OpenStatusMenuBtn").transform.childCount; i++)
            {
                GameObject.Find("OpenStatusMenuBtn").transform.GetChild(i).gameObject.SetActive(false);
                GameObject.Find("OpenStatusMenuBtn").GetComponents<Image>()[0].enabled = false;
            }
        }
    }

    public void CloseStatusMenu()
    {
        this.isStatusMenuOpen = false;
        statusMenuUI.DOAnchorPos(new Vector2(1600, 0), .25f);
        for (int i = 0; i < GameObject.Find("OpenStatusMenuBtn").transform.childCount; i++)
        {
            GameObject.Find("OpenStatusMenuBtn").transform.GetChild(i).gameObject.SetActive(true);
            GameObject.Find("OpenStatusMenuBtn").GetComponents<Image>()[0].enabled = true;
        }
    }

    public void ShowGameOverScreen()
    {
        gameOverUI.DOAnchorPos(Vector2.zero, .25f);
    }

    public void HideGameOverScreen()
    {
        Player.s_Instance.Respawn();
        gameOverUI.DOAnchorPos(new Vector2(0, 1000), .25f);
    }

    public void DebugBtn()
    {
        // Debug.Log("EXPCap : " + Player.s_Instance.Progression.EXPCap);
        Player.s_Instance.Progression.ReceiveEXP(10, EnumRef.DamageType.Magical);
    }
}
