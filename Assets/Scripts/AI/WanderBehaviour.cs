using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderBehaviour : MonoBehaviour
{    
    public SeekingBehaviour _seek;
    private bool _waiting = false;
    //GameObject _obj = new GameObject("Point Generated");

    //void Awake()
    //{
    //    _seek._maxSeekingVelocity /= 2.0f;
    //}

    private void Awake()
    {
        Debug.Assert(_seek != null);
    }

    void OnEnable()
    {
        _seek._maxSeekingVelocity /= 2.0f;
    }

    void OnDisable()
    {
        _seek._maxSeekingVelocity *= 2.0f;
    }

    void FixedUpdate()
    {
        if(!_waiting)
        {
            StartCoroutine(gen());
        }

    // if(!_seek.enabled)
    //     this.enabled = false;
    }

    private Vector3 GeneratePoint()
    {
        Vector3 vec = gameObject.transform.position;
        vec.x += Random.Range(-10,10);
        vec.y += Random.Range(-10,10);
        return vec;
    }

    private IEnumerator gen()
    {
        _waiting = true;
        _seek._target = GeneratePoint();
        _seek._targetObject = null;
        yield return new WaitForSeconds(Random.Range(5, 8));
        _waiting = false;
    }



}
