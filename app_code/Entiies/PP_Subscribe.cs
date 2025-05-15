using PetaPoco;
using System;
using System.ComponentModel.DataAnnotations.Schema;

[Table("pp_subscribe")]
public class PP_Subscribe : EntityBase
{
    public string Email { get; set; }

    public DateTime SubscribeDate { get; set; }

    public string Ip { get; set; }

}