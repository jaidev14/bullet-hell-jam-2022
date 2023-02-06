using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageColorChange : MonoBehaviour {
	public Color color1 = Color.white;
	public Color color2 = Color.white;
	public float duration = 1f;
	Image image = null;
	private Color startColor;

	public bool active = false;


	void Start() {
		image = GetComponent<Image>();
		startColor = image.color;
	}

	void Activate() {
		active = true;
	}

	void Disactivate() {
		active = false;
		image.color = startColor;
	}

	void LateUpdate()
	{
		if (!active) { return; }
		float t = Mathf.PingPong(Time.time, duration) / duration;
		image.color = Color.Lerp(color1, color2, t);
	}
}