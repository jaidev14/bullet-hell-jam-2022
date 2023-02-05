using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;
using UnityEngine;
using TopDownCharacter2D;
using TopDownCharacter2D.Health;
using TopDownCharacter2D.Attacks.Range;
public class FireColumnAttackController : MonoBehaviour
{
    public int attackAnimationIndex = 0;
    private enum AttackType {
        HOMING,
        CROSS,
        STAR
    }

    public GameObject _fireColumnPrefab;
    private List<GameObject> _fireColumnPool = null;
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

    void Start() {
        _fireColumnPool = new List<GameObject>();
    }

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
                Vector3 _spawnOffsetPosition = new Vector3(Random.Range(-_spawnOffset.x, _spawnOffset.x), _spawnOffset.y, Random.Range(-_spawnOffset.z, _spawnOffset.z));
                GameObject newAttack = Instantiate(_fireColumnPrefab, _targetTransform.position + _spawnOffsetPosition, Quaternion.identity, this.transform);
                _fireColumnPool.Add(newAttack);
                _currentSpawnedAttacks++;
            } else {
                _nextAttackTime -= Time.deltaTime;
            }
        }
    }

    public void StartAttack() {
        _currentSpawnedAttacks = 0;
        _nextAttackTime = _betweenAttacksDelay;
        StartCoroutine(AttackCoroutine());
    }

    public void FinishAttack() {
        _currentSpawnedAttacks = 0;
        _isAttacking = false;
        if (_fireColumnPool != null && _fireColumnPool.Count >= 1) {
            foreach(GameObject fireColumn in _fireColumnPool) {
                Destroy(fireColumn);
            }
        }
        _fireColumnPool = new List<GameObject>();
    }

    IEnumerator AttackCoroutine() {
        _isAttacking = true;
        yield return new WaitForSeconds(_totalAttackTime);
        FinishAttack();
    }
}
