using System.Collections.Generic;
using UnityEngine;

public class Mano : MonoBehaviour
{
    [SerializeField] private Mazo _mazoReferencia;
    [SerializeField] private List<Carta> _cartasEnMano = new List<Carta>();

    public List<Carta> CartaEnMano => _cartasEnMano;


    void Start()
    {
        if (_mazoReferencia != null)
        {
            _cartasEnMano = _mazoReferencia.GenerarManoInicial(12);
            print($"Mano creada con {_cartasEnMano.Count} cartas." );
        }
    }

    public void PedirCartaAlMazo()
    {
        Carta nueva = _mazoReferencia.RobarCartaSuperior();
        if (nueva != null)
        {
            _cartasEnMano.Add(nueva);
            Debug.Log($"Robaste la carta: {nueva.Nombre}");
        }
    }

}
