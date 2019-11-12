using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 合成模块右侧功能区控制器.
/// </summary>
public class CraftingController : MonoBehaviour
{
    private Transform m_Transform;
    private Image m_GoodItemImage;                  // 最终合成物品图片.
    private Button m_Craft_Btn;                     // 合成按钮.

    private int mapId = -1;                         // 最终合成物品编号.
    private string mapName;                         // 最终合成物品名称.

    private GameObject prefab_InventoryItem;        // 最终合成物品.

    public GameObject Prefab_InventoryItem { set => prefab_InventoryItem = value; }

    void Awake()
    {
        FindAndLoadInit();
        NormalButton();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_GoodItemImage = m_Transform.Find("GoodItem/Item").GetComponent<Image>();
        m_Craft_Btn = m_Transform.Find("CraftButton").GetComponent<Button>();

        m_GoodItemImage.gameObject.SetActive(false);
        m_Craft_Btn.onClick.AddListener(CraftingItem);
    }

    /// <summary>
    /// 外部调用初始化.
    /// </summary>
    public void Init(int mapId, string spriteName)
    {
        this.mapId = mapId;
        this.mapName = spriteName;

        m_GoodItemImage.gameObject.SetActive(true);
        m_GoodItemImage.sprite = Resources.Load<Sprite>("Textures/Inventory/Item/" + spriteName);
    }

    /// <summary>
    /// 按钮默认状态.
    /// </summary>
    private void NormalButton()
    {
        m_Craft_Btn.interactable = false;
    }

    /// <summary>
    /// 按钮激活状态.
    /// </summary>
    public void ActiveButton()
    {
        m_Craft_Btn.interactable = true;
    }

    /// <summary>
    /// 物品合成.
    /// </summary>
    private void CraftingItem()
    {
        GameObject item = GameObject.Instantiate<GameObject>(prefab_InventoryItem, m_Transform.Find("GoodItem"));
        
        // 重置图片尺寸.
        RectTransform itemTransform = item.GetComponent<RectTransform>();
        itemTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 110);
        itemTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 110);

        // 设置相关属性.
        item.GetComponent<InventoryItemController>().InitItem(new InventoryItem(mapId, mapName, 1));

        // 按钮重新回归不可用状态.
        NormalButton();

        SendMessageUpwards("CraftingOK");
    }
}
