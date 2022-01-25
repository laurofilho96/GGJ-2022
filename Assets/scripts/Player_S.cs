using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_S : MonoBehaviour
{
    [SerializeField] private float p_speed;
    [SerializeField] private float ImpulseJump;

    private bool noChao = true;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(Input.GetAxis("Horizontal") != 0)
        {
            rb.velocity = new Vector2(p_speed * Input.GetAxis("Horizontal"), rb.velocity.y);
        }
        if (Input.GetButton("Jump") && noChao == true)
        {
            print("PULANDO !!!");
            rb.AddForce(Vector2.up * ImpulseJump, ForceMode2D.Impulse);
            noChao = false;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            Debug.LogWarning("TOMANDO DANO");
        }
        if (coll.gameObject.tag == "OnChao")
        {
            noChao = true;
            Debug.LogWarning("ON CHAO");
        }
    }
    /*
    void OnCollisionExit2D(Collision2D coll_Saida)
    {
        if (coll.gameObject.tag == "Enemy")
        {

        }
    }
    */
}
