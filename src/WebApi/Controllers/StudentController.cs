using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Students.Commands.CreateStudent;
using Application.Features.Students.Queries.FindStudentByStudentId;
using Application.Features.Students.Queries.FindStudents;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentByStudentId([FromRoute] string id)
        {
            var query = new FindStudentByStudentIdQuery(id);
            var student = await _mediator.Send(query);
            return student.Match(Ok, CustomResults.Problem);
        }

        [HttpGet]
        public async Task<IActionResult> GetStudent()
        {
            var query = new FindStudentsQuery();
            var student = await _mediator.Send(query);
            return student.Match(Ok, CustomResults.Problem);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentDto dto)
        {
            var command = new CreateStudentCommand(dto);
            var student = await _mediator.Send(command);
            return student.Match(
                student => CreatedAtAction(nameof(CreateStudentCommandHandler), new { id = student.Value.StudentId }, student),
                error => CustomResults.Problem(error)
            );
        }
    }
}