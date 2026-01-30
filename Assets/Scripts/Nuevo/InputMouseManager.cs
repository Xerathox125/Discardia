using UnityEngine;
using UnityEngine.InputSystem;


public class InputMouseManager : MonoBehaviour
{
    private Camera _mainCamera;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        // 1. Detectamos el clic una sola vez por frame
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            ProcesarClic();
        }
    }


    private void ProcesarClic()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null)
        {
            
            IClickable objetoClickado = hit.collider.GetComponentInParent<IClickable>();

            if (objetoClickado != null)
            {
                objetoClickado.OnClick(); // Notificamos al objeto
            }
        }
    }





}
