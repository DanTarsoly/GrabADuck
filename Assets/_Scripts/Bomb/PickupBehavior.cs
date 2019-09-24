using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehavior : MonoBehaviour {

	public enum PickupType
    {
        SPIKEBOMB,
        SOAPBOMB
    }

    public PickupType Type;
    public int RespawnTime;

    private AudioSource Audio;
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;
    private WaitForSeconds wait;
    private bool active;

    void Awake()
    {
        Audio = GetComponent<AudioSource>();
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;
        wait = new WaitForSeconds(RespawnTime);
    }

    public void Respawn()
    {
        GetComponent<SphereCollider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
        //GetComponent<Light>().enabled = true;
        active = true;
    }

    private IEnumerator RespawnRoutine()
    {
        yield return wait;
        if (!active)
        {
            Respawn();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var shooting = other.GetComponent<BoatShooting>();
            if (shooting == null)
                return;
            switch (Type)
            {
                case PickupType.SPIKEBOMB:
                    shooting.BombType = BombType.SPIKEBOMB;
                    shooting.SpecialCount = 2;
                    break;
                case PickupType.SOAPBOMB:
                    shooting.BombType = BombType.SOAPBOMB;
                    shooting.SpecialCount = 2;
                    break;
            }
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            //GetComponent<Light>().enabled = false;
            active = false;
            Audio.Play();
            StartCoroutine("RespawnRoutine");
        }
    }
}
