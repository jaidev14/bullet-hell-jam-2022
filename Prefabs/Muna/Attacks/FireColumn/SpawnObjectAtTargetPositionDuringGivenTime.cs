using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectAtTargetPositionDuringGivenTime : MonoBehaviour
{
    public GameObject theObject;
    public Transform theTarget;
    public bool theGiven;
    public float theTime;
    private Vector3 currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
