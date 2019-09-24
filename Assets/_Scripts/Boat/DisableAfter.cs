using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfter : MonoBehaviour {

    public float MaxSeconds;

    private float currectSeconds;
    private ParticleSystem particles;

    void Awake()
    {
        particles = GetComponent<ParticleSystem>();
    }

    public void Restart()
    {
        currectSeconds = 0;
    }

    void OnEnable()
    {
        currectSeconds = 0;
    }
    
    void Update()
    {
        currectSeconds += Time.deltaTime;
        if (currectSeconds >= MaxSeconds)
        {
            particles.Stop();
        }
    }
}
