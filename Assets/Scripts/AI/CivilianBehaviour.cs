using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianBehaviour : EntityBehaviour
{
    void Awake()
    {
        _targets = new byte[4] {0,0,1,2};
        _seek = GetComponent<SeekingBehaviour>();
        _wander = GetComponent<WanderBehaviour>();

    }

    public override void Death(bool deathByZombie)
    {
        base.Death(deathByZombie);

        EventManager.KillCivilian(gameObject);
    }

    public void turned()
    {
        _targets = new byte[4] {1,1,1,0};
        this.gameObject.tag = "Zombie";
        this.gameObject.GetComponent<SpriteRenderer>().sprite = _zSprite;
        this.gameObject.GetComponent<Animator>().runtimeAnimatorController = _zAnim;
    }
}
