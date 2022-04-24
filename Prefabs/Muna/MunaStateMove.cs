using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunaStateMove : StateMachineBehaviour
{
    public float _moveSpeed;
    public float _startAttackRange;
    private Transform _attackLocation;
    private Rigidbody _rb;
    private bool _isAttacking;
    MunaCharacterController _controller;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        _controller = animator.GetComponent<MunaCharacterController>();
        _rb = _controller.GetComponent<Rigidbody>();

        // Choose random location
        _isAttacking = false;
        _attackLocation = _controller._attackLocations[_controller._nowIndex];
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Vector3 newPos = Vector3.MoveTowards(_rb.position, _attackLocation.position, _moveSpeed * Time.fixedDeltaTime);
        _rb.MovePosition(newPos);

        if (Vector3.Distance(_attackLocation.position, _rb.position) <= _startAttackRange && !_isAttacking) {
            _isAttacking = true;
            animator.SetTrigger("Attack");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.ResetTrigger("Attack");
    }
}
