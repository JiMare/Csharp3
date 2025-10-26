namespace ToDoList.WebApi;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence;

[Route("api/[controller]")]
[ApiController]
public class ToDoItemsController : ControllerBase
{
    private readonly ToDoItemsContext context;

    public ToDoItemsController(ToDoItemsContext context)
    {
        this.context = context;

        //ToDoItem item = new ToDoItem { Name = "Prvni ukol", Description = "Prvni popisek", IsCompleted = false };

        //context.ToDoItems.Add(item);
        //context.SaveChanges();
    }

    [HttpPost]
    public ActionResult<ToDoItemGetResponseDto> Create([FromBody] ToDoItemCreateRequestDto request)
    {

        //map to Domain object as soon as possible
        var item = request.ToDomain();

        //try to create an item
        try
        {
            // item.ToDoItemId = items.Count == 0 ? 1 : items.Max(o => o.ToDoItemId) + 1;
            // items.Add(item);
            context.ToDoItems.Add(item);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        //  return Created(); //201 //tato metoda z nějakého důvodu vrací status code No Content 204, zjištujeme proč ;)
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
            var dbItems = context.ToDoItems.ToList();
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

            var dbItem = context.ToDoItems.FirstOrDefault(i => i.ToDoItemId == toDoItemId);

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
            var dbItem = context.ToDoItems.FirstOrDefault(i => i.ToDoItemId == toDoItemId);
            if (dbItem == null)
            {
                return NotFound();
            }

            dbItem.Name = request.Name;
            dbItem.Description = request.Description;
            dbItem.IsCompleted = request.IsCompleted;
            context.SaveChanges();

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
            var dbItems = context.ToDoItems.ToList();
            var dbItem = dbItems.FirstOrDefault(i => i.ToDoItemId == toDoItemId);
            if (dbItem == null)
            {
                return NotFound();
            }

            context.ToDoItems.Remove(dbItem);
            context.SaveChanges();

            return NoContent();
        }
        catch (Exception ex)
        {
            //500
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    public void AddItemToStorage(ToDoItem item)
    {
        context.ToDoItems.Add(item);
        context.SaveChanges();
    }

}
