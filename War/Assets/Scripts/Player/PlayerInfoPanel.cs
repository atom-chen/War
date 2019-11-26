using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 玩家UI信息管理器.
/// </summary>
public class PlayerInfoPanel : MonoBehaviour
{
    public static PlayerInfoPanel Instance;

    private Transform m_Transform;

    // 生命值和体力值UI.
    private Image m_LifeBar;
    private Image m_VitBar;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        FindAndLoadInit();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Transform = gameObject.GetComponent<Transform>();

        m_LifeBar = m_Transform.Find("Life/Bar").GetComponent<Image>();
        m_VitBar = m_Transform.Find("Vit/Bar").GetComponent<Image>();
    }

    /// <summary>
    /// 更新生命值UI.
    /// </summary>
    public void UpdateLifeBarUI(int lifeValue)
    {
        m_LifeBar.fillAmount = lifeValue / 1000.0f;
    }

    /// <summary>
    /// 更新体力值UI.
    /// </summary>
    public void UpdateVitBarUI(int vitValue)
    {
        m_VitBar.fillAmount = vitValue / 100.0f;
    }
}
