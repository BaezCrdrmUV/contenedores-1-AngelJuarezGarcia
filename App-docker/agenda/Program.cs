using System;
using System.Collections.Generic;

namespace agenda
{
    public class Program
    {
        public static void Main(String[] args)
        {
            ConectionMYSQL baseDeDatos = new ConectionMYSQL();
            bool programaActivo = true;

            do
            {
                Console.WriteLine("Selecciona lo que deseas hacer: \n1 - Mostrar agenda\n2 - Registrar nuevo contacto\n3 - Eliminar contacto\n4 - Editar contacto\n5 - Salir del programa");
                int opcionSeleccionada = int.Parse(Console.ReadLine());

                switch (opcionSeleccionada)
                {
                    case 1:
                        List<Contacto> listaDeContactos = baseDeDatos.ObtenerContactos();
                        
                        foreach (Contacto contacto in listaDeContactos)
                        {
                            Console.WriteLine("Nombre: " + contacto.Nombre);
                            Console.WriteLine("Correo electronico: " + contacto.Correo);
                            Console.WriteLine("Numero de telefono: " + contacto.Telefono);
                            Console.WriteLine("=============================");
                        }

                    break;

                    case 2:
                        Contacto nuevoContacto = new Contacto();

                        Console.WriteLine("\nIngresa el nombre de tu nuevo contacto");
                        nuevoContacto.Nombre = Console.ReadLine();

                        Console.WriteLine("\nIngresa el correo de tu nuevo contacto");
                        nuevoContacto.Correo = Console.ReadLine();

                        Console.WriteLine("\nIngresa el telefono de tu nuevo contacto");
                        nuevoContacto.Telefono = Console.ReadLine();

                        if (baseDeDatos.RegistrarContacto(nuevoContacto))
                        {
                            Console.WriteLine("\n===Registro exitoso===\n");
                        }else{
                            Console.WriteLine("Ocurrio un error durante el registro, intente de nuevo");
                        }

                    break;

                    case 3:
                        Console.WriteLine("\nIngresa el nombre del contacto a eliminar");
                        string nombreDeContacto = Console.ReadLine();

                        if (baseDeDatos.EliminarContacto(nombreDeContacto))
                        {
                            Console.WriteLine("\n===Eliminación exitosa===");
                        }else{
                            Console.WriteLine("\n===Ocurrio un error durante la eliminación===");
                        }

                    break;

                    case 4:
                        List<Contacto> contactos = baseDeDatos.ObtenerContactos();
                        foreach (Contacto contacto in contactos)
                        {
                            Console.WriteLine("- " + contacto.Nombre);
                        }
                        
                        Console.WriteLine("Escribe el nombre del contacto a editar");
                        string nombreContacto = Console.ReadLine();

                        Console.WriteLine("¿Qué información deseas actualizar?\n1 - Correo\n2 - Telefono");
                        int infoAActualizar = int.Parse(Console.ReadLine());

                        switch (infoAActualizar)
                        {
                            case 1:
                                Console.WriteLine("\n===Ingresa el nuevo correo===\n");
                                string nuevoCorreo = Console.ReadLine();
                                if (baseDeDatos.ActualizarCorreo(nombreContacto,nuevoCorreo))
                                {
                                    Console.WriteLine("\n===Aactualización exitosa===\n");
                                }else{
                                    Console.WriteLine("\n===Ocurrio un error durante la actualización===\n");
                                }
                            break;
                            
                            case 2:
                                Console.WriteLine("\n===Ingresa el nuevo telefono===\n");
                                string nuevoTelefono = Console.ReadLine();
                                if (baseDeDatos.ActualizarTelefono(nombreContacto, nuevoTelefono))
                                {
                                    Console.WriteLine("\n===Aactualización exitosa===\n");
                                }else{
                                    Console.WriteLine("\n===Ocurrio un error durante la actualización===\n");
                                }
                            break;
                            
                            default:
                                Console.WriteLine("\n===Selecciona una opcion valida===\n");   
                            break;
                        }
                    break;

                    case 5:
                        programaActivo = false;
                    break;

                    default:
                        Console.WriteLine("\n===Selecciona una opcion valida===\n");
                    break;
                }

            } while(programaActivo);
        }
    }
}