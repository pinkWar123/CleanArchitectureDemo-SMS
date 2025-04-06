using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.Features.Students.Commands.ImportStudents;

public record ImportStudentsCommand(Stream data, ImportStrategy strategy) : ICommand;