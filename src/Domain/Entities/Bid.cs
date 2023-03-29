﻿using Cegeka.Auction.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.Domain.Entities;

public class Bid : BaseAuditableEntity
{
    public int Id { get; set; }
    public Guid PublicId { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public AuctionItem AuctionItem { get; set; }
}
