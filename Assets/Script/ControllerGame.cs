using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerGame : MonoBehaviour
{

    private int Score;
    public Text txtScore;
    public AudioSource fxGame;
    public AudioClip fxCenoura;
    public void Pontuacao(int qtdPontos)
    {
        Score += qtdPontos;
        txtScore.text = Score.ToString();
        fxGame.PlayOneShot(fxCenoura);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
