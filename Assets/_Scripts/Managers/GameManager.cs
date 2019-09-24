using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int NumRoundsToWin = 2;
    public float StartDelay = 4f;
    public float EndDelay = 3f;
    public Text MessageText;
    public Text TimeText;
    public Image Fading;
    public AudioClip StartClip;
    public AudioClip DrawClip;
    public AudioClip[] RoundClips;
    public TeamManager[] Teams;
    public PickupBehavior[] Pickups;
	public IslandAgent Island;
    public AudioSource MusicPlayer;
    public AudioSource Audio;

    private int RoundNumber;
    private WaitForSeconds StartWait;
    private WaitForSeconds EndWait;
    private WaitForSeconds WaitASecond;
    private TeamManager RoundWinner;
    private TeamManager GameWinner;
    private TimeManager TimeManager;
    private string TempText;
    private Color White;
    private bool IsOver;
    private bool Paused;

    void Awake()
    {
        StartWait = new WaitForSeconds(StartDelay);
        EndWait = new WaitForSeconds(EndDelay);
        WaitASecond = new WaitForSeconds(1f);
        TimeManager = GetComponent<TimeManager>();
        
        White = new Color(255, 255, 255, 0.25f);
    }

    void Start()
    {
        for (int i = 0; i < Teams.Length; i++)
        {
            Teams[i].TeamNumber = i + 1;
            Teams[i].Setup();
        }
        RoundNumber = 1;
        
        StartCoroutine(GameLoop());
        StartCoroutine(PauseCoroutine());
        StartCoroutine(ExitCoruotine());
    }


    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (GameWinner != null)
            SceneManager.LoadScene(0);
        else
            StartCoroutine(GameLoop());
    }

    private IEnumerator PauseCoroutine()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (Paused)
                    Continue();
                else
                    Pause();
            }
            yield return null;
        }
    }

    private IEnumerator ExitCoruotine()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Escape();
            }
            yield return null;
        }
    }

    private IEnumerator RoundStarting()
    {
        IsOver = false;

        foreach (var team in Teams)
            team.Reset();
        foreach (var pickup in Pickups)
            pickup.Respawn();
        TimeManager.ReSet();
        Island.ReSet();
        RespawnBoats();
        RespawnDucks();
        DisableMovements();
        DisableControls();

        MusicPlayer.volume = 0.1f;
        Audio.clip = RoundClips[RoundNumber - 1];
        Audio.Play();

        TimeText.gameObject.SetActive(false);
        foreach (var team in Teams)
            team.ScoreText.gameObject.SetActive(false);
        Fading.color = White;
        Fading.gameObject.SetActive(true);
        MessageText.text = "ROUND " + RoundNumber;

        yield return WaitASecond;

        Audio.clip = StartClip;
        Audio.Play();

        for (int i = 3; i > 0; i--)
        {
            MessageText.text = "" + i;
            yield return WaitASecond;
        }

        MessageText.text = "START!";
        yield return WaitASecond;
    }

    private IEnumerator RoundPlaying()
    {
        TimeManager.Continue();
        EnableMovements();
        EnableControls();

        MusicPlayer.volume = 0.3f;

        MessageText.text = "";
        TimeText.gameObject.SetActive(true);
        foreach (var team in Teams)
            team.ScoreText.gameObject.SetActive(true);
        Fading.gameObject.SetActive(false);
        while (true)
        {
            if (TimeManager.IsOver)
                break;

            foreach (var team in Teams)
            {
				if (team.Ducks.Count < 3)
					ShootDucks();
            }
            yield return null;
        }
    }

    private IEnumerator RoundEnding()
    {
        TimeManager.Stop();
        DisableMovements();

        RoundWinner = GetRoundWinner();
        if (RoundWinner != null)
        {
            ++RoundWinner.Wins;
            ++RoundNumber;
        }

        GameWinner = GetGameWinner();

        MusicPlayer.volume = 0.1f;
        Audio.clip = GetWinnerClip();
        Audio.Play();

        Fading.color = GetWinnerColor();
        Fading.gameObject.SetActive(true);

        string message = GetWinnerText();
        MessageText.text = message;

        foreach (var team in Teams)
        {
            foreach(var duck in team.Ducks)
            {
                if (duck != null)
                    Destroy(duck);
            }
        }
        yield return EndWait;
    }
    
    private TeamManager GetRoundWinner()
    { 
        if (Teams[0].Points > Teams[1].Points)
            return Teams[0];

        if (Teams[0].Points < Teams[1].Points)
            return Teams[1];

        return null;
    }

    private TeamManager GetGameWinner()
    {
        for (int i = 0; i < Teams.Length; i++)
        {
            if (Teams[i].Wins == NumRoundsToWin)
                return Teams[i];
        }
        return null;
    }
    
    private string GetWinnerText()
    {
        string message = "DRAW!";

        if (RoundWinner != null)
            message = RoundWinner.ColoredTeamText + " WINS!";
        
        if (GameWinner != null)
            message = GameWinner.ColoredTeamText + " WINS THE GAME!";

        return message;
    }

    private Color GetWinnerColor()
    {
        var color = White;

        if (GameWinner != null)
        {
            color = GameWinner.TeamColor;
            color.a = 0.2f;
        }
        else if (RoundWinner != null)
        {
            color = RoundWinner.TeamColor;
            color.a = 0.2f;
        }
        return color;
    }

    private AudioClip GetWinnerClip()
    {
        var clip = DrawClip;

        if (GameWinner != null)
        {
            clip = GameWinner.WinClip;
        }
        else if (RoundWinner != null)
        {
            clip = RoundWinner.WinClip;
        }
        return clip;
    }

    private void Continue()
    {
        Paused = false;
        MessageText.text = TempText;
        Fading.gameObject.SetActive(false);
        EnableControls();
        Time.timeScale = 1f;
    }

    private void Pause()
    {
        Paused = true;
        Fading.color = White;
        Fading.gameObject.SetActive(true);
        TempText = MessageText.text;
        MessageText.text = "PAUSED!";
        DisableControls();
        Time.timeScale = 0f;
    }

    private void Escape()
    {
        SceneManager.LoadScene("Menu");
    }
    
    private void EnableMovements()
    {
        for (int i = 0; i < Teams.Length; i++)
            Teams[i].EnableMovements();
    }


    private void DisableMovements()
    {
        for (int i = 0; i < Teams.Length; i++)
            Teams[i].DisableMovements();
    }

    private void EnableControls()
    {
        for (int i = 0; i < Teams.Length; i++)
            Teams[i].EnableControls();
    }

    private void DisableControls()
    {
        for (int i = 0; i < Teams.Length; i++)
            Teams[i].DisableControls();
    }

    private void RespawnBoats()
    {
        foreach (var team in Teams)
        {
            foreach (var boat in team.Boats)
            {
                boat.Respawn();
            }
        }
    }

    private void RespawnDucks()
    {
        foreach (var team in Teams)
        {
            foreach (var spawnPoint in team.DuckSpawnPoints)
            {
                var duck = Instantiate(team.DuckPrefab, spawnPoint.position, spawnPoint.rotation);
                duck.GetComponent<DuckManager>().Team = team;
                team.Ducks.Add(duck);
            }
        }
    }

    private void ShootDucks() 
	{
		foreach(var team in Teams)
        {
            for (int i = team.Ducks.Count; i < 4; i++)
            {
                var newDuck = Instantiate(team.DuckPrefab);
                newDuck.GetComponent<DuckManager>().Team = team;
                team.Ducks.Add(newDuck);
                newDuck.SetActive(false);
                Island.ShootDuck(newDuck);
            }
        }
	}
    

}
