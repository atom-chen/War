using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AI模块总管理器.
/// </summary>
public class AIManagers : MonoBehaviour
{
    private Transform m_Transform;
    private Transform[] aiPoints;                   // AI生成点父物体.

    void Start()
    {
        FindAndLoadInit();
        CreateAIManager();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        aiPoints = m_Transform.GetComponentsInChildren<Transform>();
    }

    /// <summary>
    /// 生成AI生成器.
    /// </summary>
    private void CreateAIManager()
    {
        for (int i = 1; i < aiPoints.Length; ++i)
        {
            if (i % 2 == 0)
            {
                aiPoints[i].gameObject.AddComponent<AIManager>().M_AIType = AIType.CANNIBAL;
            }
            else
            {
                aiPoints[i].gameObject.AddComponent<AIManager>().M_AIType = AIType.BOAR;
            }
        }
    }
}
