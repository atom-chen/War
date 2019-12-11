using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 屋顶模型建造控制.
/// </summary>
public class RoofController : MaterialModelBase
{
    private const float roofSize = 3.3f;                // 屋顶模型的大小.

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
        if (other.gameObject.tag == "WallToRoof")
        {
            canPut = true;
            isAttach = true;

            m_Transform.position = other.GetComponent<Transform>().position;
        }

        // 屋顶上吸附.
        else if(other.gameObject.tag == "RoofToRoof")
        {
            canPut = true;
            isAttach = true;

            Vector3 centerPos = other.GetComponent<Transform>().parent.position;
            Vector3 offset = Vector3.zero;

            switch (other.gameObject.name)
            {
                case "A":
                    offset = Vector3.left * roofSize;
                    break;

                case "B":
                    offset = Vector3.forward * roofSize;
                    break;

                case "C":
                    offset = Vector3.right * roofSize;
                    break;

                case "D":
                    offset = Vector3.back * roofSize;
                    break;
            }

            m_Transform.position = centerPos + offset;
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
        if (isAttach && canPut)
            return;

        if (other.gameObject.tag == "WallToRoof" || other.gameObject.tag == "RoofToRoof")
        {
            canPut = true;
            isAttach = true;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
    }
}
