using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneSoda_S : MonoBehaviour
{
    [Header("Atributos do Inimigo")]
    [SerializeField] private float e_Life = 100;
    [SerializeField] private float e_speed = 3f;
    [SerializeField] private float e_impulseJump = 50f;

    [Header("Atacando o Jogador")]
    [SerializeField] private GameObject player_obj;
    [SerializeField] private float forceImpulse;

    private Rigidbody2D rb;
    private SpriteRenderer SprRender;
    private int GoingTo_Right = 1; // -1 == LEFT | RIGHT == 1
    private bool canJump = true;
    private bool sendoAtacado = false;
    [HideInInspector] public bool Atacando = false;

    void Awake()
    {
        transform.localRotation = Quaternion.Euler(0, 180, 0);

        if (player_obj == null)
        {
            player_obj = GameObject.FindGameObjectWithTag("Player");
        }
        rb = GetComponent<Rigidbody2D>();
        SprRender = GetComponentInChildren<SpriteRenderer>();
        Physics2D.IgnoreLayerCollision(6, 6);
    }
    void FixedUpdate()
    {
        if (!Atacando)
        {
            enemy_walk();
        }
    }

    void enemy_walk()
    {
        rb.velocity = new Vector2(e_speed * GoingTo_Right, rb.velocity.y);
    }
    public void Verificar_PosPlayer()
    {
        if (player_obj.transform.position.x > transform.position.x)
        {
            GoingTo_Right = 1;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (player_obj.transform.position.x < transform.position.x)
        {
            GoingTo_Right = -1;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public void push_player()
    {
        if (transform.position.x > player_obj.transform.position.x)
        {
            player_obj.GetComponent<playerMove>().Temporary_Break_W_Surface(); // Travar controles do Jogador temporariamente
            player_obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(-forceImpulse, 0), ForceMode2D.Impulse);

            //GoingTo_Right = 1;
            //transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (transform.position.x < player_obj.transform.position.x)
        {
            //GoingTo_Right = -1;
            //transform.localRotation = Quaternion.Euler(0, 180, 0);

            player_obj.GetComponent<playerMove>().Temporary_Break_W_Surface();
            player_obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(forceImpulse, 0), ForceMode2D.Impulse);

        }
    }
    // XXXXXxxxxx TRIGGERs xxxxxXXXXX
    void OnTriggerEnter2D(Collider2D coll)
    {
        // Direcoes
        if (coll.gameObject.tag == "GoToRight")
        {
            GoingTo_Right = 1;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (coll.gameObject.tag == "GoToLeft")
        {
            GoingTo_Right = -1;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (coll.gameObject.tag == "GoJump")
        {
            rb.AddForce(Vector2.up * e_impulseJump, ForceMode2D.Impulse);
        }
        // Danos do Jogador
        if (coll.gameObject.tag == "PlayerLump")
        {
            Destroy(coll.gameObject);
            e_Life -= 27;
            if (e_Life < 1)
            {
                SpawnEne_S.call_NewEnemy = true;
                Destroy(gameObject);
            }
            StartCoroutine("PequenaPausa");
        }
        if (coll.gameObject.tag == "DanoDash")
        {
            Atacando = true;
            if (coll.gameObject.transform.position.x < transform.position.x)
            {
                rb.AddForce(new Vector2(55, 0), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(-55, 0), ForceMode2D.Impulse);
            }

            e_Life -= 27;
            if (e_Life < 1)
            {
                Destroy(gameObject);
            }

            StartCoroutine(PequenaPausa());
        }
        // Other System
    }

    IEnumerator PequenaPausa()
    {
        SprRender.color = new Color(0.94f, 0.3f, 0.26f);
        yield return new WaitForSeconds(0.5f);
        SprRender.color = new Color(1, 1, 1);
        Atacando = false;
    }
}