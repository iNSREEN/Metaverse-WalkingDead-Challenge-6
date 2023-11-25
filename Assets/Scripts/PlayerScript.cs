using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Movment")]
    public float playerSpeed = 1.9f;
    public float playerSprint = 3f;

    [Header("Player Health Things")]
    private float playerHealth = 120f;
    public float presentHealth;
    public GameObject playerDamage;
    /*    public HealthBar healthBar;*/


    [Header("Player Script Camera")]
    public Transform playerCamera;
    public GameObject EndGameMenuUI;

    [Header("Player Animation and Gravitiy")]
    public CharacterController cC;
    public float gravity = -9.81f;
    public Animator animator;


    [Header("Player Movment Jumping and Velocity")]
    public float turnCalmTime = 0.1f;
    float turnCamVelocity;
    public float jumpRange = 1f;
    Vector3 velocity;
    public Transform surfaceCheck;
    bool onSurface;
    public float surfaceDistance = 0.4f;
    public LayerMask surfaceMask;


/*    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //to lock mousei n the middle in the start of the game
        presentHealth = playerHealth;
        //Debug.Log("---------------Start PLayer SCript");
        //Debug.Log(healthBar);
        //Debug.Log(playerHealth);

        *//*   healthBar.GiveFullHealth(playerHealth);*//* // call other script to gave full value
                                                       //Debug.Log("after Calling  Healthbar Script");
                                                       //Debug.Log(playerHealth);
    }*/
    private void Update()
    {
        /*        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask); //check if the player in the surface or air
                if (onSurface && velocity.y < 0)
                {
                    velocity.y = -2f;
                }
                velocity.y += gravity * Time.deltaTime;
                cC.Move(velocity * Time.deltaTime);*/
        playerMove();
        /*        Jump();
                Sprint();*/

    }
    void playerMove()
    {
        float horizontal_axis = Input.GetAxisRaw("Horizontal"); //left right
        float vertical_axis = Input.GetAxisRaw("Vertical"); //up down

        Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            cC.Move(direction.normalized * playerSpeed * Time.deltaTime);
        }

        /* if (direction.magnitude >= 0.1f)
         {
             animator.SetBool("Idle", false);
             animator.SetBool("Walk", true);
             animator.SetBool("Running", false);
             animator.SetBool("RifleWalk", false);
             animator.SetBool("IdleAim", false);


             //------ Rotate charecter----------
             float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
             float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCamVelocity, turnCalmTime);
             transform.rotation = Quaternion.Euler(0f, angle, 0f);
             //-------------------------------
             Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

             cC.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
         }
         else
         {
             // for idle enmation
             animator.SetBool("Idle", true);
             animator.SetBool("Walk", false);
             animator.SetBool("Running", false);
         }
     }

     /* void Jump()
      {
          if (Input.GetButtonDown("Jump") && onSurface)
          {
              animator.SetBool("Idle", false);
              animator.SetTrigger("Jump");
              velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
          }
          else
          {
              animator.SetBool("Idle", true);
              animator.ResetTrigger("Jump");
          }
      }

      void Sprint()
      {
          if ((Input.GetButton("Sprint") && Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.UpArrow) && onSurface))
          {
              float horizontal_axis = Input.GetAxisRaw("Horizontal"); //left right
              float vertical_axis = Input.GetAxisRaw("Vertical"); //up down

              Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;
              if (direction.magnitude >= 0.1f)
              {
                  animator.SetBool("Walk", false);
                  animator.SetBool("Running", true);

                  //------ Rotate charecter----------
                  float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
                  float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCamVelocity, turnCalmTime);
                  transform.rotation = Quaternion.Euler(0f, angle, 0f);
                  //-------------------------------
                  Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                  cC.Move(moveDirection.normalized * playerSprint * Time.deltaTime);
              }
              else
              {
                  animator.SetBool("Walk", true);
                  animator.SetBool("Running", false);
              }

          }
      }

      public void playerHitDamage(float takeDamage)
      {
          presentHealth -= takeDamage;
          StartCoroutine(PlayerDamage());

          //Debug.Log("Inside  Damage script, Present Health before calling healthbar script" + presentHealth );


          *//*healthBar.SetHealth(presentHealth);*//* //update health slider
                                              // Debug.Log("Present Health after calling healthbar script: " + presentHealth);
                                              // Debug.Log("------------------");

          if (presentHealth <= 0)
          {
              PlayerDie();
          }
      }

      private void PlayerDie()
      {
          EndGameMenuUI.SetActive(true);
          Cursor.lockState = CursorLockMode.None;
          Object.Destroy(gameObject, 1.0f);
      }


      // to show image blood when zombi hit player
      IEnumerator PlayerDamage()
      {
          playerDamage.SetActive(true);
          yield return new WaitForSeconds(0.2f); // change healthbar damage for player after 0.2 Sec
          playerDamage.SetActive(false);
      }*/

    }
}