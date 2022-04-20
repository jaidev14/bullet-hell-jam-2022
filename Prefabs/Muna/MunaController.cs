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
    public MunaState state = MunaState.START;
    public Vector2 moveTimeRange = new Vector2(1f, 2f);  

    public float startTime = 2f; 
    public float timeBetweenAttacks = 2f;

    public int _moveCounter = 0;
    public int _maxConsecutiveMoves = 4;

    private void Start()
    {
        if (m_goShotCtrlList != null)
        {
            for (int i = 0; i < m_goShotCtrlList.Length; i++)
            {
                m_goShotCtrlList[i].SetActive(false);
            }
        }

        _nowIndex = -1;
        StartCoroutine(StartBattle());
        ChangeShot(true);
    }

    public IEnumerator StartBattle() {
        state = MunaState.IDLE;
        Debug.Log("Starting battle");
        Debug.Log("Starting time " + startTime);

        yield return new WaitForSeconds(startTime);
        StartCoroutine(Attack());
    }

    public IEnumerator Attack() {
        state = MunaState.ATTACK;
        Debug.Log("Attacking");
        int nextAttack = (int) Mathf.Floor(Random.Range(0, m_goShotCtrlList.Length));
        ChangeShot(true, nextAttack);

        float waitTime = Random.Range(
            m_goShotCtrlList[_nowIndex].GetComponent<UbhShotCtrl>().controllerMaxShootingTimeRange.x,
            m_goShotCtrlList[_nowIndex].GetComponent<UbhShotCtrl>().controllerMaxShootingTimeRange.y
        );
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(DecideNextAction());
    }

    public IEnumerator Move() {
        state = MunaState.MOVE;
        Debug.Log("Moving");

        float moveTime = Random.Range(moveTimeRange.x, moveTimeRange.y);
        // float moveDirection = Random.insideUnitCircle.eulerAngles; // Upgrade to make intelligent nextplace choices
        yield return new WaitForSeconds(moveTime);
        
        StartCoroutine(DecideNextAction());
    }

    public IEnumerator DecideNextAction() {
        state = MunaState.IDLE;
        Debug.Log("Deciding next action");

        // Possible actions: Move / Attack 
        float nextAction = Random.Range(0, 1);
        float attackChances = (((1 + _moveCounter) / _maxConsecutiveMoves));

        float waitTime = Random.Range(
            m_goShotCtrlList[_nowIndex].GetComponent<UbhShotCtrl>().controllerMaxWaitAfterTimeRange.x,
            m_goShotCtrlList[_nowIndex].GetComponent<UbhShotCtrl>().controllerMaxWaitAfterTimeRange.y
        );
        yield return new WaitForSeconds(waitTime);

        if (nextAction <= attackChances) {
            _moveCounter = 0;
            StartCoroutine(Attack());
        } else {
            _moveCounter++;
            StartCoroutine(Move());
        }
    }

    public void ChangeShot(bool toNext, int nextIndex = -1)
    {
        if (m_goShotCtrlList == null)
        {
            return;
        }

        StopAllCoroutines();

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