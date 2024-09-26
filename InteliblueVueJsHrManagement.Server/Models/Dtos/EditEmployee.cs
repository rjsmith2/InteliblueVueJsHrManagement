// This file is provided freely for use, modification, and distribution without restriction. 
// No specific license applies, and you are free to share, modify, and redistribute this file as needed.


using System.ComponentModel.DataAnnotations;

namespace InteliblueVueJsHrManagement.Server.Models.Dtos;

public class EditEmployee
{
    [Required(ErrorMessage = "First Name is required")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Last Name is required")]
    public string LastName { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    [Phone]
    public string? Phone { get; set; }
    [Required(ErrorMessage = "Department is required")]
    public int Department { get; set; }
}
