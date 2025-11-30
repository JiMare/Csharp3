namespace ToDoList.WebApi;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;

[Route("api/[controller]")]
[ApiController]
public class ToDoItemsController : ControllerBase
{

    private readonly IRepositoryAsync<ToDoItem> repositoryAsync;

    public ToDoItemsController(IRepositoryAsync<ToDoItem> repositoryAsync)
    {
        this.repositoryAsync = repositoryAsync;
    }

    [HttpPost]
    public async Task<ActionResult<ToDoItemGetResponseDto>> CreateAsync([FromBody] ToDoItemCreateRequestDto request)
    {

        //map to Domain object as soon as possible
        var item = request.ToDomain();

        //try to create an item
        try
        {
            await repositoryAsync.CreateAsync(item);

        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        var dto = ToDoItemGetResponseDto.FromDomain(item);
        return CreatedAtAction(
            nameof(ReadByIdAsync),
            new { toDoItemId = item.ToDoItemId },
            dto
        );
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDoItemGetResponseDto>>> ReadAsync()
    {

        try
        {
            var dbItems = await repositoryAsync.ReadAsync();
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
    public async Task<ActionResult<ToDoItemGetResponseDto>> ReadByIdAsync(int toDoItemId)
    {
        try
        {

            var dbItem = await repositoryAsync.ReadByIdAsync(toDoItemId);

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
    public async Task<ActionResult<ToDoItemGetResponseDto>> UpdateByIdAsync(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {

        try
        {
            var dbItem = await repositoryAsync.UpdateByIdAsync(toDoItemId, item =>
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
    public async Task<IActionResult> DeleteByIdAsync(int toDoItemId)
    {
        try
        {

            var result = await repositoryAsync.DeleteByIdAsync(toDoItemId);

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
