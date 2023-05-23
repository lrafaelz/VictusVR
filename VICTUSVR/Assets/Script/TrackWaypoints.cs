using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackWaypoints : MonoBehaviour{

    private List<Waypoints> waypointsList;
    private int nextWaypointIndex;

    private void Awake(){
        Transform waypointsTransform = transform.Find("Waypoints");

        waypointsList = new List<Waypoints>();
        foreach(Transform WaypointSingleTransform in waypointsTransform){
            Waypoints wayPoint =  WaypointSingleTransform.GetComponent<Waypoints>();
            wayPoint.SetTrackWaypoints(this);
            waypointsList.Add(wayPoint);
        }

        nextWaypointIndex = 0;
    }

    public void PlayerThroughtCheckpoint(Waypoints wayPoint){
        if(waypointsList.IndexOf(wayPoint) == nextWaypointIndex){
            // correct waypoint
            Debug.Log("Correct");
            nextWaypointIndex = (nextWaypointIndex + 1) % waypointsList.Count;
        } else{
            //wrong waypoint
            Debug.Log("Wrong");
        }
        // Debug.Log(wayPoint.transform.name);
    }
}
