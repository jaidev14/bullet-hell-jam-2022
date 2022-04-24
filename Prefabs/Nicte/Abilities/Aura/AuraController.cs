using System.Collections;
using System.Collections.Generic;
using TopDownCharacter2D.Controllers;
using TopDownCharacter2D.Attacks;
using UnityEngine;

public class AuraController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private TopDownCharacterController _controller;
    private bool _isSpecialAttack;

    private void Awake()
    {
        _controller = GetComponent<TopDownCharacterController>();
    }

    void Start()
    {
        _isSpecialAttack = false;
        _controller.OnAttackEvent.AddListener(Attack);
    }

    void Attack(AttackConfig attackConfig) {
        if (_isSpecialAttack) {
            SpecialAttack();
        } else {
            Activate();
        }
    }
    public void Activate()  {
        _animator.SetTrigger("Activate");
    }

    public void SpecialAttack()  {
        _animator.SetTrigger("SpecialAttack");
    }
}
