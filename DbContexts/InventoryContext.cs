using Anbaedari_Exam.Models;
using Microsoft.EntityFrameworkCore;

namespace Anbaedari_Exam.DbContexts
{
    public class InventoryContext : DbContext
    {
        public InventoryContext()
        {

        }

        public InventoryContext(DbContextOptions<InventoryContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Features_of_the_receiptDto> Features_Of_The_ReceiptDtos { get; set; }
        public virtual DbSet<OrderDto> OrderDtos { get; set; }
        public virtual DbSet<Product_group_characteristicsDto> Product_Group_CharacteristicsDtos { get; set; }
        public virtual DbSet<ProductfeaturesDto> ProductfeaturesDtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=**;Initial Catalog=Inventory; Integrated Security=True");
            }
        }
    }


}






