using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviour : EntityBehaviour
{
    private Rigidbody2D rb;
    private Animator _animator;
    
    public GameObject DeathEffect;

    public AudioSource _idle1;
    public AudioSource _idle2;
    public AudioSource _idle3;
    public AudioSource _death;

    void Awake()
    {
        _targets = new byte[4] {1,1,1,0};
        _seek = GetComponent<SeekingBehaviour>();
        _wander = GetComponent<WanderBehaviour>();
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Grunt());
    }

    private void FixedUpdate()
    {
        if (rb.velocity.y > 0.1)
        {
            _animator.SetInteger("move", 4);
        }else if (rb.velocity.y < -0.1)
        {
            _animator.SetInteger("move", 2);
        }else if (rb.velocity.x>0.1)
        {
            _animator.SetInteger("move", 3);
        }
        else
        {
            _animator.SetInteger("move", 1);
        }
    }

    public override void Death(bool deathByZombie)
    {
        _death.Play();
        base.Death(deathByZombie);

        EventManager.KillZombie(gameObject);

        Instantiate(DeathEffect, transform);
    }

    override protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            collision.collider.GetComponent<PlayerController>().Kill();
        else if (collision.collider.CompareTag("Civilian") || collision.collider.CompareTag("Military"))
             collision.collider.GetComponent<EntityBehaviour>().Death(true);
        GetComponent<EntityBehaviour>().stopSeek();
    }

    void OnCollisionStay2D(Collision2D col)
    {
        Destroy(this);
    }

    private IEnumerator Grunt()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(10, 30));

        switch(UnityEngine.Random.Range(0,3))
        {
            case 0:
                _idle1.Play();
                break;
            case 1:
                _idle2.Play();
                break;
            case 2:
                _idle3.Play();
                break;
        }

        StartCoroutine(Grunt());

    }
}
