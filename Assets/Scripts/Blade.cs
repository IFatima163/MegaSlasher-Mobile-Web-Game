using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Camera mainCamera;
    private Collider bladeCollider;
    //gameplay activation
    private bool slicing;
    public float sliceForce = 5f;
    //where player is (public getter and pvt setter)
    public Vector3 direction {get; private set;}
    //required game speed
    public float minSlicingVelocity = 0.01f;
    private TrailRenderer bladeTrail;
    private void Awake()
    {
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider>();
        bladeTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        StopSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        else if (slicing)
        {
            ContinueSlicing();
        }
    }

    private void StartSlicing()
    {
        //Starting position
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;
        transform.position = newPosition;
        slicing = true;
        bladeCollider.enabled = true;
        //start mouse trail
        bladeTrail.enabled = true;
        bladeTrail.Clear();
    }

    private void StopSlicing()
    {
        slicing = false;
        bladeCollider.enabled = false;
        //end mouse trail
        bladeTrail.enabled = false;
    }

    private void ContinueSlicing()
    {
        //getting position where slicer should go to
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;
        direction = newPosition - transform.position;
        //getting velocity of movement by comparing the distance covered within the last frame & to disable if mouse not moving
        float velocity = direction.magnitude / Time.deltaTime; 
        bladeCollider.enabled = velocity > minSlicingVelocity;
        //updating position to where it should be
        transform.position = newPosition;
    }
}
