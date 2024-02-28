using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    SpriteRenderer sr;
    Animator anim;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInput.Instance.moveInput.x < 0)
            sr.flipX = true;
        else if (PlayerInput.Instance.moveInput.x > 0)
            sr.flipX = false;

        anim.SetFloat("speed", PlayerInput.Instance.moveInput.magnitude);
    }
}
