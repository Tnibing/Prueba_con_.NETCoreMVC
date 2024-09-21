using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DWES.Models;
using System.Text.RegularExpressions;

namespace DWES.Controllers;

public class Ejercicio2Controller : Controller
{
    private readonly ILogger<Ejercicio2Controller> _logger;

    public Ejercicio2Controller(ILogger<Ejercicio2Controller> logger)
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

        string regex = @"[aeiouáéíóúàèìòùäëïöü]";

        MatchCollection coincidencias = Regex.Matches(cadena, regex, RegexOptions.IgnoreCase);

        List<string> totales = new List<string>();

        foreach (Match c in coincidencias)
        {
            totales.Add(c.Value);
        }

        ViewBag.Cadena = cadena;
        ViewBag.Vocales = totales.Count;

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
