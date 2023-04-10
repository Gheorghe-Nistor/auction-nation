namespace Cegeka.Auction.Domain.Enums
{
    public enum Status: int
    {
        Unknown = 0,
        New = 1,
        Submitted = 1,
        Approved = 2,
        Cancelled = 3,
        Finished = 4
    }
}
