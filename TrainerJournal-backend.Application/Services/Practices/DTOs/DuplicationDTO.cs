namespace TrainerJournal_backend.Application.Services.Practices.DTOs;

public record DuplicationDTO(
    DateOnly DateStartCopy,
    DateOnly DateEndCopy,
    DateOnly DateStartPaste,
    DateOnly DateEndCPaste
);