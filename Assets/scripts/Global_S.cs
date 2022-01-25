using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Global_S : MonoBehaviour
{
    [Header("On HUD")]
    [SerializeField] private GameObject pause_screen;

    void Awake()
    {

    }

    public void Pausar_Jogo()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pause_screen.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pause_screen.SetActive(false);
        }
    }
    public void Fechar_Jogo()
    {
        Application.Quit();
    }

    public IEnumerator Reset_Scene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
    }
}
