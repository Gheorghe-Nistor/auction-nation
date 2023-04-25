using Cegeka.Auction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.Infrastructure.Data.Configurations
{
    public class BidConfiguration: IEntityTypeConfiguration<Bid>
    {
        public void Configure(EntityTypeBuilder<Bid> builder)
        {
            builder.Property(t => t.Amount)
                .HasColumnType("decimal(10,2)")
                .IsRequired();
        }
    }
}
