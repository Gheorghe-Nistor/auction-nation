using Cegeka.Auction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.Infrastructure.Data.Configurations
{
    public class AuctionItemConfiguration: IEntityTypeConfiguration<AuctionItem>
    {
        public void Configure(EntityTypeBuilder<AuctionItem> builder)
        {
            builder.Property(t => t.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Description)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(t => t.Category)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.Images)
                .IsRequired()
                .HasConversion(
                    images => string.Join("/////", images),
                    str => str.Split("/////", StringSplitOptions.RemoveEmptyEntries).ToList(),
                    new ValueComparer<List<string>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()
                ));

            builder.Property(t => t.StartingBidAmount)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(t => t.CurrentBidAmount)
                .HasColumnType("decimal(10,2)");

            builder.Property(t => t.BuyItNowPrice)
                .IsRequired() 
                .HasColumnType("decimal(10,2)");

            builder.Property(t => t.ReservePrice)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(t => t.DeliveryMethod)
                .IsRequired();

            builder.Property(t => t.Status)
                .IsRequired();

            builder.Property(t => t.StartDate)
                .IsRequired();

            builder.Property(t => t.EndDate)    
                .IsRequired();
        }
    }
}
