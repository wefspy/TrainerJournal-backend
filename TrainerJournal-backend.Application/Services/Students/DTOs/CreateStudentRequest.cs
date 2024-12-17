namespace TrainerJournal_backend.Application.Services.Students.DTOs;

public record CreateStudentRequest(
    Guid GroupId,
    StudentInfoItemDTO StudentInfoItemDto,
    List<ContactItemDTO> Contacts
);