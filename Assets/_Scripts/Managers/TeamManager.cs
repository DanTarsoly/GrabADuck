using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TeamManager {

    public string TeamName;
    public Color TeamColor;
    public AudioClip WinClip;
    public Text ScoreText;
    public BaseBehavior Base;
    public BoatManager[] Boats;
    public Transform[] DuckSpawnPoints;
    public List<GameObject> Ducks;
    public Rigidbody[] Buoys;
    public GameObject DuckPrefab;

    [HideInInspector]
    public int TeamNumber;
    [HideInInspector]
    public string ColoredTeamText;
    [HideInInspector]
    public int Points;
    [HideInInspector]
    public int Wins;

    public void Setup()
    {
        Ducks = new List<GameObject>();

        Base.Team = this;

		foreach (var boat in Boats)
			boat.Team = this;

        ScoreText.color = TeamColor;

        ColoredTeamText = "<color=#" + ColorUtility.ToHtmlStringRGB(TeamColor) + "> " + TeamName + "</color>";

        UpdateUI();
    }

    public void AddPoints(int amount)
    {
        Points += amount;
        UpdateUI();
    }

    public void EnableMovements()
    {
        foreach (var boat in Boats)
            boat.EnableMovement();
        foreach (var duck in Ducks)
            duck.GetComponent<DuckManager>().EnableMovement();
        foreach (var buoy in Buoys)
            buoy.isKinematic = false;
    }


    public void DisableMovements()
    {
        foreach (var boat in Boats)
            boat.DisableMovement();
        foreach (var duck in Ducks)
            duck.GetComponent<DuckManager>().DisableMovement();
        foreach (var buoy in Buoys)
            buoy.isKinematic = true;
    }

    public void EnableControls()
    {
        foreach (var boat in Boats)
            boat.EnableControl();
    }


    public void DisableControls()
    {
        foreach (var boat in Boats)
            boat.DisableControl();
    }

    public void Reset()
    {
        Ducks = new List<GameObject>();
        Points = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        ScoreText.text = "" + Points;
    }
}
