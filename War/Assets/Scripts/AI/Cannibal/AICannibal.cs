using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICannibal : AIBase
{
    public override void HitNormalState()
    {
        base.HitNormalState();
        AudioManager.Instance.PlayAudioClipByName(ClipName.ZombieInjured, m_Transform.position);
    }

    public override void HitHeadState()
    {
        base.HitHeadState();
        AudioManager.Instance.PlayAudioClipByName(ClipName.ZombieInjured, m_Transform.position);
    }

    protected override void DeathState()
    {
        m_Animator.enabled = false;
        gameObject.GetComponent<AIRagdoll>().StartRagdoll();

        CurrentState = AIState.DEATHSTATE;

        if (m_NavMeshAgent.enabled == true)
            m_NavMeshAgent.isStopped = true;

        AudioManager.Instance.PlayAudioClipByName(ClipName.ZombieDeath, m_Transform.position);

        Invoke("Death", 2);
    }

    protected override void AttackPlayer()
    {
        base.AttackPlayer();

        AudioManager.Instance.PlayAudioClipByName(ClipName.ZombieAttack, m_Transform.position);
    }
}
