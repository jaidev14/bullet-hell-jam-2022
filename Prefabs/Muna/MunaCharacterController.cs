using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunaCharacterController : MonoBehaviour
{
    public Transform _playerTarget;
    [SerializeField] private TrailRenderer _trailRenderer;
    public string _playerTag;
    public Transform[] _attackLocations;
    public int _nowIndex = 0;
    private int _prevIndex = 0;

    private bool _isFlipped;
    private bool _isMoving = false;
    private bool _isAttacking = false;
    public float actionDelay;
    private float nextActionTime;
    [SerializeField] private Animator _animator;
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    void Start() {
        if (_playerTarget == null) {
            _playerTarget = GameObject.FindWithTag(_playerTag).transform;
        }
        if (_trailRenderer == null) {
        _trailRenderer = GetComponent<TrailRenderer>();
        }
    }

    void Update() {
        if (_isMoving) {
            
            LookAtPosition();
            return;
        }
        if (_isAttacking) {
            LookAtPlayer();
            return;
        }
        if (nextActionTime <= 0f) {
            StartMoving();
        } else {
            LookAtPlayer();
            nextActionTime -= Time.fixedDeltaTime;
        }
    }
    void StartMoving() {
        Debug.Log("Start moving");
        if (_attackLocations != null)
        {
            _prevIndex = _nowIndex;
            _nowIndex = (int) Mathf.Floor(Random.Range(0, _attackLocations.Length));
            if (_prevIndex != _nowIndex) {
                nextActionTime = actionDelay;
                _isMoving = true;
                _trailRenderer.emitting = true;
            }
            _animator.SetBool(IsMoving, _isMoving);
        }  
    }

    public void FinishMove() {
        _isMoving = false;
        _isAttacking = true;
        _trailRenderer.emitting = false;
        _animator.SetBool(IsMoving, _isMoving);
    }

    public void FinishAttack() {
        _isAttacking = false;
    }

    public void LookAtPlayer() {
        Vector3 flipped = transform.localScale;
        flipped.x *= -1f;

        if (transform.position.x > _playerTarget.position.x && _isFlipped) {
            transform.localScale = flipped;
            _isFlipped = false;
        } else if (transform.position.x < _playerTarget.position.x && !_isFlipped) {
            transform.localScale = flipped;
            _isFlipped = true;
        }
    }

    public void LookAtPosition() {
        Vector3 flipped = transform.localScale;
        flipped.x *= -1f;

        if (transform.position.x > _attackLocations[_nowIndex].position.x && _isFlipped) {
            transform.localScale = flipped;
            _isFlipped = false;
        } else if (transform.position.x < _attackLocations[_nowIndex].position.x && !_isFlipped) {
            transform.localScale = flipped;
            _isFlipped = true;
        }
    }


}
