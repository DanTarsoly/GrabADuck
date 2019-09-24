using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simplified floating effect
/// </summary>
public class FloatingBehavior : MonoBehaviour {

    public Transform Water;                                 // Transform of the water plane, where 'y' is the water level
    public float Volume = 1;                                // Volume of displace fluid (static for simplicity)
    public Vector3 MassPiont = new Vector3(0, -0.5f, 0);    // The center of mass
    public Vector3 BouyancyPoint = new Vector3(0, 0.5f, 0); // The point to apply bouyancy force (less dense part)
    public AudioClip SplashClip;

    private AudioSource AudioSource;
    private System.Random Randomizer;

    public bool InWater { get; set; }                       // Whether is in the water (controlled by WaterBehavior)

    private float BouyancyModifier;                         // For tuning the force
    private Rigidbody Rigidbody;

    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        AudioSource = gameObject.AddComponent<AudioSource>();
        Randomizer = new System.Random(GetHashCode() + System.DateTime.Now.Millisecond);
    }

    void Start()
    {
        BouyancyModifier = 2000f;
        Rigidbody.centerOfMass = MassPiont;
        AudioSource.clip = SplashClip;
        AudioSource.playOnAwake = false;
    }

    void Update()
    {
		Rigidbody.centerOfMass = MassPiont;
        float depth = Water.transform.position.y - MassPiont.y - transform.position.y;
        if (depth > 0)
        {
            float bouyancyMagnitude = Volume * BouyancyModifier * depth * Time.deltaTime;
            Vector3 bouyancy = bouyancyMagnitude * Vector3.up;
            Rigidbody.AddForceAtPosition(bouyancy, transform.TransformPoint(BouyancyPoint));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            InWater = true;
            AudioSource.pitch = 1 + (float)Randomizer.NextDouble() * 0.8f - 0.4f;
            AudioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            InWater = false;
        }
    }
}
