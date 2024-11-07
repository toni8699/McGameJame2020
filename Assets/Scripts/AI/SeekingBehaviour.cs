using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingBehaviour : MonoBehaviour
{
    public Vector3 _target;
    public GameObject _targetObject;
    private Rigidbody2D _rb;

    [SerializeField] private float _radius = 0.1f;
    [SerializeField] private float _threashold = 1f;
    [SerializeField] private float _avoidanceForceValue = 5f;
    [SerializeField] private float _blockedThreashold = 0.5f;
    [SerializeField] private float _timeOut = 2f;

    public float _maxFleeingVelocity = 2.0f;
    public float _maxSeekingVelocity = 6.0f;
    public int _fleeing = 1; // 1 = Don't flee, -1 = Flee

    private Vector2 lastPosition;
    private float timer = -1;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        lastPosition = transform.position;
        _targetObject = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (_target == null) return;

        if (_targetObject != null) _target = _targetObject.transform.position;

        if (Vector2.Distance(lastPosition, transform.position) < _blockedThreashold)
        {
            if (timer == -1) timer = 0;
            else
            {
                timer += Time.deltaTime;
                if (timer >= _timeOut)
                {
                    _rb.velocity += new Vector2(-_rb.velocity.y, _rb.velocity.x); // Adding perpendicular velocity to unstick character
                    timer = -1;
                }
            }
        }

        //if (Vector2.Distance(_target, transform.position) < _threashold && !_targetObject.CompareTag("Sensory"))
        //{
        //    if (_targetObject != null && gameObject.CompareTag("Zombie") && !_targetObject.CompareTag("Sensory"))
        //    {
                
        //        Debug.Log(_targetObject.tag);
          
        //        if (_targetObject.CompareTag("Player"))
        //            _targetObject.GetComponent<PlayerController>().Kill();
        //        else
        //            _targetObject.GetComponent<EntityBehaviour>().Death(true);
        //    }
        //    return;
        //}
        
        var combinedForce = AvoidanceForce();

        if (_fleeing == -1) // Is fleeing
        {
            combinedForce += FleeForce();
            combinedForce = combinedForce.normalized * _maxFleeingVelocity;
        }
        else
        {
            combinedForce += PursuitForce();
            combinedForce = combinedForce.normalized * _maxSeekingVelocity;
        }


        _rb.AddForce(combinedForce);
        if (_rb.velocity.magnitude > (_fleeing == -1 ? _maxFleeingVelocity : _maxSeekingVelocity))
        {
            _rb.velocity = _rb.velocity.normalized * (_fleeing == -1 ? _maxFleeingVelocity : _maxSeekingVelocity);
        }
    }

    private Vector2 FleeForce()
    {
        float desiredX = transform.position.x - _target.x;
        float desiredY = transform.position.y - _target.y;

        var normalDesired = new Vector2(desiredX, desiredY).normalized;

        Vector2 fleeForce = normalDesired - _rb.velocity;

        return fleeForce;
    }

    private Vector2 PursuitForce()
    {
        float desiredX = _target.x - transform.position.x;
        float desiredY = _target.y - transform.position.y;

        var normalDesired = new Vector2(desiredX, desiredY).normalized;

        Vector2 pursuitForce = normalDesired - _rb.velocity;

        return pursuitForce;
    }

    private Vector2 AvoidanceForce()
    {
        Vector2 extremity = _rb.position + new Vector2(-_rb.velocity.y, _rb.velocity.x).normalized * _radius;
        Vector2 potentialAvoidanceForce = CheckForCollision(extremity);

        if (!potentialAvoidanceForce.Equals(Vector2.zero)) return potentialAvoidanceForce;

        extremity = _rb.position + new Vector2(_rb.velocity.y, -_rb.velocity.x).normalized * _radius;

        return CheckForCollision(extremity);
    }

    private Vector2 CheckForCollision(Vector2 originOfRay)
    {
        Debug.DrawRay(originOfRay, _rb.velocity);
        var hit = Physics2D.Raycast(originOfRay, _rb.velocity.normalized, _threashold);

        if (hit.collider != null && hit.distance < _threashold)
        {
            return (hit.point - new Vector2(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y)).normalized * _avoidanceForceValue;
        }

        return Vector2.zero;
    }

}
