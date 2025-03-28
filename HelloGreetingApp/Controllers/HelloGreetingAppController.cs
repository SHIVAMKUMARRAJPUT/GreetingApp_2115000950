using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using NLog;
using RepositoryLayer.Services;
using BusinessLayer.Interface;
using RepositoryLayer.Interface;
using Middleware.GlobalExceptionHandler;


/// <summary>
/// Class Providing API for HelloGreetingApp
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class HelloGreetingAppController : ControllerBase
{
    private IGreetingBL _greetingBL;
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    public HelloGreetingAppController(IGreetingBL greeting)
    {
        _greetingBL = greeting;
    }
    //UC3

    // <summary>
    // To Print Greeting Message
    // </summary>
    // <returns> "Hello World" </returns>
    [HttpPost("Hello")]
    public IActionResult GetGreeting([FromBody] RequestModel model)
    {

        string greetingMessage = _greetingBL.GetGreeting(model);
        return Ok(new { Message = greetingMessage });
    }

    //UC2

    [HttpGet("greet")]
    public IActionResult GetHello()
    {
        try
        {
            logger.Info("GREET request received.");
            return Ok(new { Success = true, Message = "Hello, welcome to our API!", data = _greetingBL.SayHello() });
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error occurred in GREET request.");
            return StatusCode(500, new { Success = false, Message = "Internal Server Error" });
        }
    }

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            logger.Info("GET request received.");
            List<RequestModel> data = _greetingBL.GetAll();
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
            _greetingBL.Add(model);
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

            var existingEntry = _greetingBL.GetById(id);
            if (existingEntry == null)
            {
                logger.Warn($"PUT request failed - Record with ID {id} not found.");
                return NotFound(new { Success = false, Message = "Record not found" });
            }

            updatedModel.id = id;
            _greetingBL.Update(updatedModel);
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

            var existingEntry = _greetingBL.GetById(id);
            if (existingEntry == null)
            {
                logger.Warn($"PATCH request failed - Record with ID {id} not found.");
                return NotFound(new { Success = false, Message = "Record not found" });
            }

            if (!string.IsNullOrEmpty(updatedModel.name))
                existingEntry.name = updatedModel.name;

            if (!string.IsNullOrEmpty(updatedModel.lname))
                existingEntry.lname = updatedModel.lname;

            _greetingBL.Update(existingEntry);

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

            var existingEntry = _greetingBL.GetById(id);
            if (existingEntry == null)
            {
                logger.Warn($"DELETE request failed - Record with ID {id} not found.");
                return NotFound(new { Success = false, Message = "Record not found" });
            }

            _greetingBL.Delete(id);
            return Ok(new { Success = true, Message = "Record deleted successfully" });
        }
        catch (Exception ex)
        {
            logger.Error(ex, $"Error occurred in DELETE request for ID {id}");
            return StatusCode(500, new { Success = false, Message = "Internal Server Error" });
        }
    }

    //UC4

    [HttpPost]
    [Route("save")]

    public IActionResult SaveGreeting([FromBody] GreetingModel greetingModel)
    {
        var result = _greetingBL.SaveGreetingBL(greetingModel);
        if (result == null)
        {
            var response1 = new ResponseModel<object>
            {
                Success = false,
                Message = "Unable to save Greeting. Please verify that the user exists. !",
                Data = result

            };
            return BadRequest(response1);

        }
        var response = new ResponseModel<object>
        {
            Success = true,
            Message = "Greeting Created",
            Data = result

        };
        return Created("Greeting Created", response);

    }


    //UC5

    [HttpGet("GetGreetingById/{id}")]
    public IActionResult GetGreetingById(int id)
    {
        try
        {
            var entity = _greetingBL.GetGreetingByIdBL(id);
            if (entity != null)
            {
                var model = new GreetingModel
                {
                    Id = entity.Id,
                    Message = entity.Message,
                    Uid = entity.Uid
                };
                return Ok(new ResponseModel<GreetingModel>
                {
                    Success = true,
                    Message = "Greeting Message Found",
                    Data = model
                });
            }
            return NotFound(new ResponseModel<GreetingModel>
            {
                Success = false,
                Message = "Greeting Message Not Found"
            });
        }
        catch (Exception ex)
        {
            var errorResponse = ExceptionHandler.CreateErrorResponse(ex);
            return StatusCode(500, errorResponse);
        }
    }



//UC6


    [HttpGet("GetAllGreetings")]
    public IActionResult GetAllGreetings()
    {
        try
        {
            var entities = _greetingBL.GetAllGreetingsBL();
            if (entities != null && entities.Count > 0)
            {
                var models = entities.Select(entity => new GreetingModel
                {
                    Id = entity.Id,
                    Message = entity.Message,
                    Uid = entity.Uid
                }).ToList();

                return Ok(new ResponseModel<List<GreetingModel>>
                {
                    Success = true,
                    Message = "Greeting Messages Found",
                    Data = models
                });
            }
            return NotFound(new ResponseModel<List<GreetingModel>>
            {
                Success = false,
                Message = "No Greeting Messages Found"
            });
        }
        catch (Exception ex)
        {
            var errorResponse = ExceptionHandler.CreateErrorResponse(ex);
            return StatusCode(500, errorResponse);
        }
    }


    //UC7

    [HttpPut("EditGreeting/{id}")]
    public IActionResult EditGreeting(int id, [FromBody] GreetingModel greetModel)
    {
        try
        {
            var entity = _greetingBL.EditGreetingBL(id, greetModel);
            if (entity == null)
            {
                return BadRequest(new ResponseModel<object>
                {
                    Success = false,
                    Message = $"No Greeting found with ID {id} to update!"
                });
            }

            var updatedModel = new GreetingModel
            {
                Id = entity.Id,
                Message = entity.Message,
                Uid = entity.Uid
            };

            return Ok(new ResponseModel<GreetingModel>
            {
                Success = true,
                Message = "Greeting Message Updated Successfully",
                Data = updatedModel
            });
        }
        catch (Exception ex)
        {
            var errorResponse = ExceptionHandler.CreateErrorResponse(ex);
            return StatusCode(500, errorResponse);
        }
    }


    //UC8

    [HttpDelete("DeleteGreeting/{id}")]
    public IActionResult DeleteGreeting(int id)
    {
        try
        {
            bool result = _greetingBL.DeleteGreetingBL(id);
            if (result)
            {
                return Ok(new ResponseModel<string>
                {
                    Success = true,
                    Message = "Greeting Message Deleted Successfully"
                });
            }
            return NotFound(new ResponseModel<string>
            {
                Success = false,
                Message = "Greeting Message Not Found"
            });
        }
        catch (Exception ex)
        {
            var errorResponse = ExceptionHandler.CreateErrorResponse(ex);
            return StatusCode(500, errorResponse);
        }
    }
}