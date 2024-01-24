using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class barreiraCollision : MonoBehaviour{
    
    private GameObject Player;

    private void Awake(){
        Player = GameObject.Find("BMXBike");
        Debug.Log("Player: " + Player.name);
    }
    private void OnCollisionEnter(Collision collision){
        Debug.Log("Collision with: " + collision.gameObject.name);
        if(collision.gameObject == Player){
            Debug.Log("Collision with: " + collision.gameObject.name);
            Destroy(gameObject);
        }
    }
    // IEnumerator wait(float sec){
    //     yield return new WaitForSeconds(sec);
    //     SetCollidersActive(false);
    // }

}