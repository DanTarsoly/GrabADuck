using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckManager : MonoBehaviour {
    
    public int Value = 1;

    //[HideInInspector]
    public TeamManager Team;

    private Rigidbody Rigidbody;
    private Vector3 SpawnPosition;
    private Quaternion SpawnRotation;

    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        SpawnPosition = transform.position;
        SpawnRotation = transform.rotation;
    }

    void Start() { }

    public void EnableMovement()
    {
        Rigidbody.isKinematic = false;
    }

    public void DisableMovement()
    {
        Rigidbody.isKinematic = true;
    }

    public void Respawn()
    {
        transform.position = SpawnPosition;
        transform.rotation = SpawnRotation;

        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}
