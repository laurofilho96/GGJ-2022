using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lump : MonoBehaviour {

    public float throwFowardSpeed = 50;
    public float throwUpSpeed = 300;

    void Start() {
        
        GetComponent<Rigidbody2D>().AddForce(new Vector2(throwFowardSpeed, throwUpSpeed), ForceMode2D.Force);
    }

    private void Update() {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject.CompareTag("ground")) {
            Destroy(this.gameObject);
        }
    }
    
}