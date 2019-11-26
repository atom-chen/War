using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 人形AI布娃娃系统.
/// </summary>
public class AIRagdoll : MonoBehaviour
{
    private Transform m_Transform;

    // 布娃娃系统两个碰撞体.
    private BoxCollider m_BoxCollider_1;
    private BoxCollider m_BoxCollider_2;

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

        m_BoxCollider_1 = m_Transform.Find("Armature").GetComponent<BoxCollider>();
        m_BoxCollider_2 = m_Transform.Find("Armature/Hips/Middle_Spine").GetComponent<BoxCollider>();
    }

    /// <summary>
    /// 开启布娃娃系统.
    /// </summary>
    public void StartRagdoll()
    {
        m_BoxCollider_1.enabled = false;
        m_BoxCollider_2.enabled = false;
    }
}
