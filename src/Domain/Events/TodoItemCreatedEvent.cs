using Cegeka.Auction.Domain.Common;
using Cegeka.Auction.Domain.Entities;

namespace Cegeka.Auction.Domain.Events;

public class TodoItemCreatedEvent : BaseEvent
{
    public TodoItemCreatedEvent(TodoItem item)
    {
        Item = item;
    }

    public TodoItem Item { get; }
}
