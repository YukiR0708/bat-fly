using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField, Header("アイテムプレハブ")] GameObject[] objectPrefabs;
    private float spawnDelay = 2;
    private float spawnInterval = 1.5f;
    [SerializeField, Header("Player")] GameObject player;
    private PlayerController playerControllerScript;
    [SerializeField, Header("スポーン距離")] float _spawnDistance;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnObjects), spawnDelay, spawnInterval);
        playerControllerScript = player.GetComponent<PlayerController>();

    }

    void SpawnObjects()
    {
        // Set random spawn location and random object index
        Vector3 spawnLocation = new Vector3(player.transform.position.x + _spawnDistance, Random.Range(1.0f,3.0f), 11);
        int index = Random.Range(0, objectPrefabs.Length);

        // If game is still active, spawn new object
        if (playerControllerScript.isGame)
        {
            Instantiate(objectPrefabs[index], spawnLocation, objectPrefabs[index].transform.rotation);
        }

    }
}
