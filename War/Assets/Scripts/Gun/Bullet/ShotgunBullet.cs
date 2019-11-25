using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 霰弹枪子弹管理器.
/// </summary>
public class ShotgunBullet : BulletBase
{
    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    protected override void FindAndLoadInit()
    {
        
    }

    /// <summary>
    /// 发射子弹.
    /// </summary>
    /// <param name="dir">方向.</param>
    /// <param name="force">力度.</param>
    /// <param name="damage">弹头伤害.</param>
    public override void Shoot(Vector3 dir, int force, int damage)
    {
        this.Damage = damage;

        M_Rigidbody.AddForce(dir * force, ForceMode.Impulse);

        ray = new Ray(M_Transform.position, dir);
        int layer = (1 << LayerMask.NameToLayer("EnvModel")) | (1 << LayerMask.NameToLayer("AIModel"));
        Physics.Raycast(ray, out hit, 1000, layer);
    }

    protected override void CollisionEnter(Collision other)
    {
        M_Rigidbody.Sleep();

        BulletMark bulletMark = other.gameObject.GetComponent<BulletMark>();
        AIModel ai = other.gameObject.GetComponentInParent<AIModel>();

        // 攻击环境物体.
        if (bulletMark != null)
        {
            bulletMark.CreateBulletMark(hit);
            bulletMark.Hp -= Damage;
        }

        // 攻击AI角色.
        else if (ai != null)
        {
            ai.Life -= Damage;
            ai.PlayEffect(hit);
        }

        gameObject.SetActive(false);
    }
}
