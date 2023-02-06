using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FireColumnController : MonoBehaviour
{
    private enum AttackType {
        HOMING,
        CROSS,
        STAR
    }
    [SerializeField] private ParticleSystem _fireParticles;
    [SerializeField] private AttackType _attackType;
    private Animator _animator;
    [SerializeField] private bool _isAttacking;
    
    [FormerlySerializedAs("_SetTargetFromTag")]
    public bool _setTargetFromTag = true;
    // "Set a unique tag name of target at using SetTargetFromTag."
    [FormerlySerializedAs("_TargetTagName"), UbhConditionalHide("_setTargetFromTag")]
    public string _targetTagName = "Player";
    // "Transform of lock on target."
    // "It is not necessary if you want to specify target in tag."
    [FormerlySerializedAs("_TargetTransform")]
    public Transform _targetTransform;

    void Start() {
        _animator = GetComponent<Animator>();
        StartAttack();
    }

    void Update() {
        if (_isAttacking) {
            if (_targetTransform == null && _setTargetFromTag)
            {
                _targetTransform = UbhUtil.GetTransformFromTagName(_targetTagName, false, false, transform);
            }

            if (_attackType == AttackType.HOMING) {
                transform.position = new Vector3(_targetTransform.position.x, transform.position.y, _targetTransform.position.z);
            }
        }
    }

    public void StartAttack() {
        if (!_isAttacking) {
            _isAttacking = true;
            var emission = _fireParticles.emission;
            emission.enabled = true;
        }
    }

    public void StopFire() {
        _isAttacking = false;
        var emission = _fireParticles.emission;
        emission.enabled = false;
        Destroy(this.gameObject, 2f);
        this.gameObject.SetActive(false);
    }
}
