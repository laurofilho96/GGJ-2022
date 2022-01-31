using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateForever : MonoBehaviour
{
    [SerializeField] private float Velocidade;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(0, 0, Velocidade * Time.deltaTime);
    }
}
