using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lump : MonoBehaviour {

    public float throwFowardSpeed = 50;
    public float throwUpSpeed = 300;
    private float ImpulseDirection = 1;

    private Transform player_transf;

    private void Awake()
    {
        player_transf = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start() {
        if (player_transf.rotation.y < 0){
            ImpulseDirection = -1;
        }
        Destroy(gameObject, 5f);

        GetComponent<Rigidbody2D>().AddForce(new Vector2(throwFowardSpeed * ImpulseDirection, throwUpSpeed), ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject.CompareTag("ground")) {
            Destroy(this.gameObject);
        }

    }
    
}