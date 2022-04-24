using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunaStateAttackAction : StateMachineBehaviour
{
    MunaCharacterController _controller;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        _controller = animator.GetComponent<MunaCharacterController>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        _controller.FinishAttack();
    }
}
