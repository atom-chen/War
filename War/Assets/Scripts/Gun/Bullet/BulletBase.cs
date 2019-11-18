using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹基类.
/// </summary>
public abstract class BulletBase : MonoBehaviour
{
    private Transform m_Transform;
    private Rigidbody m_Rigidbody;

    private int damage;                         // 武器伤害值.

    // 射线决定弹痕信息.
    protected Ray ray;
    protected RaycastHit hit;

    public Transform M_Transform { get => m_Transform; }
    public Rigidbody M_Rigidbody { get => m_Rigidbody; }
    public int Damage { get => damage; set => damage = value; }

    void Awake()
    {
        InitBase();
        FindAndLoadInit();
    }

    void OnCollisionEnter(Collision other)
    {
        CollisionEnter(other);
    }

    /// <summary>
    /// 初始化基类.
    /// </summary>
    private void InitBase()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 弓箭、长矛尾部动画效果.
    /// </summary>
    protected IEnumerator TailAnimation(Transform pivotTransform)
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
            pivotTransform.localRotation = startRot * Quaternion.Euler(
                new Vector3(Random.Range(-range, range), Random.Range(-range, range), 0));

            // 平滑阻尼.
            range = Mathf.SmoothDamp(range, 0, ref arg, stopTime - Time.time);

            yield return null;
        }
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    protected abstract void FindAndLoadInit();

    /// <summary>
    /// 发射武器.
    /// </summary>
    /// <param name="dir">方向.</param>
    /// <param name="force">力度.</param>
    /// <param name="damage">弹头伤害.</param>
    public abstract void Shoot(Vector3 dir, int force, int damage);

    /// <summary>
    /// 碰撞事件.
    /// </summary>
    protected abstract void CollisionEnter(Collision other);
}
