namespace ToDoApplication.Common.Models.Domain.Request
{
    public class CreateTodoRequest
    {
        public DateTime ExpirationDate { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? CompletePercent { get; set; }
    }
}
