using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 采集角色控制器.
/// </summary>
public class StoneHatchet : MonoBehaviour
{
    private Transform m_Transform;
    private Animator m_Animator;

    private Transform rayPoint_Transform;       // 射线起始点和方向.

    private int damage;                         // 武器伤害.
    private int durable;                        // 武器耐久.
    private float durable_Bak;                  // 武器耐久备份.
    private GunType m_GunType;                  // 武器类型.

    private GameObject toolBarItem;             // 在工具栏的背包物品.

    // 射击相关.
    private Ray ray;
    protected RaycastHit hit;

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
                GameObject.Destroy(gameObject);
            }
        }
    }
    public GunType M_GunType { get => m_GunType; set => m_GunType = value; }
    public GameObject ToolBarItem { get => toolBarItem; set => toolBarItem = value; }

    void Start()
    {
        FindAndLoadInit();
    }

    void Update()
    {
        LeftMouseButtonDown();
        ShootReady();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Animator = gameObject.GetComponent<Animator>();

        rayPoint_Transform = m_Transform.Find("Armature/Root/Hand_R/Weapon/RayPoint");
    }

    /// <summary>
    /// 放下采集武器.
    /// </summary>
    public void HolsterStoneHatchet()
    {
        m_Animator.SetTrigger("Holster");
    }

    /// <summary>
    /// 鼠标左键开始攻击.
    /// </summary>
    private void LeftMouseButtonDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    /// <summary>
    /// 石斧攻击方法.
    /// </summary>
    private void Attack()
    {
        m_Animator.SetTrigger("Hit");
        Durable--;

        UpdateBarUI();
    }

    /// <summary>
    /// 动做事件碰撞到障碍, 采集.
    /// </summary>
    private void AttackEnvModel()
    {
        if (hit.collider != null && hit.collider.tag == "Stone")
        {
            hit.collider.GetComponentInParent<BulletMark>().HatchetHit(hit, damage);
        }
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
        ray = new Ray(rayPoint_Transform.position, rayPoint_Transform.forward);
        Physics.Raycast(ray, out hit, 2.0f);
    }
}
