using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地基建造控制器.
/// </summary>
public class PlatformController : MaterialModelBase
{
    private const float platformWidth = 3.3f;                   // 地基平台的宽度.
    private const float attachRange = 0.4f;                     // 地基相互吸引的的范围.

    protected override void OnCollisionEnter(Collision other)
    {
        // 与环境物体交互.
        if (other.gameObject.tag != "Terrain")
        {
            canPut = false;
        }
    }

    protected override void OnCollisionStay(Collision other)
    {
        if (canPut == false)
            return;

        // 与环境物体交互.
        if (other.gameObject.tag != "Terrain" && other.gameObject.tag != "Platform")
        {
            canPut = false;
        }
        else
        {
            canPut = true;

            // 放置之后, 不能再次在此地放置物体.
            if (other.gameObject.tag == gameObject.tag &&
                other.gameObject.GetComponent<Transform>().position == m_Transform.position)
            {
                canPut = false;
            }
        }
    }

    protected override void OnCollisionExit(Collision other)
    {
        // 离开环境, 最大可能就是地面.
        if (other.gameObject.tag != "Terrain")
        {
            canPut = true;
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (isAttach)
            return;

        if (other.gameObject.tag == "Platform")
        {
            canPut = true;
            isAttach = true;

            Vector3 offset = Vector3.zero;
            Vector3 centerPos = other.GetComponent<Transform>().position;

            float distX = m_Transform.position.x - centerPos.x;
            float distZ = m_Transform.position.z - centerPos.z;

            // 从模型右侧靠近吸附.
            if (distX > 0 && Mathf.Abs(distZ) < attachRange)
            {
                offset = Vector3.right * platformWidth;
            }
            // 从模型左侧靠近吸附.
            else if (distX < 0 && Mathf.Abs(distZ) < attachRange)
            {
                offset = Vector3.left * platformWidth;
            }
            m_Transform.position = centerPos + offset;
            

            if (offset == Vector3.zero)
            {
                // 从模型前方靠近吸附.
                if (distZ > 0 && Mathf.Abs(distX) < attachRange)
                {
                    offset = Vector3.forward * platformWidth;
                }
                // 从模型后方靠近吸附.
                else if (distZ < 0 && Mathf.Abs(distX) < attachRange)
                {
                    offset = Vector3.back * platformWidth;
                }

                m_Transform.position = centerPos + offset;
            }
        }
    }

    protected override void OnTriggerStay(Collider other)
    {       
    }

    protected override void OnTriggerExit(Collider other)
    {
    }
}
