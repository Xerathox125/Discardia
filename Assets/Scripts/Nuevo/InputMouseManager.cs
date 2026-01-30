using UnityEngine;
using UnityEngine.InputSystem;


public class InputMouseManager : MonoBehaviour
{
    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {

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
                objetoClickado.OnClick(); 
            }
        }
    }





}
