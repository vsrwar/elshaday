using System.ComponentModel.DataAnnotations.Schema;

namespace ElShaday.Domain.ValueObjects;

public abstract class Document
{
    public string Value { get; protected set; }
    [NotMapped] public bool Valid { get; set; }
}