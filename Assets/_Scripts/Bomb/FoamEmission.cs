using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoamEmission : MonoBehaviour {

    public LayerMask ObjectMask;
    public GameObject FoamPrefab;
    public float MaxLifeTime = 5f;

    private AudioSource splashAudio;

    private void Start()
    {
        splashAudio = GetComponent<AudioSource>();
        Destroy(gameObject, MaxLifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var foamInstance = Instantiate(FoamPrefab, transform.position, Quaternion.Euler(-90,0,-90));
        splashAudio.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<Light>().enabled = false;
        Destroy(gameObject, 1f);
    }
}
