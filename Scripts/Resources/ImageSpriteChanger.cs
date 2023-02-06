using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSpriteChanger : MonoBehaviour
{
	Image image = null;
    public Sprite startImage = null;
    public Sprite targetImage = null;
	public float duration = 1f;
	private float curDuration = 0f;
	public bool loop = true;
	private bool active = true;


	void Start() {
		image = GetComponent<Image>();
        image.sprite = startImage;
	}

	void LateUpdate()
	{
        if (!loop) { return; }
        if (curDuration < 0) {
            curDuration = duration;
            active = !active;
            ChangeSprite(active);
        } else {
            curDuration -= Time.deltaTime;
        }
	}

    public void ChangeSprite(bool start) {
        image.sprite = start ? startImage : targetImage;
    }
}
