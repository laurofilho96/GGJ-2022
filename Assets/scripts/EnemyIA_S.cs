using UnityEngine;

public class EnemyIA_S : MonoBehaviour
{
    void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
