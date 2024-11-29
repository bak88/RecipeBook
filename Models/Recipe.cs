namespace FinalСertificationRecipeBook.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual User Author { get; set; }
        public virtual List<Ingredient> Ingredients { get; set; }
        public virtual Category Category { get; set; }
        public Recipe()
        {
            Ingredients = new List<Ingredient>();
        }
    }

}
