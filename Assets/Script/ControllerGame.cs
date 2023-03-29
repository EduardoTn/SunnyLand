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
    public GameObject explosion;
    public AudioClip fxDestroyEnemie;
    public Sprite[] lifes;
    public Image actualLife;
    public void Pontuacao(int qtdPontos)
    {
        Score += qtdPontos;
        txtScore.text = Score.ToString();
        fxGame.PlayOneShot(fxCenoura);
    }

    public void BarraVida(int life)
    {
        actualLife.sprite = lifes[life];
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
