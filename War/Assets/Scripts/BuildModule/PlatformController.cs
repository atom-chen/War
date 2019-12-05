using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地基建造控制器.
/// </summary>
public class PlatformController : MonoBehaviour
{
    private Transform m_Transform;

    private Material m_Material;                                // 默认材质球.
    private Material m_PreviewMaterial;                         // 建造完成之前透明材质球.

    private bool canPut = true;                                 // 是否可以摆放地基模型.
    private bool isAttach = false;                              // 两个模型是否吸附.

    private const float platformWidth = 3.3f;                   // 地基平台的宽度.
    private const float attachRange = 0.4f;                     // 地基相互吸引的的范围.

    public bool CanPut { get => canPut; }
    public bool IsAttach { get => isAttach; set => isAttach = value; }

    void Start()
    {
        FindAndLoadInit();
    }

    void Update()
    {
        if (canPut)
        {
            m_PreviewMaterial.color = new Color32(0, 255, 0, 80);
        }
        else
        {
            m_PreviewMaterial.color = new Color32(255, 0, 0, 80);
        }
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Transform = gameObject.GetComponent<Transform>();

        m_Material = gameObject.GetComponent<MeshRenderer>().material;
        m_PreviewMaterial = Resources.Load<Material>("BuildModule/Materials/Building Preview");

        gameObject.GetComponent<MeshRenderer>().material = m_PreviewMaterial;
    }

    void OnCollisionEnter(Collision other)
    {
        // 与环境物体交互.
        if (other.gameObject.tag != "Terrain")
        {
            canPut = false;
        }
    }

    void OnCollisionStay(Collision other)
    {
        // 与环境物体交互.
        if (other.gameObject.tag != "Terrain")
        {
            canPut = false;
        }
    }

    void OnCollisionExit(Collision other)
    {
        // 离开环境, 最大可能就是地面.
        if (other.gameObject.tag != "Terrain")
        {
            canPut = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Platform")
        {
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

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Platform")
        {
            isAttach = false;
        }
    }

    /// <summary>
    /// 将模型恢复默认颜色.
    /// </summary>
    public void NormalModel()
    {
        gameObject.GetComponent<MeshRenderer>().material = m_Material;
    }
}
