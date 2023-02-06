using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPortraitHandler : MonoBehaviour
{
    public GameObject[] _characterPortraits;
    private int _curIndex = 0;

    void Start() {
        foreach (GameObject charPortrait in _characterPortraits)
        {
            charPortrait.SetActive(false);
        }
    }

    public void ChangePortrait(int index) {
        _characterPortraits[_curIndex].SetActive(false);
        _characterPortraits[index].SetActive(true);
        _curIndex = index;
    }

}
