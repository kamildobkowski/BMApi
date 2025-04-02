using Api.Extensions;
using Application.Features.Menu.AddMenuItem;
using Application.Features.Menu.DeleteMenuItem;
using Application.Features.Menu.GetMenu;
using Application.Features.Menu.UpdateMenuItem;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType<ErrorResult>(400)]
[ProducesResponseType<ErrorResult>(404)]
[ProducesResponseType<ErrorResult>(500)]
public class MenuController(IMediator mediator) : ControllerBase
{
	[HttpGet]
	[ProducesResponseType<GetMenuResponse>(200)]
	public async Task<IActionResult> GetMenu([FromQuery] GetMenuQuery query)
		=> (await mediator.Send(query)).ToActionResult();
	
	[HttpPost]
	[ProducesResponseType<AddMenuItemResponse>(201)]
	public async Task<IActionResult> AddMenuItem([FromBody] AddMenuItemCommand command)
		=> (await mediator.Send(command)).ToActionResult(201);
	
	[HttpDelete("{id}")]
	[ProducesResponseType<DeleteMenuItemResponse>(204)]
	public async Task<IActionResult> DeleteMenuItem([FromRoute] int id)
		=> (await mediator.Send(new DeleteMenuItemCommand(id))).ToActionResult(204);
	
	[HttpPut]
	[ProducesResponseType<UpdateMenuItemResponse>(200)]
	public async Task<IActionResult> UpdateMenuItem([FromBody] UpdateMenuItemCommand command)
		=> (await mediator.Send(command)).ToActionResult();
}