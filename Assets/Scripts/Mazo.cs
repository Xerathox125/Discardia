using System;
using System.Collections.Generic;
using UnityEngine;

public class Mazo : MonoBehaviour
{
    [SerializeField] private List<Carta> _cartasBase;
    [SerializeField] private GameObject _prefabMazo;
    [SerializeField] private Transform _contenedorMazo;

    public event Action OnMazoChanged;


    private void Awake()
    {
        PrepararMazo();
    }

    public void PrepararMazo()
    {
        for (int i = 0; i < _cartasBase.Count; i++ )
        {
            int randomIndex = UnityEngine.Random.Range(i, _cartasBase.Count);
            
            Carta temp = _cartasBase[i];
            _cartasBase[i] = _cartasBase[randomIndex];
            _cartasBase[randomIndex] = temp;
        }
    }

    public Carta RobarCartaSuperior()
    {
        if (_cartasBase.Count > 0)
        {
            int ultimoIndice = _cartasBase.Count - 1;
            Carta cartaSeleccionada = _cartasBase[ultimoIndice];

            _cartasBase.RemoveAt(ultimoIndice);
            OnMazoChanged?.Invoke();

            return cartaSeleccionada;

        }
        Debug.LogWarning("El mazo esta vacio.");
        return null;
    }

    public List<Carta> GenerarMano(int cantidad)
    {
        List<Carta> nuevMano = new List<Carta>();
        for(int i = 0; i < cantidad; i++)
        {
            Carta c = RobarCartaSuperior();

            if (c != null) nuevMano.Add(c);
        }
        return nuevMano;
    }

    public bool QuitarCartaEspecifica(Carta cartaAQuitar)
    {
        if (_cartasBase.Contains(cartaAQuitar))
        {
            _cartasBase.Remove(cartaAQuitar);
            Debug.Log($"Carta {cartaAQuitar.name} eliminada selectivamente.");
            OnMazoChanged?.Invoke();
            return true;
        }
        return false;
    }


    //Get para conocer cuantas cartas hay en el mazo
    public int Count => _cartasBase.Count;
    public List<Carta> MazoBase => _cartasBase;
}
