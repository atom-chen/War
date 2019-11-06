using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 合成面板单个选项卡逻辑控制器.
/// </summary>
public class CraftingTabItemController : MonoBehaviour
{
    private Transform m_Transform;
    private Button m_Button;                            // 按钮.
    private Image m_IconImage;                          // 选项卡显示图标.
    private GameObject m_BG;                            // 控制选中或默认.

    private int index = -1;                             // 选项卡索引.

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
        m_Button = gameObject.GetComponent<Button>();
        m_IconImage = m_Transform.Find("Content").GetComponent<Image>();
        m_BG = m_Transform.Find("BG").gameObject;

        m_Button.onClick.AddListener(() => SendMessageUpwards("ResetTabsAndContents", index));
    }

    /// <summary>
    /// 外部调用初始化.
    /// </summary>
    public void InitTabItem(int index, Sprite icon)
    {
        this.index = index;
        gameObject.name = "Tabs_" + index;

        m_IconImage.sprite = icon;
    }

    /// <summary>
    /// 选项卡未选中默认状态.
    /// </summary>
    public void NormalItem()
    {
        if (m_BG.activeSelf == false)
            m_BG.SetActive(true);
    }

    /// <summary>
    /// 选项卡选中激活状态.
    /// </summary>
    public void ActiveItem()
    {
        if (m_BG.activeSelf == true)
            m_BG.SetActive(false);
    }
}
