using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour {

    public LayerMask ObjectMask;
    //public GameObject Explosion;
    //ParticleSystem ExplosionParticles;
    public BombType Type;
    public float MaxDamage = 50f;
    public float ExplosionForce = 30f;
    public float MaxLifeTime = 5f;
    public float ExplosionRadius = 3f;
    public GameObject ImpactPrefab;

    private AudioSource explosionAudio;

    private void Start()
    {
        explosionAudio = GetComponent<AudioSource>();
        Destroy(gameObject, MaxLifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (Type)
        {
            case BombType.BOMB:
                Explode();
                break;
            case BombType.SPIKEBOMB:
                if (other.CompareTag("Player"))
                    Explode();
                break;
            default:
                break;
        }
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius, ObjectMask);
        foreach (Collider collider in colliders)
        {
            Rigidbody targetRigidbody = collider.GetComponent<Rigidbody>();
            if (targetRigidbody != null)
            {
                targetRigidbody.AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius, 0.05f);
                BoatHealth targetHealth = targetRigidbody.GetComponent<BoatHealth>();
                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(CalculateDamage(targetRigidbody.position));
                }
            }
        }
        
        explosionAudio.Play();
        var impact = Instantiate(ImpactPrefab, transform.position, transform.rotation);
        Destroy(impact, 1);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 0.5f);
    }

    private float CalculateDamage(Vector3 position)
    {
        float distance = (transform.position - position).magnitude;
        float realtiveDistance = (1 - distance / ExplosionRadius);
        return Mathf.Max(0f, MaxDamage * realtiveDistance);
    }
}
