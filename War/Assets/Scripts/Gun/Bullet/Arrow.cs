using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弓箭、长矛管理器.
/// </summary>
public class Arrow : MonoBehaviour
{
    private Transform m_Transform;
    private Rigidbody m_Rigidbody;
    private BoxCollider m_BoxCollider;

    private Transform m_PivotTransform;         // 中心点用于尾部动画.

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

            StartCoroutine("TailAnimation");
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

        m_PivotTransform = m_Transform.Find("Pivot");
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

    /// <summary>
    /// 弓箭、长矛尾部动画效果.
    /// </summary>
    private IEnumerator TailAnimation()
    {
        // 动画结束时间.
        float stopTime = Time.time + Random.Range(1f, 2f);

        // 动画偏转范围.
        float range = 1.0f;
        float arg = 0.0f;

        // 起始旋转.
        Quaternion startRot = Quaternion.Euler(new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0));

        while (Time.time < stopTime)
        {
            m_PivotTransform.localRotation = startRot * Quaternion.Euler(
                new Vector3(Random.Range(-range, range), Random.Range(-range, range), 0));

            // 平滑阻尼.
            range = Mathf.SmoothDamp(range, 0, ref arg, stopTime - Time.time);

            yield return null;
        }
    }
}
