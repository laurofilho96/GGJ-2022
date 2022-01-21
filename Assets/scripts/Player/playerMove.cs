using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{

    public float moveSpeed = 5;
    public bool isRight = true;
    public float jumpSpeed = 3;
    public bool isGround = true;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

     Walk();
     Jump();
    }

    void Walk() {

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        transform.position += move * moveSpeed * Time.deltaTime;

        if(Input.GetKeyDown ( KeyCode.X ) && isGround) {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Force);
        }
    }

    void Jump() {

        if(Input.GetKeyDown ( KeyCode.X ) && isGround) {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Force);
        }
    }

    void OnCollisionEnter2D(Collider2D other) {

        if(other.gameObject.CompareTag("ground")) {
            isGround = true;
        }
    }

    void OnCollisionStay2D(Collider2D other) {
        
        if(other.gameObject.CompareTag("ground")) {
            isGround = true;
        }
    }

    void OnCollisionExit2D(Collider2D other) {
        
        if(other.gameObject.CompareTag("ground")) {
            isGround = false;
        }
    }
}
