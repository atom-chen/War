using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 建造角色控制器.
/// </summary>
public class BuildingPlan : MonoBehaviour
{
    void OnEnable()
    {
        InputManager.Instance.BuildState = true;
    }

    void OnDisable()
    {
        InputManager.Instance.BuildState = false;
    }

    /// <summary>
    /// 隐藏建造模型.
    /// </summary>
    public void HolsterBuildingPlan()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Holster");
    }
}
