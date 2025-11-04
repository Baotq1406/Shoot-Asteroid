using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private Transform _minPos;
    [SerializeField] private Transform _maxPos;

    [SerializeField] private int _waveNumber;
    [SerializeField] private List<Wave> _waves;

    [System.Serializable]
    public class Wave
    {
        public GameObject prefab;
        public float spawnTimer;
        [SerializeField] private float _spawnInterval;
        public float spawnInterval { get { return _spawnInterval; } }
        [SerializeField] private int objectsPerWave;
        public int ObjectsPerWave { get { return objectsPerWave; } }

        [SerializeField] private int spawnedObjectsCount;
        public int SpawnedObjectsCount { get { return spawnedObjectsCount; } set { spawnedObjectsCount = value; } }

    }

    // Update is called once per frame
    void Update()
    {
        _waves[_waveNumber].spawnTimer += Time.deltaTime * PlayerController.Instance.boost;

        // neu den thoi diem spawn thi spawn doi tuong
        if (_waves[_waveNumber].spawnTimer >= _waves[_waveNumber].spawnInterval)
        {
            _waves[_waveNumber].spawnTimer = 0f;
            SpawnObject();
        }
        // neu da spawn het so luong doi tuong trong wave thi chuyen sang wave tiep theo
        if (_waves[_waveNumber].SpawnedObjectsCount >= _waves[_waveNumber].ObjectsPerWave)
        {
            _waves[_waveNumber].SpawnedObjectsCount = 0;
            _waveNumber++;
            if (_waveNumber >= _waves.Count)
            {
                _waveNumber = 0;
            }
        }
    }

    // spawn doi tuong
    private void SpawnObject()
    {
        Instantiate(_waves[_waveNumber].prefab, RandomSpawnPoint(), transform.rotation);
        _waves[_waveNumber].SpawnedObjectsCount++;
    }

    // tao diem spawn ngau nhien trong khoang minPos va maxPos
    private Vector2 RandomSpawnPoint()
    {
        Vector2 spawnPoint;

        spawnPoint.x = Random.Range(_minPos.position.x, _maxPos.position.x);
        spawnPoint.y = _minPos.position.y;

        return spawnPoint;
    }
}
