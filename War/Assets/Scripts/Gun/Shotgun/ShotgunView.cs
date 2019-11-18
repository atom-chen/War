using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 霰弹枪V层.
/// </summary>
public class ShotgunView : GunViewBase
{
    private Transform shellPoint;                   // 弹壳弹出位置.
    private AudioClip pumpAudio;                    // 弹壳弹出音效.
    private GameObject prefab_Shell;                // 弹壳预制体.
    private GameObject prefab_Bullet;               // 子弹预制体.

    public Transform ShellPoint { get => shellPoint; }
    public AudioClip PumpAudio { get => pumpAudio; }
    public GameObject Prefab_Shell { get => prefab_Shell; }
    public GameObject Prefab_Bullet { get => prefab_Bullet; }

    protected override void FindAndLoadInit()
    {
        shellPoint = M_Transform.Find("Armature/Weapon/EffectPos_B");
        pumpAudio = Resources.Load<AudioClip>("Audios/Gun/Shotgun_Pump");
        prefab_Shell = Resources.Load<GameObject>("Gun/Bullet/Shotgun_Shell");
        prefab_Bullet = Resources.Load<GameObject>("Gun/Bullet/Shotgun_Bullet");
    }

    protected override void FindGunPoint()
    {
        gunPoint = M_Transform.Find("Armature/Weapon/EffectPos_A");
    }

    protected override void InitHoldPose()
    {
        startPos = M_Transform.localPosition;
        startRot = M_Transform.localRotation.eulerAngles;
        endPos = new Vector3(-0.14f, -1.78f, -0.03f);
        endRot = new Vector3(0, 10, -0.25f);
    }
}
