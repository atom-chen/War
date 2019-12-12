using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingLightController : MaterialModelBase
{
    private GameObject targetTrigger;                   // 触发的目标物体.

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RoofToLight")
        {
            canPut = true;
        }
        else
        {
            canPut = false;
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
        if (isAttach)
            return;

        if (other.gameObject.tag == "RoofToLight")
        {
            canPut = true;
            isAttach = true;

            m_Transform.position = other.GetComponent<Transform>().position;
            m_Transform.SetParent(other.GetComponent<Transform>().parent);

            targetTrigger = other.gameObject;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "RoofToLight")
        {
            canPut = false;
            isAttach = false;
        }
    }

    public override void NormalModel()
    {
        base.NormalModel();

        if (targetTrigger != null)
            GameObject.Destroy(targetTrigger);
    }
}
