namespace Cegeka.Auction.Application.Common.Services.Data;

public interface IApplicationDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<AuctionItem> AuctionItems { get; }

    DbSet<Bid> Bids { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
