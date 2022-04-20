using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject munaPrefab;
    [SerializeField] private float offSetRadius = 5f;
    [SerializeField] private float spawnRadius = 20f;
    [SerializeField] private float spawnRate = 5f;
    private float spawnTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTime < 0f) {
            Debug.Log("Instantiating");
            Vector3 randomPosition = Random.insideUnitCircle * spawnRadius;
            Instantiate (munaPrefab, (randomPosition - transform.position) * offSetRadius, Quaternion.identity);
            spawnTime = spawnRate;
        } else {
            spawnTime -= Time.deltaTime;
        }
    }
}
