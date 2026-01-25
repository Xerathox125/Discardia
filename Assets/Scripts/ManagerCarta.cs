using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ManagerCarta : MonoBehaviour
{
    [SerializeField] private GameObject _prefabCarta;
    [SerializeField] private Transform _contendorMano;
    [SerializeField] private Mano _mano;

    [Header("Ajustes Visuales")]
    [Range(-0, 4.5f)] public float _espaciado = 2.25f;
    [Range(-0.3f, 0.3f)] public float _intensidadCurva = 0.05f;
    [Range(-5f, 5f)] public float _intensidadRotacion = 0.6f;


    void Update()
    {
        // Esto permite que el abanico se actualice mientras mueves los sliders
        if (_contendorMano.childCount > 0)
        {
            ActualizarSeparacionDeMano();
        }        
    }

    public void DibujarManoInicial()
    {
        if (_mano == null) { Debug.LogError("No hay referencia a Mano"); return; }

        // Limpiar mano anterior si existe (opcional)
        foreach (Transform hijo in _contendorMano) Destroy(hijo.gameObject);


        for (int i = 0; i < _mano.CartaEnMano.Count; i++)
        {
            GameObject nuevaCartaGO = Instantiate(_prefabCarta, _contendorMano);     
            CartaVisual visual = nuevaCartaGO.GetComponent<CartaVisual>();

            if (visual != null)
            {
                visual.ConfigurarVisual(_mano.CartaEnMano[i]);
            }
        }
        ActualizarSeparacionDeMano();
    }

    public void ActualizarSeparacionDeMano()
    {
        int totalCartas = _contendorMano.childCount;
        if (totalCartas == 0) return;

        float anchoTotal = (totalCartas - 1) * _espaciado;
        float puntoInicioX = -anchoTotal / 2f;

        for (int i = 0; i < totalCartas; i++)
        {
            Transform carta = _contendorMano.GetChild(i);

            // 1. Cálculo de posición horizontal
            float posicionX = puntoInicioX + (i * _espaciado);

            // 2. Cálculo de la curva (Y) y rotación (Z)
            // Usamos Mathf.Abs para que las cartas de los extremos bajen
            float curvaY = -Mathf.Abs(posicionX) * _intensidadCurva;
            float rotacionZ = posicionX * -_intensidadRotacion;

            // 3. Aplicar Transformaciones
            carta.localPosition = new Vector3(posicionX, curvaY, 0);
            carta.localRotation = Quaternion.Euler(0, 0, rotacionZ);

            // 4. Ajuste de Capa para que la carta x+1 esté encima de la carta x (Sorting Order)
            SpriteRenderer sr = carta.GetComponentInChildren<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingOrder = i;
            }
        }
    }
}
