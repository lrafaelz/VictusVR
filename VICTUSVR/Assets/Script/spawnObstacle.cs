using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObstacle : MonoBehaviour
{
private Renderer barrierRenderer;
    private Collider barrierCollider;
    private bool isBarrierActive = true;

    private void Start()
    {
        barrierRenderer = GetComponent<Renderer>();
        barrierCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Change the input condition as needed
        {
            isBarrierActive = !isBarrierActive;
            barrierRenderer.enabled = isBarrierActive;
            barrierCollider.enabled = isBarrierActive; // Toggle collider along with visibility
        }
    }
}