namespace TrainerJournal_backend.Domain.Entities;

/// <summary>
/// Базовый класс для сущностей домена, определяющий уникальный идентификатор.
/// </summary>
/// <typeparam name="T">Тип идентификатора (например, <see cref="Guid"/>).</typeparam>
public abstract class Entity<T>
{
    /// <summary>
    /// Уникальный идентификатор сущности.
    /// </summary>
    public T Id { get; set; }
}