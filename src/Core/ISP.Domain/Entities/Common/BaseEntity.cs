using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISP.Domain.Entities.Common;

public abstract class BaseEntity : IBaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public string Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();
    public DateTime? UpdatedAt { get; set; }

    public string CreatedBy { get; set; } = "System";
    public string? UpdatedBy { get; set; }

    public bool IsDeleted { get; set; } = false;
    public string DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
}