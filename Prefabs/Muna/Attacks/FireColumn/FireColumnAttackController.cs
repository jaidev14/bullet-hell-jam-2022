using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;
using UnityEngine;

public class FireColumnAttackController : MonoBehaviour
{
    private enum AttackType {
        HOMING,
        CROSS,
        STAR
    }

    public GameObject _fireColumnPrefab;
    public float _betweenAttacksDelay;
    public float _totalAttackTime;
    public Vector3 _spawnOffset;
    
    public int _maxAttacksToSpawn;
    private int _currentSpawnedAttacks;

    [FormerlySerializedAs("_SetTargetFromTag")]
    public bool _setTargetFromTag = true;
    // "Set a unique tag name of target at using SetTargetFromTag."
    [FormerlySerializedAs("_TargetTagName"), UbhConditionalHide("_setTargetFromTag")]
    public string _targetTagName = "Player";
    // "Transform of lock on target."
    // "It is not necessary if you want to specify target in tag."
    [FormerlySerializedAs("_TargetTransform")]
    public Transform _targetTransform;

    private float _nextAttackTime;
    public bool _isAttacking;
    private Vector3 currentPosition;

    // Update is called once per frame
    void Update()
    {
        if (_targetTransform == null && _setTargetFromTag)
        {
            _targetTransform = UbhUtil.GetTransformFromTagName(_targetTagName, false, false, transform);
        }
        
        if (_isAttacking) {
            if (_nextAttackTime <= 0) {
                _nextAttackTime = _betweenAttacksDelay;
                GameObject newAttack = Instantiate(_fireColumnPrefab, _targetTransform.position - _spawnOffset, Quaternion.identity, this.transform);
                _currentSpawnedAttacks++;
            } else {
                _nextAttackTime -= Time.deltaTime;
            }
        }
    }

    void StartAttack() {
        _currentSpawnedAttacks = 0;
        _nextAttackTime = _betweenAttacksDelay;
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine() {
        _isAttacking = true;
        yield return new WaitForSeconds(_totalAttackTime);
        _isAttacking = false;
    }
}
