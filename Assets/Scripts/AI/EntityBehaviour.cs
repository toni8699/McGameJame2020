using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour : MonoBehaviour
{
    protected SeekingBehaviour _seek;
    protected WanderBehaviour _wander;
    protected byte[] _targets; // 1. Player 2. Civilians 3. Military 4. Zombies
                               // 0 = Ignore, 1 = Attack, 2 = Flee

    public Sprite _zSprite;
    public RuntimeAnimatorController _zAnim;

    private float _despawnTime = 0.5f;

    public AudioSource _attack;

    void Awake()
    {
        _wander.enabled = true;
        _wander._seek = _seek;
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        int act = 0;
        switch(col.gameObject.tag)
        {
            case "Sensory":
                act = _targets[0];
                break;

            case "Civilian":
                act = _targets[1];
                break;

            case "Military":
                act = _targets[2];
                break;

            case "Zombie":
                act = _targets[3];
                break;

            default:
                Debug.Log("help in choosing");
                break;
        }

        _seek._fleeing = 1; // Resets flee for if the target changes

        switch(act)
        {
            case 0: // Ignore
                //Debug.Log(this.gameObject.name + " is ignoring " + col.gameObject.name);
                break;

            case 1: // Attack
                Debug.Log(this.gameObject.name + " is attacking " + col.gameObject.name);
                attack(col.gameObject);
                break;

            case 2: // Flee
                Debug.Log("Fleeing"); // Have to create a fleeing script that incorporates the seeking
                _seek._fleeing = -1;
                moveTo(col.gameObject);
                break;

            default:
                Debug.Log("Action problem");
                break;
        }
    }

    private void moveTo(GameObject obj)
    {
        //_seek.enabled = true;
        _wander.enabled = false;

        _seek._targetObject = obj;
        _seek._target = obj.transform.position;

        
    }

    public void stopSeek()
    {
        //_seek.enabled = false;
        _wander.enabled = true;
    }

    virtual protected void attack(GameObject obj)
    {
        moveTo(obj);
    }

    public virtual void Death(bool deathByZombie)
    {
        if (deathByZombie)
        {
            _attack.Play();
            Turned();
        }
        else
        {
            _wander.enabled = false;
            _seek.enabled = false;
            StartCoroutine(Decay());
        }
    }

    private IEnumerator Decay()
    {
        yield return new WaitForSeconds(_despawnTime);
        gameObject.SetActive(false);
    }

    public void Turned()
    {
        Debug.Log("ZOmbie created");
        _targets = new byte[4] { 1, 1, 1, 0 };
        this.gameObject.tag = "Zombie";
        this.gameObject.GetComponent<SpriteRenderer>().sprite = _zSprite;
        this.gameObject.GetComponent<Animator>().runtimeAnimatorController = _zAnim;
        stopSeek();
    }

    virtual protected void OnCollisionEnter2D(Collision2D collision)
    {

    }
}