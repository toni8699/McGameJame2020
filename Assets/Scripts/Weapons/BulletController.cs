using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    [SerializeField] private float _timeOut = 2;
    public ParticleSystem _ps;
    private Vector2 direction;
    private Rigidbody2D _rb;

    private GameObject parent;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        
    }


    //void FixedUpdate()
    //{

    //     direction * _speed * Time.deltaTime;
    //        //Vector2.MoveTowards(transform.position, direction, step);
    //}

    public void Fire(Vector2 direction, GameObject parent)
    {
        GetComponent<SpriteRenderer>().enabled = true;
        this.direction = direction.normalized * _speed;
        this.parent = parent;
        gameObject.SetActive(true);
        _rb.velocity = this.direction;
        StartCoroutine(Deactivate());
    }

    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(_timeOut);
        gameObject.SetActive(false);
    }

    //private IEnumerator BulletExplosion(Transform transform)
    //{
    //    yield return Instantiate(_ps, transform);
    //    gameObject.SetActive(false);

    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.collider.gameObject.CompareTag("Zombie"))
        {
            Debug.Log("ZOMBIE IS DEAAAAD");
            //StartCoroutine(BulletExplosion(transform));
            //collision.collider.GetComponent<EntityBehaviour>().Death(false);
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
        }

        
        //Instantiate(_ps, transform);
        if (collision.collider.gameObject.Equals(parent))
        {
           
            return;
        }
        if (collision.collider.gameObject.CompareTag("Player"))
            //Instantiate(_ps, transform);
            
            collision?.collider?.gameObject?.GetComponent<PlayerController>()?.Kill(); // TODO Kill player
        if (collision.collider.gameObject.CompareTag("Obstacle"))
        {
            //Instantiate(_ps, transform);
            gameObject.SetActive(false);
        }
        else
        {
            //Instantiate(_ps, transform);
            collision?.collider?.GetComponent<EntityBehaviour>()?.Death(false);
        }

        transform.position = new Vector3(-1000, -1000, -1000);

    }
}
