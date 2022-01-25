using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvocadoThrow : MonoBehaviour
{    
    public GameObject lumpPrefab;

    void Update()
    {
        
        Throw();
    }

    void Throw() {

        if(Input.GetKeyDown( KeyCode.Z )  || Input.GetKeyDown(KeyCode.J)) {
            Instantiate(lumpPrefab, transform.position, transform.rotation);
        }
    }
}
