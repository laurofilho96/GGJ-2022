using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnePatrulha_Cutucada_S : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollDano;
    [SerializeField] private Enemy_S enemy_s;
    private int toma = 0;
    //Ativar Trigger para chamar "Cutucada"
    void Toma_Cutucada()
    {
        toma++;

        if(toma == 1)
        {
            enemy_s.Atacando = true;
            boxCollDano.enabled = true;
        } else {
            enemy_s.Atacando = false;
            boxCollDano.enabled = false;
            toma = 0;
            
        }
    }
}
