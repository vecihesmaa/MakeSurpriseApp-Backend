using System;
using System.Collections.Generic;
using MakeSurpriseProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace MakeSurpriseProject.Contexts;

public partial class MakeSurpriseFinalDbContext : DbContext
{
    public MakeSurpriseFinalDbContext()
    {
    }

    public MakeSurpriseFinalDbContext(DbContextOptions<MakeSurpriseFinalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<FormAnswer> FormAnswers { get; set; }

    public virtual DbSet<FormOption> FormOptions { get; set; }

    public virtual DbSet<FormQuestion> FormQuestions { get; set; }

    public virtual DbSet<Ilceler> Ilcelers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Sehirler> Sehirlers { get; set; }

    public virtual DbSet<SemtMah> SemtMahs { get; set; }

    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

    public virtual DbSet<SpecialDay> SpecialDays { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    public virtual DbSet<UserRelative> UserRelatives { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=MakeSurpriseFinalDB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Addresse__091C2AFB67B00EC4");

            entity.Property(e => e.AddressTag)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullAddress).HasColumnType("text");

            entity.HasOne(d => d.District).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("FK__Addresses__Distr__6C190EBB");

            entity.HasOne(d => d.Neighbourhood).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.NeighbourhoodId)
                .HasConstraintName("FK__Addresses__Neigh__6D0D32F4");

            entity.HasOne(d => d.Province).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.ProvinceId)
                .HasConstraintName("FK__Addresses__Provi__6B24EA82");

