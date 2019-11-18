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

    // 射线决定弹痕信息.
    private Ray ray;
    private RaycastHit hit;

    private int damage;                     // 伤害值.

    void Awake()
    {
        FindAndLoadInit();
    }

    void OnCollisionEnter(Collision other)
    {
        m_Rigidbody.Sleep();

        BulletMark bulletMark = other.gameObject.GetComponent<BulletMark>();
        if (bulletMark != null)
        {
            bulletMark.CreateBulletMark(hit);
            bulletMark.Hp -= damage;
        }

        GameObject.Destroy(gameObject);
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
    /// <param name="damage">弹头伤害.</param>
    public void Shoot(Vector3 dir, int force, int damage)
    {
        this.damage = damage;

        m_Rigidbody.AddForce(dir * force, ForceMode.Impulse);

        ray = new Ray(m_Tranform.position, dir);
        Physics.Raycast(ray, out hit, 1000, 1 << LayerMask.NameToLayer("EnvModel"));
    }
}
