using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Controle : MonoBehaviour
{
    Vector3 direção; // nao precisa ser publica pq é o input
    public float velocidade;

    Rigidbody rb; // nao ta usando agr mas ta aqui pra poder adicionar outras opções de "movimento" no futuro, pulos etc.
    Renderer cor;

    bool estaverde = false;
    float tempoverde = 0;
    public float TempoVerdeLimite = 1f; // customizar o limite de tempo que fica verde

    public Material verde;
    public Material cororiginal;

    private void Awake() // daria na mesma que usar Start
    {
        rb = GetComponent<Rigidbody>();
        cor = GetComponent<Renderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        TemporizadorDeCor();
        Movimentação();
    }

    private void TemporizadorDeCor()
    {
        if (estaverde)
        {
            tempoverde += Time.deltaTime;
            if (tempoverde > TempoVerdeLimite)
            {
                MudaCor(cororiginal);
                tempoverde = 0; //reseta temporizador
            }
        }
    }

    private void Movimentação()
    {
        direção = new Vector3(Input.GetAxis("Horizontal"), 0, 0); // o input é cada toque nas setas do teclado ou o movimento analógico (controle de videogame)
        transform.Translate(direção * velocidade * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider outroObjeto)
    {
        if (outroObjeto.GetComponent<Vermelho>() != null)
        {
            Morre();
        }
        else if (outroObjeto.GetComponent<Verde>() != null)
        {
            if (estaverde)
            {
                Morre();
            }
            else
            {
                MudaCor(verde);
            }
        }
    }

    private void MudaCor(Material material)
    {
        cor.material = material;
        if (material == verde)
        {
            estaverde = true;
        }
        else
        {
            estaverde = false;
        }
    }

    private void Morre()
    {
        // Destroy(gameObject); - se usar destroy vai destruir a camera junto pois é filha do objeto
        cor.enabled = false;
        Time.timeScale = 0; // tempo pausado
    }
}
