namespace ToDoApplication.Common.Models.Domain.Response
{
    public class GetIncomingTodosResponse
    {
        public DateTime ExpirationDate { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? CompletePercent { get; set; }
    }
}
