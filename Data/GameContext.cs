using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using W9_assignment_template.Models;

namespace W9_assignment_template.Data;

public class GameContext : DbContext
{
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Ability> Abilities { get; set; }

    public GameContext(DbContextOptions<GameContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure TPH for Character hierarchy
        modelBuilder.Entity<Character>()
            .HasDiscriminator<string>("Discriminator")
            .HasValue<Player>("Player")
            .HasValue<Goblin>("Goblin");

        // Configure TPH for Abilities hierarchy
        modelBuilder.Entity<Ability>()
            .HasDiscriminator<string>("Discriminator")
            .HasValue<PlayerAbility>("PlayerAbility")
            .HasValue<GoblinAbility>("GoblinAbility");

        // Configure many-to-many relationship between Character and Ability
        modelBuilder.Entity<Character>()
            .HasMany(c => c.Abilities)
            .WithMany(a => a.Characters)
            .UsingEntity(j => j.ToTable("CharacterAbilities"));

        // Seed data for Characters
        modelBuilder.Entity<Player>().HasData(
            new Player { Id = 1, Name = "Hero", Level = 5, RoomId = 1 },
            new Player { Id = 2, Name = "Warrior", Level = 3, RoomId = 1 }
        );

        modelBuilder.Entity<Goblin>().HasData(
            new Goblin { Id = 3, Name = "Goblin Scout", Level = 2, RoomId = 2 },
            new Goblin { Id = 4, Name = "Goblin Chief", Level = 4, RoomId = 2 }
        );

        // Seed data for Rooms
        modelBuilder.Entity<Room>().HasData(
            new Room { Id = 1, Name = "Starting Chamber", Description = "A dimly lit entrance hall" },
            new Room { Id = 2, Name = "Goblin Den", Description = "A foul-smelling cave" }
        );
        
        // Seed data for Abilities
        modelBuilder.Entity<PlayerAbility>().HasData(
            new PlayerAbility
                { Id = 1, Name = "Mighty Shove", Description = "Push enemies back with force", Shove = 5 },
            new PlayerAbility { Id = 2, Name = "Power Push", Description = "A powerful pushing attack", Shove = 8 }
        );

        modelBuilder.Entity<GoblinAbility>().HasData(
            new GoblinAbility
                { Id = 3, Name = "Annoying Taunt", Description = "Distract enemies with insults", Taunt = 3 },
            new GoblinAbility { Id = 4, Name = "Rage Taunt", Description = "Enrage enemies to attack you", Taunt = 6 }
        );


        base.OnModelCreating(modelBuilder);
    }
}