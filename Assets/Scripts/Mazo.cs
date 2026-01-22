using System.Collections.Generic;
using UnityEngine;

public class Mazo : MonoBehaviour
{
    private Carta _carta;
    [SerializeField] private List<Carta> _Mazos = new List<Carta>();
    private SpriteRenderer _image;

      
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _image = GetComponent<SpriteRenderer>();


        CrearMazoCarta(_carta);

        CrearImagenCarta();
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    void CrearImagenCarta()
    {
        if (_Mazos.Count > 0)
        {
            _image.sprite = _Mazos[0].Imagen;
        }
    }

    void CrearMazoCarta(Carta carta)
    {
        _Mazos.Add(carta);
        print("CREO CARTA");
    }


}
