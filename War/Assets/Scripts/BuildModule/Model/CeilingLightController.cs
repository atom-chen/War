using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingLightController : MaterialModelBase
{
    protected override void OnCollisionEnter(Collision other)
    {
        canPut = false;
    }

    protected override void OnCollisionStay(Collision other)
    {
        if (canPut == false)
            return;

        // 放置之后, 不能再次在此地放置物体.
        if (other.gameObject.tag == gameObject.tag &&
            other.gameObject.GetComponent<Transform>().position == m_Transform.position)
        {
            canPut = false;
        }
    }

    protected override void OnCollisionExit(Collision other)
    {
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (isAttach)
            return;

        // 墙壁上吸附.
        if (other.gameObject.tag == "RoofToLight")
        {
            canPut = true;
            isAttach = true;

            m_Transform.position = other.GetComponent<Transform>().position;
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
        if (isAttach && canPut)
            return;

        if (other.gameObject.tag == "RoofToLight")
        {
            canPut = true;
            isAttach = true;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
    }
}
