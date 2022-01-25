using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetect_S : MonoBehaviour
{
    [SerializeField] private Enemy_S enemy_s;
    [SerializeField] private GameObject player_obj;
    [SerializeField] private float forceImpulse;

    [SerializeField] private Animator anim;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            anim.Play("EnemyLanca_AttackT");
        }
    }
}
