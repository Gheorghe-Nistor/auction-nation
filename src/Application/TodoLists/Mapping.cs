using Cegeka.Auction.WebUI.Shared.TodoItems;
using Cegeka.Auction.WebUI.Shared.TodoLists;

namespace Cegeka.Auction.Application.TodoLists;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<TodoList, TodoListDto>();
        CreateMap<TodoItem, TodoItemDto>();
    }
}
