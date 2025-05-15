using PetaPoco;
using System.ComponentModel.DataAnnotations.Schema;

[Table("pp_stats_monthly")]
[PrimaryKey("Month", AutoIncrement = false)]
public class PP_Stats_Monthly
{
    public int Month { get; set; }

    public int VisitCount { get; set; }

    public int OrderCount { get; set; }
}