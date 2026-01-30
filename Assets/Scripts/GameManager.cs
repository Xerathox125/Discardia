using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Propiedades")]
    [SerializeField] private Transform _contenedorCartasJuego;

    [Header("Referencias a Scripts")]    
    [SerializeField] private Mazo _mazo;
    [SerializeField] private ManagerCarta _manejadorMano;
    [SerializeField] private ManagerMazo _manejadorMazo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        
        StartCoroutine(EsperarYDibujar());

        EmpezarJuegoCartaRandom();
    }

    IEnumerator EsperarYDibujar()
    {
        yield return new WaitForEndOfFrame();
        _manejadorMano.DibujarManoInicial();
        _manejadorMazo.DibujarMazoVisual();
    }




    private void EmpezarJuegoCartaRandom()
    {
        if (_manejadorMazo == null) return ;

        int randomCard = Random.Range(0, _manejadorMazo.MazoLogica.MazoBase.Count);
        DibujarCartasTablero(_manejadorMazo.MazoLogica.MazoBase[randomCard]);      
        _manejadorMazo.MazoLogica.MazoBase.RemoveAt(randomCard);

    }

    private GameObject DibujarCartasTablero(Carta carta)
    {
        GameObject nuevaCartaGO = Instantiate(_manejadorMano.PrefabCarta, _contenedorCartasJuego);
        CartaVisual visual = nuevaCartaGO.GetComponent<CartaVisual>();
        if (visual != null)
        {
            visual.ConfigurarCarta(carta, true, 9);
        }

        return nuevaCartaGO;

    }

    private GameObject EnviarCartaAlJuego()
    {
        //if (_manejadorMano == null) return null;


        return null;
    }

 



}
