using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunaStateAttack : StateMachineBehaviour
{
    MunaCharacterController _controller;
    MunaAttackController _attackController;
    private int _nextAttack = 0;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        _controller = animator.GetComponent<MunaCharacterController>();
        // Get attack controller of the location
        Transform attackLocation = _controller._attackLocations[_controller._nowIndex];
        _attackController = attackLocation.GetComponent<MunaAttackController>();

        // Choose an attack from the controller (should be done from controller?)
        _nextAttack = (int) Mathf.Floor(Random.Range(0, _attackController.attackList.Length));
        _controller.LookAtPlayer();
        _attackController.Attack(_nextAttack);
        animator.SetInteger("NextAttack", _attackController._curAttackAnimationIndex);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        _controller.FinishMove();
        
    }
}
