using PetaPoco;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("pp_stats_url")]
[PrimaryKey("Url", AutoIncrement = false)]
public class PP_Stats_Url
{
    [Key]
    public string Url { get; set; }

    public int VisitCount { get; set; }
}