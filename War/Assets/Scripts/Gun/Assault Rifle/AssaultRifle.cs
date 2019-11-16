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

    // 射击相关.
    private Ray ray;
    private RaycastHit hit;

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
        ShootReady();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_AssaultRifleView = gameObject.GetComponent<AssaultRifleView>();

        m_Audio = Resources.Load<AudioClip>("Audios/Gun/AssaultRifle_Fire");
        effect = Resources.Load<GameObject>("Effects/Gun/AssaultRifle_GunPoint_Effect");
    }

    /// <summary>
    /// 播放音效.
    /// </summary>
    private void PlayAudio()
    {
        AudioSource.PlayClipAtPoint(m_Audio, m_AssaultRifleView.GunPoint.position);
    }

    /// <summary>
    /// 播放特效.
    /// </summary>
    private void PlayEffect()
    {
        PlayGunPointEffect();
        PlayShellEffect();
    }

    /// <summary>
    /// 枪口特效播放.
    /// </summary>
    private void PlayGunPointEffect()
    {
        GameObject go = GameObject.Instantiate<GameObject>(effect,
            m_AssaultRifleView.ShellPoint.position, Quaternion.identity);
        go.GetComponent<ParticleSystem>().Play();
    }

    /// <summary>
    /// 播放弹壳弹出特效.
    /// </summary>
    private void PlayShellEffect()
    {
        GameObject go = GameObject.Instantiate<GameObject>(m_AssaultRifleView.Prefab_Shell,
            m_AssaultRifleView.ShellPoint.position, m_AssaultRifleView.ShellPoint.rotation);
        go.GetComponent<Rigidbody>().AddForce(Random.Range(50f, 70f) * m_AssaultRifleView.ShellPoint.up);
    }

    /// <summary>
    /// 射击准备.
    /// </summary>
    private void ShootReady()
    {
        ray = new Ray(m_AssaultRifleView.GunPoint.position, m_AssaultRifleView.GunPoint.forward);
        if (Physics.Raycast(ray, out hit))
        {
            // 准星跟随.
            Vector2 pos = RectTransformUtility.WorldToScreenPoint(m_AssaultRifleView.M_EnvCamera, hit.point);
            m_AssaultRifleView.GunStar.position = pos;
        }
        else
        {
            hit.point = Vector3.zero;
        }
    }

    /// <summary>
    /// 射击.
    /// </summary>
    private void Shoot()
    {
        if (hit.point != Vector3.zero)
        {
            BulletMark bulletMark = hit.collider.GetComponent<BulletMark>();
            if (bulletMark != null)
            {
                bulletMark.CreateBulletMark(hit);
            }
        }
        else
        {
            GameObject.Instantiate<GameObject>(m_AssaultRifleView.Prefab_Bullet,
                hit.point, Quaternion.identity);
        }
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
            Shoot();
            PlayAudio();
            PlayEffect();
        }

        // 按下鼠标右键开镜.
        if (Input.GetMouseButtonDown(1))
        {
            m_AssaultRifleView.M_Animator.SetBool("HoldPose", true);
            m_AssaultRifleView.EnterHoldPose();
            m_AssaultRifleView.GunStar.gameObject.SetActive(false);
        }

        // 松开鼠标右键退出开镜.
        if (Input.GetMouseButtonUp(1))
        {
            m_AssaultRifleView.M_Animator.SetBool("HoldPose", false);
            m_AssaultRifleView.ExitHoldPose();
            m_AssaultRifleView.GunStar.gameObject.SetActive(true);
        }
    }
}
