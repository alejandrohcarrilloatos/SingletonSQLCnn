using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonSQLCnn
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hola mundo");

            SqlConnection conexion = SingletonDatabase.getConnection;

            Console.WriteLine(conexion.ConnectionString);
            //Console.ReadLine();

            conexion = SingletonDatabase.getConnection;
            Console.WriteLine(conexion.ConnectionString);

            Console.ReadLine();

        }
    }
}
