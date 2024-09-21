using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DWES.Models;
using System.Text.RegularExpressions;
using System.Collections;

namespace DWES.Controllers;

public class Ejercicio4Controller : Controller
{
    private readonly ILogger<Ejercicio4Controller> _logger;

    public Ejercicio4Controller(ILogger<Ejercicio4Controller> logger)
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
        if (string.IsNullOrWhiteSpace(cadena) || string.IsNullOrWhiteSpace(busqueda))
        {
            return RedirectToAction("Index");
        }

        List<String> listaCoincidencias = new List<String>();

        String[] arrayCadena = Regex.Split(cadena, @"\s");

        foreach (String palabra in arrayCadena)
        {
            MatchCollection coincidencias = Regex.Matches(palabra, busqueda);

            if (coincidencias.Count > 0)
            {
                listaCoincidencias.Add(palabra);
            }
        }

        ViewBag.Coincidencias = listaCoincidencias;
        ViewBag.Cadena = cadena;
        ViewBag.Busqueda = busqueda;

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
