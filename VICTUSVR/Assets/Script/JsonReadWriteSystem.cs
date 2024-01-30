using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonReadWriteSystem : MonoBehaviour{

    private Controle controleScript;
    private GameObject controle;

    void Awake(){
        controle = GameObject.Find("GUIPartida");
    }



}