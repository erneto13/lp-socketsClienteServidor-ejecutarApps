using System;
using System.Net;
using System.Net.Sockets;

class Client
{
    static void Main(string[] args)
    {
        try
        {
            // Creamos el objeto cliente y le asignamos la IP
            TcpClient cliente = new TcpClient("0.0.0.0", 8000);

            // Flujo de red pa' comunicarse al server
            NetworkStream stream = cliente.GetStream();

            Console.WriteLine("Aplicaciones remotas disponibles:");
            Console.WriteLine("1. Excel");
            Console.WriteLine("2. PowerPoint");
            Console.WriteLine("3. Bloc de Notas");

            // Se le pide al cliente que seleccione una opcion y se almacena en una cadena
            Console.Write("Seleccione la aplicación (1/2/3): ");
            string op = Console.ReadLine();

            // Convertimos la elección del usuario a un arreglo de bytes y lo enviamos al servidor.
            byte[] infoBytes = System.Text.Encoding.ASCII.GetBytes(op);
            stream.Write(infoBytes, 0, infoBytes.Length);

            // Se cierra la conexión
            cliente.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al conectar al servidor: " + ex.Message); // Error por si el cliente no esta disponible (encender el server primero)
        }
    }
}
