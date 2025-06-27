using System;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{   //set proper walkpath
    public Transform[] spawmPoints;
    public GameObject Zombie;
    public Transform[] activeSpawnPoints ;

    public int zombieSpawn = 1;
    public float spawnInterval = 20f;
    public float timer = 0f;

    bool canSpawn = false;
    public int spawnCount = 0;

    public int maxNumOfSpawnPoints = 4;
    public int minNumOfSpawnPoints = 2;

    public GameObject nextWaveNotification;

    WalkPointsData walkPointsData;

    private void Start()
    {
        walkPointsData = new WalkPointsData();
        spawmPoints =gameObject.GetComponent<WalkPath>().walkpoints ;
    }
    void Update()
    {
        CheckForZombie();
        Spawn();

    }
    void CheckForZombie()
    {
        if (GameObject.FindGameObjectWithTag("Zombie") == null && !canSpawn)
        {
            nextWaveNotification.SetActive(true);
            ZombieEar.Target = Vector3.zero;
            canSpawn = true;
            timer = Time.time + spawnInterval;
            RandomSpawnPoints();
        }
    }

    void Spawn()
    {

        if (canSpawn)
        {
                if (spawnCount < zombieSpawn)
                {
                    if (timer < Time.time)
                    {
                        foreach (Transform location in activeSpawnPoints)
                        {
                            GameObject zombie = Instantiate(Zombie, location.position, location.rotation);
                            Transform nextlocation = walkPointsData.SelectNextPoint(location);
                            
                            zombie.GetComponent<ZombieMovement>().curwalkpoint = location;
                        }
                        spawnCount++;
                        timer = Time.time + spawnInterval;
                    }
                }
                else
                {
                    spawnCount = 0;
                    canSpawn = false;
                    zombieSpawn ++;
                    maxNumOfSpawnPoints++;
                    minNumOfSpawnPoints++;
            }
        }
    }
    void RandomSpawnPoints()
    {
        int numOfSpawnPoints = Math.Abs((int)UnityEngine.Random.Range(minNumOfSpawnPoints,maxNumOfSpawnPoints));
        activeSpawnPoints = new Transform[numOfSpawnPoints];
        int i = 0;
        while ( i < numOfSpawnPoints)
        {
            int num = Math.Abs((int)UnityEngine.Random.Range(0,16));
            if (!activeSpawnPoints.Contains(spawmPoints[num]))
            {
                activeSpawnPoints[i] = spawmPoints[num];
                i++;
            }
            
        }
    }
}


