using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 普通墙壁建造控制器.
/// </summary>
public class WallController : MaterialModelBase
{
    protected override void OnCollisionEnter(Collision other)
    {
    }

    protected override void OnCollisionStay(Collision other)
    {
    }

    protected override void OnCollisionExit(Collision other)
    {
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlatformToWall")
        {
            canPut = true;
            isAttach = true;

            m_Transform.position = other.GetComponent<Transform>().position;
            m_Transform.rotation = other.GetComponent<Transform>().rotation;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlatformToWall")
        {
            canPut = false;
            isAttach = false;
        }
    }
}
