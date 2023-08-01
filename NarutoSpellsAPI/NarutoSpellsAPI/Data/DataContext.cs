using Microsoft.EntityFrameworkCore;
using NarutoCharactersAPI.Models;

namespace NarutoCharactersAPI.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Spell> Spells { get; set; }
        public DbSet<Models.Type> Types { get; set; }
        public DbSet<Affiliation> Affiliation { get; set;}
        public DbSet<Reviewer> Reviewers { get; set; }  
        public DbSet<SpellCharacter> SpellCharacters { get; set; }
        public DbSet<SpellType> SpellTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SpellType>().HasKey(st => new {st.SpellId, st.TypeId });
            modelBuilder.Entity<SpellType>().HasOne(s => s.Spell).WithMany(st => st.SpellTypes).HasForeignKey(s => s.SpellId);
            modelBuilder.Entity<SpellType>().HasOne(t => t.Type).WithMany(st => st.SpellTypes).HasForeignKey(t => t.TypeId);

            modelBuilder.Entity<SpellCharacter>().HasKey(sc => new { sc.SpellId, sc.CharacterId });
            modelBuilder.Entity<SpellCharacter>().HasOne(s => s.Spell).WithMany(st => st.SpellCharacters).HasForeignKey(s => s.SpellId);
            modelBuilder.Entity<SpellCharacter>().HasOne(t => t.Character).WithMany(st => st.SpellCharacters).HasForeignKey(t => t.CharacterId);
        }
    }
}
