using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 霰弹枪C层.
/// </summary>
public class Shotgun : GunControllerBase
{
    private ShotgunView m_ShotgunView;

    private const int bulletCount = 10;                     // 弹头数量.

    protected override void FindAndLoadInit()
    {
        m_ShotgunView = m_GunViewBase as ShotgunView;
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
        GameObject go = GameObject.Instantiate<GameObject>(Effect, m_ShotgunView.GunPoint.position,
            m_ShotgunView.GunPoint.rotation);
        go.GetComponent<ParticleSystem>().Play();
        GameObject.Destroy(go, 0.5f);
    }

    /// <summary>
    /// 播放弹壳弹出特效.
    /// </summary>
    private void PlayShellEffect()
    {
        GameObject go = GameObject.Instantiate<GameObject>(m_ShotgunView.Prefab_Shell,
            m_ShotgunView.ShellPoint.position, m_ShotgunView.ShellPoint.rotation);
        go.GetComponent<Rigidbody>().AddForce(m_ShotgunView.ShellPoint.up * Random.Range(70f, 90f));
        GameObject.Destroy(go, 3);
    }

    protected override void Shoot()
    {
        StartCoroutine("CreateBullets");
    }

    /// <summary>
    /// 生成全部弹头.
    /// </summary>
    private IEnumerator CreateBullets()
    {
        for (int i = 0; i < bulletCount; ++i)
        {
            Vector3 offset = new Vector3(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f), 0);
            GameObject go = GameObject.Instantiate<GameObject>(m_ShotgunView.Prefab_Bullet,
                m_ShotgunView.GunPoint.position, Quaternion.identity);
            go.GetComponent<ShotgunBullet>().Shoot(m_ShotgunView.GunPoint.forward + offset, 3000);

            yield return new WaitForSeconds(0.01f);
        }
    }
}
