using System;
using System.Collections.Generic;

namespace OneTop.Models;

public partial class Report
{
    public int ReportId { get; set; }

    public DateOnly? ReportDate { get; set; }

    public decimal? TotalRevenue { get; set; }
}
