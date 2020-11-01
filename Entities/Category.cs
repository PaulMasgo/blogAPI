namespace BlogApi.Entities
{
    public class Category : BaseEntity
    {
        public int Name { get; set; }
        public string Order { get; set; }
        public string Image { get; set; }
    }
}