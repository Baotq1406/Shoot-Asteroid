using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private Transform _minPos;
    [SerializeField] private Transform _maxPos;

    [SerializeField] GameObject prefab;

    public float spawnTimer;
    [SerializeField] private float _spawnInterval;
 
    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime * PlayerController.Instance.boost;
        if (spawnTimer >= _spawnInterval)
        {
            spawnTimer = 0f;
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        Instantiate(prefab, RandomSpawnPoint(), transform.rotation);
    }

    private Vector2 RandomSpawnPoint()
    {
        Vector2 spawnPoint;

        spawnPoint.x = Random.Range(_minPos.position.x, _maxPos.position.x);
        spawnPoint.y = _minPos.position.y;

        return spawnPoint;
    }
}
