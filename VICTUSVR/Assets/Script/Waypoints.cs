using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour{

    private TrackWaypoints trackWaypoints;
    private MeshRenderer meshRenderer;

    private void Awake(){
        meshRenderer = GetComponent<MeshRenderer>();

    }

    private void Start(){
        Hide();
    }

    private void OnTriggerEnter(Collider other){
        if(other.tag.Equals("Player")){
            trackWaypoints.PlayerThroughtCheckpoint(this);
        }
    }

    public void SetTrackWaypoints(TrackWaypoints trackWaypoints){
        this.trackWaypoints = trackWaypoints;
    }

    public void Show(){
        meshRenderer.enabled = true;
    }

    public void Hide(){
        meshRenderer.enabled = false;
    }
}
