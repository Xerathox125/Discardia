using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class ManagerCarta : MonoBehaviour
{
    [SerializeField] private GameObject _prefabCarta;
    [SerializeField] private Transform _contenedorMano;
    [SerializeField] private Mano _manoReferencia;

    [Header("Ajustes Visuales")]
    //Ancho Fijo en public para cambiarlo facile en Unity
    [SerializeField] public float _anchoManoMax = 15f;
    //[Range(-0, 4.5f)] public float _espaciado = 2.25f;
    [Range(-0.3f, 0.3f)] public float _intensidadCurva = 0.05f;
    [Range(-5f, 5f)] public float _intensidadRotacion = 0.6f;


    public GameObject PrefabCarta => _prefabCarta;


    void Update()
    {
        //// Esto permite que el abanico se actualice mientras mueves los sliders
        //if (_contenedorMano.childCount > 0)
        //{

        // ActualizarSeparacionDeMano();

        //}      

    }

    private void OnValidate()
    {
        ActualizarSeparacionDeMano();
    }

    // AQUI ESCOGE EL OBJECTO, PUEDES INTENTAR EN VIAR EL OBJECT O VER SI ES MEJOR OBTENER INFOMRACION DE LA CLASE CARTA.
    public Carta SeleccionarCartaDeMano()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            //RaycastAll porque ayuda con el overlap
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos,Vector2.zero,Mathf.Infinity, 1 << 8);
            //RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, 1 << 8);

            if (hits.Length > 0)
            {
                //Se clasifica segun la distancia con el click comparando ambas y buscando el mas gameobject mas cercano
                System.Array.Sort(hits, (a, b) => 
                b.transform.GetComponent<SpriteRenderer>().sortingOrder.CompareTo(a.transform.GetComponent<SpriteRenderer>().sortingOrder)
                );
                CartaVisual visual = hits[0].collider.gameObject.GetComponent<CartaVisual>();
                if (visual != null && visual.Data != null)
                {
                    return visual.Data;
                }
            }
            /*if (hit)
            {
                Debug.Log("CLICK DETECTADO en Mano!");
                CartaVisual visual = hit.collider.gameObject.GetComponent<CartaVisual>();
                if (visual != null && visual.Data != null)
                {
                    return visual.Data;
                }
                //return hit.collider.gameObject;
            }*/
            return null;
        }
        return null;
    }

    public void DibujarManoInicial()
    {
        if (_manoReferencia == null) { Debug.LogError("No hay referencia a Mano"); return; }

        StartCoroutine(RedrawManoCoroutine());
    }

    private IEnumerator RedrawManoCoroutine()
    {
        // Limpiar mano anterior
        foreach (Transform hijo in _contenedorMano) Destroy(hijo.gameObject);

        // Espera end of frame para que destroy se complete 
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < _manoReferencia.CartaEnMano.Count; i++)
        {
            GameObject nuevaCartaGO = Instantiate(_prefabCarta, _contenedorMano);

            CartaVisual visual = nuevaCartaGO.GetComponent<CartaVisual>();
            if (visual != null)
            {
                visual.ConfigurarCarta(_manoReferencia.CartaEnMano[i], true, 8);
            }
        }
        ActualizarSeparacionDeMano();
    }


/*
    public void DibujarManoInicial()
    {
        if (_manoReferencia == null) { Debug.LogError("No hay referencia a Mano"); return; }

        // Limpiar mano anterior si existe (opcional)
        foreach (Transform hijo in _contenedorMano) Destroy(hijo.gameObject);

        for (int i = 0; i < _manoReferencia.CartaEnMano.Count; i++)
        {
            GameObject nuevaCartaGO = Instantiate(_prefabCarta, _contenedorMano);     
            CartaVisual visual = nuevaCartaGO.GetComponent<CartaVisual>();

            if (visual != null)
            {
                visual.ConfigurarCarta(_manoReferencia.CartaEnMano[i], true, 8);
            }
        }
        ActualizarSeparacionDeMano();
    }*/

    public void ActualizarSeparacionDeMano()
    {
        int totalCartas = _contenedorMano.childCount;
        if (totalCartas == 0) return;
        Debug.Log($"Total cartas: {totalCartas}");
        //Espaciado para que entre en el ancho, suplanta a _espaciado
        float espaciadoCalculado = (totalCartas > 1) ? _anchoManoMax / (totalCartas - 1) : 0f;
        float anchoTotal = (totalCartas - 1) * espaciadoCalculado;  
        float puntoInicioX = -anchoTotal / 2f;


        for (int i = 0; i < totalCartas; i++)
        {
            Transform carta = _contenedorMano.GetChild(i);

            // 1. C�lculo de posici�n horizontal
            float posicionX = puntoInicioX + (i * espaciadoCalculado);

            // 2. C�lculo de la curva (Y) y rotaci�n (Z)
            // Usamos Mathf.Abs para que las cartas de los extremos bajen
            float curvaY = -Mathf.Abs(posicionX) * _intensidadCurva;
            float rotacionZ = posicionX * -_intensidadRotacion;

            // 3. Aplicar Transformaciones
            carta.localPosition = new Vector3(posicionX, curvaY, 0);
            carta.localRotation = Quaternion.Euler(0, 0, rotacionZ);

            // 4. Ajuste de Capa para que la carta x+1 est� encima de la carta x (Sorting Order)
            SpriteRenderer sr = carta.GetComponentInChildren<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingOrder = i;
            }

            
        }
    }
}
