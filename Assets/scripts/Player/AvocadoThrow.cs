using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvocadoThrow : MonoBehaviour
{    
    public Transform lumpPrefab;

    void Update()
    {
        
        Throw();
    }

    void Throw() {

        if(Input.GetKeyDown( KeyCode.Z )) {
            Instantiate(lumpPrefab, transform.position, transform.rotation);
        }
    }
}
