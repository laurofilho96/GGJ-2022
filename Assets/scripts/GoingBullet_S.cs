using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingBullet_S : MonoBehaviour
{
    [SerializeField] private Vector2 DiretionToGo;
    [SerializeField] private float SpeedGoing;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = DiretionToGo * SpeedGoing;
    }
}
