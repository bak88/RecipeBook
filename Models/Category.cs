namespace FinalСertificationRecipeBook.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Recipe> Recipes { get; set; }
        public Category()
        {
            Recipes = new List<Recipe>(); 
        }
    }
}
