using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICannibal : AIBase
{
    protected override void DeathState()
    {
        m_Animator.enabled = false;
        gameObject.GetComponent<AIRagdoll>().StartRagdoll();

        CurrentState = AIState.DEATHSTATE;

        if (m_NavMeshAgent != null)
            m_NavMeshAgent.isStopped = true;

        Invoke("Death", 2);
    }
}
