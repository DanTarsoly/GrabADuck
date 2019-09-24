using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoatMovement : MonoBehaviour {

    public int PlayerNumber = 1;
    public float MovementSpeed = 500f;
    public float TurnSpeed = 200f;
    public float SlowRatio = 0.4f;
    
    private string MovementAxisName;     
    private string TurnAxisName;         
    private Rigidbody Rigidbody;         
    private float MovementInputValue;    
    private float TurnInputValue;
	public bool Slowed;


    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable ()
    {
        //Rigidbody.isKinematic = false;
        MovementInputValue = 0f;
        TurnInputValue = 0f;
        Slowed = false;
    }


    private void OnDisable ()
    {
        //Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        MovementAxisName = "Vertical" + PlayerNumber;
        TurnAxisName = "Horizontal" + PlayerNumber;
        
    }

    private void Update()
    {
        MovementInputValue = Input.GetAxis(MovementAxisName);
        TurnInputValue = Input.GetAxis(TurnAxisName);
        
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }


    private void Move()
    {
        float move = MovementInputValue * MovementSpeed * Time.deltaTime;
        if (Slowed)
            move *= SlowRatio;
        Vector3 movement = transform.forward * move;
        Rigidbody.AddForce(movement);
    }


    private void Turn()
    {
        float turn = TurnInputValue * TurnSpeed * Time.deltaTime;
        if (Slowed)
            turn *= SlowRatio;
        Rigidbody.AddTorque(new Vector3(0f, turn, 0f));
    }
}
