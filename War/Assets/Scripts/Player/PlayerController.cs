using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色逻辑控制器.
/// </summary>
public class PlayerController : MonoBehaviour
{
    private Transform m_Transform;

    private GameObject m_BuildingPlan;              // 建造角色.
    private GameObject m_WoodenSpear;               // 长矛角色.

    private GameObject currentWeapon;               // 当前角色.
    private GameObject targetWeapon;                // 目标角色.

    void Start()
    {
        FindAndLoadInit();
    }

    void Update()
    {
        // 按下B建造.
        if (Input.GetKeyDown(KeyCode.B))
        {
            targetWeapon = m_BuildingPlan;
            Change();
        }

        // 按下N长矛.
        if (Input.GetKeyDown(KeyCode.N))
        {
            targetWeapon = m_WoodenSpear;
            Change();
        }
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Transform = gameObject.GetComponent<Transform>();

        m_BuildingPlan = m_Transform.Find("EnvCamera/PersonCamera/Building Plan").gameObject;
        m_WoodenSpear = m_Transform.Find("EnvCamera/PersonCamera/Wooden Spear").gameObject;

        currentWeapon = m_BuildingPlan;
        m_WoodenSpear.SetActive(false);
    }

    /// <summary>
    /// 切换装备.
    /// </summary>
    private void Change()
    {
        currentWeapon.GetComponent<Animator>().SetTrigger("Holster");

        StartCoroutine("DelayToHide");
    }

    /// <summary>
    /// 延迟时间, 隐藏角色.
    /// </summary>
    private IEnumerator DelayToHide()
    {
        yield return new WaitForSeconds(1);
        currentWeapon.SetActive(false);
        targetWeapon.SetActive(true);

        currentWeapon = targetWeapon;
    }
}
