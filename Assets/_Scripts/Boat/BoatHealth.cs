using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatHealth : MonoBehaviour {

    public float StartingHealth = 100f;
    public Slider Slider;
    public Image FillImage;
    public Color FullHealthColor = Color.green;
    public Color ZeroHealthColor = Color.red;
    public ParticleSystem Smoke;
    public ParticleSystem Sparks;
    public ParticleSystem Fire;
    public GameObject SinkObject;
    public AudioSource DeathAudio;
    public AudioClip CrashSound;
    public AudioClip[] RespawnSounds;

    //public GameObject ExplosionPrefab;

    //private AudioSource ExplosionAudio;          
    //private ParticleSystem ExplosionParticles;
    private Vector3 SpawnPosition;
    private Quaternion SpawnRotation;
    private float CurrentHealth;  
    private bool Dead;
    private System.Random Randomizer;

    private void Awake()
    {
        Randomizer = new System.Random(GetHashCode() + System.DateTime.Now.Millisecond);
        SpawnPosition = transform.position;
        SpawnRotation = transform.rotation;
        //ExplosionParticles = Instantiate(ExplosionPrefab).GetComponent<ParticleSystem>();
        //ExplosionAudio = ExplosionParticles.GetComponent<AudioSource>();

        //ExplosionParticles.gameObject.SetActive(false);
    }
    
    private void OnEnable()
    {
        CurrentHealth = StartingHealth;
        Smoke.Stop();
        Sparks.Stop();
        Fire.Stop();
        Dead = false;

        UpdateUI();
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        UpdateUI();

        if (CurrentHealth <= 0f && !Dead)
        {
            OnDeath();
        }
    }


    private void UpdateUI()
    {
        if (Slider != null)
            Slider.value = CurrentHealth;

        if (FillImage != null)
            FillImage.color = Color.Lerp(ZeroHealthColor, FullHealthColor, CurrentHealth / StartingHealth);

        if (CurrentHealth <= StartingHealth * 3 / 4 && Smoke.isStopped)
        {
            Smoke.Play();
        }
        if (CurrentHealth <= StartingHealth / 2 && Sparks.isStopped)
        {
            Sparks.Play();
        }
        if (CurrentHealth <= StartingHealth / 4 && Fire.isStopped)
        {
            Fire.Play();
        }
    }


    private void OnDeath()
    {

        var sinkIstance = Instantiate(SinkObject, transform.position, transform.rotation);
        Destroy(sinkIstance, 10);

        enabled = false;
        transform.position = SpawnPosition;
        transform.rotation = SpawnRotation;
        enabled = true;
        DeathAudio.clip = RespawnSounds[Randomizer.Next(0, RespawnSounds.Length)];
        DeathAudio.Play();
    }
}
