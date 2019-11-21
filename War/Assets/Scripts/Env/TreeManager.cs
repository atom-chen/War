using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : EnvManagerBase
{
    // 树木预制体.
    private GameObject prefab_Broadleaf;
    private GameObject prefab_Conifer;
    private GameObject prefab_Palm;

    protected override void FindAndHideEnvPoints()
    {
        envPoints = m_Transform.Find("TreePoints").GetComponentsInChildren<Transform>();

        for (int i = 1; i < envPoints.Length; ++i)
        {
            envPoints[i].GetComponent<MeshRenderer>().enabled = false;
        }
    }

    protected override void LoadEnvModels()
    {
        prefab_Broadleaf = Resources.Load<GameObject>("Env/Trees/Broadleaf");
        prefab_Conifer = Resources.Load<GameObject>("Env/Trees/Conifer");
        prefab_Palm = Resources.Load<GameObject>("Env/Trees/Palm");
    }

    protected override void CreateEnvModels()
    {
        for (int i = 1; i < envPoints.Length; ++i)
        {
            GameObject go;

            // 增加随机效果.
            Quaternion randomRoation = Quaternion.Euler(new Vector3(0, Random.Range(0f, 90f), 0));

            if (i % 2 == 0)
            {
                go = GameObject.Instantiate<GameObject>(prefab_Broadleaf, envPoints[i].position,
                    randomRoation, m_Transform);
                go.name = "Broadleaf";

            }
            else if (i % 2 == 1)
            {
                go = GameObject.Instantiate<GameObject>(prefab_Conifer, envPoints[i].position,
                   randomRoation, m_Transform);
                go.name = "Conifer";
            }
            else
            {
                go = GameObject.Instantiate<GameObject>(prefab_Palm, envPoints[i].position,
                   randomRoation, m_Transform);
                go.name = "Palm";
            }
        }
    }
}
