using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DWES.Models;
using System.Text.RegularExpressions;

namespace DWES.Controllers;

public class Ejercicio3Controller : Controller
{
    private readonly ILogger<Ejercicio3Controller> _logger;

    public Ejercicio3Controller(ILogger<Ejercicio3Controller> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Solucion(string cadena)
    {
        if (string.IsNullOrWhiteSpace(cadena))
        {
            return RedirectToAction("Index");
        }

        int masVocales = 0;
        string palabraMasVocales = "";

        int menosVocales = 10;
        string palabraMenosVocales = "";

        String[] cadenaArray = Regex.Split(cadena, @"\s|,\s");

        foreach (string palabra in cadenaArray)
        {
            MatchCollection coincidencias = Regex.Matches(palabra, @"[aeiouáéíóúàèìòùäëïöü]", RegexOptions.IgnoreCase);

            if (coincidencias.Count > masVocales)
            {
                masVocales = coincidencias.Count;
                palabraMasVocales = palabra;
            }

            if (coincidencias.Count < menosVocales && coincidencias.Count > 0)
            {
                menosVocales = coincidencias.Count;
                palabraMenosVocales = palabra;
            }
        }

        ViewBag.MasVocalesNum = masVocales;
        ViewBag.MasVocales = palabraMasVocales;
        ViewBag.MenosVocalesNum = menosVocales;
        ViewBag.MenosVocales = palabraMenosVocales;
        ViewBag.Cadena = cadena;

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
