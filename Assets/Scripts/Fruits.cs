using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;
    private Rigidbody fruitRigidbody;
    private Collider fruitCollider;
    private ParticleSystem juiceParticles;

    private void Awake()
    {
        fruitRigidbody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        juiceParticles = GetComponentInChildren<ParticleSystem>();
    }

    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        //updating score
        FindObjectOfType<GameManager>().IncreaseScore();
        //converting whole fruit to sliced when mouse overlaps
        whole.SetActive(false);
        sliced.SetActive(true);
        fruitCollider.enabled = false; //cannot be cut further
        juiceParticles.Play();
        //rotating after getting cut
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        //velocity of each slice setting same as whole fruit was
        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody slice in slices)
        {
            slice.velocity = fruitRigidbody.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse); //impulse to make it a one-time force only
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            //cutting fruit with respect to blade (mouse) properties
            Slice(blade.direction, blade.transform.position, blade.sliceForce);
        }
    }
}
