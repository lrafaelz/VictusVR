using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackWaypoints : MonoBehaviour{

    public event EventHandler OnPlayerCorrectWaypoint;
    public event EventHandler OnPlayerWrongWaypoint;
    public List<Waypoints> waypointsList;
    public int nextWaypointIndex;

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
            Waypoints correctWaypoint = waypointsList[nextWaypointIndex];
            correctWaypoint.Hide();

            nextWaypointIndex = (nextWaypointIndex + 1) % waypointsList.Count;
            OnPlayerCorrectWaypoint?.Invoke(this, EventArgs.Empty);


        } else{
            //wrong waypoint
            Debug.Log("Wrong");
            OnPlayerWrongWaypoint?.Invoke(this, EventArgs.Empty);

            Waypoints correctWaypoint = waypointsList[nextWaypointIndex];
            correctWaypoint.Show();
        }
        // Debug.Log(wayPoint.transform.name);
    }
}
