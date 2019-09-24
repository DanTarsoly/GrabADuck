using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehavior : MonoBehaviour {

    [HideInInspector]
    public TeamManager Team;

    void Start() { }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Duck"))
        {
            var duck = other.GetComponent<DuckManager>();

            if (duck == null)
                return;

            if (duck.Team != Team)
            {
                Team.AddPoints(duck.Value);
                duck.Team.Ducks.Remove(duck.gameObject);
                duck.GetComponent<DuckBehavior>().PlayScoreSound();
                duck.GetComponent<MeshRenderer>().enabled = false;
                duck.GetComponent<DuckBehavior>().enabled = false;
                duck.GetComponent<SphereCollider>().enabled = false;
                Destroy(duck.gameObject, 1f);
            }
        }
    }
}
