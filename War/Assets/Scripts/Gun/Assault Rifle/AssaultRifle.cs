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
    [SerializeField]
    private int id;                             // 武器编号.
    [SerializeField]
    private int damage;                         // 武器伤害.
    [SerializeField]
    private int durable;                        // 武器耐久.
    [SerializeField]
    private GunType m_GunType;                  // 武器类型.

    private AudioClip m_Audio;                  // 射击音效.
    private GameObject effect;                  // 射击特效.

    // 射击相关.
    private Ray ray;
    private RaycastHit hit;

    private ObjectPool[] objectPools;           // 对象池临时资源管理.

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
                GameObject.Destroy(m_AssaultRifleView.GunStar.gameObject);
                GameObject.Destroy(gameObject);
            }
        }
    }
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

        objectPools = gameObject.GetComponents<ObjectPool>();
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
        GameObject go;
        if (objectPools[0].IsEmpty())
        {
            go = GameObject.Instantiate<GameObject>(effect, m_AssaultRifleView.ShellPoint.position,
                Quaternion.identity, m_AssaultRifleView.EffectParent);
        }
        else
        {
            go = objectPools[0].GetObject();
            go.GetComponent<Transform>().position = m_AssaultRifleView.ShellPoint.position;
        }
         
        go.GetComponent<ParticleSystem>().Play();
        StartCoroutine(DelayToPool(objectPools[0], go, 0.5f));
    }

    /// <summary>
    /// 播放弹壳弹出特效.
    /// </summary>
    private void PlayShellEffect()
    {
        GameObject go;
        if (objectPools[1].IsEmpty())
        {
            go = GameObject.Instantiate<GameObject>(m_AssaultRifleView.Prefab_Shell,
                m_AssaultRifleView.ShellPoint.position, m_AssaultRifleView.ShellPoint.rotation,
                m_AssaultRifleView.ShellParent);
        }
        else
        {
            go = objectPools[1].GetObject();
            Transform tempTransform = go.GetComponent<Transform>();
            Rigidbody tempRigidbody = go.GetComponent<Rigidbody>();

            tempRigidbody.isKinematic = true;
            tempTransform.position = m_AssaultRifleView.ShellPoint.position;
            tempTransform.rotation = m_AssaultRifleView.ShellPoint.rotation;
            tempRigidbody.isKinematic = false;
        }
         
        go.GetComponent<Rigidbody>().AddForce(Random.Range(50f, 70f) * m_AssaultRifleView.ShellPoint.up);

        StartCoroutine(DelayToPool(objectPools[1], go, 1.0f));
    }

    /// <summary>
    /// 延时进入对象池.
    /// </summary>
    private IEnumerator DelayToPool(ObjectPool pool, GameObject go, float time)
    {
        yield return new WaitForSeconds(time);
        pool.AddObject(go);
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
                bulletMark.Hp -= Damage;
            }
        }
        else
        {
            GameObject.Instantiate<GameObject>(m_AssaultRifleView.Prefab_Bullet,
                hit.point, Quaternion.identity);
        }

        Durable--;
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
