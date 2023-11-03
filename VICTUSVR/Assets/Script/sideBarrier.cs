using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sideBarrier : MonoBehaviour{

    public GameObject Player;
    public GameObject TeleportTo;
    public GameObject stopWall;

    private Collider[] wallColliders;
    private bool areCollidersActive = true;
    // public MotorcycleController motorcycleController;
 
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject == Player){
            // MotorcycleController bikeController = Player.GetComponent<MotorcycleController>();
            Player.transform.position = TeleportTo.transform.position;
            Player.transform.rotation = TeleportTo.transform.rotation;
            Physics.SyncTransforms();
            // Stop bike
            SetCollidersActive(true);
            // wait a second
            StartCoroutine(wait(1));
            
            
        }
    }

    IEnumerator wait(int sec){
        yield return new WaitForSeconds(sec);
        SetCollidersActive(false);
    }

    private void SetCollidersActive(bool active)
    {
        foreach (var collider in wallColliders)
        {
            collider.enabled = active;
        }
    }

    private MeshRenderer meshRenderer;
    TrackWaypoints waypoints;
    private void Awake(){
        meshRenderer = GetComponent<MeshRenderer>();
        wallColliders = stopWall.GetComponentsInChildren<Collider>();
        SetCollidersActive(false);
    }

    private void Start(){
        Debug.Log("wallColliders: " + wallColliders.Length);
        Hide();
    }

    public void Show(){
        meshRenderer.enabled = true;
    }

    public void Hide(){
        meshRenderer.enabled = false;
    }
}
 