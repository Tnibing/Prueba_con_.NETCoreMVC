using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DWES.Models;
using System.Text.RegularExpressions;

namespace DWES.Controllers;

public class Ejercicio1Controller : Controller
{
    private readonly ILogger<Ejercicio1Controller> _logger;

    public Ejercicio1Controller(ILogger<Ejercicio1Controller> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Solucion1()
    {
        string cadena = "Hola, buenos días";

        ViewBag.Cadena = cadena;

        int vocalesEncontradas = 0;

        HashSet<char> vocales = new HashSet<char> { 'a', 'e', 'i', 'o', 'u', 
                                                    'á', 'é', 'í', 'ó', 'ú', 
                                                    'à', 'è', 'ì', 'ò', 'ù', 
                                                    'ä', 'ë', 'ï', 'ö', 'ü'
        };

        for (int i = 0; i < cadena.Length; i++)
        {
            char letra = char.ToLower(cadena[i]);

            if (vocales.Contains(letra))
            {
                vocalesEncontradas++;
            }
        }

        ViewBag.Vocales = vocalesEncontradas;

        return View();
    }

    public IActionResult Solucion2()
    {
        string regex = @"[aeiouáéíóúàèìòùäëïöü]";
        string frase = "Hola, ¿cómo estás?";

        List<string> totalCoincidencias = new List<string>();

        MatchCollection coincidencia = Regex.Matches(frase, regex);

        foreach (Match c in coincidencia)
        {
            totalCoincidencias.Add(c.Value);
        }

        ViewBag.Frase = frase;
        ViewBag.Vocales = totalCoincidencias.Count;

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
