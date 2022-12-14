using UnityEngine;
using UnityEngine.UI;
using ItemSystem;

public class RotateItemBtn : MonoBehaviour
{
    [SerializeField]
    private Button useItemBtn;
    private Button rotateItemBtn;
    // Start is called before the first frame update
    void Start()
    {
        rotateItemBtn = GameObject.Find(gameObject.name).GetComponent<Button>();
        rotateItemBtn.onClick.AddListener(rotateItemIndex);
        if (useItemBtn != null)
        {
            useItemBtn.onClick.AddListener(useItem);
            setUseItemBtnImage(); // need to use invoke to send notify when inventory is changed
        }
        else
            Debug.LogWarning("UseItem button didn't set, so the UseItem button will not work");
    }

    private void rotateItemIndex()
    {
        Item i = Inventory.s_Instance.NextItem();
        setUseItemBtnImage();
    }

    private void setUseItemBtnImage()
    {
        Image useItemBtnImage;
        Item i = Inventory.s_Instance.CurrentItem();
        if (i != null)
            foreach (Transform child in useItemBtn.transform)
            {
                if (child.name == "Image")
                {
                    useItemBtnImage = child.GetComponent<Image>();
                    useItemBtnImage.sprite = i.Icon;
                }
            }
        else
            Debug.LogWarning("CurrentItem is null");
    }

    private void useItem()
    {
        Item i = Inventory.s_Instance.CurrentItem();
        i.Use();
        setUseItemBtnImage();
    }
}
