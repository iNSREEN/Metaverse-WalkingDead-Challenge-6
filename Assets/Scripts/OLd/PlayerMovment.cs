using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovment : MonoBehaviour
{
    public Rigidbody rb;
    public Vector2 InputMovment;
    bool jump;
    bool run;
    float speed = 5;
    public void movment(InputAction.CallbackContext context)
    {
        InputMovment = context.ReadValue<Vector2>();

    }
    public void Jump(InputAction.CallbackContext context)
    {
        jump = context.action.triggered;
    }
    public void Run(InputAction.CallbackContext context)
    {
        run = context.action.triggered;
    }




    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Move = new Vector3(InputMovment.x, -0.2f, InputMovment.y);
        rb.velocity = Move * speed;
        if (jump)
        {
            rb.AddForce(Vector3.up * 100);
        }
        if (run)
        {
            speed = 10;
        }
    }
}
