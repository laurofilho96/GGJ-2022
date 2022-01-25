using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly_S : MonoBehaviour
{
    [Header("Atributos do Inimigo")]
    [SerializeField] private float e_speed = 3f;
    [SerializeField] private float e_impulseJump = 50f;

    private Rigidbody2D rb;
    private int GoingTo_Right = 1; // -1 == LEFT | RIGHT == 1
    private bool SoltarSemente = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(6, 6); // Camada dos Inimigos
    }
    void FixedUpdate()
    {
            enemy_fly();
    }

    void enemy_fly()
    {
        rb.velocity = new Vector2(e_speed * GoingTo_Right, rb.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
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
    }

}
