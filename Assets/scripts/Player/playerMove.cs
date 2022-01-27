using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    [Header("Atributos do Jogador")]
    [SerializeField] private float moveSpeed = 7.5f; // Velocidade
    [SerializeField] private float jumpForce = 60f;
    private bool isGround = true;
    private bool enemyHit = false;
    [HideInInspector] public bool OnUnderG = false; // Estar no Subterraneo
    private int p_Life = 2; // Vidas do Jogador
    
    //[SerializeField] private bool isRight = true;

    private Rigidbody2D rb;
    private CapsuleCollider2D CapsuleColl;
    private SpriteRenderer sprRender,LumpRender;
    private AvocadoThrow Skill_s;

    [Header("Others Objects")]
    [SerializeField] private Global_S global_s;

    void Awake()
    {
        p_Life = 2;
        rb = GetComponent<Rigidbody2D>();
        Skill_s = GetComponent<AvocadoThrow>();
        CapsuleColl = GetComponent<CapsuleCollider2D>();
        sprRender = GetComponentInChildren<SpriteRenderer>();
        LumpRender = gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
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
           case false: Walk_Surface();
                break;
            case true: Walk_UnderGround();
                break;
        }
    }

    public void Temporary_Break_W_Surface()
    {
        // Jogador sofre dano, Atrasar retormada aos Controles "Surface"
        enemyHit = true;
        StartCoroutine(PequenaPausa(0.5f)); // Metade de 1 segudo para "enemyHit" receber "False"
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    void Walk_Surface()
    {
        // Player Walk
        if (Input.GetAxisRaw("Horizontal") > 0 && !enemyHit && !Skill_s.DoingDash)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else if(Input.GetAxisRaw("Horizontal") < 0 && !enemyHit && !Skill_s.DoingDash)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        } else if(!enemyHit && !Skill_s.DoingDash)
        {
            
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        //transform.position += move * moveSpeed * Time.deltaTime;
    }
    void Jump()
    {
        // Player Jump ( Jump ficou melhor no Update )
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

    void Turn_Lump()
    {
        p_Life--;
        Skill_s.isLump = true; // Para de Atirar semente. ( Dash = true; )
        sprRender.enabled = false; // Render do Abacate
        LumpRender.enabled = true;
        CapsuleColl.offset = new Vector2(0.3f, -1.16f);
        CapsuleColl.size = new Vector2(0.62f, 1.16f);
        Invoke("Turn_Avocado", 10f); // Voltar ao Normal em 10 segundos
        global_s.StartLoad_Avocado(); // Os 10 segundos Visualmente, Hehehe.
    }
    void Turn_Avocado()
    {
        p_Life++; // Restituir Vida
        Skill_s.isLump = false;
        sprRender.enabled = true;
        LumpRender.enabled = false;
        CapsuleColl.offset = new Vector2(0.04f, -0.05f);
        CapsuleColl.size = new Vector2(1.2f, 3.27f);
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

        // Se gravidade Zero
        if (gravidade < 1)
        {
            StartCoroutine(PequenaPausa(0.15f));
            OnUnderG = true; // Jogador foi para o Solo
        } else {
            OnUnderG = false; // Voltou a Superficie
            rb.AddForce(new Vector2(0, 60), ForceMode2D.Impulse);
        }
    }

    public IEnumerator PequenaPausa(float TempoPausa)
    {
        yield return new WaitForSeconds(TempoPausa);
        rb.velocity = Vector2.zero;
        enemyHit = false;
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
        sprRender.color = new Color(0.94f, 0.3f, 0.26f); // Sprite em Vermelho | Abacate
        LumpRender.color = new Color(0.94f, 0.3f, 0.26f); // Sprite em Vermelho | Lump
        yield return new WaitForSeconds(0.45f);
        sprRender.color = new Color(1, 1, 1); // Sprite Normal
        LumpRender.color = new Color(1, 1, 1); // Sprite Normal
        Turn_Lump();

        // Jogador is Dead xD
        if (p_Life < 1)
        {
            global_s.StartCoroutine("Reset_Scene"); // Atrasar um pouco e Reiniciar Cena
            Destroy(gameObject); // Apagar jogador
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        
        if(other.gameObject.CompareTag("ground")) {
            isGround = false;
        }
    }
}
