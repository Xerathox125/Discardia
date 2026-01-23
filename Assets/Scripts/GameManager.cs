using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Mazo _mazo;
    [SerializeField] private ManagerCarta _manejadorMano;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 2. Le decimos a la mano que pida sus cartas
        _manejadorMano.DibujarManoInicial();
    }

    
}
