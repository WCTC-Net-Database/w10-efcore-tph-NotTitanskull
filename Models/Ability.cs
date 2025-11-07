namespace W9_assignment_template.Models;

public abstract class Ability
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<Character> Characters { get; set; } = new List<Character>();
}