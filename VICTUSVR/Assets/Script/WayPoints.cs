using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour{

    private void OnCollisionEnter(Collider collision)
    {
        if (collision.tag.Equals("Player"))
        {
            Debug.Log("Player detected by waypoint" + gameObject.name);
            Destroy(gameObject);
        }
    }
}
