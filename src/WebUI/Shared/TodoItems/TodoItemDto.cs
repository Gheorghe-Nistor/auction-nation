namespace Cegeka.Auction.WebUI.Shared.TodoItems;

public class TodoItemDto
{
    public int Id { get; set; }

    public int ListId { get; set; }

    public string Title { get; set; } = string.Empty;

    public bool Done { get; set; }

    public int Priority { get; set; }

    public string Note { get; set; } = string.Empty;
}
