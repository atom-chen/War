using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 长矛V层.
/// </summary>
public class WoodenSpearView : GunViewBase
{
    private GameObject prefab_Spear;                // 长矛模型.

    public GameObject Prefab_Spear { get => prefab_Spear; }

    protected override void FindAndLoadInit()
    {
        prefab_Spear = Resources.Load<GameObject>("Gun/Bullet/Wooden_Spear");
    }

    protected override void FindGunPoint()
    {
        gunPoint = M_Transform.Find("Armature/Arm_R/Forearm_R/Wrist_R/Weapon/EffectPos_A");
    }

    protected override void InitHoldPose()
    {
        startPos = M_Transform.localPosition;
        startRot = M_Transform.localRotation.eulerAngles;
        endPos = new Vector3(0, -1.58f, 0.32f);
        endRot = new Vector3(0, 4, 0.3f);
    }
}
