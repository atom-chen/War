using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 突击步枪V层.
/// </summary>
public class AssaultRifleView : MonoBehaviour
{
    private Transform m_Transform;
    private Animator m_Animator;
    private Camera m_EnvCamera;

    // 枪械开镜动作优化.
    private Vector3 startPos;
    private Vector3 startRot;
    private Vector3 endPos;
    private Vector3 endRot;

    private Transform gunPoint;                     // 枪口位置.
    private Transform shellPoint;                   // 弹壳弹出位置.
    private Transform gunStar;                      // 准星UI.

    private GameObject prefab_Bullet;               // 子弹预制体(临时).
    private GameObject prefab_Shell;                // 弹壳预制体.

    public Transform M_Transform { get => m_Transform; }
    public Animator M_Animator { get => m_Animator; }
    public Camera M_EnvCamera { get => m_EnvCamera; }
    public Transform GunPoint { get => gunPoint; }
    public Transform ShellPoint { get => shellPoint; }
    public Transform GunStar { get => gunStar; }
    public GameObject Prefab_Bullet { get => prefab_Bullet; }
    public GameObject Prefab_Shell { get => prefab_Shell; }

    void Awake()
    {
        FindAndLoadInit();
        InitHoldPose();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Animator = gameObject.GetComponent<Animator>();
        m_EnvCamera = GameObject.Find("FPSController/EnvCamera").GetComponent<Camera>();

        gunPoint = m_Transform.Find("Armature/Weapon/EffectPos_A");
        shellPoint = m_Transform.Find("Armature/Weapon/EffectPos_B");
        gunStar = GameObject.Find("TempPanel/GunStar").GetComponent<Transform>();

        prefab_Bullet = Resources.Load<GameObject>("Gun/Bullet/Bullet");
        prefab_Shell = Resources.Load<GameObject>("Gun/Bullet/Shell");
    }

    /// <summary>
    /// 初始化开镜动作.
    /// </summary>
    private void InitHoldPose()
    {
        startPos = m_Transform.localPosition;
        startRot = m_Transform.localRotation.eulerAngles;
        endPos = new Vector3(-0.065f, -1.85f, 0.25f);
        endRot = new Vector3(2.8f, 1.3f, 0.08f);
    }

    /// <summary>
    /// 进入开镜状态.
    /// </summary>
    public void EnterHoldPose(float time = 0.2f, float fov = 40.0f)
    {
        m_Transform.DOLocalMove(endPos, time);
        m_Transform.DOLocalRotate(endRot, time);

        m_EnvCamera.DOFieldOfView(fov, time);
    }

    /// <summary>
    /// 退出开镜状态.
    /// </summary>
    public void ExitHoldPose(float time = 0.2f, float fov = 60.0f)
    {
        m_Transform.DOLocalMove(startPos, time);
        m_Transform.DOLocalRotate(startRot, time);

        m_EnvCamera.DOFieldOfView(fov, time);
    }
}
