using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 具体AI角色管理器.
/// </summary>
public class AIModel : MonoBehaviour
{
    private Transform m_Transform;

    void Start()
    {
        FindAndLoadInit();
    }

    void Update()
    {
        // 临时测试AI死亡逻辑.
        if (Input.GetKeyDown(KeyCode.Space))
            Death();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Transform = gameObject.GetComponent<Transform>();
    }

    /// <summary>
    /// AI死亡.
    /// </summary>
    private void Death()
    {
        SendMessageUpwards("AIDeath", gameObject);
    }
}
