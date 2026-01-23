using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ManagerCarta : MonoBehaviour
{
    [SerializeField] private GameObject _prefabCarta;
    [SerializeField] private Transform _contendorMano;
    [SerializeField] private Mano _mano;

     private float _espaciado = 3.5f;


    public void DibujarManoInicial()
    {
        if (_mano == null) { Debug.LogError("No hay referencia a Mano"); return; }

        int totalCartas = _mano.CartaEnMano.Count;
        float anchoTotal = (totalCartas - 1) * _espaciado;
        float puntoInicioX = -anchoTotal / 2f;

        for (int i = 0; i < totalCartas; i++)
        {
            GameObject nuevaCartaGO = Instantiate(_prefabCarta, _contendorMano);

            float posicionX = puntoInicioX + (i * _espaciado);

            nuevaCartaGO.transform.localPosition = new Vector3(posicionX, 0, 0);

            CartaVisual visual = nuevaCartaGO.GetComponent<CartaVisual>();

            if (visual != null)
            {
                visual.ConfigurarVisual(_mano.CartaEnMano[i]);
            }
        }
    }
}
