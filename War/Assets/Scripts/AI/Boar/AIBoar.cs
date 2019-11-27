using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBoar : AIBase
{
    public override void HitNormalState()
    {
        base.HitNormalState();
        AudioManager.Instance.PlayAudioClipByName(ClipName.BoarInjured, m_Transform.position);
    }

    public override void HitHeadState()
    {
        base.HitHeadState();
        AudioManager.Instance.PlayAudioClipByName(ClipName.BoarInjured, m_Transform.position);
    }

    protected override void DeathState()
    {
        m_Animator.SetTrigger("Death");

        CurrentState = AIState.DEATHSTATE;

        if (m_NavMeshAgent.enabled == true)
            m_NavMeshAgent.isStopped = true;

        AudioManager.Instance.PlayAudioClipByName(ClipName.BoarDeath, m_Transform.position);

        Invoke("Death", 2);
    }

    protected override void AttackPlayer()
    {
        base.AttackPlayer();

        AudioManager.Instance.PlayAudioClipByName(ClipName.BoarAttack, m_Transform.position);
    }
}
