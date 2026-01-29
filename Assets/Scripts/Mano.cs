using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mano : MonoBehaviour
{
    [SerializeField] private Mazo _mazoReferencia;
    [SerializeField] private ManagerCarta _manejadorCarta;
    [SerializeField] private List<Carta> _cartasEnMano = new List<Carta>();

    public List<Carta> CartaEnMano => _cartasEnMano;


    void Start()
    {
        if (_mazoReferencia != null)
        {
            _cartasEnMano = _mazoReferencia.GenerarMano(12);
            print($"Mano creada con {_cartasEnMano.Count} cartas." );
        }
    }

    private void Update()
    {
        if (Mouse.current == null) return;
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            SelecionarCartaAlJuego();
        }
    }

    public void PedirCartaAlMazo()
    {
        if (_mazoReferencia != null)
        {
            Carta nueva = _mazoReferencia.RobarCartaSuperior();
            if (nueva != null)
            {
                _cartasEnMano.Add(nueva);
                Debug.Log($"Robaste la carta: {nueva.Nombre}");
            }
        }
        else
            return;
    }


    public void SelecionarCartaAlJuego()
    {
        GameObject nueva = _manejadorCarta.SeleccionarCartaDeMano();

        if (nueva != null)
        {
            Debug.Log(nueva.name );
        }

    }

}
