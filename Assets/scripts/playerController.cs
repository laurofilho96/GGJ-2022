using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{

    public float moveSpeed = 5;
    public bool isRight = true;
    public float jumpSpeed = 3;
    public bool isGround = true;

    void Update()
    {

     Walk();   
    }

    void Walk() {

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        transform.position += move * moveSpeed * Time.deltaTime;

        if(Input.GetKeyDown ( KeyCode.X ) && isGround) {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Force);
        }
    }

    void OnTriggerEnter2D() {

        isGround = true;
    }

    void OnTriggerStay2D() {
        
        isGround = true;
    }

    void OnTriggerExit2D() {
        
        isGround = false;
    }
}
