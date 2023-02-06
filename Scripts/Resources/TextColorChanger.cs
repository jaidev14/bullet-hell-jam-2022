using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextColorChanger : MonoBehaviour {
	public Color color1 = Color.white;
	public Color color2 = Color.white;
	public float duration = 1f;
	TMP_Text text = null;
	private Color startColor;

	public bool active = false;


	void Start() {
		text = GetComponent<TMP_Text>();
		startColor = text.color;
	}

	void Activate() {
		active = true;
	}

	void Disactivate() {
		active = false;
		text.color = startColor;
	}

	void LateUpdate()
	{
		if (!active) { return; }
		float t = Mathf.PingPong(Time.time, duration) / duration;
		text.color = Color.Lerp(color1, color2, t);
	}
}