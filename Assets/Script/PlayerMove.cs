using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator playerAnim;
    private Rigidbody2D playerRig;
    public Transform groundCheck;
    public bool isGrounded = false;
    public float Speed;
    public float JumpForce;
    private float movement = 0.0f;
    public SpriteRenderer Sprite;
    private ControllerGame _control;
    public AudioSource fxGame;
    public AudioClip fxJump;
    public AudioClip fxDestroyEnemie;
    void Start()
    {

        playerAnim = GetComponent<Animator>();
        playerRig = GetComponent<Rigidbody2D>();
        _control = FindObjectOfType<ControllerGame>();
    }
    void Update()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        Walk();
        Jump();
    }

    void Walk()
    {
        movement = Input.GetAxis("Horizontal");
        playerRig.velocity = new Vector2(movement * Speed, playerRig.velocity.y);
        if (movement > 0f)
        {
            Sprite.flipX = false;
        }
        else if (movement < 0f)
        {
            Sprite.flipX = true;
        }
        setBoolAnimation("Walk", movement != 0f);
    }

    void Jump()
    {
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        setBoolAnimation("isGrounded", isGrounded);
        setFloatAnimation("eixoY", playerRig.velocity.y);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            fxGame.PlayOneShot(fxJump);
        }
    }

    void setBoolAnimation(string Animation, bool Validation)
    {
        playerAnim.SetBool(Animation, Validation);
    }

    void setFloatAnimation(string Animation, float Validation)
    {
        playerAnim.SetFloat(Animation, Validation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Coletaveis":
                _control.Pontuacao(1);
                Destroy(collision.gameObject);
                break;
            case "Enemie":
                Destroy(collision.gameObject);
                playerRig.AddForce(new Vector2(0f, JumpForce*1.8f), ForceMode2D.Impulse);
                fxGame.PlayOneShot(fxDestroyEnemie);
                break;
            default: 
                break;
        }
    }
}
