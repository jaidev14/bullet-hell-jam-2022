using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    [Header ("Oscillating Variables")]
    public Vector2 speedRange;
    public Vector2 radiusRange;
    private float speed = 1;
    private float radius = 1;
    public bool random = true;
    private float timeCounter = 0;

    void Start () {
        speed = Random.Range(speedRange.x, speedRange.y);
        radius = Random.Range(radiusRange.x, radiusRange.y);
    }

    void Update() {
        timeCounter += Time.deltaTime * speed;

        float x = transform.parent.transform.position.x + Mathf.Cos(timeCounter) * radius;
        float y = transform.parent.transform.position.y + Mathf.Sin(timeCounter) * radius;

        transform.position = new Vector3(x, y, transform.position.z);
    }
}
