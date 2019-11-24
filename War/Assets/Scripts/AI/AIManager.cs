using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 具体AI管理器.
/// </summary>
public class AIManager : MonoBehaviour
{
    private Transform m_Transform;

    private GameObject prefab_Cannibal;                     // 人形丧尸AI.
    private GameObject prefab_Boar;                         // 野猪AI.

    private AIType m_AIType = AIType.NULL;                  // AI角色类型.

    private const int aiNum = 6;                            // AI单次生成的总数.
    private List<GameObject> aiList;                        // AI集合.

    public AIType M_AIType { get => m_AIType; set => m_AIType = value; }

    void Start()
    {
        FindAndLoadInit();
        CreateAIByEnum();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Transform = gameObject.GetComponent<Transform>();

        prefab_Cannibal = Resources.Load<GameObject>("AI/Models/Cannibal");
        prefab_Boar = Resources.Load<GameObject>("AI/Models/Boar");

        aiList = new List<GameObject>(aiNum);
    }

    /// <summary>
    /// 通过AI类型生成AI.
    /// </summary>
    private void CreateAIByEnum()
    {
        switch(m_AIType)
        {
            case AIType.CANNIBAL:
                CreateAllAI(prefab_Cannibal);
                break;

            case AIType.BOAR:
                CreateAllAI(prefab_Boar);
                break;
        }
    }

    /// <summary>
    /// 生成全部AI.
    /// </summary>
    private void CreateAllAI(GameObject prefab_AI)
    {
        for (int i = 0; i < aiNum; ++i)
        {
            CreateOneAI(prefab_AI);
        }
    }

    /// <summary>
    /// 生成一个具体AI.
    /// </summary>
    private void CreateOneAI(GameObject prefab_AI)
    {
        GameObject go = GameObject.Instantiate<GameObject>(prefab_AI, m_Transform.position,
            Quaternion.identity, m_Transform);
        aiList.Add(go);
    }

    /// <summary>
    /// AI死亡, 并生成新的AI.
    /// </summary>
    private void AIDeath(GameObject deathAI)
    {
        aiList.Remove(deathAI);
        GameObject.Destroy(deathAI);

        StartCoroutine("CreateNewAI");
    }

    /// <summary>
    /// 生成新的AI.
    /// </summary>
    private IEnumerator CreateNewAI()
    {
        yield return new WaitForSeconds(3);

        switch (m_AIType)
        {
            case AIType.CANNIBAL:
                CreateOneAI(prefab_Cannibal);
                break;

            case AIType.BOAR:
                CreateOneAI(prefab_Boar);
                break;
        }
    }
}
