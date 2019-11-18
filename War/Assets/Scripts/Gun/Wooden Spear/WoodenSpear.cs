using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 长矛C层.
/// </summary>
public class WoodenSpear : ThrowWeaponContollerBase
{
    private WoodenSpearView m_WoodenSpearView;

    protected override void FindAndLoadInit()
    {
        m_WoodenSpearView = m_GunViewBase as WoodenSpearView;
        CanShoot(0);
    }

    protected override void LoadAudio()
    {
        M_Audio = Resources.Load<AudioClip>("Audios/Gun/Arrow Release");
    }

    protected override void PlayAudio()
    {
        AudioSource.PlayClipAtPoint(M_Audio, m_WoodenSpearView.GunPoint.position);
    }

    protected override void Shoot()
    {
        GameObject go = GameObject.Instantiate<GameObject>(m_WoodenSpearView.Prefab_Spear,
            m_WoodenSpearView.GunPoint.position, m_WoodenSpearView.GunPoint.rotation);
        go.GetComponent<Arrow>().Shoot(m_WoodenSpearView.GunPoint.forward, 80, Damage);
    }
}
