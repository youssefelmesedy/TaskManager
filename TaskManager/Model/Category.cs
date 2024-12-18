namespace TaskManager.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // علاقة الربط مع Task
        public ICollection<Tasks>? Tasks { get; set; } 

    }
}
