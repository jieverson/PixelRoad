using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Spawner : MonoBehaviour {

    public float spawnTime = 2;
    public Transform[] objectValues;
    public string[] objectKeys;
    public Vector3[] spawnPositions;
    public string[] spawnPatterns;

    private float _timer;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        _timer += Time.deltaTime;
        if (_timer >= spawnTime)
        {
            _timer -= spawnTime;
            Spawn();
        }
	}

    private void Spawn()
    {
        var index = Random.Range(0, spawnPatterns.Length);
        var pattern = spawnPatterns[index];
        SpawnPattern(pattern);
    }

    private void SpawnPattern(string pattern)
    {
        for (int i = 0; i < pattern.Length; i++)
        {
            var code = pattern[i];
            SpawnObject(code, spawnPositions[i]);
        }
    }

    private void SpawnObject(char code, Vector3 position)
    {
        for (int i = 0; i < objectKeys.Length; i++)
        {
            var key = objectKeys[i][0];
            if (key == code)
            {
                var prefab = objectValues[i];
                var localPosition = position + Vector3.up * prefab.localScale.y * 0.5f;
                CreateObject(prefab, localPosition);
            }
        }
    }

    private void CreateObject(Transform prefab, Vector3 position)
    {
        Instantiate(prefab, position, Quaternion.identity);
    }
}
