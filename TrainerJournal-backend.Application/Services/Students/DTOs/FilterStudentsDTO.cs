using TrainerJournal_backend.Domain.Enums;

namespace TrainerJournal_backend.Application.Services.Students.DTOs;

public record FilterStudentsDTO(
    HashSet<int> Classes,
    DateOnly StartDateOfBirth,
    DateOnly EndDateOfBirth,
    HashSet<int> Kyues,
    HashSet<Gender> Genders);