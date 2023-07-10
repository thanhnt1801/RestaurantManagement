using Microsoft.EntityFrameworkCore;
using QLNH_Client.Models;

namespace QLNH_Client.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>().HasOne(x => x.CreatedUser).WithMany(x => x.RestaurantCreator).HasForeignKey(x => x.CreatedUserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Restaurant>().HasOne(x => x.UpdatedUser).WithMany(x => x.RestaurantUpdator).HasForeignKey(x => x.UpdatedUserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<User>().HasOne(x => x.Role).WithMany(x => x.User).HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<User>().HasOne(x => x.Restaurant).WithMany(x => x.Users).HasForeignKey(x => x.RestaurantId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Category>().HasOne(x => x.Restaurant).WithMany(x => x.Categories).HasForeignKey(x => x.RestaurantId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Item>().HasOne(x => x.Restaurant).WithMany(x => x.Items).HasForeignKey(x => x.RestaurantId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Item>().HasOne(x => x.Category).WithMany(x => x.Items).HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Item>().HasOne(x => x.Category).WithMany(x => x.Items).HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Price>().HasOne(x => x.Unit).WithMany(x => x.Prices).HasForeignKey(x => x.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Price>().HasOne(x => x.Restaurant).WithMany(x => x.Prices).HasForeignKey(x => x.RestaurantId)
               .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Price>().HasOne(x => x.Size).WithMany(x => x.Prices).HasForeignKey(x => x.SizeId)
               .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Price>().HasOne(x => x.Item).WithMany(x => x.Prices).HasForeignKey(x => x.ItemId)
               .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Guest>().HasOne(x => x.Restaurant).WithMany(x => x.Guests).HasForeignKey(x => x.RestaurantId)
               .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Guest>().HasOne(x => x.CreatedBy).WithMany(x => x.GuestCreator).HasForeignKey(x => x.CreatedById)
               .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Guest>().HasOne(x => x.UpdatedBy).WithMany(x => x.GuestUpdator).HasForeignKey(x => x.UpdatedById)
               .OnDelete(DeleteBehavior.ClientSetNull);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemImage> ItemImages { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<GuestTable> GuestTables { get; set; }
        public DbSet<Guest> Guests { get; set; }


    }
}
