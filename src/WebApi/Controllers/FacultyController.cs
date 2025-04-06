using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Faculties.Queries.GetFaculties;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacultyController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FacultyController(IMediator mediator)
        {
            _mediator = mediator;   
        }
        [HttpGet]
        public async Task<IActionResult> GetFaculties()
        {
            var query = new GetFacultiesQuery();
            var faculties = await _mediator.Send(query);
            return faculties.Match(Ok, CustomResults.Problem);
        }
    }
}