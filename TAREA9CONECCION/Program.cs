using Microsoft.Data.SqlClient;
using SistemaGastos;
using System;
using System.Collections.Generic;

namespace RegistroGastos
{
    class Program
    {
        static string cadenaConexion = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GastosBD;Integrated Security=True;TrustServerCertificate=True;";

        public static void Main(string[] args)
        {
            string opcion;

            do
            {
                opcion = LeerOpcionMenu();

                switch (opcion)
                {
                    case "1":
                        Agregar();
                        break;

                    case "2":
                        Listar();
                        break;

                    case "3":
                        BuscarPorCategoria();
                        break;

                    case "4":
                        Console.WriteLine("Saliendo...");
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

            } while (opcion != "4");
        }

        private static string LeerOpcionMenu()
        {
            Console.WriteLine("=== REGISTRO DE GASTOS ===");
            Console.WriteLine("1. Agregar gasto");
            Console.WriteLine("2. Listar gastos");
            Console.WriteLine("3. Buscar por categoría");
            Console.WriteLine("4. Salir");
            Console.Write("Elige una opción: ");

            return Console.ReadLine();
        }

        private static void Agregar()
        {
            Console.Write("Descripción: ");
            string descripcion = Console.ReadLine();

            Console.Write("Monto: ");
            string montoTexto = Console.ReadLine();

            Console.Write("Categoría: ");
            string categoria = Console.ReadLine();

            if (!decimal.TryParse(montoTexto, out decimal monto) ||
                monto <= 0 ||
                string.IsNullOrWhiteSpace(descripcion) ||
                string.IsNullOrWhiteSpace(categoria))
            {
                Console.WriteLine("Datos inválidos.");
                return;
            }

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                string consulta = "INSERT INTO Gastos (Descripcion, Monto, Categoria) VALUES (@Descripcion, @Monto, @Categoria)";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Descripcion", descripcion.Trim());
                    comando.Parameters.AddWithValue("@Monto", monto);
                    comando.Parameters.AddWithValue("@Categoria", categoria.Trim());

                    comando.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Gasto agregado.");
        }

        private static void Listar()
        {
            decimal totalGeneral = 0;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                string consulta = "SELECT Id, Descripcion, Monto, Categoria FROM Gastos";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            Console.WriteLine(
                                lector["Id"] + " " +
                                lector["Descripcion"] + " Q " +
                                lector["Monto"] + " " +
                                lector["Categoria"]);

                            totalGeneral += Convert.ToDecimal(lector["Monto"]);
                        }
                    }
                }
            }

            Console.WriteLine($"TOTAL GASTADO: Q {totalGeneral:N2}");
        }

        private static void BuscarPorCategoria()
        {
            Console.Write("Categoría a buscar: ");
            string texto = Console.ReadLine();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                string consulta = "SELECT Id, Descripcion, Monto, Categoria FROM Gastos WHERE Categoria LIKE @Categoria";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Categoria", "%" + texto + "%");

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        bool encontrado = false;

                        while (lector.Read())
                        {
                            Console.WriteLine(
                                lector["Id"] + " " +
                                lector["Descripcion"] + " Q " +
                                lector["Monto"] + " " +
                                lector["Categoria"]);

                            encontrado = true;
                        }

                        if (!encontrado)
                        {
                            Console.WriteLine("Sin coincidencias.");
                        }
                    }
                }
            }
        }
    }
}