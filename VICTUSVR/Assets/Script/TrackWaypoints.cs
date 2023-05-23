using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackWaypoints : MonoBehaviour{
    private void Awake(){
        Transform waypointsTransform = transform.Find("Waypoints");

        foreach(Transform WaypointSingleTransform in waypointsTransform){
            Debug.Log(WaypointSingleTransform);
        }
    }
}
