namespace Cegeka.Auction.Domain.Enums
{
    public enum Status: int
    {
        Unknown = 0,
        New = 1,
        Submitted = 2,
        Approved = 3,
        InProgress = 4,
        Cancelled = 5,
        Finished = 6
    }
}
