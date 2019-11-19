using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 枪械模块C层顶级父类.
/// </summary>
public abstract class GunControllerBase : MonoBehaviour
{
    protected GunViewBase m_GunViewBase;

    // 枪械类.
    private int damage;                         // 武器伤害.
    private int durable;                        // 武器耐久.
    private float durable_Bak;                  // 武器耐久备份.
    private GunType m_GunType;                  // 武器类型.

    private AudioClip m_Audio;                  // 射击音效.

    private GameObject toolBarItem;             // 在工具栏的背包物品.

    // 射击相关.
    private Ray ray;
    protected RaycastHit hit;

    private bool canShoot = true;               // 当前是否可以射击.

    public int Damage { get => damage; set => damage = value; }
    public int Durable
    {
        get => durable;
        set
        {
            durable = value;
            durable_Bak = Mathf.Max(durable, durable_Bak);

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
    public GameObject ToolBarItem { get => toolBarItem; set => toolBarItem = value; }

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

    void OnDisable()
    {
        m_GunViewBase.HideGunStar();
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
        UpdateBarUI();
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
    /// 武器放下动作.
    /// </summary>
    public void HolsterWeapon()
    {
        m_GunViewBase.M_Animator.SetTrigger("Holster");
    }

    /// <summary>
    /// 更新武器耐久UI.
    /// </summary>
    private void UpdateBarUI()
    {
        toolBarItem.GetComponent<InventoryItemController>().UpdateBarUI(durable / durable_Bak);
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
