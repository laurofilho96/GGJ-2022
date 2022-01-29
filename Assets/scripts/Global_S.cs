using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Global_S : MonoBehaviour
{
    //[Header("On HUD")]
    [Header("TEMPO DE JOGO:      X = Minutos | Segundos = Y")]
    [SerializeField] private Vector2 timeCounter; // X = Minutos | Segundos = Y
    [SerializeField] private Text ScreenTime;
    [Header("On HUD")]
    [SerializeField] private GameObject pause_screen;
    [SerializeField] private Image BarTurnAvocado;

    private float timeTurnAvoc = 1f;

    void Awake()
    {
        timeCounter.x = 2;
    }
    void Time_Counter()
    {
        timeCounter.y -= Time.deltaTime; // Diminuir segundos
        if (timeCounter.y < 0) { timeCounter.y = 59; timeCounter.x--; } // Apos segundos chegar a 0, tira 1 de minutos

        if (timeCounter.x >= 0) // Enquanto minutos não for menor que zero continue mostrando na tela
        {
            ScreenTime.text = timeCounter.x + ":" + Mathf.Round(timeCounter.y);
        }

    }

    void Update()
    {
        
        Time_Counter();
        TimeTurn_Avocado();
    }

    public void StartLoad_Avocado()
    {
        timeTurnAvoc = 0;
    }
    void TimeTurn_Avocado()
    {
        timeTurnAvoc += 0.1f * Time.deltaTime;
        if(timeTurnAvoc < 1.0f)
        {
            BarTurnAvocado.fillAmount = timeTurnAvoc;
        }
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
    public void Restart_scene()
    {
        SceneManager.LoadScene(0);
    }

    public IEnumerator Reset_Scene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
    }
}
