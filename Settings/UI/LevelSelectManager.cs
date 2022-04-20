using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    public GameObject[] worlds = null;
    public int index = 0;
    public GameObject buttonLeft = null;
    public GameObject buttonRight = null;
    void Start()
    {
        index = 0;
        buttonLeft.SetActive(false);
        SetupLevelButtons();
    }

    public void NextWorld() {
        if (index == worlds.Length - 1) return;
        worlds[index].SetActive(false);
        index++;
        worlds[index].SetActive(true);

        buttonLeft.SetActive(true);
        if (index == worlds.Length - 1) {
            buttonRight.SetActive(false);
        }
        SetupLevelButtons();
    }

    public void PrevWorld() {
        if (index == 0) return;
        worlds[index].SetActive(false);
        index--;
        worlds[index].SetActive(true);

        buttonRight.SetActive(true);
        if (index == 0) {
            buttonLeft.SetActive(false);
        }
        SetupLevelButtons();
    }

    void SetupLevelButtons() {
        int children = worlds[index].transform.childCount;
        for (int i = 0; i < children; ++i) {
            worlds[index].transform.GetChild(i).gameObject.GetComponent<ButtonManager>().SetIndex(
                (index * children) + (i));
        }
    }
}
