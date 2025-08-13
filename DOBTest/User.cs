namespace DOBTest;

internal class User
{
    public long Id { get; set; }
    public string? Forename { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; }
    public DateTime? DateOfBirth { get; set; }

    //internal ViewResult Id(object value) => throw new NotImplementedException();
}
