using System.Collections;
using System.Collections.Generic;
using TopDownCharacter2D;
using TopDownCharacter2D.Health;
using TopDownCharacter2D.Attacks.Range;
using UnityEngine;

public class FireColumnController : MonoBehaviour
{
    [SerializeField] private GameObject _fireParticles;

    void Start() {
        _fireParticles.SetActive(false);
    }

    public void StartFire() {
        _fireParticles.SetActive(true);
    }

    public void StopFire() {
        ParticleSystem.EmissionModule em = _fireParticles.GetComponent<ParticleSystem>().emission;
        em.rateOverTime = 0;
        _fireParticles.SetActive(false);
        Destroy(_fireParticles, 1f);
    }
}
