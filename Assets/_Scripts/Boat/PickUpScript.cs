using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class PickUpScript : MonoBehaviour {

    public float KeepDistance = 1f;
    public float LooseDistance = 2f;
    public float PullForce = 5f;
    public float PushForce = 2f;

    private Rigidbody Body;
    private Rigidbody Duck;
    private BoatManager BoatManager;

    void Awake()
    {
        Body = GetComponent<Rigidbody>();
        BoatManager = GetComponent<BoatManager>();
    }

    void Update()
    {
        if (Duck == null)
            return;

        if (Vector3.Distance(Body.position, Duck.transform.position) >= LooseDistance)
        {
            Duck = null;
            return;
        }

        if (Mathf.Abs(Vector3.Distance(Body.position, Duck.transform.position)) >= KeepDistance)
            Duck.AddForce(-((Duck.transform.position - Body.position) * PullForce));
        if (Mathf.Abs(Vector3.Distance(Body.position, Duck.transform.position)) < KeepDistance)
            Duck.AddForce(((Duck.transform.position - Body.position) * PushForce));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Duck == null && collision.collider.CompareTag("Duck"))
        {
            var duckManager = collision.collider.GetComponent<DuckManager>();
            var duckBody = collision.collider.GetComponent<Rigidbody>();

            if (duckManager == null && duckBody == null)
                return;

            if (duckManager.Team != BoatManager.Team)
            {
                Duck = duckBody;
                Duck.GetComponent<DuckBehavior>().PlayPickSound();
            }
        }
    }
}