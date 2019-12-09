using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 门型建造管理器.
/// </summary>
public class DoorController : MaterialModelBase
{
    protected override void OnCollisionEnter(Collision other)
    {
        canPut = false;
    }

    protected override void OnCollisionStay(Collision other)
    {
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
        if (other.gameObject.name == "DoorTrigger")
        {
            canPut = true;
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
        if (isAttach)
            return;

        if (other.gameObject.name == "DoorTrigger")
        {
            canPut = true;
            isAttach = true;

            m_Transform.position = other.GetComponent<Transform>().position;
            m_Transform.rotation = other.GetComponent<Transform>().rotation;

            m_Transform.SetParent(other.GetComponent<Transform>().parent);
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "DoorTrigger")
        {
            canPut = false;
            isAttach = false;
        }
    }
}
