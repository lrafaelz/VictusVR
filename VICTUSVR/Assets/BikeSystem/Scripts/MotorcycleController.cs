// //********************** © KD-Studios 2023 ***************************\\
// //************* Desenvolvido Por: Atílio De Jesus *********************\\
// //******************* Bike System versão 0.1v **************************\\

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace BikeSystem.controller
{
  public class MotorcycleController : MonoBehaviour
  {
    public type TypeOfBike;
    public Transform SteerTransform;
    public WheelCollider frontWheel;
    public WheelCollider rearWheel;
    public Transform t_wheelR;
    public Transform t_wheelF;
    public float movementForce = 1000;
    public float steeringForce = 20;
    public float maxInclinationSet = 0.5f;
    public float ActualVelocity;
    public Controle serial = new Controle();



    [HideInInspector] public Rigidbody rigidbody;

    float maxInclination;
    float limitBike = 50;
    bool useLimit;
    public bool haveADriver;
    [HideInInspector] public AudioSource audio;
    public enum type
    {
      Motorcycle,
      Bike
    }

    private void Awake()
    {
      audio = GetComponent<AudioSource>();
      rigidbody = GetComponent<Rigidbody>();
      rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;
    }

    private void Start()
    {
      if (TypeOfBike == type.Bike)
      {
        useLimit = true;
      }
      else
      {
        useLimit = false;
      }
      StartCoroutine("SetGame");
    }

    IEnumerator SetGame()
    {
      yield return new WaitForSeconds(0.5f);
    }

    void Update()
    {
      UpdatePos(frontWheel, t_wheelF);
      UpdatePos(rearWheel, t_wheelR);
      Tensor();
    }
    private void FixedUpdate()
    {
      SteerBase();
      AccelBase();
      LerpSteerDecrementalL();
      LerpSteerDecrementalR();
      LerpSteerIncrementalL();
      LerpSteerIncrementalR();
    }

    void UpdatePos(WheelCollider wheel, Transform wheelTrans)
    {
      Vector3 pos;
      Quaternion quaternion;
      wheel.GetWorldPose(out pos, out quaternion);
      wheelTrans.position = pos;
      wheelTrans.rotation = quaternion;
    }

    //Função que controla o movimento da moto/bike
    private void AccelBase()
    {
      float InputAccel = Input.GetAxis("Vertical");
      if (InputAccel <= -0.2f)
      {
        rearWheel.motorTorque = InputAccel * (movementForce - (movementForce / 4));
      }
      else
      {
        if (useLimit)
        {
          if (ActualVelocity < limitBike)
          {
            rearWheel.motorTorque = InputAccel * movementForce;
          }
          else
          {
            rearWheel.motorTorque = 0;
          }
        }
        else
        {
          rearWheel.motorTorque = InputAccel * movementForce;

        }
      }
      ActualVelocity = (rigidbody.velocity.magnitude / 5) * 15;

      if (InputAccel == 0 || (rearWheel.rpm > 5 && InputAccel <= -0.2f))
      {
        rearWheel.brakeTorque = 25;
      }
      else
      {
        rearWheel.brakeTorque = 0;
      }
    }

    float valueToFix = 0;
    [HideInInspector] public float smoothSteerR;
    [HideInInspector] public float smoothSteerL;

    [HideInInspector] public bool hit;

    //Função que controla a direção da moto/bike
    private void SteerBase()
    {
      float InputSteer = Input.GetAxis("Horizontal");
      float InputAccel = Input.GetAxis("Vertical");
      frontWheel.steerAngle = InputSteer * steeringForce;

      SteerTransform.localEulerAngles = new Vector3(SteerTransform.localEulerAngles.x, steeringForce * (smoothSteerR - smoothSteerL), SteerTransform.localEulerAngles.z);

      if (hit)
      {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
      }
      else
      {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, maxInclination * (smoothSteerR - smoothSteerL));
      }
      rigidbody.solverIterations = 100;
      rigidbody.solverIterations = 100;
    }

    //Suavizadores do movimento da moto/bike
    private void LerpSteerDecrementalR()
    {
      if (Input.GetAxis("Horizontal") == 0 || Input.GetAxis("Horizontal") < 0)
      {
        if (smoothSteerR > 0.01f)
        {
          smoothSteerR = smoothSteerR - 0.01f;
        }
        else if (smoothSteerR <= 0.01f)
        {
          smoothSteerR = 0;
        }
      }
    }


    private void LerpSteerDecrementalL()
    {
      if (Input.GetAxis("Horizontal") == 0 || Input.GetAxis("Horizontal") > 0)
      {
        if (smoothSteerL > 0.01f)
        {
          smoothSteerL = smoothSteerL - 0.01f;
        }
        else if (smoothSteerL <= 0.01f)
        {
          smoothSteerL = 0;
        }
      }
    }

    void LerpSteerIncrementalR()
    {
      if (Input.GetAxis("Horizontal") > 0 && ActualVelocity >= 5)
      {
        if (smoothSteerR < 1)
        {
          smoothSteerR = smoothSteerR + 0.02f;
        }
        else if (smoothSteerR >= 1f)
        {
          smoothSteerR = 1;
        }
      }
    }
    void LerpSteerIncrementalL()
    {
      if (Input.GetAxis("Horizontal") < 0 & ActualVelocity >= 5)
      {
        if (smoothSteerL < 1)
        {
          smoothSteerL = smoothSteerL + 0.1f;
        }
        else if (smoothSteerL >= 1f)
        {
          smoothSteerL = 1;
        }
      }
    }


    //Função que controla situações de acidente da moto/bike
    private void OnCollisionEnter()
    {
      if ((transform.rotation.x > 90 || transform.rotation.x < -90))
      {
        hit = true;
        rigidbody.constraints = RigidbodyConstraints.None;
      }
    }

    //Função que faz o Reset da moto/bike
    void RestBike()
    {
      transform.rotation = Quaternion.identity;
      hit = false;
      rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;

      if (haveADriver)
      {
        RagdollDriver.DestroRagdoll();
      }
    }


    private void OnGUI()
    {
      if (GUILayout.Button("Reset The Bike"))
      {
        RestBike();
      }
    }

    //Medidor de estabilidade
    void Tensor()
    {
      /* if (transform.localRotation.y > 100 || transform.localRotation.y < -100)
       {
         maxInclination = maxInclinationSet;
       }
       else*/
      {
        maxInclination = -maxInclinationSet;
      }
    }

    // public void OnTriggerEnter(Collider objColisao){
    //   if(objColisao.tag.Equals("wayPoint")){
    //     Debug.Log("Houve colisão: " + objColisao.gameObject.name);
    //   }
    // }

  }
}

