using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum MunaState { START, IDLE, ATTACK, MOVE, DEATH }
public class MunaController : MonoBehaviour
{
    [SerializeField, FormerlySerializedAs("_GoShotCtrlList")]
    private GameObject[] m_goShotCtrlList = null;
    
    [SerializeField]
    private int _nowIndex = 0;
    private int _prevIndex = 0;

    [Header("AI Parameters")]
    [SerializeField]
    private Transform[] attackLocations = null;
    public Vector2 moveTimeRange = new Vector2(1f, 2f);  
    private int _nowMoveLocation = 0;
    public float _moveSpeed = 10f;

    public float _waitTime = 2f;

    public int _moveCounter = 0;
    public int _maxConsecutiveMoves = 4;
    private bool _isActing = true;
    public Transform _munaRenderer = null;
    public MunaState state = MunaState.START;
    public FireColumnAttackController _fireColumnAttackController;

    private void Start()
    {
        if (m_goShotCtrlList != null)
        {
            for (int i = 0; i < m_goShotCtrlList.Length; i++)
            {
                m_goShotCtrlList[i].SetActive(false);
            }
        }

        _nowIndex = 0;
        state = MunaState.IDLE;
        _isActing = false;
    }

    private void Update() {
        if (!_isActing) {
            _isActing = true;
            if (state == MunaState.IDLE) {
                StartCoroutine(DecideNextAction());
            } else if (state == MunaState.MOVE) {
                StartCoroutine(Move());
            } else if (state == MunaState.ATTACK) {
                StartCoroutine(Attack());
            }
        } else {
            if (state == MunaState.MOVE) {
                float step = _moveSpeed * Time.deltaTime;
                _munaRenderer.position = Vector3.MoveTowards(transform.position, attackLocations[_nowMoveLocation].position, step);
            }
        } 
    }

    public IEnumerator Attack() {
        Debug.Log("Attacking");
        int nextAttack = (int) Mathf.Floor(Random.Range(0, m_goShotCtrlList.Length + 1));
        if (nextAttack == m_goShotCtrlList.Length) {
            _fireColumnAttackController.StartAttack();
        } else {
            ChangeShot(true, nextAttack);
        }

        _waitTime = Random.Range(
            m_goShotCtrlList[_nowIndex].GetComponent<UbhShotCtrl>().controllerMaxShootingTimeRange.x,
            m_goShotCtrlList[_nowIndex].GetComponent<UbhShotCtrl>().controllerMaxShootingTimeRange.y
        );
        Debug.Log("Next attack:" + nextAttack);
        Debug.Log("Wait time:" + _waitTime);
        yield return new WaitForSeconds(_waitTime);
        state = MunaState.IDLE;
        _isActing = false;
    }

    public IEnumerator Move() {
        Debug.Log("Moving");
        _nowMoveLocation = (int) Mathf.Floor(Random.Range(0, attackLocations.Length));
        _waitTime = Random.Range(moveTimeRange.x, moveTimeRange.y);
        // float moveDirection = Random.insideUnitCircle.eulerAngles; // Upgrade to make intelligent nextplace choices
        yield return new WaitForSeconds(_waitTime);
        state = MunaState.IDLE;
        _isActing = false;
    }

    public IEnumerator DecideNextAction() {
        // Possible actions: Move / Attack
        Debug.Log("Deciding");
        float nextAction = Random.Range(0, 1);
        float attackChances = 0f; // (((1 + _moveCounter) / _maxConsecutiveMoves));

        _waitTime = Random.Range(
            m_goShotCtrlList[_nowIndex].GetComponent<UbhShotCtrl>().controllerMaxWaitAfterTimeRange.x,
            m_goShotCtrlList[_nowIndex].GetComponent<UbhShotCtrl>().controllerMaxWaitAfterTimeRange.y
        );
        yield return new WaitForSeconds(_waitTime);
        if (nextAction <= attackChances) {
            _moveCounter = 0;
            state = MunaState.ATTACK;
        } else {
            _moveCounter++;
            state = MunaState.MOVE;
        }
        _isActing = false;
    }

    public void ChangeShot(bool toNext, int nextIndex = -1)
    {
        if (m_goShotCtrlList == null)
        {
            return;
        }

        if (0 <= _nowIndex && _nowIndex < m_goShotCtrlList.Length)
        {
            _prevIndex = _nowIndex;
            m_goShotCtrlList[_nowIndex].SetActive(false);
        }

        if (0 <= nextIndex && nextIndex < m_goShotCtrlList.Length)
        {
            _nowIndex = nextIndex;
        } else {
            if (toNext)
            {
                _nowIndex = (int)Mathf.Repeat(_nowIndex + 1f, m_goShotCtrlList.Length);
            }
            else
            {
                _nowIndex--;
                if (_nowIndex < 0)
                {
                    _nowIndex = m_goShotCtrlList.Length - 1;
                }
            }
        }

        if (0 <= _nowIndex && _nowIndex < m_goShotCtrlList.Length)
        {
            m_goShotCtrlList[_nowIndex].SetActive(true);

            StartCoroutine(StartShot());
        }
    }

    private IEnumerator StartShot()
    {
        float cntTimer = 0f;
        while (cntTimer < 1f)
        {
            cntTimer += UbhTimer.instance.deltaTime;
            yield return null;
        }

        yield return null;

        UbhShotCtrl shotCtrl = m_goShotCtrlList[_nowIndex].GetComponent<UbhShotCtrl>();
        if (shotCtrl != null)
        {
            shotCtrl.StartShotRoutine();
        }
    }

    private void StopShot() {
        StopAllCoroutines();
    }
}