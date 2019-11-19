using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 枪械模块V层顶级基类.
/// </summary>
public abstract class GunViewBase : MonoBehaviour
{
    private Transform m_Transform;
    private Animator m_Animator;
    private Camera m_EnvCamera;

    // 枪械开镜动作优化.
    protected Vector3 startPos;
    protected Vector3 startRot;
    protected Vector3 endPos;
    protected Vector3 endRot;

    private GameObject prefab_GunStar;                  // 准星UI预制体.

    protected Transform gunPoint;                       // 枪口位置.
    private Transform gunStar;                          // 准星UI.

    protected Transform M_Transform { get => m_Transform; }
    public Animator M_Animator { get => m_Animator; }
    public Camera M_EnvCamera { get => m_EnvCamera; }
    public Transform GunPoint { get => gunPoint; }
    public Transform GunStar { get => gunStar; }

    void Awake()
    {
        InitBase(); 
        CreateGunStar();

        FindAndLoadInit();
        InitHoldPose();
        FindGunPoint();
    }

    /// <summary>
    /// 初始化基类.
    /// </summary>
    private void InitBase()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Animator = gameObject.GetComponent<Animator>();
        m_EnvCamera = GameObject.Find("FPSController/EnvCamera").GetComponent<Camera>();

        prefab_GunStar = Resources.Load<GameObject>("Gun/UI/GunStar");
    }

    /// <summary>
    /// 查找准星.
    /// </summary>
    private void CreateGunStar()
    {
        Transform gunStarParent = GameObject.Find("Canvas/TempPanel").GetComponent<Transform>();
        gunStar = GameObject.Instantiate<GameObject>(prefab_GunStar, gunStarParent).GetComponent<Transform>();
    }

    /// <summary>
    /// 显示准星.
    /// </summary>
    public void ShowGunStar()
    {
        gunStar.gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏准星.
    /// </summary>
    public void HideGunStar()
    {
        if (gunStar != null)
            gunStar.gameObject.SetActive(false);
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

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    protected abstract void FindAndLoadInit();

    /// <summary>
    /// 初始化开镜动作.
    /// </summary>
    protected abstract void InitHoldPose();

    /// <summary>
    /// 查找枪口位置.
    /// </summary>
    protected abstract void FindGunPoint();
}
