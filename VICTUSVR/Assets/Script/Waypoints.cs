using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour{

    private TrackWaypoints trackWaypoints;
    private void OnTriggerEnter(Collider other){
        if(other.tag.Equals("Player")){
            trackWaypoints.PlayerThroughtCheckpoint(this);
        }
    }

    public void SetTrackWaypoints(TrackWaypoints trackWaypoints){
        this.trackWaypoints = trackWaypoints;
    }
}
