﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirstApi.Models;

namespace MyFirstApi.Controllers
{
    [Route("api/pizzas")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        static List<Pizza> _pizzas;

        static PizzasController()
        {
            var pizza = new Pizza {Id = 1,Size= "Medium",Toppings = new List<string>{"Pepperoni"}};
            var pizza2 = new Pizza {Id = 2,Size= "Medium",Toppings = new List<string>{"Sausage"}};
            var pizza3 = new Pizza {Id = 3,Size= "Medium",Toppings = new List<string>{"Bacon"}};

            _pizzas = new List<Pizza> {pizza,pizza2,pizza3};
        }

        [HttpGet]
        public List<Pizza> GetAllPizzas()
        {
           return _pizzas;
        }

        //api/pizzas/{id}
        //api/pizzas/2
        [HttpGet("{id}")]
        public IActionResult GetPizzaById(int id)
        {
            var result = _pizzas.SingleOrDefault(pizza => pizza.Id == id);

            if (result == null)
            {
                return NotFound($"Could not find a pizza with the id {id}");
            }

            return Ok(result);
        }

        //api/pizzas
        [HttpPost]
        public IActionResult CreatePizza(CreatePizzaCommand command)
        {
            var newPizza = new Pizza {Size = command.Size};
            newPizza.Id = _pizzas.Select(p => p.Id).Max() + 1;

            _pizzas.Add(newPizza);

            return Created($"api/pizzas/{newPizza.Id}", newPizza);
        }

    }
}
