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

    void Awake()
    {
        FindAndLoadInit();
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
    }

    /// <summary>
    /// 外部调用初始化.
    /// </summary>
    public void Init(string spriteName)
    {
        m_GoodItemImage.gameObject.SetActive(true);
        m_GoodItemImage.sprite = Resources.Load<Sprite>("Textures/Inventory/Item/" + spriteName);
    }
}
