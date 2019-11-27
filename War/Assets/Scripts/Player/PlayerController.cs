using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// 角色逻辑控制器.
/// </summary>
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private Transform m_Transform;
    private FirstPersonController m_FPSController;

    private int lifeValue = 1000;                       // 角色生命值.
    private int vitValue = 100;                         // 角色体力值.
    private int timeClick = 0;                          // 计时点.

    public int LifeValue 
    { 
        get => lifeValue; 
        set
        {            
            if (value <= 0)
                value = 0;

            lifeValue = value;

            // 更新UI.
            PlayerInfoPanel.Instance.UpdateLifeBarUI(lifeValue);
            BloodScreenPanel.Instance.UpdateBloodScreen(lifeValue);
        }
    }
    public int VitValue 
    {
        get => vitValue; 
        set
        {
            vitValue = value;

            // 越界保护.
            if (vitValue > 100)
                vitValue = 100;
            else if (vitValue <= 30)
                vitValue = 30;

            // 体力值影响速度.
            m_FPSController.M_RunSpeed = 10.0f * (vitValue / 100.0f);
            m_FPSController.M_WalkSpeed = 5.0f * (vitValue / 100.0f);

            // 更新UI.
            PlayerInfoPanel.Instance.UpdateVitBarUI(vitValue);
        }
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        FindAndLoadInit();

        StartCoroutine("RestoreVit");
    }

    void Update()
    {
        CutVit();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_FPSController = gameObject.GetComponent<FirstPersonController>();
    }

    /// <summary>
    /// 角色体力值自动恢复.
    /// </summary>
    private IEnumerator RestoreVit()
    {
        Vector3 lastPos;
        while (true)
        {
            lastPos = m_Transform.position;

            yield return new WaitForSeconds(1);
            if (m_Transform.position == lastPos)
            {
                VitValue += 5;
            }
        }
    }

    /// <summary>
    /// 玩家体力值消耗.
    /// </summary>
    private void CutVit()
    {
        ++timeClick;
        if (timeClick < 60)
            return;

        if (m_FPSController.CurrentState == PlayerState.WALK)
        {
            VitValue -= 1;
        }
        else if (m_FPSController.CurrentState == PlayerState.RUN)
        {
            VitValue -= 2;
        }

        timeClick = 0;
    }
}
