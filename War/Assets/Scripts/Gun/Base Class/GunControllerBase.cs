﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 枪械模块C层顶级父类.
/// </summary>
public abstract class GunControllerBase : MonoBehaviour
{
    protected GunViewBase m_GunViewBase;

    // 枪械类.
    [SerializeField]
    private int id;                             // 武器编号.
    [SerializeField]
    private int damage;                         // 武器伤害.
    [SerializeField]
    private int durable;                        // 武器耐久.
    [SerializeField]
    private GunType m_GunType;                  // 武器类型.

    private AudioClip m_Audio;                  // 射击音效.

    // 射击相关.
    private Ray ray;
    protected RaycastHit hit;

    private bool canShoot = true;               // 当前是否可以射击.

    public int Id { get => id; set => id = value; }
    public int Damage { get => damage; set => damage = value; }
    public int Durable
    {
        get => durable;
        set
        {
            durable = value;
            if (durable == 0)
            {
                // 销毁准星和武器.
                GameObject.Destroy(m_GunViewBase.GunStar.gameObject);
                GameObject.Destroy(gameObject);
            }
        }
    }
    public GunType M_GunType { get => m_GunType; set => m_GunType = value; }
    public AudioClip M_Audio { get => m_Audio; set => m_Audio = value; }

    protected virtual void Start()
    {
        InitBase();
        FindAndLoadInit();
        LoadAudio();
    }

    void Update()
    {
        MouseControl();
        ShootReady();
    }

    /// <summary>
    /// 基类初始化.
    /// </summary>
    private void InitBase()
    {
        m_GunViewBase = gameObject.GetComponent<GunViewBase>();
    }

    /// <summary>
    /// 鼠标控制.
    /// </summary>
    private void MouseControl()
    {
        // 按下鼠标左键射击.
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            LeftMouseButtonDown();
        }

        // 按下鼠标右键开镜.
        if (Input.GetMouseButtonDown(1))
        {
            RightMouseButtonDown();            
        }

        // 松开鼠标右键退出开镜.
        if (Input.GetMouseButtonUp(1))
        {
            RightMouseButtonUp();
        }
    }

    /// <summary>
    /// 按下鼠标左键.
    /// </summary>
    protected virtual void LeftMouseButtonDown()
    {
        m_GunViewBase.M_Animator.SetTrigger("Fire");
        Shoot();
        PlayAudio();        
    }

    /// <summary>
    /// 按下鼠标右键.
    /// </summary>
    protected virtual void RightMouseButtonDown()
    {
        m_GunViewBase.M_Animator.SetBool("HoldPose", true);
        m_GunViewBase.EnterHoldPose();
    }

    /// <summary>
    /// 抬起鼠标右键.
    /// </summary>
    protected virtual void RightMouseButtonUp()
    {
        m_GunViewBase.M_Animator.SetBool("HoldPose", false);
        m_GunViewBase.ExitHoldPose();
    }

    /// <summary>
    /// 射击准备.
    /// </summary>
    private void ShootReady()
    {
        ray = new Ray(m_GunViewBase.GunPoint.position, m_GunViewBase.GunPoint.forward);
        if (Physics.Raycast(ray, out hit))
        {
            // 准星跟随.
            Vector2 pos = RectTransformUtility.WorldToScreenPoint(m_GunViewBase.M_EnvCamera, hit.point);
            m_GunViewBase.GunStar.position = pos;
        }
        else
        {
            hit.point = Vector3.zero;
        }
    }

    /// <summary>
    /// 延时进入对象池.
    /// </summary>
    protected virtual IEnumerator DelayToPool(ObjectPool pool, GameObject go, float time)
    {
        yield return new WaitForSeconds(time);
        pool.AddObject(go);
    }

    /// <summary>
    /// 动作事件改变射击状态.
    /// </summary>
    protected void CanShoot(int state)
    {
        if (state == 0)
            canShoot = false;
        else
            canShoot = true;
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    protected abstract void FindAndLoadInit();

    /// <summary>
    /// 加载音效.
    /// </summary>
    protected abstract void LoadAudio();

    /// <summary>
    /// 播放音效.
    /// </summary>
    protected abstract void PlayAudio();

    /// <summary>
    /// 射击逻辑.
    /// </summary>
    protected abstract void Shoot();
}