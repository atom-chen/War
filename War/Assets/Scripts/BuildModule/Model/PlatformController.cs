using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地基建造控制器.
/// </summary>
public class PlatformController : MaterialModelBase
{
    private const float platformWidth = 3.3f;                   // 地基平台的宽度.

    private string indexName;                                   // 四个方向触发器的名称.
    private Transform targetPlatform;                           // 触发到的地基.

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
        if (other.gameObject.tag != "Terrain" && other.gameObject.tag != "PlatformToWall")
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

        if (other.gameObject.tag == "PlatformToWall")
        {
            canPut = true;
            isAttach = true;

            Vector3 offset = Vector3.zero;
            Vector3 centerPos = other.GetComponent<Transform>().parent.position;

            indexName = other.gameObject.name;
            targetPlatform = other.GetComponent<Transform>().parent.Find(indexName);

            switch (other.gameObject.name)
            {
                case "A":
                    offset = Vector3.forward * platformWidth;
                    break;

                case "B":
                    offset = Vector3.right * platformWidth;
                    break;

                case "C":
                    offset = Vector3.back * platformWidth;
                    break;

                case "D":
                    offset = Vector3.left * platformWidth;
                    break;
            }

            m_Transform.position = centerPos + offset;
        }
    }

    protected override void OnTriggerStay(Collider other)
    {       
    }

    protected override void OnTriggerExit(Collider other)
    {
    }

    public override void NormalModel()
    {
        base.NormalModel();

        ///
        /// 测试代码.
        ///

        // 移除交界处的触发器.
        if (targetPlatform != null)
        {
            GameObject.Destroy(targetPlatform.gameObject);
        }

        // 移除自身交界处的触发器.
        switch (indexName)
        {
            case "A":
                GameObject.Destroy(m_Transform.Find("C").gameObject);
                break;

            case "B":
                GameObject.Destroy(m_Transform.Find("D").gameObject);
                break;

            case "C":
                GameObject.Destroy(m_Transform.Find("A").gameObject);
                break;

            case "D":
                GameObject.Destroy(m_Transform.Find("B").gameObject);
                break;
        }
    }
}
