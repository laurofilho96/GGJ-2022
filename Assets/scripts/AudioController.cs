using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Jogador Audio Clips")]
    [SerializeField] private AudioClip[] AtirarCaroco;
    [SerializeField] private AudioClip[] CarocoDash;
    [SerializeField] private AudioClip[] PuloJogador;
    [SerializeField] private AudioClip[] MorteJogador;
    [SerializeField] private AudioClip[] CaixaQuebrando;
    [SerializeField] private AudioClip impactoClip;

    private AudioSource au;
    void Awake()
    {
        au = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public void Atirando_Caroco()
    {
        int variaClip = Random.Range(0, 6);
        au.PlayOneShot(AtirarCaroco[variaClip]);
    }
    public void Caroco_Dash()
    {
        int variaClip = Random.Range(0, 7);
        au.PlayOneShot(CarocoDash[variaClip]);
    }
    public void Jogador_Pulando()
    {
        int variaClip = Random.Range(0, 7);
        au.PlayOneShot(PuloJogador[variaClip]);

    }
    public void Jogador_Morte()
    {
        int variaClip = Random.Range(0, 4);
        au.PlayOneShot(MorteJogador[variaClip]);
    }
    public void Impacto()
    {
        //int variaClip = Random.Range(0, 4);
        au.PlayOneShot(impactoClip);
    }
    public void Caixa_Quebrando()
    {
        int variaClip = Random.Range(0, 3);
        au.PlayOneShot(CaixaQuebrando[variaClip]);
    }
}
