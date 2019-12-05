using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 建造材料模型抽象父类.
/// </summary>
public abstract class MaterialModelBase : MonoBehaviour
{
    protected Transform m_Transform;

    private Material m_Material;                            // 默认材质球.
    private Material m_PreviewMaterial;                     // 建造完成之前透明材质球.

    protected bool canPut = true;                           // 是否可以摆放地基模型.
    protected bool isAttach = false;                        // 两个模型是否吸附.

    public bool CanPut { get => canPut; }
    public bool IsAttach { get => isAttach; set => isAttach = value; }

    void Start()
    {
        InitBase();
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
    private void InitBase()
    {
        m_Transform = gameObject.GetComponent<Transform>();

        m_Material = gameObject.GetComponent<MeshRenderer>().material;
        m_PreviewMaterial = Resources.Load<Material>("BuildModule/Materials/Building Preview");

        gameObject.GetComponent<MeshRenderer>().material = m_PreviewMaterial;
    }

    /// <summary>
    /// 将模型恢复默认颜色.
    /// </summary>
    public void NormalModel()
    {
        gameObject.GetComponent<MeshRenderer>().material = m_Material;
    }

    protected abstract void OnCollisionEnter(Collision other);
    protected abstract void OnCollisionStay(Collision other);
    protected abstract void OnCollisionExit(Collision other);
    protected abstract void OnTriggerEnter(Collider other);
    protected abstract void OnTriggerExit(Collider other);
}
