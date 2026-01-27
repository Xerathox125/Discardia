using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Mazo : MonoBehaviour
{
    [SerializeField] private List<Carta> _cartasBase;
    [SerializeField] private GameObject _prefabMazo;
    [SerializeField] private Transform _contenedorMazo;
       
    private Stack<Carta> _pilaDeCartas = new Stack<Carta>();
   
    private void Awake()
    {
        PrepararMazo();
    }

    public void PrepararMazo()
    {
        List<Carta> listaTemporal = new List<Carta>(_cartasBase);

        for (int i = 0; i < listaTemporal.Count; i++ )
        {
            Carta temp = listaTemporal[i];
            int randomIndex = Random.Range(i, listaTemporal.Count);
            listaTemporal[i] = listaTemporal[randomIndex];
            listaTemporal[randomIndex] = temp;
        }
        _pilaDeCartas.Clear();

        foreach (Carta c in listaTemporal)
        {
            _pilaDeCartas.Push(c);
        }

        Debug.Log($"Mazo listo con {_pilaDeCartas.Count} cartas.");
    }

    public Carta RobarCartaSuperior()
    {
        if (_pilaDeCartas.Count > 0)
        {
            return _pilaDeCartas.Pop();
        }
        Debug.LogWarning("El mazo est� vac�o.");
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

    //Get para conocer cuantas cartas hay en el mazo
     public int Count => _pilaDeCartas.Count; 
}
