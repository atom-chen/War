using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 屋顶模型建造控制.
/// </summary>
public class RoofController : MaterialModelBase
{
    private const float roofSize = 3.3f;                // 屋顶模型的大小.

    private string indexName;                           // 四个方向触发器的名称.
    private GameObject targetTrigger;                   // 触发目标.

    protected override void OnTriggerEnter(Collider other)
    {
        // 墙壁上吸附.
        if (other.gameObject.tag == "WallToRoof")
        {
            canPut = true;
            isAttach = true;

            m_Transform.position = other.GetComponent<Transform>().position;
            targetTrigger = other.gameObject;
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
                    indexName = "C";
                    break;

                case "B":
                    offset = Vector3.forward * roofSize;
                    indexName = "D";
                    break;

                case "C":
                    offset = Vector3.right * roofSize;
                    indexName = "A";
                    break;

                case "D":
                    offset = Vector3.back * roofSize;
                    indexName = "B";
                    break;
            }

            m_Transform.position = centerPos + offset;
            targetTrigger = other.gameObject;
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "RoofToRoof" || other.gameObject.tag == "WallToRoof")
        {
            canPut = false;
            isAttach = false;
        }
    }

    public override void NormalModel()
    {
        base.NormalModel();

        // 销毁触发的触发器和自身的触发器.
        if (targetTrigger != null)
        {
            GameObject.Destroy(targetTrigger);

            if (string.IsNullOrEmpty(indexName) == false)
                GameObject.Destroy(m_Transform.Find(indexName).gameObject);
        }
    }
}
