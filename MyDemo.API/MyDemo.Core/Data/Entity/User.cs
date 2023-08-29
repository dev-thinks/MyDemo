using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyDemo.Core.Data.Entity;

[Table("User")]
[Index(nameof(Id))]
public class User : BaseEntity{  
  public string Name { get; set; }
  public string Password { get; set; }
  public string Email { get; set; }
}


public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.CreatedAt).HasDefaultValue(DateTime.Now);
    }
}