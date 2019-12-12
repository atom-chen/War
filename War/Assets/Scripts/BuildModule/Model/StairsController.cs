using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 台阶模型建造类.
/// </summary>
public class StairsController : MaterialModelBase
{
    private const float stairOffset = 2.5f;

    private GameObject targetTrigger;                   // 触发目标.

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlatformToWall" && other.gameObject.name != "E")
        {
            canPut = true;
            isAttach = true;

            Vector3 offset = Vector3.zero;
            Quaternion rotation = Quaternion.identity;

            switch (other.gameObject.name[0].ToString())
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

            targetTrigger = other.gameObject;
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlatformToWall")
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
            targetTrigger.name = "E";
        }
    }
}
