using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;

public class DatabaseManager : MonoBehaviour
{   
   FirebaseFirestore db;
   Controle controleScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void saveFirebaseData(string date, string pacientName, float distanceTravelled, int sessionTime, int score, int[] velocity, int[] BPMSensor, int[] EMGSensor){
        db = FirebaseFirestore.DefaultInstance;
        FirebaseData data = new FirebaseData{
            date = date,
            pacientName = pacientName, 
            distanceTravelled = distanceTravelled,
            sessionTime = sessionTime,
            score = score,
            velocity = velocity,
            BPMSensor = BPMSensor,
            EMGSensor = EMGSensor
        };
        DocumentReference docRef = db.Collection("Testes").Document("teste");
        docRef.SetAsync(data).ContinueWithOnMainThread(task => {
            Debug.Log("Data saved successfully");
        });
    }
}

