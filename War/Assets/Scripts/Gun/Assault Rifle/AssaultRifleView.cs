using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 突击步枪V层.
/// </summary>
public class AssaultRifleView : GunViewBase
{
    private Transform shellPoint;                   // 弹壳弹出位置.

    private GameObject prefab_Bullet;               // 子弹预制体(临时).
    private GameObject prefab_Shell;                // 弹壳预制体.

    private Transform effectParent;                 // 特效父物体.
    private Transform shellParent;                  // 弹壳父物体.

    public Transform ShellPoint { get => shellPoint; }
    public GameObject Prefab_Bullet { get => prefab_Bullet; }
    public GameObject Prefab_Shell { get => prefab_Shell; }
    public Transform EffectParent { get => effectParent; }
    public Transform ShellParent { get => shellParent; }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    protected override void FindAndLoadInit()
    {
        shellPoint = M_Transform.Find("Armature/Weapon/EffectPos_B");

        prefab_Bullet = Resources.Load<GameObject>("Gun/Bullet/Bullet");
        prefab_Shell = Resources.Load<GameObject>("Gun/Bullet/Shell");

        effectParent = GameObject.Find("TempObject/Effect_GunPoint_Parent").GetComponent<Transform>();
        shellParent = GameObject.Find("TempObject/Effect_Shell_Parent").GetComponent<Transform>();
    }

    /// <summary>
    /// 初始化开镜动作.
    /// </summary>
    protected override void InitHoldPose()
    {
        startPos = M_Transform.localPosition;
        startRot = M_Transform.localRotation.eulerAngles;
        endPos = new Vector3(-0.065f, -1.85f, 0.25f);
        endRot = new Vector3(2.8f, 1.3f, 0.08f);
    }

    protected override void FindGunPoint()
    {
        gunPoint = M_Transform.Find("Armature/Weapon/EffectPos_A");
    }
}
