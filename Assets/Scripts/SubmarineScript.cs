using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class SubmarineScript : MonoBehaviour
{
	Rigidbody body;
	public string state = "patrolling";
	public GameObject[] waypoints;
	public float speed;
	public float rotationSpeed;
	public float WpRange;
	public int CurrentWp = 0;
	public List<GameObject> targets = new List<GameObject> ();
	public GameObject CurrentTarget;
	public int targetcounter = 0;
	public int counter=0;
	public Rigidbody BulletPrefab;
	public float ShotForce=10;
	private int shotcounter;
	public int reloadtimeinSec;
	// Use this for initialization
	void Start ()
	{
		reloadtimeinSec = 1;
		shotcounter = 0;
		counter = 0;
		rotationSpeed = 3;
		speed = 1;
		WpRange = 1;
		body = GetComponent<Rigidbody> ();
	}
	// Update is called once per frame
	void Update ()
	{
		Debug.Log (state);	
		movement ();
		stateHolder ();
	}

	private void movement ()
	{
//		Vector3 dir;
//		if (waypoints.Length != 0) {
//			dir = waypoints [CurrentWp].transform.position - body.position;
//		}
//		if ( state == "attacking") {
//			Vector3 direction = targets [targetcounter].transform.position - body.transform.position;
//			if (direction.magnitude > 4) {
//				state = "patrolling";
//			}
//		}
		 if (state == "rotating"){
			//			Debug.Log ("state rotating");
			if (targets.Count != 0) {
				Vector3 direction = targets [targetcounter].transform.position - body.transform.position;
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), rotationSpeed * 2 * Time.deltaTime);
			} else {
				state = "patrolling";
			}
		}  else if (state == "attacking") {
			shotcounter++;
			if (shotcounter % (reloadtimeinSec * 60) == 0) {
				Shoot ();
		
				targets.Remove (targets [targetcounter]);
//			state = "patrolling";
//			if (counter % 50 == 0) {
//				targets.Remove (targets[targetcounter]);
//				//				targetcounter++;
//				counter = 0;
				state = "rotating";
			}
		}
//		}
		else if (state == "patrolling") {
            if(CurrentWp<waypoints.Length)
			if (Vector3.Magnitude (body.transform.position - waypoints [CurrentWp].transform.position) < WpRange) {
				wpHolder ();
			}
			movementTowardsWp ();
		}

	}

	private void wpHolder ()
	{
		CurrentWp++;
		if (CurrentWp >= waypoints.Length) {
			CurrentWp = 0;
		}
	}

	private void movementTowardsWp ()
	{
        if (CurrentWp < waypoints.Length)
        {
            Vector3 direction = waypoints[CurrentWp].transform.position - body.position;
            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            this.transform.Translate(0, 0, Time.deltaTime * speed);
        }
//		this.body.rotation.eulerAngles = direction.x / direction.z;

	}

//	float angle = 10;
//	if  ( Vector3.Angle(player.transform.forward, transform.position - player.transform.position) < angle) {
//	}
	private void stateHolder ()
	{
//		if (targets.Count != 0||targets!=null) {
//			Vector3 direction = targets [targetcounter].transform.position - body.transform.position;
//			if (direction.magnitude >= 3) {
//				state = "patrolling";
//			}
//		}
		if (state == "patrolling")
			return;
		if(Mathf.Abs((Vector3.Angle(-body.transform.forward,transform.position-targets[targetcounter].transform.position)))<15.0f)
			{
			state="attacking";
			Debug.Log (state);	
			}
//		Debug.Log (targets.Count);

	}
	private void Shoot()
	{
//		counter++;
//		if (shotcounter % (reloadtimeinSec * 60) == 0) {
			Rigidbody shellInstance = Instantiate (BulletPrefab, gameObject.transform.position + Vector3.Normalize (gameObject.transform.up) / 2, gameObject.GetComponent<Rigidbody> ().rotation) as Rigidbody;
			shellInstance.velocity = ShotForce * (Vector3.Normalize (gameObject.transform.forward + gameObject.transform.up / 5));

//		}
	}
//	void detectcollision()
//	{
//		
//	}
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			state = "rotating";
//			targets [targetcounter] = other.gameObject;
			targets.Add(other.gameObject);
			Debug.Log(targets.Count);
		}
			
//		}
	}

//	void onTrigggerStay (Collider other)
//	{
//		Debug.Log ("ontriggerstay");
//	}

//	void OnTriggerExit (Collider other)
//	{
//		if (other.gameObject.tag == "Boat") {
//			foreach (GameObject target in targets) {
//				if (target == other.gameObject) {
//					Debug.Log ("removing an object");
//					Debug.Log (targets.Count);
//					targets.Remove (target);
//				}
//			}
//		}
//	}
}
