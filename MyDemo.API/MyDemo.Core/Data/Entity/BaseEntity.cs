using System.ComponentModel.DataAnnotations.Schema;

namespace MyDemo.Core.Data.Entity;


public abstract class BaseEntity
{    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public bool IsActive { get; set;}
    public bool IsDeleted { get; set;}
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}