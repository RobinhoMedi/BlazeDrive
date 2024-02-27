using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class Explosiones : MonoBehaviour
{
    public float backwardForce = 10f;
    private bool isUserInputEnabled = true;
    private float userInputDisabledDuration = 0.1f;
    private bool isMovingForward;
    public ParticleSystem Explosion;
}
