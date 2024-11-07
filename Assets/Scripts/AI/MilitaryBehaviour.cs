using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilitaryBehaviour : EntityBehaviour
{
    public GameObject bullethandler;
    public Rigidbody2D rb;
    [SerializeField]
    private Animator _animator;
    public float movespeed = 10f;

    private GameObject target;
    private bool inRange = false;

    void Awake()
    {

        _targets = new byte[4] {1,0,0,1};
        _seek = GetComponent<SeekingBehaviour>();
        _wander = GetComponent<WanderBehaviour>();
    }
    void Update(){
        

    }
    void FixedUpdate(){
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

    void aggroPlayer()
    {
        _targets[0] = 1;
    }

    void fleeZombies()
    {
        _targets[3] = 2;
    }

    public override void Death(bool deathByZombie)
    {
        base.Death(deathByZombie);

        EventManager.KillMilitary(gameObject);
    }


    override protected void attack(GameObject obj)
    {
        if (obj == null) return;
        inRange = true;
        target = obj;

        Vector2 dir = target.transform.position - transform.position;
        bullethandler.GetComponent<BulletHandler>().ShootBullet(new Vector2(transform.position.x, transform.position.y) + dir, dir, gameObject);

        StartCoroutine(Wait());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.Equals(target)) return;

        inRange = false;
        target = null;
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        if (inRange)
        {
            attack(target);
        }
    }
}
