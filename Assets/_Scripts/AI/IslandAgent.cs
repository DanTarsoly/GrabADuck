using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	[RequireComponent(typeof(FloatingBehavior))]
public class IslandAgent : MonoBehaviour {
    
	public Vector3 Torque;
	public float TurningTime = 4f;
	public float LaunchingTime = 0.5f;
    public float WaitingTime = 1f;
    public AudioSource Audio;
    public ParticleSystem Smoke;
    public enum State { TURNVULCANO, SHOOT, TURNISLAND, WAIT }
    private State _currentState;
    public State CurrentState
    {
        get { return _currentState; }
        set
        {
            ExitState(_currentState);
            _currentState = value;
            EnterState(_currentState);
        }
    }

    private List<GameObject> newDucks;
    private FloatingBehavior floating;
	private Rigidbody rigidBody;
	private WaitForSeconds waitForTuring;
	private WaitForSeconds waitForLaunching;
    private WaitForSeconds wait;
    private bool isVulcano;

    // Use this for initialization

    void Awake() {
        newDucks = new List<GameObject>();
		floating = GetComponent<FloatingBehavior> ();
		rigidBody = GetComponent<Rigidbody> ();
        waitForTuring = new WaitForSeconds(TurningTime);
        waitForLaunching = new WaitForSeconds(LaunchingTime);
        wait = new WaitForSeconds(WaitingTime);
    }

	void Start () {
	}

    public void ReSet()
    {
        newDucks.Clear();
        CurrentState = State.TURNISLAND;
    }

    public void ShootDuck(GameObject duck)
    {
        duck.transform.position = new Vector3(0, 1, 0);
        newDucks.Add(duck);
    }

    private void EnterState(State state)
    {
        switch (state)
        {
            case State.TURNVULCANO:
                StartCoroutine("TurnVulcano");
                break;
            case State.SHOOT:
                StartCoroutine("ShootDucks");
                break;
            case State.TURNISLAND:
                StartCoroutine("TurnIsland");
                break;
            case State.WAIT:
                StartCoroutine("Wait");
                break;
        }
    }

    private void ExitState(State state)
    {
        switch (state)
        {
            case State.TURNVULCANO:
                StopCoroutine("TurnVulcano");
                break;
            case State.SHOOT:
                StopCoroutine("ShootDucks");
                break;
            case State.TURNISLAND:
                StopCoroutine("TurnIsland");
                break;
            case State.WAIT:
                StopCoroutine("Wait");
                break;
        }
    }

    private IEnumerator TurnVulcano()
	{
        if (!isVulcano)
        {
            Vector3 tempVect = floating.MassPiont;
            floating.MassPiont = floating.BouyancyPoint;
            floating.BouyancyPoint = tempVect;

            rigidBody.centerOfMass = floating.MassPiont;
            rigidBody.AddTorque(Torque, ForceMode.Impulse);
            isVulcano = true;

            yield return waitForTuring;
        }
        CurrentState = State.SHOOT;
    }

    private IEnumerator ShootDucks()
    {
        foreach (var duck in newDucks)
        {
            if (duck == null)
                yield break;
            duck.SetActive(true);
            var rigidbody = duck.GetComponent<Rigidbody>();
            rigidbody.velocity = new Vector3(0, 30, 0);
            Audio.Play();
            Smoke.Play();
            yield return waitForLaunching;
        }
        newDucks.Clear();
        CurrentState = State.TURNISLAND;
    }

    private IEnumerator TurnIsland()
    {
        if (isVulcano)
        {
            Vector3 tempVect = floating.MassPiont;
            floating.MassPiont = floating.BouyancyPoint;
            floating.BouyancyPoint = tempVect;

            rigidBody.centerOfMass = floating.MassPiont;
            rigidBody.AddTorque(Torque, ForceMode.Impulse);
            isVulcano = false;

            yield return waitForTuring;

        }
        CurrentState = State.WAIT;
    }

    private IEnumerator Wait()
    {
        while (true)
        {
            if (newDucks.Count > 0)
            {
                CurrentState = State.TURNVULCANO;
            }
            yield return wait;
        }
    }
}
