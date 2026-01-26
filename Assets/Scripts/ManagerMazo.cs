using UnityEngine;

public class ManagerMazo : MonoBehaviour
{
    [SerializeField] private Mazo _mazoLogico;
    [SerializeField] private GameObject _prefabVisualMazo;
    [SerializeField] private Transform _contenedorMazo;
    [SerializeField] private int _cartasAVisualizar = 5;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DibujarMazoVisual();
    }

    public void DibujarMazoVisual()
    {    
        foreach (Transform t in _contenedorMazo) Destroy(t.gameObject);

        for (int i = 0; i < _cartasAVisualizar; i++)
        {
            GameObject nuevaCarta = Instantiate(_prefabVisualMazo, _contenedorMazo);
            nuevaCarta.transform.localPosition = new Vector3(0, i * 0.05f, 0);

            CartaVisual visual = nuevaCarta.GetComponent<CartaVisual>();
            if (visual != null)
            {
                visual.ConfigurarVisual(null, false);
            }
        }
    }
}
