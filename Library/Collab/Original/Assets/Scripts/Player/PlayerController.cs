using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public enum PlayerType
{
    Soldier,
    Accountant,
    Civilian
}

public class PlayerController : MonoBehaviour
{
  [FormerlySerializedAs("Animator")] public Animator animator;

  public Rigidbody2D rb;
  public Transform startPosition;
  
  public PlayerType Type { get; set; }
  public bool IsTeleporting { get; set; }

  [SerializeField]
  private float thrust = 1.0f;

  [SerializeField] private bool cannotMoveBack = true;
  private bool isDead = false;

  Vector2 positiveXAxis = new Vector2(1, 0);
  
  public RuntimeAnimatorController _anim;

  void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    transform.position = startPosition.position;
    Debug.Log("isdead ?"+isDead);
  }

  void Start()
  {
      switch (Type)
      {
        case PlayerType.Accountant:
          thrust = 3f;
          break;
        case PlayerType.Civilian:
          thrust = 5f;
          break;
        case PlayerType.Soldier:
          thrust = 1f;
          break;
      }
  }

  void FixedUpdate()
  {
    if (isDead) {
     // GameOver();
      //Debug.Log("isdead ?"+isDead);
      //Debug.Log("is dead: "+isDead);
      return;
     // SceneManager.LoadScene(2);
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

    float angleBetweenAxisAndMouse;

    Vector2 tmpV2 = Input.mousePosition;
    Vector2 playerPosition = Camera.main.WorldToScreenPoint(transform.position);
    Vector2 mouseDirection = new Vector2(tmpV2.x - playerPosition.x, tmpV2.y - playerPosition.y);


    angleBetweenAxisAndMouse = Vector2.SignedAngle(positiveXAxis, mouseDirection);
    // animator.SetFloat("angleDirection", angleBetweenAxisAndMouse);

    if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
    {
      rb.AddForce(thrust * mouseDirection.normalized, ForceMode2D.Impulse);
    }
    if (angleBetweenAxisAndMouse < -157.5)
    {
      animator.SetInteger("moveDirection", 1);
    }
    else if (angleBetweenAxisAndMouse > -157.5 && angleBetweenAxisAndMouse < -112.5)
    {
      animator.SetInteger("moveDirection", 2);
    }
    else if (angleBetweenAxisAndMouse > -112.5 && angleBetweenAxisAndMouse < -67.5)
    {
      animator.SetInteger("moveDirection", 3);
    }
    else if (angleBetweenAxisAndMouse > -67.5 && angleBetweenAxisAndMouse < -22.5)
    {
      animator.SetInteger("moveDirection", 4);
    }
    else if (angleBetweenAxisAndMouse > -22.5 && angleBetweenAxisAndMouse < 22.5)
    {
      animator.SetInteger("moveDirection", 5);
    }
    else if (angleBetweenAxisAndMouse > 22.5 && angleBetweenAxisAndMouse < 67.5)
    {
      animator.SetInteger("moveDirection", 6);
    }
    else if (angleBetweenAxisAndMouse > 67.5 && angleBetweenAxisAndMouse < 112.5)
    {
      animator.SetInteger("moveDirection", 7);
    }
    else if (angleBetweenAxisAndMouse > 112.5 && angleBetweenAxisAndMouse < 157.5)
    {
      animator.SetInteger("moveDirection", 8);
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
    isDead = true;
  }
public void GameOver(){
  //isDead = false;
  SceneManager.LoadScene(2);
}

}
