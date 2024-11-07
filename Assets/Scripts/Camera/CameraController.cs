using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private bool _clamp;
    private bool _followPlayer;
    private float _camZ;

    void Awake()
    {
        _followPlayer = true;
        _camZ = transform.position.z;
        _clamp = true;
    }

    void Start()
    {
        EventManager.TeleportBegunEvent += OnTeleportBegun;
        EventManager.TeleportEndedEvent += OnTeleportEnded;
    }

    private void OnTeleportEnded(object sender, EventArgs e)
    {
        _followPlayer = true;
    }

    private void OnTeleportBegun(object sender, EventArgs e)
    {
        _followPlayer = false;
    }

    void FixedUpdate()
    {
        if (_followPlayer)
        {
            //Follow the player
            transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y,
                transform.position.z);
        }

        if (_player.GetComponent<PlayerController>().IsTeleporting)//(GameManager.Instance.Player.IsTeleporting)
        {
           var playerPos = _player.gameObject.transform.position;
           var targetPosition = new Vector3(playerPos.x, playerPos.y);
           var original = new Vector2(transform.position.x, transform.position.y);
           var newPos = Vector2.Lerp(original, targetPosition, Time.deltaTime);

            //Interpolate
           transform.position = new Vector3(newPos.x, newPos.y, _camZ);
       }

        if (_clamp)
        {
            var pos = transform.position;

            pos.x = Mathf.Clamp(pos.x, 5.1f, 75.35f);
            pos.y = Mathf.Clamp(pos.y, -16.35f, -2.86f);

            transform.position = pos;
        }
    }

    void OnDestroy()
    {
        EventManager.TeleportBegunEvent -= OnTeleportBegun;
        EventManager.TeleportEndedEvent -= OnTeleportEnded;
    }

}
