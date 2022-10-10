using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class dogController : MonoBehaviour
{

    [Header("Stats")]
    public float        moveSpeed = 1f;
    public float        sprintSpeed = 2f;
    public Vector3      feet;

    [Header("Dog Data")]
    public Rigidbody    body;
    public Transform    pos;
    public Transform    cameraPos;
    public multiCam     cameraController;

    [Header("Debug")]
    public float        speed;
    public Vector2      move;
    public bool         jump = false;

    public void OnMove(InputAction.CallbackContext ctx)
    {
        move = ctx.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext ctx)
    {
        if(ctx.ReadValue<float>() > 0f)
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = moveSpeed;
        }
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        jump = true;
    }

    private void Jump()
    {
        jump = false;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(pos.TransformPoint(feet), -pos.up, 0.1f);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(body != null)
        {
            pos = body.transform;
        }
        speed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        /*var movev = new Vector3(0f, 0f, move.y)*speed;
        body.velocity = pos.TransformVector(movev);
        movev = new Vector3(move.x, 0f, move.y).normalized;
        var ler = Vector3.Lerp(pos.forward, pos.TransformDirection(movev), Time.deltaTime*2);
        pos.LookAt(ler+pos.position);*/


        if(move.magnitude > 0f) //if move, then move
        {
            if(cameraPos == null)
            {
                Debug.Log("dogController: oops, didn't assign camera input");
            }
            else
            {
                var move3D = new Vector3(move.x, 0f, move.y)*moveSpeed;
                body.velocity = Vector3.ProjectOnPlane(cameraPos.TransformDirection(move3D)*speed, Vector3.up);
                var lerp = Vector3.Lerp(pos.forward, (Vector3.ProjectOnPlane(cameraPos.TransformDirection(move3D)*speed, Vector3.up)).normalized, Time.deltaTime*4f);
                pos.LookAt(lerp+pos.position);
            }

        }

        if(jump)
        {
            if(IsGrounded())
            {
                Jump();
            }
        }
    }
}
