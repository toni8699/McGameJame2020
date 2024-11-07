using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public enum PlayerType
{
    Accountant,
    Soldier,
    Civilian
}

public class PlayerController : MonoBehaviour
{
  [FormerlySerializedAs("Animator")] public Animator animator;

  public Rigidbody2D rb;
  public Transform startPosition;

  public GameObject animatorAccountant;
  public GameObject animatorCivilian;
  public GameObject animatorSoldier;
  
  public RuntimeAnimatorController anim;
  
  public PlayerType Type { get; set; }
  public bool IsTeleporting { get; set; }

  [SerializeField]
  private float thrust = 1.0f;

  [SerializeField] private bool cannotMoveBack = true;
  private bool isDead = false;

  Vector2 positiveXAxis = new Vector2(1, 0);

  public AudioSource _footsteps;
  public AudioSource _death;

  private PlayerAttack PA;

  void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    transform.position = startPosition.position;
    Debug.Log("isdead ?"+isDead);
    
    animator = gameObject.AddComponent<Animator>() as Animator;


//    Type = PlayerType.Civilian;

//    DontDestroyOnLoad(gameObject);

    PA = GetComponent<PlayerAttack>();
    
  }

  void Start()
  {
    int type = GameManager.Instance.playerIndex;

    if (type == 0)
    {
      Type = PlayerType.Accountant;
    } else if (type == 1)
    {
      Type = PlayerType.Civilian;
    }
    else
    {
      Type = PlayerType.Soldier;
    }
//      DontDestroyOnLoad(gameObject);
     
      switch (Type)
      {
          case PlayerType.Accountant:
            thrust = 1f;
            anim = animatorAccountant.GetComponent<Animator>().runtimeAnimatorController;
            break;
          case PlayerType.Civilian:
            thrust = 2f;
            anim = animatorCivilian.GetComponent<Animator>().runtimeAnimatorController;
            break;
          case PlayerType.Soldier:
            thrust = 0.5f;
            anim = animatorSoldier.GetComponent<Animator>().runtimeAnimatorController;
            PA.enabled = true;
            break;
          default:
            thrust = 1f;
            anim = animatorAccountant.GetComponent<Animator>().runtimeAnimatorController;
            break;
      }
    
      this.GetComponent<Animator>().runtimeAnimatorController = anim as RuntimeAnimatorController;
  }

  void FixedUpdate()
  {
    if (isDead) {

     // GameOver();
      //Debug.Log("isdead ?"+isDead);
      //Debug.Log("is dead: "+isDead);

     SceneManager.LoadScene(2);
      }

    if (IsTeleporting)
    {
      animator.SetBool("isMoving", false);
      return;
    }

    // Vector3 v3 = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f);
    // rb.AddForce(thrust * v3.normalized, ForceMode2D.Impulse);

    if (Mathf.Abs(rb.velocity.x) < 1 && Mathf.Abs(rb.velocity.y) < 1)
    {
      animator.SetBool("isMoving", false);
    }
    else
    {
      animator.SetBool("isMoving", true);
    }

    if(Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
    {
      _footsteps.Stop();
    }

    float angleBetweenAxisAndMouse;

    Vector2 tmpV2 = Input.mousePosition;
    Vector2 playerPosition = Camera.main.WorldToScreenPoint(transform.position);
    Vector2 mouseDirection = new Vector2(tmpV2.x - playerPosition.x, tmpV2.y - playerPosition.y);


    angleBetweenAxisAndMouse = Vector2.SignedAngle(positiveXAxis, mouseDirection);
    // animator.SetFloat("angleDirection", angleBetweenAxisAndMouse);

    if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
    {
      rb.AddForce(thrust * mouseDirection.normalized, ForceMode2D.Impulse);
      if(!_footsteps.isPlaying)
        _footsteps.Play();
    }
    if (angleBetweenAxisAndMouse < -157.5)
    {
      animator.SetInteger("moveDirection", 1);
      animator.SetInteger("move", 1);
    }
    else if (angleBetweenAxisAndMouse > -157.5 && angleBetweenAxisAndMouse < -112.5)
    {
      animator.SetInteger("moveDirection", 2);
      animator.SetInteger("move", 2);
    }
    else if (angleBetweenAxisAndMouse > -112.5 && angleBetweenAxisAndMouse < -67.5)
    {
      animator.SetInteger("moveDirection", 3);
      animator.SetInteger("move", 2);
    }
    else if (angleBetweenAxisAndMouse > -67.5 && angleBetweenAxisAndMouse < -22.5)
    {
      animator.SetInteger("moveDirection", 4);
      animator.SetInteger("move", 2);
    }
    else if (angleBetweenAxisAndMouse > -22.5 && angleBetweenAxisAndMouse < 22.5)
    {
      animator.SetInteger("moveDirection", 5);
      animator.SetInteger("move", 3);
    }
    else if (angleBetweenAxisAndMouse > 22.5 && angleBetweenAxisAndMouse < 67.5)
    {
      animator.SetInteger("moveDirection", 6);
      animator.SetInteger("move", 4);
    }
    else if (angleBetweenAxisAndMouse > 67.5 && angleBetweenAxisAndMouse < 112.5)
    {
      animator.SetInteger("moveDirection", 7);
      animator.SetInteger("move", 4);
    }
    else if (angleBetweenAxisAndMouse > 112.5 && angleBetweenAxisAndMouse < 157.5)
    {
      animator.SetInteger("moveDirection", 8);
      animator.SetInteger("move", 4);
    }
    else if (angleBetweenAxisAndMouse > 157.5)
    {
      animator.SetInteger("moveDirection", 1);
    }

  }

  void OnTriggerExit2D(Collider2D other)
  {
    //Does not allow player to move back to the choice he made
    if (other.gameObject.CompareTag("Choice"))
    {
      other.isTrigger = false;
    }
  }

  public void Kill()
  {
    if(isDead)
    {
      return;
    }
    isDead = true;
    _death.Play();
  
  }
public void GameOver(){
  //isDead = false;
  SceneManager.LoadScene(2);
}

}
