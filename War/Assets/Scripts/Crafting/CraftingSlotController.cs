using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 单个合成图谱槽管理器.
/// </summary>
public class CraftingSlotController : MonoBehaviour
{
    private Transform m_Transform;
    private Image m_Image;                      // 参考图片显示.

    void Awake()
    {
        FindAndLoadInit();
        ResetSlot();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Image = m_Transform.Find("Icon").GetComponent<Image>();
    }

    /// <summary>
    /// 外部调用初始化.
    /// </summary>
    public void InitSlot(Sprite sprite)
    {
        m_Image.gameObject.SetActive(true);
        m_Image.sprite = sprite;
    }

    /// <summary>
    /// 重置合成图谱槽.
    /// </summary>
    public void ResetSlot()
    {
        m_Image.gameObject.SetActive(false);
    }
}
