using System.Collections.Generic;
using UnityEngine;

public class ManagerJuegoCartasa : MonoBehaviour
{
    private List<Carta> cartaList = new List<Carta>();


    [Header("Referencias a Scripts")]
    [SerializeField] private Mazo _mazo;
    [SerializeField] private ManagerCarta _manejadorMano;
    [SerializeField] private GameManager _gameManager;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
