using System.Data.SqlClient;
using System.Configuration;
using System;

namespace SingletonSQLCnn
{

    public class SingletonDatabase
    {
        // Paso 1: Hacer privado el contructor
        private SingletonDatabase() {
        
        }

        // Paso 2: Crear propiedad privada para contener la instancia
        private static SqlConnection _instance;
        // Para manejar hilos multiples usamos un objeto para sicronizar los hilos durante el primer acceso al singleton
        private static readonly object _syncObject = new object();
        
        //const string strCnn = @"Server=.,14033;Database=SchoolAdmin;User Id=sa;Password=Admin4dm1n!;";

        // Paso 3: Regresa la instancia, si no existe crea una nueva.
        public static SqlConnection getConnection {
            get
            {
                if (_instance == null)
                {
                    lock (_syncObject) // Sincronizamos los hilos
                    {
                        if (_instance == null)
                        {
                            string environment = ConfigMan.ReadSetting("environment");
                            string strConnectionSettings = "";
                            //ReadAllSettings();
                            switch (environment.ToLower() )
                            {
                                case "dev": strConnectionSettings = ConfigMan.ReadSetting("connectuionStringDev"); 
                                    break;
                                case "qa": strConnectionSettings = ConfigMan.ReadSetting("connectuionStringQA"); 
                                    break;
                                case "prod": strConnectionSettings = ConfigMan.ReadSetting("connectuionStringProd"); 
                                    break;
                                default:
                                    break;
                            }
                            
                            _instance = new SqlConnection(strConnectionSettings);
                        }
                    }
                }
                return _instance;
            }
        }

        public static void Open()
        {
            if (_instance != null)
                _instance.Open();
        }
        public static void Close()
        {
            if (_instance != null)
                _instance.Close();
        }

    }

}