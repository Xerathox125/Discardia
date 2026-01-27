using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class ManagerMazo : MonoBehaviour
{
    [SerializeField] private Mazo _mazoLogico;
    [SerializeField] private GameObject _prefabVisualMazo;
    [SerializeField] private Transform _contenedorMazo;
    //[SerializeField] private int _cartasAVisualizar = 20;

    //Para clicar y robar una carta
    [SerializeField] public Mano _manoReferencia;
    [SerializeField] public ManagerCarta _managerCartaReferencia;

    [SerializeField] private float _offsetMazo = 0.02f;


    void Start()
    {
        DibujarMazoVisual();
    }

    void Update()
{
    //Esto es con el Input Manager nuevo
    if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
    {
        // Raycast desde mouse a mundo 2D
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == this.gameObject)
        {
            Debug.Log("CLICK DETECTADO en Mazo!"); 

            if (_manoReferencia != null)
            {
                _manoReferencia.PedirCartaAlMazo();
                Debug.Log("Carta robada!");
            }
            else
            {
                Debug.LogError("_manoReferencia es NULL!");
            }

            if (_managerCartaReferencia != null)
            {
                _managerCartaReferencia.DibujarManoInicial();
                Debug.Log("Mano actualizada!");
            }
            else
            {
                Debug.LogError("managerReferenciaCarta es NULL!");
            }

            DibujarMazoVisual();
            Debug.Log($"Mazo ahora: {_mazoLogico.Count} cartas");
        }
    }
}

    public void DibujarMazoVisual()
    {    
        foreach (Transform t in _contenedorMazo) Destroy(t.gameObject);

        //Intento de mostrar todas las cartas del mazo
        int numCartas = _mazoLogico.Count;
        for (int i = 0; i < numCartas; i++)
        {
            GameObject nuevaCarta = Instantiate(_prefabVisualMazo, _contenedorMazo);
            nuevaCarta.transform.localPosition = new Vector3(0, i * _offsetMazo, 0);

            CartaVisual visual = nuevaCarta.GetComponent<CartaVisual>();
            if (visual != null)
            {
                visual.ConfigurarVisual(null, false);
            }

            //Se daba vuelta el mazo, con esto se mantiene
            SpriteRenderer sr = nuevaCarta.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingOrder = i; // i=0: atrás, i=max: adelante
            }
        }
    }
}
