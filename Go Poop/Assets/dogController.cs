using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class dogController : MonoBehaviour
{
    public Rigidbody    body;
    public float        moveSpeed = 1f;
    public float        sprintSpeed = 2f;
    private float       speed;
    public Transform    pos;
    public Vector2      move;



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

    // Start is called before the first frame update
    void Start()
    {
        if(body != null)
        {
            pos = body.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var movev = new Vector3(0f, 0f, move.y)*speed;
        body.velocity = pos.TransformVector(movev);
        movev = new Vector3(move.x, 0f, move.y).normalized;
        var ler = Vector3.Lerp(pos.forward, pos.TransformDirection(movev), Time.deltaTime*2);
        pos.LookAt(ler+pos.position);
    }
}
