using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Mazo _mazo;
    [SerializeField] private ManagerCarta _manejadorMano;
    [SerializeField] private ManagerMazo _manejadorMazo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        
        StartCoroutine(EsperarYDibujar());
    }

    IEnumerator EsperarYDibujar()
    {
        yield return new WaitForEndOfFrame();
        _manejadorMano.DibujarManoInicial();
        _manejadorMazo.DibujarMazoVisual();
    }
}
