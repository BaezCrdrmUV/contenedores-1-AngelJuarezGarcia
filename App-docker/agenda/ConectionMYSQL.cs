using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;

namespace agenda
{
    public class ConectionMYSQL
    {
        private String connectionString;
        private MySqlConnection conexion;
        private string ID_PERSONA_ERROR = "0";

        public ConectionMYSQL(){
            connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            conexion = new MySqlConnection(connectionString);
        }

        public List<Contacto> ObtenerContactos(){

            List<Contacto> listaDeContactos = new List<Contacto>();

            try
            {
                conexion.Open();
                var comando = new MySqlCommand("SELECT Nombre,Numero,Correo FROM Persona,Telefono,Correo WHERE Persona.idPersona = Telefono.Persona_idPersona AND Persona.idPersona = Correo.Persona_idPersona;", conexion);
                var reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Contacto contactoObtenido = new Contacto();
                    contactoObtenido.Nombre = reader.GetString(0);
                    contactoObtenido.Telefono = reader.GetString(1);
                    contactoObtenido.Correo = reader.GetString(2);

                    listaDeContactos.Add(contactoObtenido);
                }
            }
            catch (Exception error)
            {
                System.Console.WriteLine("Ocurrio un error: " + error.Message);

            }finally{
                conexion.Close();
            }

            return listaDeContactos;
        }

        public bool RegistrarContacto(Contacto nuevoContacto){
            bool registroExitoso = false;
            
            if (RegistrarNombre(nuevoContacto.Nombre))
            {
                string idPersona = ObtenerId(nuevoContacto.Nombre);

                if (RegistrarCorreo(nuevoContacto.Correo, idPersona) && RegistrarTelefono(nuevoContacto.Telefono, idPersona))
                {
                    registroExitoso = true;
                }
            }

            return registroExitoso;
        }

        public bool EliminarContacto(string nombreContacto){
            bool eliminacionExitosa = false;
            string idContacto = ObtenerId(nombreContacto);

            if(EliminarTelefono(idContacto) && EliminarCorreo(idContacto) && EliminarNombre(idContacto)){
                eliminacionExitosa = true;
            }

            return eliminacionExitosa;
        }

        public bool ActualizarCorreo(string nombreContacto, string nuevoCorreo){
            bool actualizacionExitosa = false;
            string idPersona = ObtenerId(nombreContacto);

            try
            {
                conexion.Open();

                var registroPersona = new MySqlCommand($"UPDATE Correo SET Correo = '{nuevoCorreo}' WHERE Persona_idPersona='{idPersona}'", conexion);
                registroPersona.ExecuteNonQuery();

                actualizacionExitosa = true;

            } catch(Exception error){
                Console.WriteLine("Ocurrió un error (Método ActualizarCorreo): " + error.Message);
            }finally{
                conexion.Close();
            }

            return actualizacionExitosa;
        }

        public bool ActualizarTelefono(string nombreContacto, string nuevoTelefono){
            bool actualizacionExitosa = false;
            string idPersona = ObtenerId(nombreContacto);

            try
            {
                conexion.Open();

                var registroPersona = new MySqlCommand($"UPDATE Telefono SET Numero = '{nuevoTelefono}' WHERE Persona_idPersona='{idPersona}'", conexion);
                registroPersona.ExecuteNonQuery();

                actualizacionExitosa = true;

            } catch(Exception error){
                Console.WriteLine("Ocurrió un error (Método ActualizarTelefono): " + error.Message);
            }finally{
                conexion.Close();
            }

            return actualizacionExitosa;
        }


        //METODOS PRIVADOS PARA REALIZAR LAS INSERCIONES DE DATOS

        private bool RegistrarNombre(string nombre){
            bool registroExitoso = false;

            try
            {
                conexion.Open();

                var registroPersona = new MySqlCommand($"INSERT INTO Persona(Nombre) VALUES ('{nombre}');", conexion);
                registroPersona.ExecuteNonQuery();

                registroExitoso = true;

            } catch(Exception error){
                Console.WriteLine("Ocurrió un error (Método RegistrarNombre): " + error.Message);
            }finally{
                conexion.Close();
            }

            return registroExitoso;
        } 
        private bool EliminarNombre(string idPersona){
            bool eliminacionExitosa = false;
            try
            {
                conexion.Open();

                var comandoEliminar = new MySqlCommand($"DELETE FROM Persona WHERE idPersona='{idPersona}';", conexion);
                comandoEliminar.ExecuteNonQuery();

                eliminacionExitosa = true;

            } catch(Exception error){
                Console.WriteLine("Ocurrió un error (Método EliminarNombre): " + error.Message);
            }finally{
                conexion.Close();
            }
            return eliminacionExitosa;
        }
        
        private bool RegistrarCorreo(string correo, string idPersona){
            bool registroExitoso = false;

            try
            {
                conexion.Open();

                var registroCorreo = new MySqlCommand($"INSERT INTO Correo(Correo,Persona_idPersona) VALUES ('{correo}','{idPersona}');", conexion);
                registroCorreo.ExecuteNonQuery();

                registroExitoso = true;

            } catch(Exception error){
                Console.WriteLine("Ocurrió un error (Método RegistrarCorreo): " + error.Message);
            }finally{
                conexion.Close();
            }

            return registroExitoso;
        } 
        private bool EliminarCorreo(string idPersona){
            bool eliminacionExitosa = false;
            try
            {
                conexion.Open();

                var comandoEliminar = new MySqlCommand($"DELETE FROM Correo WHERE Persona_idPersona='{idPersona}';", conexion);
                comandoEliminar.ExecuteNonQuery();

                eliminacionExitosa = true;

            } catch(Exception error){
                Console.WriteLine("Ocurrió un error (Método EliminarCorreo): " + error.Message);
            }finally{
                conexion.Close();
            }
            return eliminacionExitosa;
        }

        private bool RegistrarTelefono(string telefono, string idPersona){
            bool registroExitoso = false;

            try
            {
                conexion.Open();

                var registroTelefono = new MySqlCommand($"INSERT INTO Telefono(Numero,Persona_idPersona) VALUES ('{telefono}','{idPersona}');", conexion);
                registroTelefono.ExecuteNonQuery();

                registroExitoso = true;

            } catch(Exception error){
                Console.WriteLine("Ocurrió un error (Método RegistrarTelefono): " + error.Message);
            }finally{
                conexion.Close();
            }

            return registroExitoso;
        } 
        private bool EliminarTelefono(string idPersona){
            bool eliminacionExitosa = false;
            try
            {
                conexion.Open();

                var comandoEliminar = new MySqlCommand($"DELETE FROM Telefono WHERE Persona_idPersona='{idPersona}';", conexion);
                comandoEliminar.ExecuteNonQuery();

                eliminacionExitosa = true;

            } catch(Exception error){
                Console.WriteLine("Ocurrió un error (Método EliminarCorreo): " + error.Message);
            }finally{
                conexion.Close();
            }
            return eliminacionExitosa;
        }

        private string ObtenerId(string nombreContacto){
            string idObtenido = ID_PERSONA_ERROR;
            try
            {
                conexion.Open();
                var command = new MySqlCommand($"SELECT idPersona FROM Persona WHERE Nombre = '{nombreContacto}'", conexion);
                var reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    idObtenido = reader.GetInt32(0).ToString();
                }

            }catch(Exception error){
                Console.WriteLine("Ocurrió un error (Método ObtenerId)" +  error.Message);
            }finally{
                conexion.Close();
            }

            return idObtenido;
        }
    }
}