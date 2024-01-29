using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonReadWriteSystem : MonoBehaviour{

    Controle controleScript;

    public void SaveToJson(){
        SessionData data = new SessionData();
        data.date = DateTime.Now.ToString();
        Debug.Log("timestamp: " + data.date);
        data.pacientName = controleScript.pacientName;
        data.distanceTravelled = controleScript.distanceTravelled; // Adicionar
        data.sessionTime = controleScript.fimDaPartida; // Adicionar
        data.score = controleScript.score;
        data.velocity = controleScript.velArray; // Adicionar
        data.BPMSensor = controleScript.BPMArray; // Adicionar
        data.EMGSensor = controleScript.EMGArray; // Adicionar

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/sessionData.json", json); 
    }

    public void LoadFromJson(){
        string json = File.ReadAllText(Application.dataPath + "/sessionData.json");
        SessionData data = JsonUtility.FromJson<SessionData>(json);
    }

}