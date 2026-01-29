using UnityEngine;
public enum TipoCarta
{    
    Normal,
    Especial
}

public enum Palo
{
    Corazon,
    Diamante,
    Espada,
    Trebol
}

[CreateAssetMenu(fileName = "Nueva Carta", menuName = "Juego/Mazo/Carta")]
public class Carta : ScriptableObject
{
    [SerializeField] private string nombre;
    [SerializeField] private int valor;
    [SerializeField] private TipoCarta tipo;
    [SerializeField] private Palo palo;
    [SerializeField] private Sprite imagen;
    [SerializeField, TextArea] private string descripcion;
    //[SerializeField] private string tagName;
    public string Nombre => nombre;
    public int Valor => valor;
    public TipoCarta Tipo => tipo;
    public Palo Palo => palo;
    public Sprite Imagen => imagen;
    public string Descripcion => descripcion;
   //public string TagName => tagName;
}
