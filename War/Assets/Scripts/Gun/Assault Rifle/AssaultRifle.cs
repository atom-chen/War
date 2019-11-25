using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 突击步枪C层.
/// </summary>
public class AssaultRifle : GunWeaponContollerBase
{
    private AssaultRifleView m_AssaultRifleView;

    private ObjectPool[] objectPools;               // 对象池临时资源管理.

    protected override void FindAndLoadInit()
    {
        m_AssaultRifleView = m_GunViewBase as AssaultRifleView;

        objectPools = gameObject.GetComponents<ObjectPool>();
    }

    protected override void LoadAudio()
    {
        M_Audio = Resources.Load<AudioClip>("Audios/Gun/AssaultRifle_Fire");
    }

    protected override void PlayAudio()
    {
        AudioSource.PlayClipAtPoint(M_Audio, m_AssaultRifleView.GunPoint.position);
    }

    protected override void LoadEffect()
    {
        Effect = Resources.Load<GameObject>("Effects/Gun/AssaultRifle_GunPoint_Effect");
    }

    protected override void PlayEffect()
    {
        PlayGunPointEffect();
        PlayShellEffect();
    }

    private void PlayGunPointEffect()
    {
        GameObject go;
        if (objectPools[0].IsEmpty())
        {
            go = GameObject.Instantiate<GameObject>(Effect, m_AssaultRifleView.ShellPoint.position,
                Quaternion.identity, m_AssaultRifleView.EffectParent);
        }
        else
        {
            go = objectPools[0].GetObject();
            go.GetComponent<Transform>().position = m_AssaultRifleView.ShellPoint.position;
        }
         
        go.GetComponent<ParticleSystem>().Play();
        StartCoroutine(DelayToPool(objectPools[0], go, 0.5f));
    }

    private void PlayShellEffect()
    {
        GameObject go;
        if (objectPools[1].IsEmpty())
        {
            go = GameObject.Instantiate<GameObject>(m_AssaultRifleView.Prefab_Shell,
                m_AssaultRifleView.ShellPoint.position, m_AssaultRifleView.ShellPoint.rotation,
                m_AssaultRifleView.ShellParent);
        }
        else
        {
            go = objectPools[1].GetObject();
            Transform tempTransform = go.GetComponent<Transform>();
            Rigidbody tempRigidbody = go.GetComponent<Rigidbody>();

            tempRigidbody.isKinematic = true;
            tempTransform.position = m_AssaultRifleView.ShellPoint.position;
            tempTransform.rotation = m_AssaultRifleView.ShellPoint.rotation;
            tempRigidbody.isKinematic = false;
        }
         
        go.GetComponent<Rigidbody>().AddForce(Random.Range(50f, 70f) * m_AssaultRifleView.ShellPoint.up);

        StartCoroutine(DelayToPool(objectPools[1], go, 1.0f));
    }

    /// <summary>
    /// 射击.
    /// </summary>
    protected override void Shoot()
    {
        if (hit.point != Vector3.zero)
        {
            BulletMark bulletMark = hit.collider.GetComponent<BulletMark>();
            AIModel ai = hit.collider.GetComponentInParent<AIModel>();

            // 射击到环境物体.
            if (bulletMark != null)
            {
                bulletMark.CreateBulletMark(hit);
                bulletMark.Hp -= Damage;
            }

            // 攻击AI角色.
            else if (ai != null)
            {
                ai.Life -= Damage;
            }
        }
        else
        {
            GameObject.Instantiate<GameObject>(m_AssaultRifleView.Prefab_Bullet,
                hit.point, Quaternion.identity);
        }

        Durable--;
    }
}
