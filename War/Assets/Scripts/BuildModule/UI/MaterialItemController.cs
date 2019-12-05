using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 具体建造材料UI控制器.
/// </summary>
public class MaterialItemController : MonoBehaviour
{
    private Transform m_Transform;
    private Image m_MaterialIcon;                       // 材料显示图标.

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
        m_MaterialIcon = m_Transform.Find("MaterialIcon").GetComponent<Image>();
    }

    /// <summary>
    /// 外部调用初始化.
    /// </summary>
    public void InitMaterialItem(Sprite iconSprite, Quaternion localRotation, Transform parent)
    {
        m_Transform.localRotation = localRotation;
        m_MaterialIcon.GetComponent<Transform>().rotation = Quaternion.identity;

        if (iconSprite != null)
        {
            m_MaterialIcon.sprite = iconSprite;
        }
        else
        {
            m_MaterialIcon.enabled = false;
        }

        m_Transform.SetParent(parent);
    }

    /// <summary>
    /// 材料默认状态.
    /// </summary>
    public void NormalItem()
    {
        m_MaterialIcon.color = Color.white;
    }

    /// <summary>
    /// 材料选中激活状态.
    /// </summary>
    public void ActiveItem()
    {
        m_MaterialIcon.color = Color.red;
    }
}
