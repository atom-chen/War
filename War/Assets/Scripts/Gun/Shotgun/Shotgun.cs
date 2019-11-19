using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 霰弹枪C层.
/// </summary>
public class Shotgun : GunWeaponContollerBase
{
    private ShotgunView m_ShotgunView;

    private const int bulletCount = 5;                      // 弹头数量.

    private ObjectPool[] objectPools;                       // 对象池临时资源管理.

    protected override void FindAndLoadInit()
    {
        m_ShotgunView = m_GunViewBase as ShotgunView;
        objectPools = gameObject.GetComponents<ObjectPool>();
    }

    protected override void LoadAudio()
    {
        M_Audio = Resources.Load<AudioClip>("Audios/Gun/Shotgun_Fire");
    }

    protected override void LoadEffect()
    {
        Effect = Resources.Load<GameObject>("Effects/Gun/Shotgun_GunPoint_Effect");
    }

    protected override void PlayAudio()
    {
        AudioSource.PlayClipAtPoint(M_Audio, m_ShotgunView.ShellPoint.position);
    }

    /// <summary>
    /// 播放换弹声音.
    /// </summary>
    private void PlayPumpAudio()
    {
        AudioSource.PlayClipAtPoint(m_ShotgunView.PumpAudio, m_ShotgunView.ShellPoint.position);
    }

    protected override void PlayEffect()
    {
        PlayGunPointEffect();
        PlayShellEffect();
    }

    /// <summary>
    /// 播放枪口特效.
    /// </summary>
    private void PlayGunPointEffect()
    {
        GameObject go;
        if (objectPools[0].IsEmpty())
        {
            go = GameObject.Instantiate<GameObject>(Effect, m_ShotgunView.GunPoint.position,
                m_ShotgunView.GunPoint.rotation, m_ShotgunView.EffectParent);
        }
        else
        {
            go = objectPools[0].GetObject();
            Transform tempTransform = go.GetComponent<Transform>();
            tempTransform.position = m_ShotgunView.GunPoint.position;
            tempTransform.rotation = m_ShotgunView.GunPoint.rotation;
        }
        go.GetComponent<ParticleSystem>().Play();

        StartCoroutine(base.DelayToPool(objectPools[0], go, 0.5f));
    }

    /// <summary>
    /// 播放弹壳弹出特效.
    /// </summary>
    private void PlayShellEffect()
    {
        GameObject go;
        if (objectPools[1].IsEmpty())
        {
            go = GameObject.Instantiate<GameObject>(m_ShotgunView.Prefab_Shell,
                m_ShotgunView.ShellPoint.position, m_ShotgunView.ShellPoint.rotation,
                m_ShotgunView.ShellParent);
        }
        else
        {
            go = objectPools[1].GetObject();
            Transform tempTransform = go.GetComponent<Transform>();
            Rigidbody tempRigidbody = go.GetComponent<Rigidbody>();

            tempRigidbody.isKinematic = true;
            tempTransform.position = m_ShotgunView.ShellPoint.position;
            tempTransform.rotation = m_ShotgunView.ShellPoint.rotation;
            tempRigidbody.isKinematic = false;
        }

        go.GetComponent<Rigidbody>().AddForce(m_ShotgunView.ShellPoint.up * Random.Range(70f, 90f));

        StartCoroutine(base.DelayToPool(objectPools[1], go, 2.0f));
    }

    protected override void Shoot()
    {
        StartCoroutine("CreateBullets");

        Durable--;
    }

    /// <summary>
    /// 生成一个弹头.
    /// </summary>
    private void CreateOneBullet()
    {
        Vector3 offset = new Vector3(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f), 0);
        GameObject go;
        if (objectPools[2].IsEmpty())
        {
            go = GameObject.Instantiate<GameObject>(m_ShotgunView.Prefab_Bullet, 
                m_ShotgunView.GunPoint.position, Quaternion.identity, 
                m_ShotgunView.BulletParent);  
        }
        else
        {
            go = objectPools[2].GetObject();
            Transform tempTransform = go.GetComponent<Transform>();

            // 防止拖尾突变形成闪烁.
            tempTransform.Find("Trail").gameObject.SetActive(false);
            tempTransform.position = m_ShotgunView.GunPoint.position;
            go.GetComponent<Rigidbody>().WakeUp();
        }

        go.GetComponent<ShotgunBullet>().Shoot(m_ShotgunView.GunPoint.forward + offset, 3, Damage / bulletCount);
        go.GetComponent<Transform>().Find("Trail").gameObject.SetActive(true);

        StartCoroutine(DelayToPool(objectPools[2], go, 2.0f));
    }

    protected override IEnumerator DelayToPool(ObjectPool pool, GameObject go, float time)
    {
        yield return new WaitForSeconds(time);

        if (go.GetComponent<Rigidbody>().IsSleeping() == false)
            go.GetComponent<Rigidbody>().Sleep();

        pool.AddObject(go);
    }

    /// <summary>
    /// 生成全部弹头.
    /// </summary>
    private IEnumerator CreateBullets()
    {
        for (int i = 0; i < bulletCount; ++i)
        {
            CreateOneBullet();

            yield return new WaitForSeconds(0.01f);
        }
    }
}
