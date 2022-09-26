using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class dogController : MonoBehaviour
{
    public Rigidbody    body;
    public float        moveSpeed = 1f;
    public Transform    pos;
    public Vector2      move;



    public void OnMove(InputAction.CallbackContext ctx)
    {
        move = ctx.ReadValue<Vector2>();
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
        pos.position = pos.position + new Vector3(move.x, 0f, move.y)*moveSpeed*Time.deltaTime;
    }
}
