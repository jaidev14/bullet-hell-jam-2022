using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class ButtonManager : MonoBehaviour
{
    public int index = 0;
    private TMP_Text displayText = null;
    private Button button { get { return GetComponent<Button> (); } }

    void Awake() {
        button.onClick.AddListener (() => ClickButton ());
        displayText = transform.GetChild(0).GetComponent<TMP_Text>();
        displayText.text = "";
    }

    public void SetIndex(int newIndex) {
        index = newIndex;
        displayText.text = "layer " + (index + 1).ToString ("D2");
    }

    void ClickButton() {
        if (index == 0) {
            GameManager.Instance.StartGame();
        }
        GameManager.Instance.SelectLevel(index);
    }
}
