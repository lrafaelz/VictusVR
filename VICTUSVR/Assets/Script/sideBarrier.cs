using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sideBarrier : MonoBehaviour{

    [SerializeField] Transform transformTp;
    [SerializeField] GameObject player;
    private MeshRenderer meshRenderer;
    TrackWaypoints waypoints;
    private void Awake(){
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start(){
        Hide();
    }

    private void OnTriggerEnter(Collider other){
        if(other.tag.Equals("Player")){
            Debug.Log("Player pass through the side barrier");
            player.transform.position = new Vector3(transformTp.transform.position.x, transformTp.transform.position.y, transformTp.transform.position.z);
            Debug.Log("Player position: " + player.transform.position);
            // make player stop and set the rotation to waypoint rotation
            player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            player.transform.rotation = waypoints.waypointsList[waypoints.nextWaypointIndex].transform.rotation;
            // yields return new WaitForSeconds(2f);
            // trackWaypoints.PlayerThroughtCheckpoint(this);
        }
    }

    // public void SetTrackWaypoints(TrackWaypoints trackWaypoints){
    //     this.trackWaypoints = trackWaypoints;
    // }

    public void Show(){
        meshRenderer.enabled = true;
    }

    public void Hide(){
        meshRenderer.enabled = false;
    }
}
