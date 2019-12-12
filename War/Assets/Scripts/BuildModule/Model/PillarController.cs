using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 木柱管理器, 用于填充缝隙.
/// </summary>
public class PillarController : MaterialModelBase
{
    private GameObject targetTrigger;                   // 触发目标.

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlatformToPillar")
        {
            canPut = true;
            isAttach = true;

            m_Transform.position = other.GetComponent<Transform>().position;
            m_Transform.rotation = other.GetComponent<Transform>().rotation;

            targetTrigger = other.gameObject;
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlatformToPillar")
        {
            canPut = false;
            isAttach = false;
        }
    }

    public override void NormalModel()
    {
        base.NormalModel();

        if (targetTrigger != null)
        {
            GameObject.Destroy(targetTrigger);
        }
    }
}
