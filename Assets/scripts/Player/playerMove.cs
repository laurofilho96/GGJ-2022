using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    [Header("Atributos do Jogador")]
    [SerializeField] private float moveSpeed = 7.5f; // Velocidade
    [SerializeField] private float jumpForce = 60f;
    private bool isGround = true;
    private bool OnUnderG = false; // Estar Subterraneo
    private int p_Life = 2; // Vidas do Jogador
    
    //[SerializeField] private bool isRight = true;

    private Rigidbody2D rb;
    private SpriteRenderer sprRender;

    [Header("Others Objects")]
    [SerializeField] private Global_S global_s;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprRender = GetComponentInChildren<SpriteRenderer>();
    }
    // Inverte o lado do Jogador ao virar - Flip
    void ReversePlayer_Side()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void Update()
    {
        ReversePlayer_Side();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            global_s.Pausar_Jogo();
        }

        if (!OnUnderG)
        {
            Jump();
        }
    }
    void FixedUpdate()
    {
        // Troca de Controles ( Fora da Terra - Dentro da Terra )
        switch (OnUnderG)
        {
           case false: Walk_Jump();
                break;
            case true: Walk_UnderGround();
                break;
        }
    }

    void Walk_Jump()
    {
        // Player Walk
        if (Input.GetAxis("Horizontal") > 0)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
       
        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        //transform.position += move * moveSpeed * Time.deltaTime;
    }
    void Jump()
    {
        // Player Jump
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    void Walk_UnderGround()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += move * moveSpeed * Time.deltaTime;
    }

    // ================= TRIGGER ================= 
    void OnTriggerEnter2D(Collider2D collTrigger)
    {
        // Empurrar o jogador ao tocar em "Cutucada"
        if (collTrigger.gameObject.tag == "Cutucada")
        {
            collTrigger.gameObject.GetComponentInParent<Enemy_S>().push_player();
            StartCoroutine("TomandoDano");
        }

        if (collTrigger.gameObject.tag == "UnderGround")
        {
            Sistema_UnderGround(0);
        }
    }

    void Sistema_UnderGround(int gravidade)
    {
        rb.gravityScale = gravidade;

        if (gravidade < 1)
        {
            //rb.velocity = Vector2.zero;
            StartCoroutine("PequenaPausa");
            //rb.AddForce(new Vector2(0, -30), ForceMode2D.Impulse);
            OnUnderG = true;
        } else {
            OnUnderG = false;
            rb.AddForce(new Vector2(0, 60), ForceMode2D.Impulse);
        }
    }

    IEnumerator PequenaPausa()
    {
        yield return new WaitForSeconds(0.15f);
        rb.velocity = Vector2.zero;
    }

    void OnTriggerExit2D(Collider2D collTrigger)
    {
        if (collTrigger.gameObject.tag == "UnderGround")
        {
            Sistema_UnderGround(3);
        }
    }

    // ================= COLISSION ================= 

    void OnCollisionEnter2D(Collision2D other) {

        if(other.gameObject.CompareTag("ground")) {
            isGround = true;
        }

        if (other.gameObject.CompareTag("EnemyBomb"))
        {
            rb.AddForce(new Vector2(0, 60 + 15), ForceMode2D.Impulse);
            StartCoroutine("TomandoDano");
        }
    }
    IEnumerator TomandoDano()
    {
        sprRender.color = new Color(0.94f, 0.3f, 0.26f); // Sprite em Vermelho
        yield return new WaitForSeconds(0.15f);
        sprRender.color = new Color(1, 1, 1); // Sprite Normal
        p_Life--;

        if (p_Life < 1)
        {
            global_s.StartCoroutine("Reset_Scene");
            Destroy(gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        
        if(other.gameObject.CompareTag("ground")) {
            isGround = false;
        }
    }
}
