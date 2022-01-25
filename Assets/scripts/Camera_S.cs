using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_S : MonoBehaviour
{
    [Header("Propriedades da Camera")]
    [Space(5)]
    [SerializeField] private Vector2 maisDistancia;
    [Space(5)]
    [SerializeField] [Range(0f, 1f)] private float Delay_x = 0.5f, Delay_y = 0.1f;

    [Header("Target Objects")]
    [SerializeField] private Transform targetCam;
    [SerializeField] private Transform targetPlayer;

    private float GoingToX, GoingToY;
    private float delayParaCamera;

    void FixedUpdate()
    {
        if (targetPlayer != null)
        {
            seguindo_jogador();
        }
    }

    void seguindo_jogador()
    {
        GoingToX = Mathf.Lerp(targetCam.position.x, targetPlayer.position.x + maisDistancia.x, Delay_x);
        GoingToY = Mathf.Lerp(targetCam.position.y, targetPlayer.position.y + maisDistancia.y, Delay_y);

        targetCam.position = new Vector3(GoingToX, GoingToY, -10);
    }
}
