using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Ingrese la ruta del archivo de invitados:");
        string filePath = Console.ReadLine();
        
        Console.WriteLine("Ingrese el email:");
        string email = Console.ReadLine();
        IsValidEmail(email)
        var listaInvitados = LeerTipoArchivo(filePath);
        //BuscarEmail(listaInvitados);

    }

    static bool IsValidEmail(string email)
    {
        string emailPattern = @"^[a-zA-Z][\w\.]*@[gmail|hotmail|live]+\.(com|co|edu\.co|org)$";
        return Regex.IsMatch(email, emailPattern);
    }

    static List<Invitado> LeerTipoArchivo(string filePath)
    {
        string fileExtension = Path.GetExtension(filePath);
        string[] fileLines;

        if (fileExtension == ".txt")
        {
            fileLines = File.ReadAllLines(filePath);
        }
        else if (fileExtension == ".csv")
        {
            using (var reader = new StreamReader(filePath))
            {
                var csvContent = reader.ReadToEnd();
                fileLines = csvContent.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            }
        }
        else
        {
            Console.WriteLine("El archivo no es un archivo de texto (.txt) o un archivo CSV (.csv).");
            return null;
        }

        var listaInvitados = new List<Invitado>();

        foreach (string line in fileLines)
        {
            string[] elements;

            if (fileExtension == ".txt")
            {
                elements = line.Split(' ');
            }
            else // fileExtension == ".csv"
            {
                elements = line.Split(',');
            }

            string nombre = elements[0];
            int id = int.Parse(elements[1]);
            string email = elements[2];
            int edad = int.Parse(elements[3]);

            if (!IsValidEmail(email))
            {
                Console.WriteLine($"El email {email} no es válido y ha sido ignorado.");
                continue;
            }

            var invitado = new Invitado(nombre, id, email, edad);
            listaInvitados.Add(invitado);
        }

        Console.WriteLine("Contenido del archivo:");
        foreach (var invitado in listaInvitados)
        {
            Console.WriteLine($"{invitado.Nombre} ({invitado.Edad} años) - ID: {invitado.Id} - Email: {invitado.Email}");
        }

        return listaInvitados;
    }

   
}



class Invitado
{
    public string Nombre { get; set; }
    public int Id { get; set; }
    public string Email { get; set; }
    public int Edad { get; set; }

    public Invitado(string nombre, int id, string email, int edad)
    {
        Nombre = nombre;
        Id = id;
        Email = email;
        Edad = edad;
    }
}
