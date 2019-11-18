using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 霰弹枪子弹管理器.
/// </summary>
public class ShotgunBullet : MonoBehaviour
{
    private Transform m_Tranform;
    private Rigidbody m_Rigidbody;

    void Awake()
    {
        FindAndLoadInit();
    }

    void OnCollisionEnter(Collision other)
    {
        m_Rigidbody.Sleep();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Tranform = gameObject.GetComponent<Transform>();
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 发射子弹.
    /// </summary>
    /// <param name="dir">方向.</param>
    /// <param name="force">力度.</param>
    public void Shoot(Vector3 dir, int force)
    {
        m_Rigidbody.AddForce(dir * force, ForceMode.Impulse);
    }
}
