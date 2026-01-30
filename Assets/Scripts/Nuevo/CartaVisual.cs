 using UnityEngine;
/*
        Clasae Carta, esta sera el POO de la carta, donde tendra las configuracion de cada carta,
        - sprite
        - cartacterisitas y propiedaes
        - 
 */


public class CartaVisual : MonoBehaviour, IClickable
{
    [SerializeField] private Collider2D _collider;


    // ===== Propiedades =====
    private Carta _dataActual;

    private string _nombre;
    private int _valor;
    private TipoCarta _tipo;
    private Palo _palo;
    private Sprite _imagenSprite;
    private Sprite _dorsoSprite;
    private string _descripcion;

    // ===== Requerimientos =====
    [SerializeField] private SpriteRenderer _renderizador;

    public void ConfigurarCarta(Carta data, bool mostrarFrente, int layerIndex)
    {
        if (data == null) return;

        _dataActual = data;
        _nombre = _dataActual.Nombre;
        _valor = _dataActual.Valor;
        _tipo = _dataActual.Tipo;
        _palo = _dataActual.Palo;
        _imagenSprite = _dataActual.Imagen;
        _dorsoSprite = _dataActual.Dorso;
        _descripcion = _dataActual.Descripcion;

        SetLayerRecursively(this.gameObject, layerIndex);

        if (mostrarFrente)
        {
            _renderizador.sprite = _imagenSprite;
        }
        else
        {
            _renderizador.sprite = _dorsoSprite;
        }
    }

    private void SetLayerRecursively(GameObject go, int layer)
    {
        if (go == null) return;
        int clamped = Mathf.Clamp(layer, 0, 31);
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

    public void SetInteractable(bool state)
    {
        if (_collider != null) _collider.enabled = state;
    }


    public void OnClick()
    {
        Debug.Log($"Soy la carta {gameObject.name} - {_nombre} y me han seleccionado.");
        // Aquí podrías hacer que la carta se resalte o se juegue.
    }




    public Carta DataActual => _dataActual;
}
