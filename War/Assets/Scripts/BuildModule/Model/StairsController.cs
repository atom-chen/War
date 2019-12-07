using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 台阶模型建造类.
/// </summary>
public class StairsController : MaterialModelBase
{
    protected override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "PlatformToWall" && other.gameObject.tag != "Terrain")
        {
            canPut = false;
        }
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

            Vector3 offset = Vector3.zero;
            Quaternion rotation = Quaternion.identity;

            Vector3 centerPos = other.GetComponent<Transform>().position;

            float distX = m_Transform.position.x - centerPos.x;
            float distZ = m_Transform.position.z - centerPos.z;

            // 从模型右侧靠近吸附.
            if (distX > 0 && Mathf.Abs(distZ) < Mathf.Abs(distX)) 
            {
                offset = Vector3.right * 2.5f;
                rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }

            // 从模型左侧靠近吸附.
            else if (distX < 0 && Mathf.Abs(distZ) < Mathf.Abs(distX))
            {
                offset = Vector3.left * 2.5f;
                rotation = Quaternion.identity;
            }

            // 从模型前方靠近吸附.
            if (distZ > 0 && Mathf.Abs(distZ) > Mathf.Abs(distX))
            {
                offset = Vector3.forward * 2.5f;
                rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }

            // 从模型后方靠近吸附.
            else if (distZ < 0 && Mathf.Abs(distZ) > Mathf.Abs(distX))
            {
                offset = Vector3.back * 2.5f;
                rotation = Quaternion.Euler(new Vector3(0, -90, 0));
            }

            m_Transform.position = other.GetComponent<Transform>().parent.position + offset;
            m_Transform.rotation = rotation;
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlatformToWall")
        {
            canPut = true;
            isAttach = true;
        }
    }
}
