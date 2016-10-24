namespace Jamaal
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<tblProduct> tblProducts { get; set; }
        public virtual DbSet<tblProductType> tblProductTypes { get; set; }
        public virtual DbSet<tblTransaction> tblTransactions { get; set; }
        public virtual DbSet<tblTransactionItem> tblTransactionItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblProduct>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);
        }
    }
}
