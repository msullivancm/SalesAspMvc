﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SalesWebMvc.Models;

namespace SalesWebMvc.Controllers
{
    public class EnderecoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Models.Cep cep)
        {
            if (!ModelState.IsValid)
            {
                return View(cep);
            }

            var correios = new Correios.AtendeClienteClient();

            var consulta = correios.consultaCEPAsync(cep.Codigo.Replace("-", "")).Result;

            if (consulta != null)
            {
                ViewBag.Endereco = new Models.Endereco()
                {
                    Descricao = consulta.@return.end,
                    Complemento = consulta.@return.complemento2,
                    Bairro = consulta.@return.bairro,
                    Cidade = consulta.@return.cidade,
                    UF = consulta.@return.uf
                };
            }

            return View(cep);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
