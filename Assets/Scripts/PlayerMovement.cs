using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //public PlayerController controller;
    [SerializeField] float horizontalMove = 0f;
    private SpriteRenderer _spriteRenderer;
    private Animator _anim;
    float _speed = 8f;
    private Vector3 _direction;


    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
        {
            Debug.LogError("PlayerMovement:: SpriteRenderer is null");
        }
        //
        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("PlayerMovement:: Animator is null");
        }
        //--
        _anim.SetBool("IsWalking", false);
        //
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * _speed;

        if (horizontalMove == 0)
        {
            _anim.SetBool("IsWalking", false);
        }
        else if (horizontalMove > 1)
        {
            _spriteRenderer.flipX = false;
            _anim.SetBool("IsWalking", true);
        }

        else if (horizontalMove < 1)
        {
            _spriteRenderer.flipX = true;
            _anim.SetBool("IsWalking", true);
        }
    }
    private void FixedUpdate()
    {
        //move them the same amount each time
        PlayerMove();
    }

    private void PlayerMove()
    {
        _direction = new Vector3(horizontalMove, 0, transform.position.z);
        transform.Translate(_direction * Time.fixedDeltaTime);
    }

}
