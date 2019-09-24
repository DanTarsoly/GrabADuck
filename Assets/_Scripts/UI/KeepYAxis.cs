using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepYAxis : MonoBehaviour {

    public Transform YLevel;
    public float Elevation;

    private void Start()
    {
    }

	void Update () {
        transform.position = new Vector3(transform.position.x, YLevel.position.y + Elevation, transform.position.z);
	}
}
