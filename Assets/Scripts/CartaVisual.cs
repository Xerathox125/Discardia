using TMPro;
using UnityEngine;

public class CartaVisual : MonoBehaviour
{
    private SpriteRenderer _renderizador;

    private void Awake()
    {
       _renderizador = GetComponent<SpriteRenderer>();
    }

    public void ConfigurarVisual(Carta data)
    {

        Debug.Log("data es:" + data);
        Debug.Log(" renderizador es:" + _renderizador);

        if (data != null && _renderizador != null)
        {
            _renderizador.sprite = data.Imagen;
        }
        else
        {
            Debug.LogError($"Error en {gameObject.name}: Data o Renderizador faltante");
        }
    }
}
