using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    public bool canSpawn = true; 
    public GameObject sheepPrefab; 
    public List<Transform> sheepSpawnPositions = new List<Transform>();
    public float timeBetweenSpawns;
    private List<GameObject> sheepList = new List<GameObject>();

    // To make spawnrate go faster as time goes on
    public float INITIAL_SPAWNRATE = 2f;
    public float MIN_SPAWNRATE = 0.1f;
    public float difficultyScaling = 0.01f;
    private float roundStart = 0;

    // Start is called before the first frame update
    void Start()
    {
        roundStart = Time.time;
        StartCoroutine(SpawnRoutine());       
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate current spawn rate
        float currentSpawnRate = INITIAL_SPAWNRATE - ((Time.time - roundStart) * difficultyScaling);
        currentSpawnRate = Mathf.Max(currentSpawnRate, MIN_SPAWNRATE);
        
        // Set the time between spawns based on the current spawn rate
        timeBetweenSpawns = currentSpawnRate;
    }
    
    private IEnumerator SpawnRoutine() 
    {
        while (canSpawn) 
        {
            SpawnSheep();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    private void SpawnSheep()
    {
        Vector3 randomPosition = sheepSpawnPositions[Random.Range(0, sheepSpawnPositions.Count)].position; 
        GameObject sheep = Instantiate(sheepPrefab, randomPosition, sheepPrefab.transform.rotation); 
        sheepList.Add(sheep); 
        sheep.GetComponent<Sheep>().SetSpawner(this); 
    }

    public void RemoveSheepFromList(GameObject sheep)
    {
        sheepList.Remove(sheep);
    }

    public void DestroyAllSheep()
    {
        foreach (GameObject sheep in sheepList)
        {
            Destroy(sheep);
        }

        sheepList.Clear();
    }
}
