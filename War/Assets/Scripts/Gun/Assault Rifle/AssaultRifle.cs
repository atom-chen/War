using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 突击步枪C层.
/// </summary>
public class AssaultRifle : MonoBehaviour
{
    private AssaultRifleView m_AssaultRifleView;

    // 枪械类.
    private int id;                             // 武器编号.
    private int damage;                         // 武器伤害.
    private int durable;                        // 武器耐久.
    private GunType m_GunType;                  // 武器类型.

    private AudioClip m_Audio;                  // 射击音效.
    private GameObject effect;                  // 射击特效.      

    public int Id { get => id; set => id = value; }
    public int Damage { get => damage; set => damage = value; }
    public int Durable { get => durable; set => durable = value; }
    public GunType M_GunType { get => m_GunType; set => m_GunType = value; }
    public AudioClip M_Audio { get => m_Audio; set => m_Audio = value; }
    public GameObject Effect { get => effect; set => effect = value; }

    void Start()
    {
        FindAndLoadInit();
    }

    void Update()
    {
        MouseControl();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_AssaultRifleView = gameObject.GetComponent<AssaultRifleView>();
    }

    /// <summary>
    /// 播放音效.
    /// </summary>
    private void PlayAudio()
    {

    }

    /// <summary>
    /// 播放特效.
    /// </summary>
    private void PlayEffect()
    {

    }

    /// <summary>
    /// 射击.
    /// </summary>
    private void Shoot()
    {

    }

    /// <summary>
    /// 鼠标控制.
    /// </summary>
    private void MouseControl()
    {
        // 按下鼠标左键射击.
        if (Input.GetMouseButtonDown(0))
        {
            m_AssaultRifleView.M_Animator.SetTrigger("Fire");
        }

        // 按下鼠标右键开镜.
        if (Input.GetMouseButtonDown(1))
        {
            m_AssaultRifleView.M_Animator.SetBool("HoldPose", true);
            m_AssaultRifleView.EnterHoldPose();
        }

        // 松开鼠标右键退出开镜.
        if (Input.GetMouseButtonUp(1))
        {
            m_AssaultRifleView.M_Animator.SetBool("HoldPose", false);
            m_AssaultRifleView.ExitHoldPose();
        }
    }
}
