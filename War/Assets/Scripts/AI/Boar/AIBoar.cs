using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBoar : AIBase
{
    protected override void DeathState()
    {
        m_Animator.SetTrigger("Death");

        CurrentState = AIState.DEATHSTATE;

        if (m_NavMeshAgent != null)
            m_NavMeshAgent.isStopped = true;

        Invoke("Death", 2);
    }
}
