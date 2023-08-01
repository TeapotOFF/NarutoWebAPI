using NarutoCharactersAPI.Data;
using NarutoCharactersAPI.Models;
using System.Diagnostics.Metrics;

namespace NarutoCharactersAPI
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.SpellCharacters.Any())
            {
                var spellCharacters = new List<SpellCharacter>()
                {
                    new SpellCharacter()
                    {
                        Character = new Character()
                        {
                            Name = "Naruto Uzumaki",
                            BirthDate = new DateTime(2009,10,10),
                            Affiliation = new Affiliation() {Name = "Hidden Leaf Village"},
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Naruto Uzumaki",Text = "Naruto is the best", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Naruto Uzumaki", Text = "Naruto gg", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title="Naruto Uzumaki",Text = "Naruto, Naruto, Naruto", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Spell = new Spell()
                        {
                            Name = "Shadow cloning technique",
                            Description = "The user of this technique, thanks to his chakra, copies himself. The number of clones depends on the amount of chakra spent. " +
                                          "These clones are not independent beings and cannot do anything of their own free will.",
                            SpellTypes = new List<SpellType>()
                            {
                                new SpellType {Type = new Models.Type(){
                                    Name = "Ninjutsu"
                                }}
                            }
                        }
                    },
                    new SpellCharacter()
                    {
                        Character = new Character()
                        {
                            Name = "Sasuke Uchiha",
                            BirthDate = new DateTime(2009,07,24),
                            Affiliation = new Affiliation() {Name = "Akatsuki"},
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Sasuke Uchiha",Text = "Sasuke is the best", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Sasuke Uchiha", Text = "Sasuke gg", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title="Sasuke Uchiha",Text = "Sasuke, Sasuke, Sasuke", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Spell = new Spell()
                        {
                            Name = "Amaterasu",
                            Description = "Focusing on the target, the user ignites it with a black flame that will burn for seven days and seven nights until the user stops using the technique. " +
                            "This is the most powerful Fire Element technique, but its frequent use can lead to loss of vision.",
                            SpellTypes = new List<SpellType>()
                            {
                                new SpellType {Type = new Models.Type(){
                                    Name = "Dojutsu"
                                }}
                            }
                        }
                    },
                    new SpellCharacter()
                    {
                        Character = new Character()
                        {
                            Name = "Sakura Haruno",
                            BirthDate = new DateTime(2009,10,10),
                            Affiliation = new Affiliation() {Name = "Hidden Leaf Village"},
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Sakura Haruno",Text = "Sakura is the best", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Sakura Haruno", Text = "Sakura gg", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title="Sakura Haruno",Text = "Sakura, Sakura, Sakura", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Spell = new Spell()
                        {
                            Name = "High-strength equipment",
                            Description = "With the help of the chakra, the user strengthens his body, which makes it possible not to spare his hands and feet in battle.",
                            SpellTypes = new List<SpellType>()
                            {
                                new SpellType {Type = new Models.Type(){
                                    Name = "Taijutsu"
                                }}
                            }
                        }
                    }
                };
                dataContext.SpellCharacters.AddRange(spellCharacters);
                dataContext.SaveChanges();
            }
        }
    }
}