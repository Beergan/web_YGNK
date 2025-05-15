using System.Data.Entity;

public class MyDbContext : DbContext
{
    public MyDbContext(string conn) : base(conn) { }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PP_Lang>();
        modelBuilder.Entity<PP_Compt>();
        modelBuilder.Entity<PP_Config>();
        modelBuilder.Entity<PP_Page>();
        modelBuilder.Entity<PP_Category>();
        modelBuilder.Entity<PP_Node>();
        modelBuilder.Entity<PP_Product>();
        modelBuilder.Entity<PP_Order>();
        modelBuilder.Entity<PP_User>();
        modelBuilder.Entity<PP_Visit>();
        modelBuilder.Entity<PP_Json>();
        modelBuilder.Entity<PP_Stats_Daily>();
        modelBuilder.Entity<PP_Contact>();
        modelBuilder.Entity<PP_Advise>();
        modelBuilder.Entity<PP_Comment>();
        modelBuilder.Entity<PP_Register>();
        modelBuilder.Entity<PP_User_log>();
		modelBuilder.Entity<PP_Category_details>();
	}
}