namespace students_Api.DTOs
{
    public record CreateStudentDTO(
        string FirstName,
        string LastName,
        string Email,
        string ClassName
    );
}
