using Business.Models.Dtos;
//using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ClientController : Controller
    {
        //private readonly IClientService _clientService;
        //ClientController(IClientService clientService)
        //{
        //    _clientService = clientService;
        //}

        [HttpPost]
        public IActionResult Add(AddClientForm form)//TODO: change to async when the services are implemented
        {
            Console.WriteLine("AddAsync method called");
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage)
                        .ToArray()
                    );

                return BadRequest(new { sucess = false, errors });
            }
            var result = true; //TODO: remove this line when services are implemented
            //var result = await _clientService.CreateClientAsync(form);
            if (result)
            {
                return Ok(new { sucess = true });
            }
            else
            {
                return Problem("Unable to submitt data");
            }
        }



        [HttpPost]
        public IActionResult Edit(EditClientForm form) //TODO: change to async when the services are implemented
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage)
                        .ToArray()
                    );

                return BadRequest(new { sucess = false, errors });
            }

            var result = true;
            //var result = await _clientService.CreateClientAsync(form);
            if (result)
            {
                return Ok(new { sucess = true });
            }
            else
            {
                return Problem("Unable to submitt data");
            }
        }
    }
}

