using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectSoda_S : MonoBehaviour
{
    [SerializeField] private GameObject LiquidSoda;
    [SerializeField] private Vector2 localInstance;

    //[SerializeField] private Enemy_S enemy_s;
    //[SerializeField] private GameObject player_obj;
    //[SerializeField] private float forceImpulse;

    //[SerializeField] private Animator anim;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Instantiate(LiquidSoda, localInstance, Quaternion.identity);
        }
    }
}
