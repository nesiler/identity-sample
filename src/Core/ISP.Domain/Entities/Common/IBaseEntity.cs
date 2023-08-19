namespace ISP.Domain.Entities.Common;

public interface IBaseEntity : IEntity, IAuditableEntity, ISoftDelete
{
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }
}