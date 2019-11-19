using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 枪械武器二级父类.
/// </summary>
public abstract class GunWeaponContollerBase : GunControllerBase
{
    private GameObject effect;                  // 射击特效.

    public GameObject Effect { get => effect; set => effect = value; }

    void OnEnable()
    {
        if (m_GunViewBase != null)
            m_GunViewBase.ShowGunStar();
    }

    protected override void Start()
    {
        base.Start();
        LoadEffect();
    }

    protected override void LeftMouseButtonDown()
    {
        base.LeftMouseButtonDown();
        PlayEffect();
    }

    protected override void RightMouseButtonDown()
    {
        base.RightMouseButtonDown();
        m_GunViewBase.GunStar.gameObject.SetActive(false);
    }

    protected override void RightMouseButtonUp()
    {
        base.RightMouseButtonUp();
        m_GunViewBase.GunStar.gameObject.SetActive(true);
    }

    /// <summary>
    /// 加载特效.
    /// </summary>
    protected abstract void LoadEffect();

    /// <summary>
    /// 播放特效.
    /// </summary>
    protected abstract void PlayEffect();
}
