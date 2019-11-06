using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 合成选项卡单条内容控制器.
/// </summary>
public class CraftingContentItemController : MonoBehaviour
{
    private Transform m_Transform;
    private Image m_Image;                      // 用于控制选中.
    private Button m_Button;                    // 按钮.
    private Text m_Text;                        // 显示的内容.

    private int itemId;                         // 合成内容编号.

    void Awake()
    {
        FindAndLoadInit();
        NormalItem();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Image = gameObject.GetComponent<Image>();
        m_Button = gameObject.GetComponent<Button>();
        m_Text = m_Transform.Find("Content").GetComponent<Text>();

        m_Button.onClick.AddListener(() => SendMessageUpwards("ResetAllItems", this));
    }

    /// <summary>
    /// 外部调用初始化.
    /// </summary>
    public void InitItem(int index, CraftingContentItem item)
    {
        gameObject.name = "Item_" + index;

        this.itemId = item.ItemId;
        m_Text.text = "  " + item.ItemName;
    }

    /// <summary>
    /// 内容默认状态.
    /// </summary>
    public void NormalItem()
    {
        m_Image.color = new Color32(0, 0, 0, 0);
    }

    /// <summary>
    /// 内容选中激活状态.
    /// </summary>
    public void ActiveItem()
    {
        m_Image.color = new Color32(0, 0, 0, 90);
    }
}
