// This file is provided freely for use, modification, and distribution without restriction. 
// No specific license applies, and you are free to share, modify, and redistribute this file as needed.

using AutoMapper;
using AutoMapper.QueryableExtensions;
using InteliblueVueJsHrManagement.Server.Models;
using InteliblueVueJsHrManagement.Server.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InteliblueVueJsHrManagement.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly ILogger<DepartmentsController> _logger;
    private readonly HumanResourceDbContext _db;
    private readonly IMapper _mapper;

    public DepartmentsController(ILogger<DepartmentsController> logger, HumanResourceDbContext db, IMapper mapper)
    {
        _logger = logger;
        _db = db;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType<List<Result<Department>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var departments = await GetDepartmentQueryable().ToListAsync();
        return Ok(Result.Success(departments));
    }

    [HttpGet("{id}")]
    [ProducesResponseType<Result<Department>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var department = await GetDepartmentQueryable().FirstOrDefaultAsync(e => e.Id == id);
        return department == null ? NotFound() : Ok(Result.Success(department));
    }

    private IQueryable<Department> GetDepartmentQueryable() => _db.Departments.ProjectTo<Department>(_mapper.ConfigurationProvider);
}
