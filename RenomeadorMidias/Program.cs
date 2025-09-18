using System;
using System.IO;
using System.Linq;

Console.WriteLine("Bem vindo ao renomeador de mídias! Digite a pasta onde estão as mídias para começarmos:");

string path = Console.ReadLine();

if (!Directory.Exists(path))
{
    Console.WriteLine("Pasta não encontrada. Pressione qualquer tecla para sair.");
    Console.ReadKey();
    return;
}

string returnPath = Path.Combine(path, "Renomeados");

if (!Directory.Exists(returnPath))
    Directory.CreateDirectory(returnPath);

var files = Directory.GetFiles(path);

foreach (var item in files)
{
    try
    {
        using var tagFile = TagLib.File.Create(item);
        // A propriedade Tag contém metadados como Título, Artista, Álbum, etc.
        string title = tagFile?.Tag?.Title;

        if (!string.IsNullOrEmpty(title))
        {
            Console.WriteLine($"Arquivo: {Path.GetFileName(item)} | Título: {title}");
            File.Copy(item, Path.Combine(returnPath, SanitizeFileName(title) + Path.GetExtension(item)), true);
        }
        else
        {
            Console.WriteLine($"Arquivo: {Path.GetFileName(item)} não possui um título nos metadados.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao ler dados do arquivo {item} {ex.Message}");
    }
}
string SanitizeFileName(string fileName)
{
    char[] invalidChars = Path.GetInvalidFileNameChars();

    // Remove os caracteres inválidos
    string sanitized = new(fileName.Where(ch => !invalidChars.Contains(ch)).ToArray());

    // Remove espaços ou pontos no final do nome
    return sanitized.TrimEnd(' ', '.');
}