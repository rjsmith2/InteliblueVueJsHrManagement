// This file is provided freely for use, modification, and distribution without restriction. 
// No specific license applies, and you are free to share, modify, and redistribute this file as needed.

using System.Net.Mime;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using InteliblueVueJsHrManagement.Server.Models;
using InteliblueVueJsHrManagement.Server.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InteliblueVueJsHrManagement.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly ILogger<EmployeesController> _logger;
    private readonly HumanResourceDbContext _db;
    private readonly IMapper _mapper;

    public EmployeesController(ILogger<EmployeesController> logger, HumanResourceDbContext db, IMapper mapper)
    {
        _logger = logger;
        _db = db;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType<List<Result<Employee>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var employees = await GetEmployeeQueryable().ToListAsync();
        return Ok(Result.Success(employees));
    }

    [HttpGet("{id}")]
    [ProducesResponseType<Result<Employee>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var employee = await GetEmployeeQueryable().FirstOrDefaultAsync(e => e.Id == id);
        return employee == null ? NotFound() : Ok(Result.Success(employee));
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(NewEmployee model)
    {
        try
        {
            var department = await _db.Departments.FirstOrDefaultAsync(d => d.Id == model.Department);
            if (department == null)
            {
                return BadRequest(Result.Failure("Department does not exist"));
            }
            _db.Employees.Add(new Models.DbSets.Employee
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                Department = department,
            });
            await _db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Conflict(Result.Failure("Error creating a new employee"));
        }
        return Ok(Result.Success());
    }

    [HttpPut("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put(int id, EditEmployee model)
    {
        Models.DbSets.Department department;
        try
        {
            // grab's employee's new department and update it.
            department = await _db.Departments.FirstOrDefaultAsync(d => d.Id == model.Department);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest(Result.Failure("There was an error processing your request."));
        }

        if (department == null)
        {
            return BadRequest(Result.Failure("Department does not exist"));
        }

        try
        {
            var employee = _db.Employees.First(e => e.Id == id);
            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.Email = model.Email;
            employee.Phone = model.Phone;
            employee.Department = department;
            await _db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest(Result.Failure("An error occurred while saving the employee. Please try again."));
        }
        return Ok(Result.Success());
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            _db.Employees.Remove(new Models.DbSets.Employee { Id = id });
            await _db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Conflict(Result.Failure("Error deleting a new employee"));
        }
        return Ok(Result.Success());
    }

    private IQueryable<Employee> GetEmployeeQueryable() => _db.Employees.ProjectTo<Employee>(_mapper.ConfigurationProvider);



}
