using UnityEngine;

public enum TipoCarta
{
    Especial,
    Normal
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
    [SerializeField] private int id;
    [SerializeField] private string nombre;
    [SerializeField] private int valor;
    [SerializeField] private TipoCarta tipo;
    [SerializeField] private Palo palo;
    [SerializeField] private Sprite imagen;
    [SerializeField, TextArea] private string descripcion;

    public int Id => id;
    public string Nombre => nombre;
    public int Valor => valor;
    public TipoCarta Tipo => tipo;
    public Palo Palo => palo;
    public Sprite Imagen => imagen;
    public string Descripcion => descripcion;
}
