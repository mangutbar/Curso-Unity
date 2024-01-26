using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPb;
    public GameObject powerup;
    private float spawnRange = 9;
    public int enemyCount;
    public int waveNumber = 1;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        Instantiate(powerup, GenerateSpawnPosition(), powerup.transform.rotation);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0 && !playerControllerScript.gameOver)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerup, GenerateSpawnPosition(), powerup.transform.rotation);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn;  i++)
        {
            Instantiate(enemyPb, GenerateSpawnPosition(), enemyPb.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float xCoord = Random.Range(-spawnRange, spawnRange);
        float zCoord = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(xCoord, 0, zCoord);
        return randomPos;
    }
}
