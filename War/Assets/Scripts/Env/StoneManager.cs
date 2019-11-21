using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 石头障碍物管理器.
/// </summary>
public class StoneManager : EnvManagerBase
{
    // 石头预制体.
    private GameObject prefab_StoneMetal;
    private GameObject prefab_StoneNormal;

    protected override void FindAndHideEnvPoints()
    {
        envPoints = m_Transform.Find("StonePoints").GetComponentsInChildren<Transform>();

        for (int i = 1; i < envPoints.Length; ++i)
        {
            envPoints[i].GetComponent<MeshRenderer>().enabled = false;
        }
    }

    protected override void LoadEnvModels()
    {
        prefab_StoneMetal = Resources.Load<GameObject>("Env/Stones/Rock_Metal");
        prefab_StoneNormal = Resources.Load<GameObject>("Env/Stones/Rock_Normal");
    }

    /// <summary>
    /// 实例化环境物体.
    /// </summary>
    protected override void CreateEnvModels()
    {
        for (int i = 1; i < envPoints.Length; ++i)
        {
            GameObject go;

            // 增加随机效果.
            Quaternion randomRoation = Quaternion.Euler(new Vector3(
                Random.Range(0f, 90f), Random.Range(0f, 90f), Random.Range(0f, 90f)));
            Vector3 randomScale = new Vector3(Random.Range(0.5f, 1.5f), 
                Random.Range(0.5f, 1.5f), Random.Range(0.5f, 1.5f));

            if (i % 2 == 0)
            {
                go = GameObject.Instantiate<GameObject>(prefab_StoneNormal, envPoints[i].position,
                    randomRoation, m_Transform);
            }
            else
            {
                go = GameObject.Instantiate<GameObject>(prefab_StoneMetal, envPoints[i].position,
                   randomRoation, m_Transform);
            }

            go.GetComponent<Transform>().localScale = randomScale;
        }
    }
}
