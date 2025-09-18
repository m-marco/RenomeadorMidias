using System;
using System.Diagnostics;
using System.IO;

Console.WriteLine("Bem vindo ao renomeador de mídias! Digite a pasta onde estão as mídias para começarmos:");

string path = Console.ReadLine();

if (string.IsNullOrEmpty(path))
    path = "C:\\Users\\Marco\\Videos\\JWLibrary";

if (!Directory.Exists(path))
{
    Console.WriteLine("Pasta não encontrada. Pressione qualquer tecla para sair.");
    Console.ReadKey();
    return;
}

var files = Directory.GetFiles(path);

foreach (var item in files)
{
    try
    {
        using (var tagFile = TagLib.File.Create(item))
        {
            // A propriedade Tag contém metadados como Título, Artista, Álbum, etc.
            string title = tagFile.Tag.Title;

            if (!string.IsNullOrEmpty(title))
            {
                Console.WriteLine($"Arquivo: {Path.GetFileName(item)} | Título: {title}");
            }
            else
            {
                Console.WriteLine($"Arquivo: {Path.GetFileName(item)} não possui um título nos metadados.");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erroao ler dados do arquivo {item} {ex.Message}");
    }
}

