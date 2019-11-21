using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 环境物体管理器基类.
/// </summary>
public abstract class EnvManagerBase : MonoBehaviour
{
    protected Transform m_Transform;
    protected Transform[] envPoints;                    // 环境物体生成点.

    void Start()
    {
        InitBase();
        FindAndHideEnvPoints();
        LoadEnvModels();
        CreateEnvModels();
    }

    /// <summary>
    /// 基类初始化.
    /// </summary>
    private void InitBase()
    {
        m_Transform = gameObject.GetComponent<Transform>();
    }

    /// <summary>
    /// 查找生成点并隐藏.
    /// </summary>
    protected abstract void FindAndHideEnvPoints();
    
    /// <summary>
    /// 加载环境物体.
    /// </summary>
    protected abstract void LoadEnvModels();

    /// <summary>
    /// 生成环境物体.
    /// </summary>
    protected abstract void CreateEnvModels();
}
