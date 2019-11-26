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
    private Animator m_Animator;
    private NavMeshAgent m_NavMeshAgent;
    private AIType m_AIType;                        // 当前AI的类型.

    private Vector3 patrolTarget;                   // 巡逻目标点.
    private List<Vector3> patrolPointsList;         // 具体AI巡逻点集合.

    private AIState currentState;                   // 当前AI角色的动作状态.
    private Transform playerTransform;              // 玩家角色位置.

    private int life;                               // AI生命值.
    private int attack;                             // AI伤害值.

    private GameObject prefab_Effect;               // 血液飞溅特效.
    private Transform effectParent;                 // 特效父物体.

    private static ObjectPool pool;                 // 对象池管理特效资源.

    public AIType M_AIType { get => m_AIType; set => m_AIType = value; }
    public Vector3 PatrolTarget { get => patrolTarget; set => patrolTarget = value; }
    public List<Vector3> PatrolPointsList { set => patrolPointsList = value; }
    public AIState CurrentState { get => currentState; }
    public int Life 
    {
        get { return life; }
        set
        {
            life = value;

            if (life <= 0)
                ToggleState(AIState.DEATHSTATE);
        }
    }
    public int Attack { get => attack; set => attack = value; }

    void Awake()
    {
        FindAndLoadInit();
        ToggleState(AIState.IDLE);
    }

    void Update()
    {
        // 临时测试AI死亡逻辑.
        if (Input.GetKeyDown(KeyCode.Space))
            DeathState();

        DistanceToTarget();
        AIFollowPlayer();
        AIAttackPlayer();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Animator = gameObject.GetComponent<Animator>();
        m_NavMeshAgent = gameObject.GetComponent<NavMeshAgent>();

        playerTransform = GameObject.Find("FPSController").GetComponent<Transform>();

        prefab_Effect = Resources.Load<GameObject>("Effects/Gun/Bullet Impact FX_Flesh");
        effectParent = GameObject.Find("TempObject/Effect_Flesh_Parent").GetComponent<Transform>();

        pool = gameObject.GetComponent<ObjectPool>();
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
        // 判断与目标点的距离, 从而切换动作状态.
        if (currentState == AIState.IDLE || currentState == AIState.WALK)
        {
            if (Vector3.Distance(m_Transform.position, patrolTarget) <= 1)
            {
                patrolTarget = patrolPointsList[Random.Range(0, patrolPointsList.Count)];
                MoveToTarget();
                ToggleState(AIState.IDLE);
            }
            else
            {
                ToggleState(AIState.WALK);
            }
        }
    }

    /// <summary>
    /// AI跟随玩家角色.
    /// </summary>
    private void AIFollowPlayer()
    {
        // 根据距离判断跟随玩家角色.
        if (Vector3.Distance(m_Transform.position, playerTransform.position) <= 20)
        {
            ToggleState(AIState.ENTERRUN);
        }
        else
        {
            ToggleState(AIState.EXITRUN);
        }
    }

    /// <summary>
    /// AI攻击玩家角色.
    /// </summary>
    private void AIAttackPlayer()
    {
        if (currentState == AIState.ENTERRUN)
        {
            if (Vector3.Distance(m_Transform.position, playerTransform.position) <= 2)
            {
                ToggleState(AIState.ENTERATTACK);
            }
            else
            {
                ToggleState(AIState.EXITATTACK);
            }
        }
    }

    /// <summary>
    /// 角色状态切换.
    /// </summary>
    private void ToggleState(AIState state)
    {
        switch (state)
        {
            case AIState.IDLE:
                IdleState();
                break;

            case AIState.WALK:
                WalkState();
                break;

            case AIState.ENTERRUN:
                EnterRunState();
                break;

            case AIState.EXITRUN:
                ExitRunState();
                break;

            case AIState.ENTERATTACK:
                EnterAttackState();
                break;

            case AIState.EXITATTACK:
                ExitAttackState();
                break;

            case AIState.DEATHSTATE:
                DeathState();
                break;
        }
    }

    /// <summary>
    /// AI默认状态.
    /// </summary>
    private void IdleState()
    {
        m_Animator.SetBool("Walk", false);
        currentState = AIState.IDLE;
    }

    /// <summary>
    /// AI角色行走状态.
    /// </summary>
    private void WalkState()
    {
        m_Animator.SetBool("Walk", true);
        currentState = AIState.WALK;
    }

    /// <summary>
    /// AI进入奔跑状态.
    /// </summary>
    private void EnterRunState()
    {
        m_Animator.SetBool("Run", true);        

        if (m_NavMeshAgent.enabled == true)
        {
            m_NavMeshAgent.speed = 2.0f;
            m_NavMeshAgent.SetDestination(playerTransform.position);
        }

        currentState = AIState.ENTERRUN;
    }

    /// <summary>
    /// AI退出奔跑状态.
    /// </summary>
    private void ExitRunState()
    {
        m_Animator.SetBool("Run", false);

        if (m_NavMeshAgent.enabled == true)
        {
            m_NavMeshAgent.speed = 0.8f;
            m_NavMeshAgent.SetDestination(patrolTarget);
        }

        ToggleState(AIState.WALK);
    }

    /// <summary>
    /// AI进入攻击状态.
    /// </summary>
    private void EnterAttackState()
    {
        m_Animator.SetBool("Attack", true);

        if (m_NavMeshAgent.enabled == true)
            m_NavMeshAgent.enabled = false;

        currentState = AIState.ENTERATTACK;
    }

    /// <summary>
    /// AI退出攻击状态.
    /// </summary>
    private void ExitAttackState()
    {
        m_Animator.SetBool("Attack", false);

        if (m_NavMeshAgent.enabled == false)
            m_NavMeshAgent.enabled = true;

        ToggleState(AIState.ENTERRUN);
    }

    /// <summary>
    /// AI进入死亡状态.
    /// </summary>
    private void DeathState()
    {
        switch(m_AIType)
        {
            case AIType.CANNIBAL:
                m_Animator.enabled = false;
                gameObject.GetComponent<AIRagdoll>().StartRagdoll();
                break;

            case AIType.BOAR:
                m_Animator.SetTrigger("Death");
                break;
        }
        
        currentState = AIState.DEATHSTATE;

        if (m_NavMeshAgent != null)
            m_NavMeshAgent.isStopped = true;

        Invoke("Death", 2);
    }

    /// <summary>
    /// 其他部位受伤.
    /// </summary>
    public void HitNormalState()
    {
        m_Animator.SetTrigger("HitNormal");
    }

    /// <summary>
    /// 头部受伤.
    /// </summary>
    public void HitHeadState()
    {
        m_Animator.SetTrigger("HitHead");
    }

    /// <summary>
    /// AI死亡.
    /// </summary>
    private void Death()
    {
        SendMessageUpwards("AIDeath", this);
    }

    /// <summary>
    /// 血液飞溅特效播放.
    /// </summary>
    public void PlayEffect(RaycastHit hit)
    {
        if (currentState == AIState.DEATHSTATE)
            return;

        GameObject go;
        if (pool.IsEmpty())
        {
            go = GameObject.Instantiate<GameObject>(prefab_Effect, hit.point,
                Quaternion.LookRotation(hit.normal), effectParent);
        }
        else
        {
            go = pool.GetObject();
            Transform tempTransform = go.GetComponent<Transform>();

            tempTransform.position = hit.point;
            tempTransform.rotation = Quaternion.LookRotation(hit.normal);
        }

        StartCoroutine(DelayToPool(pool, go));
    }

    /// <summary>
    /// 延迟进入对象池.
    /// </summary>
    private IEnumerator DelayToPool(ObjectPool pool, GameObject go)
    {
        yield return new WaitForSeconds(0.5f);
        pool.AddObject(go);
    }
}
