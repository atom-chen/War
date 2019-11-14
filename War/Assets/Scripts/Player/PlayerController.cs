using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色逻辑控制器.
/// </summary>
public class PlayerController : MonoBehaviour
{
    private Transform m_Transform;
    private Animator m_Animator;

    private GameObject m_BuildingPlan;              // 建造角色.

    private bool isNormal = false;                  // T: 空手; F: 建造.

    void Start()
    {
        FindAndLoadInnit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Change();
        }
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInnit()
    {
        m_Transform = gameObject.GetComponent<Transform>();

        m_BuildingPlan = m_Transform.Find("EnvCamera/Building Plan").gameObject;
        m_Animator = m_BuildingPlan.GetComponent<Animator>();
    }

    /// <summary>
    /// 切换装备.
    /// </summary>
    private void Change()
    {
        if (isNormal)
        {
            m_BuildingPlan.SetActive(true);
        }
        else
        {
            m_Animator.SetTrigger("Holster");
            StartCoroutine("DelayToHide");
        }

        isNormal = !isNormal;
    }

    /// <summary>
    /// 延迟时间, 隐藏角色.
    /// </summary>
    private IEnumerator DelayToHide()
    {
        yield return new WaitForSeconds(1);
        m_BuildingPlan.SetActive(false);
    }
}
