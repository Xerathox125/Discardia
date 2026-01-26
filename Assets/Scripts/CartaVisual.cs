 using UnityEngine;

public class CartaVisual : MonoBehaviour
{
    private SpriteRenderer _renderizador;
    [SerializeField] private Sprite _dorsoSprite;

    private void Awake()
    {
       _renderizador = GetComponent<SpriteRenderer>();
    }

    public void ConfigurarVisual(Carta data, bool mostrarFrente)
    {

        if (mostrarFrente)
        {
            // Si queremos mostrar el frente, SÍ necesitamos data
            if (data != null)
            {
                _renderizador.sprite = data.Imagen;
            }
            else
            {
                Debug.LogWarning("Se intentó mostrar el frente pero 'data' es nulo.");
            }
        }
        else
        {
            _renderizador.sprite = _dorsoSprite;
        }
    }
}
