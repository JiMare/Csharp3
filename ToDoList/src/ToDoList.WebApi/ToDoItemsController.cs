namespace ToDoList.WebApi;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;

[Route("api/[controller]")]
[ApiController]
public class ToDoItemsController : ControllerBase
{
    private readonly ToDoItemsContext context;
    private readonly IRepository<ToDoItem> repository;

    public ToDoItemsController(IRepository<ToDoItem> repository)
    {
        this.repository = repository;
    }

    [HttpPost]
    public ActionResult<ToDoItemGetResponseDto> Create([FromBody] ToDoItemCreateRequestDto request)
    {

        //map to Domain object as soon as possible
        var item = request.ToDomain();

        //try to create an item
        try
        {
            repository.Create(item);

        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        var dto = ToDoItemGetResponseDto.FromDomain(item);
        return CreatedAtAction(
            nameof(ReadById),
            new { toDoItemId = item.ToDoItemId },
            dto
        );
    }

    [HttpGet]
    public ActionResult<IEnumerable<ToDoItemGetResponseDto>> Read()
    {

        try
        {
            var dbItems = repository.Read();
            if (dbItems == null)
            {
                return NotFound();
            }

            var dto = dbItems.Select(ToDoItemGetResponseDto.FromDomain);
            return Ok(dto);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

    }

    [HttpGet("{toDoItemId:int}")]
    public ActionResult<ToDoItemGetResponseDto> ReadById(int toDoItemId)
    {
        try
        {

            var dbItem = repository.ReadById(toDoItemId);

            if (dbItem == null)
            {
                return NotFound();
            }

            var dto = ToDoItemGetResponseDto.FromDomain(dbItem);
            return Ok(dto);
        }
        catch (Exception ex)
        {
            //500
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{toDoItemId:int}")]
    public ActionResult<ToDoItemGetResponseDto> UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {

        try
        {
            var dbItem = repository.UpdateById(toDoItemId, item =>
             {
                 item.Name = request.Name;
                 item.Description = request.Description;
                 item.IsCompleted = request.IsCompleted;
             });

            if (dbItem == null)
            {
                return NotFound();
            }

            var dto = ToDoItemGetResponseDto.FromDomain(dbItem);
            return Ok(dto);
        }
        catch (Exception ex)
        {
            //500
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{toDoItemId:int}")]
    public IActionResult DeleteById(int toDoItemId)
    {
        try
        {

            var result = repository.DeleteById(toDoItemId);

            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            //500
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

}
