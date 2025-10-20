namespace ToDoList.WebApi;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

[Route("api/[controller]")]
[ApiController]
public class ToDoItemsController : ControllerBase
{
    private static readonly List<ToDoItem> items = [];

    [HttpPost]
    public ActionResult<ToDoItemGetResponseDto> Create([FromBody] ToDoItemCreateRequestDto request)
    {

        //map to Domain object as soon as possible
        var item = request.ToDomain();

        //try to create an item
        try
        {
            item.ToDoItemId = items.Count == 0 ? 1 : items.Max(o => o.ToDoItemId) + 1;
            items.Add(item);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return Created(); //201 //tato metoda z nějakého důvodu vrací status code No Content 204, zjištujeme proč ;)
    }

    [HttpGet]
    public ActionResult<IEnumerable<ToDoItemGetResponseDto>> Read()
    {

        try
        {
            if (items == null)
            {
                return NotFound();
            }

            var dto = items.Select(ToDoItemGetResponseDto.FromDomain);
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

            var item = items.FirstOrDefault(i => i.ToDoItemId == toDoItemId);

            if (item == null)
            {
                return NotFound();
            }

            var dto = ToDoItemGetResponseDto.FromDomain(item);
            return Ok(dto);
        }
        catch (Exception ex)
        {
            //500
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{toDoItemId:int}")]
    public ActionResult<ToDoItemUpdateRequestDto> UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {

        try
        {
            var item = items.FirstOrDefault(i => i.ToDoItemId == toDoItemId);
            if (item == null)
            {
                return NotFound();
            }

            item.Name = request.Name;
            item.Description = request.Description;
            item.IsCompleted = request.IsCompleted;

            var dto = ToDoItemGetResponseDto.FromDomain(item);
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
            var item = items.FirstOrDefault(i => i.ToDoItemId == toDoItemId);
            if (item == null)
            {
                return NotFound();
            }

            items.Remove(item);

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
        items.Add(item);
    }

}
