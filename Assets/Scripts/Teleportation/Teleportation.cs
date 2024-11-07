using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    [SerializeField] private Teleportation _otherEnd;
    [SerializeField] private float TeleportationTime = 4.5f;

    public Vector3 Position => gameObject.transform.position;

    public bool Disabled { get; set; }

    public AudioSource _teleportSound;

    void Start()
    {
        Disabled = false;

        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (player.Type != PlayerType.Accountant)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator CloseConnection()
    {
        Destroy(_otherEnd.gameObject);

        yield return new WaitForSeconds(0.2f);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Disabled)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            Teleport();
        }
    }

    private void Teleport()
    {
        _teleportSound.Play();
        StartCoroutine(TeleportCoroutine());
    }

    private IEnumerator TeleportCoroutine()
    {
        var player = GameManager.Instance.Player;

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>();
        }

        player.IsTeleporting = true;
        _otherEnd.Disabled = true;
        EventManager.BeginTeleport();
        player.transform.position = _otherEnd.Position;

        yield return new WaitForSeconds(TeleportationTime);
        player.IsTeleporting = false;
        EventManager.EndTeleport();
        StartCoroutine(CloseConnection());
    }
}
