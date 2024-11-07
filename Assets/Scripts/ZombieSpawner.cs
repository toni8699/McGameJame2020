using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawns;
    [SerializeField] private GameObject _zPrefab;
    [SerializeField] private int _num;
    [SerializeField] private float _spawnDelay;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        for (int i = 0; i < _num; i++)
        {
            var randomIndex = Random.Range(0, _spawns.Length);

            Instantiate(_zPrefab, _spawns[randomIndex]);

            yield return new WaitForSeconds(_spawnDelay);
        }
    }
}
