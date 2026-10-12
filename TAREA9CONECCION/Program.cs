using System;
using System.Collections.Generic;

class Program
{
    
    private static List<Gasto> listaGastos = new List<Gasto>();
    private static int contadorId = 1;

    static void Main(string[] args)
    {
        int opcion;
        do
        {
            Console.Clear();
            Console.WriteLine("=== SISTEMA DE GESTIÓN DE GASTOS (Modo Memoria) ===");
            Console.WriteLine("1. Registrar nuevo gasto");
            Console.WriteLine("2. Listar todos los gastos");
            Console.WriteLine("3. Ver total gastado");
            Console.WriteLine("4. Salir");
            Console.Write("\nSelecciona una opción: ");

            if (int.TryParse(Console.ReadLine(), out opcion))
            {
                switch (opcion)
                {
                    case 1:
                        RegistrarGastoUI();
                        break;
                    case 2:
                        ListarGastosUI();
                        break;
                    case 3:
                        VerTotalGastosUI();
                        break;
                    case 4:
                        Console.WriteLine("\nSaliendo del programa. ¡Mucho éxito con tu tarea!");
                        break;
                    default:
                        Console.WriteLine("\nOpción no válida. Presiona una tecla para continuar.");
                        Console.ReadKey();
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nPor favor, ingresa un número válido.");
                Console.ReadKey();
            }

        } while (opcion != 4);
    }

    private static void RegistrarGastoUI()
    {
        Console.Clear();
        Console.WriteLine("--- REGISTRAR NUEVO GASTO ---");

        Console.Write("Ingresa la descripción del gasto: ");
        string desc = Console.ReadLine();

        Console.Write("Ingresa el monto (ej. 45.50): ");
        if (decimal.TryParse(Console.ReadLine(), out decimal monto))
        {
          
            Gasto nuevoGasto = new Gasto(contadorId++, desc, monto, DateTime.Now);
            listaGastos.Add(nuevoGasto);
            Console.WriteLine("\n[Éxito] ¡Gasto registrado correctamente!");
        }
        else
        {
            Console.WriteLine("\n[Error] El monto ingresado no es válido.");
        }

        Console.WriteLine("\nPresiona cualquier tecla para volver al menú...");
        Console.ReadKey();
    }

    private static void ListarGastosUI()
    {
        Console.Clear();
        Console.WriteLine("--- LISTADO DE GASTOS ---");

        if (listaGastos.Count == 0)
        {
            Console.WriteLine("No hay gastos registrados todavía.");
        }
        else
        {
            foreach (var g in listaGastos)
            {
                Console.WriteLine($"ID: {g.Id} | Descripción: {g.Descripcion} | Monto: Q{g.Monto:N2} | Fecha: {g.Fecha:yyyy-MM-dd HH:mm}");
            }
        }

        Console.WriteLine("\nPresiona cualquier tecla para volver al menú...");
        Console.ReadKey();
    }

    private static void VerTotalGastosUI()
    {
        Console.Clear();
        Console.WriteLine("--- TOTAL DE GASTOS ---");

        decimal total = 0;
        foreach (var g in listaGastos)
        {
            total += g.Monto;
        }

        Console.WriteLine($"Has gastado un total de: Q{total:N2}");

        Console.WriteLine("\nPresiona cualquier tecla para volver al menú...");
        Console.ReadKey();
    }
}