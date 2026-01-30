using System.Collections.Generic;
using UnityEngine;

public class ManoManager : MonoBehaviour
{
    [SerializeField] private MazoPooler mazo;
    [SerializeField] private Transform ContenederMano;

    private List<CartaVisual> cartasEnMano = new List<CartaVisual>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mazo.InicializarMazo();

        RobarManoInicial(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void RobarManoInicial(int cantidad)
    {
        for (int i = 0; i < cantidad; i++)
        {
            PedirCartaAlMazo();
        }
    }


    public void PedirCartaAlMazo()
    {
        CartaVisual nuevaCarta = mazo.DarCartaFisicaDelMazo(ContenederMano, true);
        if (nuevaCarta != null)
        {
            cartasEnMano.Add(nuevaCarta);
            OrganizarVisualmente();
        }
    }




    private void OrganizarVisualmente()
    {
        // Aquí iría la lógica para que no se amontonen (ej. un offset en X)
        for (int i = 0; i < cartasEnMano.Count; i++)
        {
            cartasEnMano[i].transform.localPosition = new Vector3(i * 1.5f, 0, 0);
        }
    }

}
