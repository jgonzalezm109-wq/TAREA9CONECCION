using System;

public class Gasto
{

    public int Id { get; set; }
    public string Descripcion { get; set; }
    public decimal Monto { get; set; }
    public DateTime Fecha { get; set; }

    
    public Gasto(int id, string descripcion, decimal monto, DateTime fecha)
    {
        Id = id;
        Descripcion = descripcion;
        Monto = monto;
        Fecha = fecha;
    }
}