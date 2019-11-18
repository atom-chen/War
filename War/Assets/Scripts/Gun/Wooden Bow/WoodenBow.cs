using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 木弓C层.
/// </summary>
public class WoodenBow : ThrowWeaponContollerBase
{
    private WoodenBowView m_WoodenBowView;

    protected override void FindAndLoadInit()
    {
        m_WoodenBowView = m_GunViewBase as WoodenBowView;
        CanShoot(0);
    }

    protected override void LoadAudio()
    {
        M_Audio = Resources.Load<AudioClip>("Audios/Gun/Arrow Release");
    }

    protected override void PlayAudio()
    {
        AudioSource.PlayClipAtPoint(M_Audio, m_WoodenBowView.GunPoint.position);
    }

    protected override void Shoot()
    {
        GameObject go = GameObject.Instantiate<GameObject>(m_WoodenBowView.Prefab_Arrow,
            m_WoodenBowView.GunPoint.position, m_WoodenBowView.GunPoint.rotation);
        go.GetComponent<Arrow>().Shoot(m_WoodenBowView.GunPoint.forward, 200, Damage);
    }
}
