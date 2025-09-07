using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //all fruits
    public GameObject[] fruitPrefabs;
    //bomb spawner & drop rate range
    public GameObject bombPrefab;
    
    [Range(0f, 1f)]
    public float bombDrop = 0.05f;
    //delay b/w fruit spawning
    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;
    //angle variation
    public float minAngle = -15f;
    public float maxAngle = 15f;
    //force randomization
    public float minForce = 18f;
    public float maxForce = 22f;
    //spawn time
    public float maxLifetime = 5f;
    //spawning platform
    private Collider spawnArea;

    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }
    
    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        //initial delay when starting the game to get comfortable - 2 secs
        yield return new WaitForSeconds(2f);

        while (enabled)
        {
            //what to spawn
            GameObject prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];
            //where to spawn
            if (Random.value < bombDrop)
            {
                //spawn a bomb instead of the fruit
                prefab = bombPrefab;
            }
            Vector3 position = new Vector3();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);
            //which angle to spawn at
            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));
            //spawning & destruction
            GameObject fruits = Instantiate(prefab, position, rotation);
            Destroy(fruits, maxLifetime);
            //spawn force application
            float force = Random.Range(minForce, maxForce);
            fruits.GetComponent<Rigidbody>().AddForce(fruits.transform.up * force, ForceMode.Impulse);
            //when to spawn
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}
