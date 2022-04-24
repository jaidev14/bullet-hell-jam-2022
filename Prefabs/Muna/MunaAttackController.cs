using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunaAttackController : MonoBehaviour
{
    public GameObject[] attackList = null;
    public int _nowIndex = 0;
    private int _prevIndex = 0;
    private GameObject _curAttack = null;
    public int _curAttackAnimationIndex = 0;

    private void Start() {
        if (attackList != null)
        {
            for (int i = 0; i < attackList.Length; i++)
            {
                UbhShotCtrl shotCtrl = attackList[i].GetComponent<UbhShotCtrl>();
                if (shotCtrl != null)
                {
                    shotCtrl.StopShotRoutineAndPlayingShot();
                } else {
                    FireColumnAttackController fireColumnAttackController = _curAttack.GetComponent<FireColumnAttackController>();
                    fireColumnAttackController.FinishAttack();
                }
            }
        }

        _nowIndex = 0;
        _curAttack = attackList[_nowIndex];
    }

    public void Attack(int nextIndex = 0) {
        if (attackList != null && 0 <= nextIndex && nextIndex < attackList.Length)
        {
            Debug.Log("Attacking" + nextIndex);
            _prevIndex = _nowIndex;

            _nowIndex = nextIndex;
            _curAttack = attackList[_nowIndex];

            UbhShotCtrl shotCtrl = _curAttack.GetComponent<UbhShotCtrl>();
            if (shotCtrl != null)
            {
                Debug.Log("Shot");
                _curAttackAnimationIndex = shotCtrl.attackAnimationIndex;
                // StopAllCoroutines();
                shotCtrl.StartShotRoutine();
            } else {
                Debug.Log("Fire");
                FireColumnAttackController fireColumnAttackController = _curAttack.GetComponent<FireColumnAttackController>();
                _curAttackAnimationIndex = fireColumnAttackController.attackAnimationIndex;
                fireColumnAttackController.StartAttack();
            }
        }
    }
}
