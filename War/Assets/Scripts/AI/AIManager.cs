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
    private List<Vector3> patrolPointsList;                 // 具体AI生成巡逻点.

    public AIType M_AIType { get => m_AIType; set => m_AIType = value; }

    void Start()
    {
        FindAndLoadInit();
        InitPatrolPoints();
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
        patrolPointsList = new List<Vector3>(aiNum);
    }

    /// <summary>
    /// 初始化巡逻点.
    /// </summary>
    private void InitPatrolPoints()
    {
        Transform[] patrolPoints = m_Transform.GetComponentsInChildren<Transform>(true);

        for (int i = 1; i < patrolPoints.Length; ++i)
            patrolPointsList.Add(patrolPoints[i].position);
    }

    /// <summary>
    /// 通过AI类型生成AI.
    /// </summary>
    private void CreateAIByEnum()
    {
        switch(m_AIType)
        {
            case AIType.CANNIBAL:
                CreateAllAI(prefab_Cannibal, AIType.CANNIBAL);
                break;

            case AIType.BOAR:
                CreateAllAI(prefab_Boar, AIType.BOAR);
                break;
        }
    }

    /// <summary>
    /// 生成全部AI.
    /// </summary>
    private void CreateAllAI(GameObject prefab_AI, AIType type)
    {
        for (int i = 0; i < aiNum; ++i)
        {
            CreateOneAI(prefab_AI, patrolPointsList[i], type);
        }
    }

    /// <summary>
    /// 生成一个具体AI.
    /// </summary>
    private void CreateOneAI(GameObject prefab_AI, Vector3 target, AIType type)
    {
        GameObject go = GameObject.Instantiate<GameObject>(prefab_AI, m_Transform.position,
            Quaternion.identity, m_Transform);

        AIModel ai = go.GetComponent<AIModel>();
        ai.PatrolTarget = target;
        ai.PatrolPointsList = patrolPointsList;
        ai.MoveToTarget();

        switch(type)
        {
            case AIType.BOAR:
                ai.Life = 300;
                ai.Attack = 100;
                break;

            case AIType.CANNIBAL:
                ai.Life = 400;
                ai.Attack = 150;
                break;
        }

        aiList.Add(go);
    }

    /// <summary>
    /// AI死亡, 并生成新的AI.
    /// </summary>
    private void AIDeath(AIModel deathAI)
    {
        Vector3 targetPos = deathAI.PatrolTarget;
        aiList.Remove(deathAI.gameObject);
        GameObject.Destroy(deathAI.gameObject);

        StartCoroutine("CreateNewAI", targetPos);
    }

    /// <summary>
    /// 生成新的AI.
    /// </summary>
    private IEnumerator CreateNewAI(Vector3 targetPos)
    {
        yield return new WaitForSeconds(3);

        switch (m_AIType)
        {
            case AIType.CANNIBAL:
                CreateOneAI(prefab_Cannibal, targetPos, AIType.CANNIBAL);
                break;

            case AIType.BOAR:
                CreateOneAI(prefab_Boar, targetPos, AIType.BOAR);
                break;
        }
    }
}
