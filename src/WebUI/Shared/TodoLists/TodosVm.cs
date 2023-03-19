using Cegeka.Auction.WebUI.Shared.Common;

namespace Cegeka.Auction.WebUI.Shared.TodoLists;

public class TodosVm
{
    public List<LookupDto> PriorityLevels { get; set; } = new();

    public List<TodoListDto> Lists { get; set; } = new();
}
