using UnityEngine;
using System.Collections;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class ClickSound : MonoBehaviour{
	private Button button { get { return GetComponent<Button> (); } }
	public AudioClip buttonClickClip = null;

	void Start(){
		button.onClick.AddListener (() => PlaySound ());
	}

	void PlaySound(){
		AudioManager.PlayButtonClickAudio(buttonClickClip);
	}
}