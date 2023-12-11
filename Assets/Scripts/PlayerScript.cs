using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.UI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
   /* public GameObject EndGameMenuUI;*/

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

    [Header("HelthBar")]
    [SerializeField]
    private Image healthBarImage;
    public AudioSource damgeAudio;
    private int Blood = 10;  // Assuming 3 is the maximum health
    public Animator animZombie;
    public AudioSource collectAudio;



    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        presentHealth = playerHealth;
        playerDamage.SetActive(false);
    }
    private void Update()
    {
        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask); 
        if (onSurface && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        cC.Move(velocity * Time.deltaTime);

        playerMove();
        Jump();
        Sprint();

    }
    void playerMove()
    {
        float horizontal_axis = Input.GetAxisRaw("Horizontal"); //left right
        float vertical_axis = Input.GetAxisRaw("Vertical"); //up down

        Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

        if (direction.magnitude >= 0.1f)
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


            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            cC.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
        }
        else
        {
            // for idle 
            animator.SetBool("Idle", true);
            animator.SetBool("Walk", false);
            animator.SetBool("Running", false);
        }
    }
    void Jump()
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
       /* StartCoroutine(PlayerDamage());*/
        Debug.Log("player heatdamgae");
        if (presentHealth <= 0)
        {
           Die();
        }
    }
 
    private void Die()
    {
      
        Cursor.lockState = CursorLockMode.None;
        Object.Destroy(gameObject, 1.0f);
        
    }

 



    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Aixi"))
        {
         /*   collectAudio.Play();*/
            other.gameObject.SetActive(false);
            Blood++;
            healthBarImage.fillAmount = (float)Blood * 10;
        }

        /*        if (other.gameObject.CompareTag("Zombie"))
                {
                    Debug.Log("Zombie Damge player -- Health PlayerScript");
                    damgeAudio.Play();
                    PlayerDamage();
                    *//*      Blood -= 5;*//*
                    Blood--;
                    StartCoroutine(PlayerDamage());

                    // Update the fillAmount of the health bar image
                    healthBarImage.fillAmount = (float)Blood / 10;

                    if (Blood == 0)
                    {
                        SceneManager.LoadScene("Loss");
                    }
                }

                if (other.gameObject.CompareTag("Boss"))
                {
                    damgeAudio.Play();
                    PlayerDamage();
                    *//* Blood -=20;*//*

                    Blood -= 4;
                    StartCoroutine(PlayerDamage());


                    // Update the fillAmount of the health bar image
                    healthBarImage.fillAmount = (float)Blood /10;

                    if (Blood == 0)
                    {
                        SceneManager.LoadScene("Loss");
                    }
                }*/

        if (other.gameObject.CompareTag("Zombie") || other.gameObject.CompareTag("Boss"))
        {
        
            damgeAudio.Play();
            PlayerDamage();

            // Adjust damage based on the enemy type
            int damageAmount = (other.gameObject.CompareTag("Zombie")) ? 1 : 4;
            Blood -= damageAmount;
            StartCoroutine(PlayerDamage());

            // Update the fillAmount of the health bar image
            healthBarImage.fillAmount = (float)Blood / 10;

            if (Blood == 0)
            {
                LoadSceneByNumber(5);
            }
        }

        if (other.gameObject.CompareTag("win"))
        {
            LoadSceneByNumber(4);
        }

    }

    IEnumerator PlayerDamage()
    {
        playerDamage.SetActive(true);
        yield return new WaitForSeconds(0.4f); // change healthbar damage for player after 0.2 Sec
        playerDamage.SetActive(false);
    }


    void LoadSceneByNumber(int sceneNumber)
    {
        // Use SceneManager to load the scene by number
        SceneManager.LoadScene(sceneNumber);
    }
}
