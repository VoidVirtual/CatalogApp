using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Models;

public abstract class CatalogItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id {get; set;} = Guid.NewGuid();

    [Required]
    public string Name {get; set;}

    public Category? Parent {get; set;}

    public Guid? ParentId {get; set;}
}