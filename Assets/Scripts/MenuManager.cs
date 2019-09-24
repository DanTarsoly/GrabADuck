using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
	public Transform MainMenu,PlayMenu;
	public Transform BoatTeam11,BoatTeam21,BoatTeam12,BoatTeam22;
	public GameObject volcano;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
//	public void PlayOptionsOpen(bool clicked)
//	{
//		if (clicked) {
//			PlayMenu.gameObject.SetActive (clicked);
//			MainMenu.gameObject.SetActive (false);
//		} else {
//			PlayMenu.gameObject.SetActive (clicked);
//			MainMenu.gameObject.SetActive (true);
//		}
//	}
//	public void Return(bool clicked)
//	{
//		if (clicked) {
//			MainMenu.gameObject.SetActive (clicked);
//			PlayMenu.gameObject.SetActive (false);
//		} else {
//			MainMenu.gameObject.SetActive (clicked);
//			PlayMenu.gameObject.SetActive (true);
//		}
//	}
//	public void TwovsTwo(bool clicked)
//	{
//		if (clicked) {
//			Start2v2 ();
//			PlayMenu.gameObject.SetActive (false);
//		}
//	}
//	public void OnevsOne(bool clicked)
//	{
//		if (clicked) {
//
//			volcano.GetComponent<VolcanoShootDucksScipt> ().spawnDucks=true;
//			Start1v1 ();
//			PlayMenu.gameObject.SetActive (false);
//		}
//	}
//	public void Start1v1()
//	{
//		BoatTeam11.GetComponent<BoatMovement> ().Enabled = true;
//		BoatTeam21.GetComponent<BoatMovement> ().Enabled = true;

		//enable controls for boats
//	}
//	public void Start2v2()
//	{
//		BoatTeam12.gameObject.SetActive (true);
//		BoatTeam22.gameObject.SetActive (true);
//		BoatTeam12.GetComponent<BoatMovement> ().Enabled = true;
//		BoatTeam22.GetComponent<BoatMovement> ().Enabled = true;
//		BoatTeam11.GetComponent<BoatMovement> ().Enabled = true;
//		BoatTeam21.GetComponent<BoatMovement> ().Enabled = true;
//		//instantiate  more boats
//	}
}
