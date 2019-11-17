using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 霰弹枪C层.
/// </summary>
public class Shotgun : GunControllerBase
{
    private ShotgunView m_ShotgunView;

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

    protected override void Shoot()
    {
    }
}
