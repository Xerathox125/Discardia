 using UnityEngine;

public class CartaVisual : MonoBehaviour
{
    private SpriteRenderer _renderizador;
    [SerializeField] private Sprite _dorsoSprite;

    //Para obtener los datos de la carta
    public Carta Data { get; private set; }

    private void Awake()
    {
        if (_renderizador == null) _renderizador = GetComponent<SpriteRenderer>();
    }

    public void ConfigurarCarta(Carta data, bool mostrarFrente, int layerIndex)
    {
        Data = data;

        SetLayerRecursively(this.gameObject, layerIndex);

        if (mostrarFrente)
        {
            // Si queremos mostrar el frente, S� necesitamos data
            if (data != null)
            {
                _renderizador.sprite = data.Imagen;
            }
            else
            {
                Debug.LogWarning("Se intent� mostrar el frente pero 'data' es nulo.");
                _renderizador.sprite = _dorsoSprite;
            }
        }
        else
        {
            _renderizador.sprite = _dorsoSprite;
        }
    }



    private void SetLayerRecursively(GameObject go, int layer)
    {
        if (go == null) return;
        int clamped = Mathf.Clamp(layer, 1, 31);
        if (clamped != layer)
        {
            Debug.LogWarning($"Layer {layer} fuera de rango [0..31]. Usando {clamped} en su lugar. Revisa llamadas a ConfigurarCarta.");
        }
        go.layer = clamped;

        foreach (Transform t in go.transform)
        {
            SetLayerRecursively(t.gameObject, layer);
        }
    }





}
