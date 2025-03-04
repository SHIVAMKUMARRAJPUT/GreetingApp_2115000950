using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using NLog;
<<<<<<< HEAD
using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Interface;
=======
using BusinessLayer.Interface;
using RepositoryLayer.Services;
>>>>>>> UC2

[ApiController]
[Route("[controller]")]
public class HelloGreetingAppController : ControllerBase
{
    private IGreetingBL _greeting;
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    public HelloGreetingAppController(IGreetingBL greeting){
        _greeting = greeting;
    }

    [HttpGet("greet")]
    public IActionResult GetHello()
    {
<<<<<<< HEAD
        IGreetingBL _greetingBl;
        private readonly ILogger<HelloGreetingAppController> _logger;
        public HelloGreetingAppController(IGreetingBL greetingBl, ILogger<HelloGreetingAppController> logger)
        {
            _logger = logger;
            _greetingBl = greetingBl;
        }

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        static int count = 1;
        public static List<RequestModel> ls = new List<RequestModel>();

        /// <summary>
        /// Helper function to initialize data
        /// </summary>
        public void helper()
=======
        try
>>>>>>> UC2
        {
            logger.Info("GREET request received.");
            return Ok(new { Success = true, Message = "Hello, welcome to our API!",data=_greeting.SayHello() });
        }
<<<<<<< HEAD

        [HttpGet("greet")]
        public IActionResult GetGreeting([FromQuery] string? firstName, [FromQuery] string? lastName)
        {
            _logger.LogInformation("Received GET request in Controller. First Name: {FirstName}, Last Name: {LastName}", firstName, lastName);

            string greetingMessage = _greetingBl.GetGreeting(firstName, lastName);

            _logger.LogInformation("Final Greeting Response: {GreetingMessage}", greetingMessage);
            return Ok(new { Message = greetingMessage });
        }



        [HttpGet("hello")]
        public IActionResult GetHello()
        {
            try
            {
                logger.Info("GET request received.");
                var response = new {
                    Success = true,
                    Message = "Hello World !",
                    Data = _greetingBl.SayHello()
                };
                logger.Info("GET request successful.");
                return Ok(response);
            }
            catch (System.Exception ex)
            {
                logger.Error(ex, "Error occurred in GET request.");
                return StatusCode(500, new { Success = false, Message = "Internal Server Error" });
            }
        }


        /// <summary>
        /// Get method to get the Greeting Message
        /// </summary>
        [HttpGet]
        public IActionResult Get()
=======
        catch (Exception ex)
>>>>>>> UC2
        {
            logger.Error(ex, "Error occurred in GREET request.");
            return StatusCode(500, new { Success = false, Message = "Internal Server Error" });
        }
    }

    [HttpGet]
    public IActionResult Get(){
        try {
            logger.Info("GET request received.");
            var data = _greeting.GetAll();
            return Ok(new { Success = true, Message = "Data fetched successfully", Data = data });
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error occurred in GET request.");
            return StatusCode(500, new { Success = false, Message = "Internal Server Error" });
        }
    }

    [HttpPost]
    public IActionResult Post([FromBody] RequestModel model)
    {
        try
        {
            logger.Info("POST request received.");
            _greeting.Add(model);
            return Ok(new { Success = true, Message = "Request received successfully", Data = model });
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error occurred in POST request.");
            return StatusCode(500, new { Success = false, Message = "Internal Server Error" });
        }
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] RequestModel updatedModel)
    {
        try
        {
            logger.Info($"PUT request received for ID: {id}");

            var existingEntry = _greeting.GetById(id);
            if (existingEntry == null)
            {
                logger.Warn($"PUT request failed - Record with ID {id} not found.");
                return NotFound(new { Success = false, Message = "Record not found" });
            }

            updatedModel.id = id;
            _greeting.Update(updatedModel);
            return Ok(new { Success = true, Message = "Record updated successfully", Data = updatedModel });
        }
        catch (Exception ex)
        {
            logger.Error(ex, $"Error occurred in PUT request for ID {id}");
            return StatusCode(500, new { Success = false, Message = "Internal Server Error" });
        }
    }

    [HttpPatch("{id}")]
    public IActionResult Patch(int id, [FromBody] RequestModel updatedModel)
    {
        try
        {
            logger.Info($"PATCH request received for ID: {id}");

            var existingEntry = _greeting.GetById(id);
            if (existingEntry == null)
            {
                logger.Warn($"PATCH request failed - Record with ID {id} not found.");
                return NotFound(new { Success = false, Message = "Record not found" });
            }

            if (!string.IsNullOrEmpty(updatedModel.name))
                existingEntry.name = updatedModel.name;

            if (!string.IsNullOrEmpty(updatedModel.lname))
                existingEntry.lname = updatedModel.lname;

            _greeting.Update(existingEntry);

            return Ok(new { Success = true, Message = "Record patched successfully", Data = existingEntry });
        }
        catch (Exception ex)
        {
            logger.Error(ex, $"Error occurred in PATCH request for ID {id}");
            return StatusCode(500, new { Success = false, Message = "Internal Server Error" });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            logger.Info($"DELETE request received for ID: {id}");

            var existingEntry = _greeting.GetById(id);
            if (existingEntry == null)
            {
                logger.Warn($"DELETE request failed - Record with ID {id} not found.");
                return NotFound(new { Success = false, Message = "Record not found" });
            }

            _greeting.Delete(id);
            return Ok(new { Success = true, Message = "Record deleted successfully" });
        }
        catch (Exception ex)
        {
            logger.Error(ex, $"Error occurred in DELETE request for ID {id}");
            return StatusCode(500, new { Success = false, Message = "Internal Server Error" });
        }
    }
}
