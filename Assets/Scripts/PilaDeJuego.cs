using System.Collections.Generic;
using UnityEngine;

public class PilaDeJuego : MonoBehaviour
{
    [SerializeField] private GameObject _prefabCarta;
    [SerializeField] private Transform _contenedorPozo;
    [SerializeField] private float _offsetPozo = 0.05f;

    //a las cartas del centro lo llame pozo, cambiar si es mas facil de entender de otra manera
    private List<Carta> _pozo = new List<Carta>();
    public List<Carta> Pozo => _pozo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DibujarPozoVisual();
    }

    //funcion agregar carta, retorna bool para validacion en game container
    public bool AgregarCarta(Carta nuevaCarta)
    {
        Carta cartaSuperior = _pozo[_pozo.Count-1];
        if(nuevaCarta.Valor == cartaSuperior.Valor || nuevaCarta.Palo == cartaSuperior.Palo)
        {
            _pozo.Add(nuevaCarta);
            DibujarPozoVisual();
            Debug.Log($"Carta agregada: {nuevaCarta.Nombre} mismo palo o valor que {cartaSuperior.Nombre}");
            return true;
        }
        Debug.LogWarning($"Carta no agregada: {nuevaCarta.Nombre} no es del mismo palo o valor que {cartaSuperior.Nombre}");
        return false;
    }
    

    //Usando el dibujar visual de ManagerMazo como plantilla
    public void DibujarPozoVisual()
    {
        
        //foreach (Transform t in _contenedorMazo) Destroy(t.gameObject);
        // Limpiar visuales anteriores (evita acumulación)
        foreach (Transform hijo in _contenedorPozo)
        {
            Destroy(hijo.gameObject);
        }

        for (int i = 0; i < _pozo.Count; i++)
        {
            GameObject nuevaCarta = Instantiate(_prefabCarta, _contenedorPozo);

            nuevaCarta.transform.localPosition = new Vector3(0, i*_offsetPozo, 0);

            CartaVisual visual = nuevaCarta.GetComponent<CartaVisual>();
            if (visual != null)
            {
                visual.ConfigurarCarta(_pozo[i], true, 9);
            }


            SpriteRenderer sr = nuevaCarta.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingOrder = i; // i=0: atrás, i=max: adelante
            }
        }
    }
}
