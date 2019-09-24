using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBehavior : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        var floatingObject = collision.collider.GetComponent<FloatingBehavior>();
        if (floatingObject != null)
            floatingObject.InWater = true;
    }

    void OnCollisionExit(Collision collision)
    {
        var floatingObject = collision.collider.GetComponent<FloatingBehavior>();
        if (floatingObject != null)
            floatingObject.InWater = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var floatingObject = other.GetComponent<FloatingBehavior>();
        if (floatingObject != null)
            floatingObject.InWater = true;
    }

    private void OnTriggerExit(Collider other)
    {
        var floatingObject = other.GetComponent<FloatingBehavior>();
        if (floatingObject != null)
            floatingObject.InWater = false;
    }
}
