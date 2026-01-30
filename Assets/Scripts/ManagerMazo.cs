using UnityEngine;
using UnityEngine.InputSystem;


public class ManagerMazo : MonoBehaviour, IClickable
{
    [SerializeField] private Mazo _mazoLogico;
    [SerializeField] private GameObject _prefabVisualMazo;
    [SerializeField] private Transform _contenedorMazo;
    //[SerializeField] private int _cartasAVisualizar = 20;

    //Para clicar y robar una carta
    [SerializeField] public Mano _manoReferencia;
    [SerializeField] public ManagerCarta _managerCartaReferencia;

    //[SerializeField] private float _offsetMazo = 0.02f;



    public Mazo MazoLogica => _mazoLogico;



    void Start() {
       
    }

    void Update()
{
    //Esto es con el Input Manager nuevo
    if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
    {
            // Raycast desde mouse a mundo 2D
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && (_contenedorMazo != null 
                                            && hit.collider.transform.IsChildOf(_contenedorMazo)
                                            || hit.collider.gameObject == this.gameObject))
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

            Debug.Log($"Mazo ahora: {_mazoLogico.Count} cartas");
        }
    }
}



    public void OnClick()
    {
        Debug.Log("Soy el Mazo y me han clickeado!");

        if (_mazoLogico.Count > 0)
        {
            _manoReferencia.PedirCartaAlMazo();
        }
    }









    //public void DibujarMazoVisual()
    //{
    //    //foreach (Transform t in _contenedorMazo) Destroy(t.gameObject);
    //    // Limpiar visuales anteriores (evita acumulación)
    //    foreach (Transform hijo in _contenedorMazo)
    //    {
    //        Destroy(hijo.gameObject);
    //    }



    //    //Intento de mostrar todas las cartas del mazo
    //    int numCartas = Mathf.Min(_mazoLogico.Count, 52);
    //    float centroCartas = -1 * ((numCartas - 1) * _offsetMazo / 2f);


    //    for (int i = 0; i < numCartas; i++)
    //    {
    //        GameObject nuevaCarta = Instantiate(_prefabVisualMazo, _contenedorMazo);
    //        float xPosx = (i * _offsetMazo) + centroCartas;

    //        nuevaCarta.transform.localPosition = new Vector3(0, xPosx, 0);

    //        CartaVisual visual = nuevaCarta.GetComponent<CartaVisual>();
    //        if (visual != null)
    //        {
    //            visual.ConfigurarCarta(null, false, 7);
    //        }


    //        SpriteRenderer sr = nuevaCarta.GetComponent<SpriteRenderer>();
    //        if (sr != null)
    //        {
    //            sr.sortingOrder = i; // i=0: atrás, i=max: adelante
    //        }
    //    }
    //}




}
