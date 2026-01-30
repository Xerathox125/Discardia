using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class MazoPooler : MonoBehaviour, IClickable
{
    [Header("Referencias")]
    [SerializeField] private CartaVisual _prefabCarta;
    [SerializeField] private List<Carta> _baseDeDatosCartas;

    [Header("Configuración")]
    [SerializeField] private Transform puntoGeneracion;

    private Stack<Carta> _pilaDeCartas = new Stack<Carta>();
    private IObjectPool<CartaVisual> _pool;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _pool = new ObjectPool<CartaVisual>(
            createFunc: () => Instantiate(_prefabCarta, puntoGeneracion),
            actionOnGet: (carta) => carta.gameObject.SetActive(true),
            actionOnRelease: (carta) => {
                carta.gameObject.SetActive(false);
                carta.transform.SetParent(puntoGeneracion);
                },
            defaultCapacity: 52,
            maxSize: 100
         );
    }


    public void InicializarMazo()
    {
        if (_pilaDeCartas.Count == 0)
        {
            BarajarYPreparar();
            int i = 0;
            foreach (Carta data in _pilaDeCartas)
            {
                CartaVisual nuevaCarta = _pool.Get();
                nuevaCarta.transform.SetParent(puntoGeneracion);
                nuevaCarta.ConfigurarCarta(data, false, 7);
                nuevaCarta.transform.localPosition = new Vector3(0, i * 0.010f, 0);
                i++;
            }
        }
        


    }



    public void BarajarYPreparar()
    {
        List<Carta> listaTemporal = new List<Carta>(_baseDeDatosCartas);

        for (int i = 0; i < listaTemporal.Count; i++)
        {
            Carta temp = listaTemporal[i];
            int randomIndex = Random.Range(i, listaTemporal.Count);
            listaTemporal[i] = listaTemporal[randomIndex];
            listaTemporal[randomIndex] = temp;
        }

        _pilaDeCartas.Clear();
        foreach (var carta in listaTemporal) _pilaDeCartas.Push(carta);
    }

    public CartaVisual DarCartaFisicaDelMazo(Transform nuevoPadre, bool mostrarFrente)
    {
        if (puntoGeneracion.transform.childCount == 0) return null;

        Transform ultimaCartaTransform = puntoGeneracion.transform.GetChild(puntoGeneracion.transform.childCount - 1);
        CartaVisual cartaAEntregar = ultimaCartaTransform.GetComponent<CartaVisual>();

        cartaAEntregar.transform.SetParent(nuevoPadre, false);
        cartaAEntregar.ConfigurarCarta(cartaAEntregar.DataActual, mostrarFrente, 8);

        return cartaAEntregar;
    }



    public void OnClick()
    {
        Debug.Log("Mazo clickeado, enviando carta a la mano...");
        // Aquí podrías llamar al ManoManager.PedirCartaAlMazo()
    }
}
