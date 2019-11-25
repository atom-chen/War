using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弓箭、长矛管理器.
/// </summary>
public class Arrow : BulletBase
{
    private BoxCollider m_BoxCollider;
    private Transform m_PivotTransform;         // 中心点用于尾部动画.

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    protected override void FindAndLoadInit()
    {
        m_BoxCollider = gameObject.GetComponent<BoxCollider>();

        m_PivotTransform = M_Transform.Find("Pivot");
    }

    /// <summary>
    /// 发射弓箭.
    /// </summary>
    /// <param name="dir">方向.</param>
    /// <param name="force">力度.</param>
    /// <param name="damage">弹头伤害.</param>
    public override void Shoot(Vector3 dir, int force, int damage)
    {
        this.Damage = damage;

        M_Rigidbody.AddForce(dir * force, ForceMode.Impulse);
    }

    /// <summary>
    /// 碰撞事件.
    /// </summary>
    protected override void CollisionEnter(Collision other)
    {
        GameObject.Destroy(M_Rigidbody);
        GameObject.Destroy(m_BoxCollider);

        BulletMark bulletMark = other.gameObject.GetComponent<BulletMark>();
        AIModel ai = other.gameObject.GetComponentInParent<AIModel>();

        // 攻击环境物体.
        if (bulletMark != null)
        {
            bulletMark.Hp -= Damage;    
        }

        // 攻击AI角色.
        else if (ai != null)
        {
            ai.Life -= Damage;
        }

        // 通用效果展现.
        M_Transform.SetParent(other.gameObject.GetComponent<Transform>());
        StartCoroutine("TailAnimation", m_PivotTransform);
    }
}
