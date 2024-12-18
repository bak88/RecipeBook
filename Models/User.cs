﻿namespace FinalСertificationRecipeBook.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public virtual List<Recipe> Recipes { get; set; }
        public User()
        {
            Recipes = new List<Recipe>();
        }
    }
}
