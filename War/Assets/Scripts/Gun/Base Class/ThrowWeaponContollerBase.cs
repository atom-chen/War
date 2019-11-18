using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 投掷武器二级父类.
/// </summary>
public abstract class ThrowWeaponContollerBase : GunControllerBase
{
    protected override void Start()
    {
        base.Start();
        m_GunViewBase.GunStar.gameObject.SetActive(false);
    }

    protected override void RightMouseButtonDown()
    {
        base.RightMouseButtonDown();
        m_GunViewBase.GunStar.gameObject.SetActive(true);
    }

    protected override void RightMouseButtonUp()
    {
        base.RightMouseButtonUp();
        m_GunViewBase.GunStar.gameObject.SetActive(false);
    }
}