            entity.HasOne(d => d.User).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Addresses__UserI__6A30C649");
        });

        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.CargoId).HasName("PK__CARGO__B4E665CD96118D29");

            entity.ToTable("CARGO");

            entity.Property(e => e.CargoStatus)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.OrderItem).WithMany(p => p.Cargos)
                .HasForeignKey(d => d.OrderItemId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__CARGO__OrderItem__6E01572D");
        });

        modelBuilder.Entity<FormAnswer>(entity =>
        {
            entity.HasKey(e => e.FormAnswerId).HasName("PK__FormAnsw__896C79B6868D6BB6");

            entity.HasOne(d => d.EighthQuestionAnswerNavigation).WithMany(p => p.FormAnswerEighthQuestionAnswerNavigations)
                .HasForeignKey(d => d.EighthQuestionAnswer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FormAnswe__Eight__72C60C4A");

            entity.HasOne(d => d.EleventhQuestionAnswerNavigation).WithMany(p => p.FormAnswerEleventhQuestionAnswerNavigations)
                .HasForeignKey(d => d.EleventhQuestionAnswer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FormAnswe__Eleve__75A278F5");

            entity.HasOne(d => d.FifteenthQuestionAnswerNavigation).WithMany(p => p.FormAnswerFifteenthQuestionAnswerNavigations)
                .HasForeignKey(d => d.FifteenthQuestionAnswer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FormAnswe__Fifte__797309D9");

            entity.HasOne(d => d.FifthQuestionAnswerNavigation).WithMany(p => p.FormAnswerFifthQuestionAnswerNavigations)
                .HasForeignKey(d => d.FifthQuestionAnswer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FormAnswe__Fifth__6FE99F9F");

            entity.HasOne(d => d.FirstQuestionAnswerNavigation).WithMany(p => p.FormAnswerFirstQuestionAnswerNavigations)
                .HasForeignKey(d => d.FirstQuestionAnswer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FormAnswe__First__7A672E12");

            entity.HasOne(d => d.FourteenthQuestionAnswerNavigation).WithMany(p => p.FormAnswerFourteenthQuestionAnswerNavigations)
                .HasForeignKey(d => d.FourteenthQuestionAnswer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FormAnswe__Fourt__787EE5A0");

            entity.HasOne(d => d.FourthQuestionAnswerNavigation).WithMany(p => p.FormAnswerFourthQuestionAnswerNavigations)
                .HasForeignKey(d => d.FourthQuestionAnswer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FormAnswe__Fourt__6EF57B66");

            entity.HasOne(d => d.NinthQuestionAnswerNavigation).WithMany(p => p.FormAnswerNinthQuestionAnswerNavigations)
                .HasForeignKey(d => d.NinthQuestionAnswer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FormAnswe__Ninth__73BA3083");

            entity.HasOne(d => d.SecondQuestionAnswerNavigation).WithMany(p => p.FormAnswerSecondQuestionAnswerNavigations)
                .HasForeignKey(d => d.SecondQuestionAnswer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FormAnswe__Secon__7B5B524B");

            entity.HasOne(d => d.SeventhQuestionAnswerNavigation).WithMany(p => p.FormAnswerSeventhQuestionAnswerNavigations)
                .HasForeignKey(d => d.SeventhQuestionAnswer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FormAnswe__Seven__71D1E811");

            entity.HasOne(d => d.SixthQuestionAnswerNavigation).WithMany(p => p.FormAnswerSixthQuestionAnswerNavigations)
                .HasForeignKey(d => d.SixthQuestionAnswer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FormAnswe__Sixth__70DDC3D8");

            entity.HasOne(d => d.TenthQuestionAnswerNavigation).WithMany(p => p.FormAnswerTenthQuestionAnswerNavigations)
                .HasForeignKey(d => d.TenthQuestionAnswer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FormAnswe__Tenth__74AE54BC");

            entity.HasOne(d => d.ThirdQuestionAnswerNavigation).WithMany(p => p.FormAnswerThirdQuestionAnswerNavigations)
                .HasForeignKey(d => d.ThirdQuestionAnswer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FormAnswe__Third__7C4F7684");

            entity.HasOne(d => d.ThirteenthQuestionAnswerNavigation).WithMany(p => p.FormAnswerThirteenthQuestionAnswerNavigations)
                .HasForeignKey(d => d.ThirteenthQuestionAnswer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FormAnswe__Thirt__778AC167");

            entity.HasOne(d => d.TwelfthQuestionAnswerNavigation).WithMany(p => p.FormAnswerTwelfthQuestionAnswerNavigations)
                .HasForeignKey(d => d.TwelfthQuestionAnswer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FormAnswe__Twelf__76969D2E");
        });

        modelBuilder.Entity<FormOption>(entity =>
        {
            entity.HasKey(e => e.FormOptionId).HasName("PK__FormOpti__58A8240FD5119CF4");

            entity.Property(e => e.OptionText).HasColumnType("text");

            entity.HasOne(d => d.FormQuestion).WithMany(p => p.FormOptions)
                .HasForeignKey(d => d.FormQuestionId)
                .HasConstraintName("FK__FormOptio__FormQ__7D439ABD");
        });

        modelBuilder.Entity<FormQuestion>(entity =>
        {
            entity.HasKey(e => e.FormQuestionId).HasName("PK__FormQues__C75108077BBFC461");

            entity.Property(e => e.QuestionText).HasColumnType("text");
        });

        modelBuilder.Entity<Ilceler>(entity =>
        {
            entity.HasKey(e => e.IlceId);

            entity.ToTable("Ilceler");

            entity.Property(e => e.IlceId)
                .ValueGeneratedNever()
                .HasColumnName("ilceId");
            entity.Property(e => e.IlceAdi).HasMaxLength(60);
            entity.Property(e => e.SehirAdi).HasMaxLength(55);

            entity.HasOne(d => d.Sehir).WithMany(p => p.Ilcelers)
                .HasForeignKey(d => d.SehirId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ilceler_Sehirler");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCF21CB80EC");

            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Orders__UserId__02FC7413");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__OrderIte__57ED068100135BC6");

            entity.Property(e => e.GiftNote).HasColumnType("text");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Address).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Addre__01142BA1");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Order__7F2BE32F");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_OrderItems_Products");

            entity.HasOne(d => d.UserRelative).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.UserRelativeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__UserR__00200768");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6CDE741F05A");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.RatingsId).HasName("PK__RATINGS__A85B63DB1292C35A");

            entity.ToTable("RATINGS");

            entity.Property(e => e.Comment).HasColumnType("text");

            entity.HasOne(d => d.OrderItem).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.OrderItemId)
                .HasConstraintName("FK__RATINGS__OrderIt__03F0984C");

            entity.HasOne(d => d.User).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__RATINGS__UserId__04E4BC85");
        });

        modelBuilder.Entity<Sehirler>(entity =>
        {
            entity.HasKey(e => e.SehirId);

            entity.ToTable("Sehirler");

            entity.Property(e => e.SehirId).ValueGeneratedNever();
            entity.Property(e => e.SehirAdi).HasMaxLength(20);
        });

        modelBuilder.Entity<SemtMah>(entity =>
        {
            entity.ToTable("SemtMah");

            entity.Property(e => e.SemtMahId).ValueGeneratedNever();
            entity.Property(e => e.IlceId).HasColumnName("ilceId");
            entity.Property(e => e.MahalleAdi).HasMaxLength(100);
            entity.Property(e => e.PostaKodu).HasMaxLength(6);
            entity.Property(e => e.SemtAdi).HasMaxLength(60);

            entity.HasOne(d => d.Ilce).WithMany(p => p.SemtMahs)
                .HasForeignKey(d => d.IlceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SemtMah_Ilceler");
        });

        modelBuilder.Entity<ShoppingCart>(entity =>
        {
            entity.HasKey(e => e.ShoppingCartId).HasName("PK__Shopping__7A789AE42CB14AD2");

            entity.Property(e => e.Note).HasColumnType("text");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.ShoppingCarts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ShoppingC__UserI__06CD04F7");

            entity.HasOne(d => d.UserRelative).WithMany(p => p.ShoppingCarts)
                .HasForeignKey(d => d.UserRelativeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ShoppingC__UserR__07C12930");
        });

        modelBuilder.Entity<SpecialDay>(entity =>
        {
            entity.HasKey(e => e.SpecialDayId).HasName("PK__SpecialD__58DACBCE077C3D71");

            entity.Property(e => e.SpecialDayDate).HasColumnType("datetime");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.SpecialDays)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__SpecialDa__UserI__08B54D69");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C9C7649C1");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534334BC7ED").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.ProfileId).HasName("PK__UserProf__290C88E461304934");

            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserProfi__UserI__09A971A2");
        });

        modelBuilder.Entity<UserRelative>(entity =>
        {
            entity.HasKey(e => e.UserRelativeId).HasName("PK__UserRela__66E9C5B29635D0A5");

            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Tag)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.FormAnswer).WithMany(p => p.UserRelatives)
                .HasForeignKey(d => d.FormAnswerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__UserRelat__FormA__0B91BA14");

            entity.HasOne(d => d.User).WithMany(p => p.UserRelatives)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserRelat__UserI__0A9D95DB");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
