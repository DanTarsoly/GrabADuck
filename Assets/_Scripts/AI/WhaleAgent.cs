using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleAgent : MonoBehaviour {
    
    public float MovementSpeed = 500f;
    public float TurnSpeed = 200f;
    public float ThinkingTime = 1f;

    private Rigidbody Rigidbody;
    private FloatingBehavior FloatingBehavior;
    private System.Random Randomizer;
    private float MovementValue;
    private float TurnValue;
    private WaitForSeconds WaitForThinking;
    private WaitForFixedUpdate WaitForFixedUpdate;
    private Vector3 ContactNormal;

    public enum State { WANDER, TURN }
    private State _currentState;
    public State CurrentState {
        get { return _currentState; }
        set {
            ExitState(_currentState);
            _currentState = value;
            EnterState(_currentState);
        }
    }

    void Awake()
    {
        Randomizer = new System.Random(GetHashCode() + DateTime.Now.Millisecond);
        Rigidbody = GetComponent<Rigidbody>();
        FloatingBehavior = GetComponent<FloatingBehavior>();
        WaitForThinking = new WaitForSeconds(ThinkingTime);
        WaitForFixedUpdate = new WaitForFixedUpdate();
    }

    void Start()
    {
        CurrentState = State.WANDER;
        StartCoroutine("Wander");
        StartCoroutine("ChangeDirection");
    }

    private void EnterState(State state)
    {
        switch (state)
        {
            case State.WANDER:
                Rigidbody.AddForce(transform.forward * 10, ForceMode.Impulse);
                MovementValue = 1;
                StartCoroutine("Wander");
                StartCoroutine("ChangeDirection");
                break;
            case State.TURN:
                MovementValue = 0;
                TurnValue = 150;
                var crossProduct = Vector3.Cross(transform.forward, ContactNormal);
                bool left = crossProduct.y >= 0;
                TurnValue = left ? 1 : -1;
                StartCoroutine("TurnAround", left);
                break;
        }
    }

    private void ExitState(State state)
    {
        switch (state)
        {
            case State.WANDER:
                StopCoroutine("Wander");
                StopCoroutine("ChangeDirection");
                break;
            case State.TURN:
                StopCoroutine("TurnAround");
                MovementValue = 10;
                Move();
                break;
        }
    }

    private IEnumerator ChangeDirection()
    {
        while (true)
        {
            float random = (float)Randomizer.NextDouble();
            TurnValue = random * 2f - 1f;
            yield return WaitForThinking;
        }
    }

    private IEnumerator Wander()
    {
        while (true)
        {
            Move();
            Turn();
            yield return WaitForFixedUpdate;
        }
    }

    private IEnumerator TurnAround(bool left)
    {
        while (true)
        {
            Turn();
            var crossProduct = Vector3.Cross(transform.forward, ContactNormal);
            var dotProduct = Vector3.Dot(transform.forward, ContactNormal);
            if (dotProduct >= 0 && ((left && crossProduct.y < 0 ) || (!left && crossProduct.y >= 0)))
            {
                CurrentState = State.WANDER;
            }
            yield return WaitForFixedUpdate;
        }
    }

    private void Move()
    {
        if (FloatingBehavior.InWater)
        {
            Vector3 movement = transform.forward * MovementValue * MovementSpeed * Time.deltaTime;
            Rigidbody.AddForce(movement);
        }
    }


    private void Turn()
    {
        float turn = TurnValue * TurnSpeed * Time.deltaTime;
        Rigidbody.AddTorque(new Vector3(0f, turn, 0f));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Obstacle"))
        {
            var contact = collision.contacts[0];
            ContactNormal = contact.normal;
            CurrentState = State.TURN;
        }
    }
}
