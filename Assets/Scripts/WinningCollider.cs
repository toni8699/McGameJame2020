using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinningCollider : MonoBehaviour
{
    [SerializeField] private Text _text;

    void Awake()
    {
        _text.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _text.enabled = true;
            Time.timeScale = 0;
        }
    }
}
