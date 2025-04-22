namespace ToDoApplication.Common.Models.Database
{
    public class TodoItem
    {
        public int Id { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? CompletePercent { get; set; }

    }
}
