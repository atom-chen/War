using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 具体AI角色管理器.
/// </summary>
public class AIModel : MonoBehaviour
{
    private Transform m_Transform;
    private NavMeshAgent m_NavMeshAgent;

    private Vector3 patrolTarget;                   // 巡逻目标点.
    private List<Vector3> patrolPointsList;         // 具体AI巡逻点集合.

    public Vector3 PatrolTarget { get => patrolTarget; set => patrolTarget = value; }
    public List<Vector3> PatrolPointsList { set => patrolPointsList = value; }

    void Awake()
    {
        FindAndLoadInit();
    }

    void Update()
    {
        // 临时测试AI死亡逻辑.
        if (Input.GetKeyDown(KeyCode.Space))
            Death();

        DistanceToTarget();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_NavMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// 移动到目标点.
    /// </summary>
    public void MoveToTarget()
    {
        m_NavMeshAgent.SetDestination(patrolTarget);
    }

    /// <summary>
    /// 计算与目标点的距离.
    /// </summary>
    private void DistanceToTarget()
    {
        if (Vector3.Distance(m_Transform.position, patrolTarget) <= 1)
        {
            patrolTarget = patrolPointsList[Random.Range(0, patrolPointsList.Count)];
            MoveToTarget();
        }
    }

    /// <summary>
    /// AI死亡.
    /// </summary>
    private void Death()
    {
        SendMessageUpwards("AIDeath", this);
    }
}
