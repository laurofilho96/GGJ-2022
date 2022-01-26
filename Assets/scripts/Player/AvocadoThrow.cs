using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvocadoThrow : MonoBehaviour
{
    [SerializeField] private GameObject lumpPrefab;
    [SerializeField] private float forceDash;
    [HideInInspector] public bool isLump = false, DoingDash = false;
    private int ImpulseDirection;
    [Header("Lump")]
    [SerializeField] private BoxCollider2D BoxDash;


void Update()
    {
        if (!isLump)
        {
            Throw();
        } else
        {
            Dash();
        }
    }

    void Throw() {

        if(Input.GetKeyDown( KeyCode.Z )  || Input.GetKeyDown(KeyCode.J)) {
            Instantiate(lumpPrefab, transform.position, transform.rotation);
        }
    }
    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.J))
        {
            DoingDash = true;
            StartCoroutine(PequenaPausa(0.3f));

            if (transform.rotation.y < 0)
            {
                ImpulseDirection = -1;
            } else ImpulseDirection = 1;

            GetComponent<Rigidbody2D>().AddForce(new Vector2(forceDash * ImpulseDirection, 0), ForceMode2D.Impulse);
            BoxDash.enabled = true;
        }
    }

    public IEnumerator PequenaPausa(float TempoPausa)
    {
        yield return new WaitForSeconds(TempoPausa);
        BoxDash.enabled = false;
        DoingDash = false;
    }
}
