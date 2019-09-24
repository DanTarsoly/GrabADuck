using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatManager : MonoBehaviour
{
    public int PlayerNumber;
	//[HideInInspector]
	public TeamManager Team;

    private Rigidbody Rigidbody;
    private Vector3 SpawnPosition;
    private Quaternion SpawnRotation;
    private BoatMovement Movement;
    private BoatShooting Shooting;
    private GameObject CanvasGameObject;
    
    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Movement = GetComponent<BoatMovement>();
        Shooting = GetComponent<BoatShooting>();
        CanvasGameObject = GetComponentInChildren<Canvas>().gameObject;

        SpawnPosition = transform.position;
        SpawnRotation = transform.rotation;

        Movement.PlayerNumber = PlayerNumber;
        Shooting.PlayerNumber = PlayerNumber;
    }

    void Start() {}

    public void EnableMovement()
    {
        Rigidbody.isKinematic = false;
        if (CanvasGameObject != null)
            CanvasGameObject.SetActive(true);
    }

    public void DisableMovement()
    {
        Rigidbody.isKinematic = true;
        if (CanvasGameObject != null)
            CanvasGameObject.SetActive(false);
    }

    public void EnableControl()
    {
        Movement.enabled = true;
        Shooting.enabled = true;
    }

    public void DisableControl()
    {
        Movement.enabled = false;
        Shooting.enabled = false;
    }

    public void Respawn()
    {
        transform.position = SpawnPosition;
        transform.rotation = SpawnRotation;

        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}