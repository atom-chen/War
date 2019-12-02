using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 建造类别UI控制器.
/// </summary>
public class CategoryItemController : MonoBehaviour
{
    private Transform m_Transform;
    private Image m_BGImage;                        // 背景UI.
    private Image m_CategoryIcon;                   // 类别图标UI.

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
        m_BGImage = gameObject.GetComponent<Image>();
        m_CategoryIcon = m_Transform.Find("Icon").GetComponent<Image>();
    }

    /// <summary>
    /// 外部调用初始化.
    /// </summary>
    public void InitItem(int index,  Sprite iconSprite)
    {
        m_Transform.localRotation = Quaternion.Euler(new Vector3(0, 0, index * 40));
        m_CategoryIcon.GetComponent<Transform>().rotation = Quaternion.identity;

        if (iconSprite == null)
        {
            m_CategoryIcon.enabled = false;
        }
        else
        {
            m_CategoryIcon.sprite = iconSprite;
            m_BGImage.enabled = false;
        }


        gameObject.name = "CategoryItem_" + index;
    }
}
