using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弓箭管理器.
/// </summary>
public class Arrow : MonoBehaviour
{
    private Transform m_Transform;
    private Rigidbody m_Rigidbody;
    private BoxCollider m_BoxCollider;

    private int damage;                         // 武器伤害值.

    void Awake()
    {
        FindAndLoadInit();
    }

    void OnCollisionEnter(Collision other)
    {
        GameObject.Destroy(m_Rigidbody);
        GameObject.Destroy(m_BoxCollider);

        BulletMark bulletMark = other.gameObject.GetComponent<BulletMark>();
        if (bulletMark != null)
        {
            bulletMark.Hp -= damage;
            m_Transform.SetParent(other.gameObject.GetComponent<Transform>());
        }
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
        m_BoxCollider = gameObject.GetComponent<BoxCollider>();
    }

    /// <summary>
    /// 发射弓箭.
    /// </summary>
    /// <param name="dir">方向.</param>
    /// <param name="force">力度.</param>
    /// <param name="damage">弹头伤害.</param>
    public void Shoot(Vector3 dir, int force, int damage)
    {
        this.damage = damage;

        m_Rigidbody.AddForce(dir * force, ForceMode.Impulse);
    }
}
