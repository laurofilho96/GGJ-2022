using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] // Iniciar Script no modo Editor

public class OrganizaPrefab_S : MonoBehaviour
{
    [Header("Aonde vai ficar o Objeto")]
    [SerializeField] private string TargetName; // Nome aonde vai

    void Start()
    {
        this.transform.parent = GameObject.Find(TargetName).transform; // Ser filho
        DestroyImmediate(this); // Apagar esse Script
    }
}
