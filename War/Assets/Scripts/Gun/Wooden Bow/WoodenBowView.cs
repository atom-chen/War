using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 木弓V层.
/// </summary>
public class WoodenBowView : GunViewBase
{
    private GameObject prefab_Arrow;                // 弓箭预制体.

    public GameObject Prefab_Arrow { get => prefab_Arrow; }

    protected override void FindAndLoadInit()
    {
        prefab_Arrow = Resources.Load<GameObject>("Gun/Bullet/Arrow");
    }

    protected override void FindGunPoint()
    {
        gunPoint = M_Transform.Find("Armature/Arm_L/Forearm_L/Wrist_L/Weapon/EffectPos_A");
    }

    protected override void InitHoldPose()
    {
        startPos = M_Transform.localPosition;
        startRot = M_Transform.localRotation.eulerAngles;
        endPos = new Vector3(0.75f, -1.2f, 0.22f);
        endRot = new Vector3(2.5f, -8, 35);
    }
}
