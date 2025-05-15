using PetaPoco;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("pp_stats_daily")]
[PrimaryKey("Date", AutoIncrement = false)]
public class PP_Stats_Daily
{
    [Key]
    public int Date { get; set; }

    public int VisitCount { get; set; }

    public int OrderCount { get; set; }
}