using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bullet;

    [SerializeField] private GameObject bulletHandler;
    [SerializeField] private float _attackStrength = 5;
    [SerializeField] private int _amo = 50;

    private BulletHandler bulletHandlerScript;

    void Start()
    {
        bulletHandlerScript = bulletHandler.GetComponent<BulletHandler>();
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (player.Type != PlayerType.Soldier)
        {
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
//        Debug.Log(GameManager.Instance.PlayerType);
        if (Input.GetMouseButtonDown(0))
        {
            if (_amo > 0)
            {
                Vector3 dir = Input.mousePosition;

                //Gizmos.DrawSphere(Input.mousePosition, 1);

                Vector2 tmpV2 = Input.mousePosition;
                Vector2 playerPosition = Camera.main.WorldToScreenPoint(transform.position);
                Vector2 mouseDirection = new Vector2(tmpV2.x - playerPosition.x, tmpV2.y - playerPosition.y);

                var pos = new Vector2(transform.position.x, transform.position.y);
                //dir = Camera.main.ViewportToWorldPoint(dir);
                //Vector2 dir2 = new Vector2(dir.x, dir.y);
                Debug.DrawRay(transform.position, mouseDirection);
                Debug.DrawRay(Camera.main.ViewportToWorldPoint(Input.mousePosition), mouseDirection, Color.red);
                ////dir = dir - pos;
                //dir.Normalize();
                bulletHandlerScript.ShootBullet(pos + mouseDirection.normalized, mouseDirection, gameObject);
                
                _amo -= 1;
            }
            else
            {
                Debug.Log("no bullets left");
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            _amo += 10;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Collider2D enemy = Physics2D.OverlapCircle(transform.position, 2f);
            if (enemy != null)
            {
                CloseRangeAttack(enemy.gameObject);
            }
        }
    }

    private void CloseRangeAttack(GameObject subject)
    {
        // if subject is attacable
        // attack subject (kill? loose life?)
        // use attack strength
    }
}
