using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepRotation : MonoBehaviour {

    private Quaternion OriginalRotation;

    private void Start()
    {
        OriginalRotation = transform.rotation;
    }

	void Update () {
        transform.rotation = OriginalRotation;
	}
}
