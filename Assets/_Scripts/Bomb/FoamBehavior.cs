using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoamBehavior : MonoBehaviour {

    public float MaxSeconds = 10f;

    private float currentSeconds;
    private List<BoatMovement> slowedList;

	void Start () {
        slowedList = new List<BoatMovement>();
        currentSeconds = MaxSeconds;
    }
    void Update()
    {
        currentSeconds -= Time.deltaTime;

        if (currentSeconds <= 0)
        {
            foreach (var movement in slowedList)
                movement.Slowed = false;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
	{
        if (other.CompareTag("Player"))
        {
            var movement = other.GetComponent<BoatMovement>();
            if (movement == null)
                return;
            movement.Slowed = true;
			slowedList.Add (movement);
		}
	} 

	void OnTriggerExit(Collider other)
	{
        if (other.CompareTag("Player"))
        {
            var movement = other.GetComponent<BoatMovement>();
            if (movement == null)
                return;
            movement.Slowed = false;
            slowedList.Remove(movement);
        }
    }

	
}
