using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 突击步枪C层.
/// </summary>
public class AssaultRifle : MonoBehaviour
{
    private Transform m_Transform;
    private Animator m_Animator;

    void Start()
    {
        FindAndLoadInit();
    }

    void Update()
    {
        // 按下鼠标左键射击.
        if (Input.GetMouseButtonDown(0))
        {
            m_Animator.SetTrigger("Fire");
        }

        // 按下鼠标右键开镜.
        if (Input.GetMouseButtonDown(1))
        {
            m_Animator.SetBool("HoldPose", true);
        }

        // 松开鼠标右键退出开镜.
        if (Input.GetMouseButtonUp(1))
        {
            m_Animator.SetBool("HoldPose", false);
        }
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Animator = gameObject.GetComponent<Animator>();
    }
}
