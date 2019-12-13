using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 采集角色控制器.
/// </summary>
public class StoneHatchet : MonoBehaviour
{
    private Transform m_Transform;
    private Animator m_Animator;

    void Start()
    {
        FindAndLoadInit();
    }

    void Update()
    {
        LeftMouseButtonDown();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Animator = gameObject.GetComponent<Animator>();
    }

    /// <summary>
    /// 放下采集武器.
    /// </summary>
    public void HolsterStoneHatchet()
    {
        m_Animator.SetTrigger("Holster");
    }

    /// <summary>
    /// 鼠标左键开始攻击.
    /// </summary>
    private void LeftMouseButtonDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_Animator.SetTrigger("Hit");
        }
    }
}
