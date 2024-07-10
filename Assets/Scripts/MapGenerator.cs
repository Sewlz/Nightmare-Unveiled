using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] mapPrefabs; // prefab  map
    public Transform player; // Transform player
    private float spawnZ = 0.0f;
    public float tileLength = 12.0f;
    public int amnTilesOnScreen = 3;

    void Start()
    {
        // Kh?i t?o ði?m spawn ð?u tiên
        player = GameObject.FindGameObjectWithTag("Player").transform;
        for ( int i = 0; i < amnTilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    private void Update()
    {
        if (player.position.z > ( spawnZ - amnTilesOnScreen * tileLength))
        {
            SpawnTile();
        }
    }

   private void SpawnTile(int prefabIndex = -1)
    {
        GameObject go;
        go = Instantiate(mapPrefabs[0]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
    }
}
