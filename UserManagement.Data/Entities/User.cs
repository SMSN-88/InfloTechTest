using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Models;

public class User
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Required, StringLength(50)]
    public string Forename { get; set; } = default!;
    [Required, StringLength(50)]
    public string Surname { get; set; } = default!;
    [Required, StringLength(100), EmailAddress]
    public string Email { get; set; } = default!;

    public bool IsActive { get; set; }
    [Required, DataType(DataType.Date)]
    public DateTime? DateOfBirth { get; set; }
    
}
