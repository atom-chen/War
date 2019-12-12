using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 台阶模型建造类.
/// </summary>
public class StairsController : MaterialModelBase
{
    private const float stairOffset = 2.5f;

    protected override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "PlatformToWall" && other.gameObject.tag != "Terrain")
        {
            canPut = false;
        }
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
        if (other.gameObject.tag == "PlatformToWall")
        {
            canPut = true;
            isAttach = true;

            Vector3 offset = Vector3.zero;
            Quaternion rotation = Quaternion.identity;

            switch (other.gameObject.name)
            {
                case "A":
                    offset = Vector3.forward * stairOffset;
                    rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                    break;

                case "B":
                    offset = Vector3.right * stairOffset;
                    rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    break;

                case "C":
                    offset = Vector3.back * stairOffset;
                    rotation = Quaternion.Euler(new Vector3(0, -90, 0));
                    break;

                case "D":
                    offset = Vector3.left * stairOffset;
                    rotation = Quaternion.identity;
                    break;
            }

            m_Transform.position = other.GetComponent<Transform>().parent.position + offset;
            m_Transform.rotation = rotation;
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlatformToWall")
        {
            canPut = true;
            isAttach = true;
        }

        // 放置之后, 不能再次在此地放置物体.
        if (other.gameObject.tag == gameObject.tag &&
            other.gameObject.GetComponent<Transform>().position == m_Transform.position)
        {
            canPut = false;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
    }
}
