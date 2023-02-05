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
        _nowIndex = 0;
        _curAttack = attackList[_nowIndex];
    }

    void Update() {
        if (!LevelManager.Instance.levelActive) {
            StopAllAttacks();
            return;
        }
    }

    void StopAllAttacks() {
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
    }

    public void Attack(int nextIndex = 0) {
        if (attackList != null && 0 <= nextIndex && nextIndex < attackList.Length)
        {
            _prevIndex = _nowIndex;
            _nowIndex = nextIndex;
            _curAttack = attackList[_nowIndex];

            UbhShotCtrl shotCtrl = _curAttack.GetComponent<UbhShotCtrl>();
            if (shotCtrl != null)
            {
                if (shotCtrl.m_shooting) {
                    shotCtrl.StopShotRoutine();
                    return;
                }
                _curAttackAnimationIndex = shotCtrl.attackAnimationIndex;
                // StopAllCoroutines();
                shotCtrl.StartShotRoutine();
            } else {
                FireColumnAttackController fireColumnAttackController = _curAttack.GetComponent<FireColumnAttackController>();
                _curAttackAnimationIndex = fireColumnAttackController.attackAnimationIndex;
                fireColumnAttackController.StartAttack();
            }
        }
    }
}
