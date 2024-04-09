using System;
using System.Collections.Generic;

namespace IntexGroup210.Models;

public partial class Order
{
    public int TransactionId { get; set; }

    public int CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    public string? DayOfWeek { get; set; }

    public int? Time { get; set; }

    public string? TypeOfCard { get; set; }

    public string? EntryMode { get; set; }

    public int? Amount { get; set; }

    public string? TypeOfTransaction { get; set; }

    public string? CountryOfTransaction { get; set; }

    public string? ShippingAddress { get; set; }

    public string? Bank { get; set; }

    public bool? Fraud { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<LineItem> LineItems { get; set; } = new List<LineItem>();
}
