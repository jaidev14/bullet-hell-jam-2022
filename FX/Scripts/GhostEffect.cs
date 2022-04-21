using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffect : MonoBehaviour {
    public GameObject ghostPrefab;
    public float ghostDelay = 0;
    public float ghostLifeTime;
    public bool active = false;
    private float currentGhostDelay;

    void Update()
    {
        if (active) {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            currentGhostDelay -= Time.deltaTime;
            if (currentGhostDelay <= 0) {
                GameObject currentGhost = Instantiate(ghostPrefab, transform.position, transform.rotation);
                currentGhost.transform.localScale = transform.localScale;
                // Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                // currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;
                currentGhostDelay = ghostDelay;
                Destroy(currentGhost, ghostLifeTime);
            }
        } else {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

        }
    }
}
