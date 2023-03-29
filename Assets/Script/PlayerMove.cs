using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator playerAnim;
    private Rigidbody2D playerRig;
    private SpriteRenderer srPlayer;
    private bool invencibility = false;
    public Transform groundCheck;
    public bool isGrounded = false;
    public float Speed;
    public float JumpForce;
    private float movement = 0.0f;
    public SpriteRenderer Sprite;
    private ControllerGame _control;
    public AudioSource fxGame;
    public AudioClip fxJump;
    public int vidas = 3;
    public Color hitColor;
    void Start()
    {

        playerAnim = GetComponent<Animator>();
        playerRig = GetComponent<Rigidbody2D>();
        srPlayer = GetComponent<SpriteRenderer>();
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

    void Hurt()
    {
        if (!invencibility)
        {
            invencibility = true;
            vidas--;
            StartCoroutine("Dano");
            _control.BarraVida(vidas);
        }
    }

    IEnumerator Dano()
    {
        srPlayer.color = hitColor;
        yield return new WaitForSeconds(0.1f);
        for (float i = 0; i<1; i += 0.1f)
        {
            srPlayer.enabled= false;
            yield return new WaitForSeconds(0.1f);
            srPlayer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        srPlayer.color= Color.white;
        invencibility = false;
    }

    void enemieHurt()
    {
        GameObject tempExplosao = Instantiate(_control.explosion, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), transform.localRotation);
        Destroy(tempExplosao, 0.5f);
        fxGame.PlayOneShot(_control.fxDestroyEnemie);
        playerRig.AddForce(new Vector2(0f, JumpForce * 1.5f), ForceMode2D.Impulse);
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
                enemieHurt();
                Destroy(collision.gameObject);
                break;
            default: 
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemie":
                Hurt();
                break;
            default:
                break;
        }
    }
}
