using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoShootDucksScipt : MonoBehaviour
{
	public Rigidbody DuckBlue, DuckYellow;
	public bool spawnedEverything;
	int duckcounter=0;
	public int blueducknum;
	public int yellowducknum;
	public bool spawnDucks;
	// Use this for initialization
	void Start ()
	{
		spawnDucks = false;
		spawnedEverything = false;
		blueducknum = 3;
		yellowducknum = 3;
	}
	
	// Update is called once per frame
	void Update ()
	{
//		if (blueducknum == 0 && yellowducknum == 0) {
//			spawnedEverything = true;
//		}
		if (spawnDucks) {
			ShootDucks ();
//			blueducknum = 3;
//			yellowducknum = 3;
//			spawnDucks = false;
		}
//		ShootDucks ();
//		if (!spawnedEverything) {
////			counter++;
//
////			if (counter % 60 == 0) {
////				ShootYellowDucks (3);
////				counter = 0;
//				spawnedEverything = true;
////			}
//		}

	}

	public void ShootDucks ()
	{	
		duckcounter++;
		if (blueducknum > 0) {
			if (duckcounter % 10 == 0) {
				Rigidbody BLueDuck = Instantiate (DuckBlue, gameObject.transform.position, gameObject.transform.rotation) as Rigidbody;
				BLueDuck.velocity = (new Vector3 (-5, 30, 0));
				blueducknum = blueducknum - 1;
				Debug.Log (blueducknum);
				duckcounter = 0;
			}
		} 
		else 
		{
			if (yellowducknum > 0) {
				if (duckcounter % 10 == 0) {
					Rigidbody yellow = Instantiate (DuckYellow, gameObject.transform.position, gameObject.transform.rotation) as Rigidbody;
					yellow.velocity = (new Vector3 (5, 30, 0));
					yellowducknum = yellowducknum - 1;
					Debug.Log (yellowducknum);
					duckcounter = 0;
				}
			}
			spawnedEverything = true;
		}

//		spawnDucks = false;
	}

//	public void ShootBlueDucks ()
//	{	
//		duckcounter++;
//		if(yellowducknum>0)
//		{
//			if (duckcounter % 60 == 0) {
//				Rigidbody yellow = Instantiate (DuckBlue, gameObject.transform.position, gameObject.transform.rotation) as Rigidbody;
//				yellow.velocity = (new Vector3 (5, 30, 0));
//				yellowducknum = yellowducknum - 1;
//				Debug.Log (yellowducknum);
//				duckcounter = 0;
//			}
//		}
//	}
}
