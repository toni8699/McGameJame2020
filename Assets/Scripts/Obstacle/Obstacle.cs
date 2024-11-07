using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Collider2D _collider;
    [SerializeField] private GameObject _obstaclePrefab;
    private float _delay = 1.0f;
    

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _obstaclePrefab.SetActive(false);
    }

    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (player.Type == PlayerType.Civilian)
        {
            _delay = 0.5f;
        }
    }

    private IEnumerator EnableObstacle()
    {
        yield return new WaitForSeconds(_delay);

        _collider.isTrigger = false;
        _obstaclePrefab.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Make sure not to enable on other
        if (other.CompareTag("Player"))
        {
            StartCoroutine(EnableObstacle());
        }
    }
}
