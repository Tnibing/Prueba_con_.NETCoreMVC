using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DWES.Models;
using System.Text.RegularExpressions;
using System.Text;

namespace DWES.Controllers;

public class Ejercicio5Controller : Controller
{
    private readonly ILogger<Ejercicio5Controller> _logger;

    public Ejercicio5Controller(ILogger<Ejercicio5Controller> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Solucion(string cadena, string busqueda)
    {
        if (String.IsNullOrWhiteSpace(cadena) || String.IsNullOrWhiteSpace(busqueda))
        {
            return RedirectToAction("Index");
        }

        string regexSeparador = @"\s|,\s";

        String[] palabras = Regex.Split(cadena, regexSeparador);

        List<String> coincidencias = new List<String>();

        string busquedaLowSinAcentos = quitarAcentos(busqueda.ToLower());

        foreach (String palabra in palabras)
        {
            string palabraLow = palabra.ToLower();

            MatchCollection encontrada = Regex.Matches(palabraLow, busquedaLowSinAcentos, RegexOptions.IgnoreCase);

            if (encontrada.Count > 0)
            {
                coincidencias.Add(palabra);
            }          
        }

        ViewBag.Cadena = cadena;
        ViewBag.Busqueda = busqueda;
        ViewBag.Encontradas = coincidencias;

        return View();
    }

    private string quitarAcentos(string palabra)
    {
        Dictionary<char, char> vocalesConTilde = new Dictionary<char, char>
        {
            {'á', 'a'}, {'à', 'a'}, {'ä', 'a'},
            {'é', 'e'}, {'è', 'e'}, {'ë', 'e'},
            {'í', 'i'}, {'ì', 'i'}, {'ï', 'i'},
            {'ó', 'o'}, {'ò', 'o'}, {'ö', 'o'},
            {'ú', 'u'}, {'ù', 'u'}, {'ü', 'u'},
        };

        StringBuilder palabraSinTilde = new StringBuilder();

        for (int i = 0; i < palabra.Length; i++)
        {
            char letra = palabra[i];

            if (vocalesConTilde.ContainsKey(letra))
            {
                palabraSinTilde.Append(vocalesConTilde[letra]);
            }
            else
            {
                palabraSinTilde.Append(letra);
            }
        }

        return palabraSinTilde.ToString();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
