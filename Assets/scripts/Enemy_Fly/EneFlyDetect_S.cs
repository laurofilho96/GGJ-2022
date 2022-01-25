using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneFlyDetect_S : MonoBehaviour
{
    [SerializeField] private GameObject Bombinha;
    private bool canDrop = true;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player" && canDrop)
        {
            canDrop = false;
            Instantiate(Bombinha, transform.position, Quaternion.identity);
            StartCoroutine("PequenaPausa");
        }
    }

    IEnumerator PequenaPausa() {
        yield return new WaitForSeconds(1f);
        canDrop = true;
    }
}
