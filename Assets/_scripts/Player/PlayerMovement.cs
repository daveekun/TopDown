using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region references
    Rigidbody2D rb;
    PlayerInput pInput;
    Vector3     move;
    [SerializeField] Transform feetpos;
    #endregion

    public LayerMask wall;

    #region speeds
    public float movespeed;
    public float sprintspeed;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pInput = PlayerInput.Instance;
    }

    void FixedUpdate()
    {
        move = pInput.moveInput;
        if (pInput.isSprinting)
        {
            if (Physics2D.Raycast(feetpos.position, Vector2.right * move.x, sprintspeed * Time.fixedDeltaTime, wall) == true)
                move.x = 0;
            if (Physics2D.Raycast(feetpos.position, Vector2.up * move.y, sprintspeed * Time.fixedDeltaTime, wall) == true)
                move.y = 0;
            transform.position += move * sprintspeed * Time.fixedDeltaTime;
        }
        else
        {
            if (Physics2D.Raycast(feetpos.position, Vector2.right * move.x, movespeed * Time.fixedDeltaTime, wall) == true)
                move.x = 0;
            if (Physics2D.Raycast(feetpos.position, Vector2.up * move.y, movespeed * Time.fixedDeltaTime, wall) == true)
                move.y = 0;
            transform.position += move * movespeed * Time.fixedDeltaTime;
        }
    }
}
