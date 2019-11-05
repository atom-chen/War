using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 单个背包物品控制器.
/// </summary>
public class InventoryItemController : MonoBehaviour
{
    private Transform m_Transform;
    private Image m_Image;                      // 物品展示图片.
    private Text m_Text;                        // 物品数量文字UI.

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
        m_Image = gameObject.GetComponent<Image>();
        m_Text = m_Transform.Find("ItemCount").GetComponent<Text>();
    }

    /// <summary>
    /// 外部调用初始化背包物品基本信息.
    /// </summary>
    public void InitItem(int index, string spriteName, int itemNum)
    {
        gameObject.name = "Item_" + index;
        m_Image.sprite = Resources.Load<Sprite>("Textures/Inventory/" + spriteName);
        m_Text.text = itemNum.ToString();
    }
}
