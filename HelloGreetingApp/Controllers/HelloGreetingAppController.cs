using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using NLog;
using RepositoryLayer.Services;

[ApiController]
[Route("[controller]")]
public class HelloGreetingAppController : ControllerBase
{
    private GreetingBL _greeting;
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    public HelloGreetingAppController(GreetingBL greeting){
        _greeting = greeting;
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
