using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEne_S : MonoBehaviour
{
    [SerializeField] private GameObject Enemy_Obj;
    [SerializeField] private Transform Local1, Local2;
    private Transform L_Destino;
    [HideInInspector] public static bool call_NewEnemy = false;
    private int e_ID;
    private int LocalRadom;

    void Update()
    {
        LocalRadom = Random.Range(1, 4);

        if (call_NewEnemy)
        {
            call_NewEnemy = false;
            Invoke("Create_Enemy", 1f);
            //Create_Enemy();
        }
    }

    void Create_Enemy()
    {
        switch (LocalRadom)
        {
            case 1:
                L_Destino = Local1;
                break;
            case 2:
                L_Destino = Local2;
                break;
            case 3:
                L_Destino = Local1;
                break;
            case 4:
                L_Destino = Local2;
                break;
        }
        e_ID++;

        Instantiate(Enemy_Obj, L_Destino.position, Quaternion.identity);
        print("Inimigo: " + e_ID + " foi criado.");
    }
}
