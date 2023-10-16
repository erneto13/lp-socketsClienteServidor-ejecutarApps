using System;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;

class Server
{
    static void Main()
    {
        // Se crea un objeto para escuchar conexiones
        TcpListener elEscuchador = new TcpListener(IPAddress.Any, 8000);
        elEscuchador.Start();
        Console.WriteLine("Servidor esperando conexiones...");

        while (true) // Ciclo para detectar clientes
        {
            // Aceptamos la entrada de un cliente
            TcpClient cliente = elEscuchador.AcceptTcpClient();
            Console.WriteLine("Cliente conectado");

            // Se crea un hilo para administrar las entradas del cliente
            Thread hiloCliente = new Thread(HandleClient);
            hiloCliente.Start(cliente);
        }
    }

    static void HandleClient(object clientObj)
    {
        TcpClient cliente = (TcpClient)clientObj;
        NetworkStream stream = cliente.GetStream();

        // Búfer para leer la solicitud del cliente.
        byte[] buffer = new byte[1024];

        // Se lee la solicitud del cliente y se convierte a cadena
        int lectorBits = stream.Read(buffer, 0, buffer.Length);
        string peticionCliente = System.Text.Encoding.ASCII.GetString(buffer, 0, lectorBits);

        Console.WriteLine("Solicitud recibida: " + peticionCliente);

        // Se abre un switch para las opciones que el cliente pidio
        switch (peticionCliente)
        {
            case "1":
                StartApplication("excel.exe");
                break;
            case "2":
                StartApplication("powerpnt.exe");
                break;
            case "3":
                StartApplication("notepad.exe");
                break;
            default:
                Console.WriteLine("Solicitud no válida");
                break;
        }

        // Cerramos la conexión con el cliente
        cliente.Close();
    }

    static void StartApplication(string app)
    {
        try
        {
            // Con el proceso Start se abre la app que pidio el cliente
            Process.Start(app);
            Console.WriteLine("Aplicación ejecutada: " + app);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al ejecutar la aplicación: " + ex.Message); // Cachar cualquier error por si la app no jala o no existe o quien sabe lo que caiga es bueno
        }
    }
}
