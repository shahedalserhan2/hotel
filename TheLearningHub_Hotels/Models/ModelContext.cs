using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TheLearningHub_Hotels.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CAboutpagecontent> CAboutpagecontents { get; set; }

    public virtual DbSet<CBank> CBanks { get; set; }

    public virtual DbSet<CBookingpagecontent> CBookingpagecontents { get; set; }

    public virtual DbSet<CContactpagecontent> CContactpagecontents { get; set; }

    public virtual DbSet<CHomepagecontent> CHomepagecontents { get; set; }

    public virtual DbSet<CHotel> CHotels { get; set; }

    public virtual DbSet<CHotelspagecontent> CHotelspagecontents { get; set; }

    public virtual DbSet<CReservation> CReservations { get; set; }

    public virtual DbSet<CRole> CRoles { get; set; }

    public virtual DbSet<CRoom> CRooms { get; set; }

    public virtual DbSet<CRoomspagecontent> CRoomspagecontents { get; set; }

    public virtual DbSet<CService> CServices { get; set; }

    public virtual DbSet<CServicespagecontent> CServicespagecontents { get; set; }

    public virtual DbSet<CTeam> CTeams { get; set; }

    public virtual DbSet<CTeampagecontent> CTeampagecontents { get; set; }

    public virtual DbSet<CTestimonial> CTestimonials { get; set; }

    public virtual DbSet<CTestimonialpagecontent> CTestimonialpagecontents { get; set; }

    public virtual DbSet<CUser> CUsers { get; set; }

    public virtual DbSet<CUserlogin> CUserlogins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521) (CONNECT_DATA=(SERVICE_NAME=xe))));User Id=c##SHAHED; Password=Test321;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("C##SHAHED")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<CAboutpagecontent>(entity =>
        {
            entity.HasKey(e => e.Aboutpagecontentid).HasName("SYS_C008793");

            entity.ToTable("C_ABOUTPAGECONTENT");

            entity.Property(e => e.Aboutpagecontentid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ABOUTPAGECONTENTID");
            entity.Property(e => e.Footeremail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTEREMAIL");
            entity.Property(e => e.Footerlocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTERLOCATION");
            entity.Property(e => e.Footerphonenumber)
                .HasColumnType("NUMBER")
                .HasColumnName("FOOTERPHONENUMBER");
            entity.Property(e => e.ImagepathTop)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH_TOP");
            entity.Property(e => e.Pagename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAGENAME");
            entity.Property(e => e.Projectname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PROJECTNAME");
            entity.Property(e => e.Userloginid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERLOGINID");
            entity.Property(e => e.WelcomeText)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("WELCOME_TEXT");

            entity.HasOne(d => d.Userlogin).WithMany(p => p.CAboutpagecontents)
                .HasForeignKey(d => d.Userloginid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USERLOGIN2");
        });

        modelBuilder.Entity<CBank>(entity =>
        {
            entity.HasKey(e => e.Bankid).HasName("SYS_C008786");

            entity.ToTable("C_BANK");

            entity.Property(e => e.Bankid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("BANKID");
            entity.Property(e => e.Creditcardexp)
                .HasColumnType("DATE")
                .HasColumnName("CREDITCARDEXP");
            entity.Property(e => e.Creditcardnumber)
                .HasColumnType("NUMBER")
                .HasColumnName("CREDITCARDNUMBER");
           
            entity.Property(e => e.MONEY )
              .HasColumnType("NUMBER")
              .HasColumnName("MONEY");

        });

        modelBuilder.Entity<CBookingpagecontent>(entity =>
        {
            entity.HasKey(e => e.Bookingpagecontentid).HasName("SYS_C008805");

            entity.ToTable("C_BOOKINGPAGECONTENT");

            entity.Property(e => e.Bookingpagecontentid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("BOOKINGPAGECONTENTID");
            entity.Property(e => e.Footeremail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTEREMAIL");
            entity.Property(e => e.Footerlocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTERLOCATION");
            entity.Property(e => e.Footerphonenumber)
                .HasColumnType("NUMBER")
                .HasColumnName("FOOTERPHONENUMBER");
            entity.Property(e => e.ImagepathTop)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH_TOP");
            entity.Property(e => e.Pagename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAGENAME");
            entity.Property(e => e.Projectname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PROJECTNAME");
            entity.Property(e => e.Userloginid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERLOGINID");

            entity.HasOne(d => d.Userlogin).WithMany(p => p.CBookingpagecontents)
                .HasForeignKey(d => d.Userloginid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USERLOGIN6");
        });

        modelBuilder.Entity<CContactpagecontent>(entity =>
        {
            entity.HasKey(e => e.Contactpagecontentid).HasName("SYS_C008814");

            entity.ToTable("C_CONTACTPAGECONTENT");

            entity.Property(e => e.Contactpagecontentid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CONTACTPAGECONTENTID");
            entity.Property(e => e.Bookingemail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BOOKINGEMAIL");
            entity.Property(e => e.Footeremail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTEREMAIL");
            entity.Property(e => e.Footerlocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTERLOCATION");
            entity.Property(e => e.Footerphonenumber)
                .HasColumnType("NUMBER")
                .HasColumnName("FOOTERPHONENUMBER");
            entity.Property(e => e.Generalemail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GENERALEMAIL");
            entity.Property(e => e.ImagepathTop)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH_TOP");
            entity.Property(e => e.Pagename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAGENAME");
            entity.Property(e => e.Projectname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PROJECTNAME");
            entity.Property(e => e.Technicalemail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TECHNICALEMAIL");
            entity.Property(e => e.Userloginid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERLOGINID");

            entity.HasOne(d => d.Userlogin).WithMany(p => p.CContactpagecontents)
                .HasForeignKey(d => d.Userloginid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USERLOGIN9");
        });

        modelBuilder.Entity<CHomepagecontent>(entity =>
        {
            entity.HasKey(e => e.Homepagecontent).HasName("SYS_C008790");

            entity.ToTable("C_HOMEPAGECONTENT");

            entity.Property(e => e.Homepagecontent)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOMEPAGECONTENT");
            entity.Property(e => e.Footeremail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTEREMAIL");
            entity.Property(e => e.Footerlocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTERLOCATION");
            entity.Property(e => e.Footerphonenumber)
                .HasColumnType("NUMBER")
                .HasColumnName("FOOTERPHONENUMBER");
            entity.Property(e => e.ImagepathTop1)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH_TOP1");
            entity.Property(e => e.ImagepathTop2)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH_TOP2");
            entity.Property(e => e.Pagename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAGENAME");
            entity.Property(e => e.Projectname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PROJECTNAME");
            entity.Property(e => e.Userloginid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERLOGINID");
            entity.Property(e => e.WelcomeText)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("WELCOME_TEXT");

            entity.HasOne(d => d.Userlogin).WithMany(p => p.CHomepagecontents)
                .HasForeignKey(d => d.Userloginid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USERLOGIN1");
        });

        modelBuilder.Entity<CHotel>(entity =>
        {
            entity.HasKey(e => e.Hotelid).HasName("SYS_C008767");

            entity.ToTable("C_HOTELS");

            entity.Property(e => e.Hotelid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOTELID");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Hotelname)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("HOTELNAME");
            entity.Property(e => e.Imagepath)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH");
            entity.Property(e => e.Location)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("LOCATION");
        });

        modelBuilder.Entity<CHotelspagecontent>(entity =>
        {
            entity.HasKey(e => e.Hotelspagecontentid).HasName("SYS_C008799");

            entity.ToTable("C_HOTELSPAGECONTENT");

            entity.Property(e => e.Hotelspagecontentid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOTELSPAGECONTENTID");
            entity.Property(e => e.Footeremail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTEREMAIL");
            entity.Property(e => e.Footerlocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTERLOCATION");
            entity.Property(e => e.Footerphonenumber)
                .HasColumnType("NUMBER")
                .HasColumnName("FOOTERPHONENUMBER");
            entity.Property(e => e.ImagepathTop)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH_TOP");
            entity.Property(e => e.Pagename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAGENAME");
            entity.Property(e => e.Projectname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PROJECTNAME");
            entity.Property(e => e.Userloginid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERLOGINID");

            entity.HasOne(d => d.Userlogin).WithMany(p => p.CHotelspagecontents)
                .HasForeignKey(d => d.Userloginid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USERLOGIN4");
        });

        modelBuilder.Entity<CReservation>(entity =>
        {
            entity.HasKey(e => e.Reservationsid).HasName("SYS_C008775");

            entity.ToTable("C_RESERVATIONS");

            entity.Property(e => e.Reservationsid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("RESERVATIONSID");
            entity.Property(e => e.CheckInDate)
                .HasColumnType("DATE")
                .HasColumnName("CHECK_IN_DATE");
            entity.Property(e => e.CheckOutDate)
                .HasColumnType("DATE")
                .HasColumnName("CHECK_OUT_DATE");
            entity.Property(e => e.Invoicepdf)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("INVOICEPDF");
            entity.Property(e => e.Roomid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROOMID");
            entity.Property(e => e.Toltalprice)
                .HasColumnType("NUMBER")
                .HasColumnName("TOLTALPRICE");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.Room).WithMany(p => p.CReservations)
                .HasForeignKey(d => d.Roomid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_ROOM5");

            entity.HasOne(d => d.User).WithMany(p => p.CReservations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USER321");
        });

        modelBuilder.Entity<CRole>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("SYS_C008757");

            entity.ToTable("C_ROLE");

            entity.Property(e => e.Roleid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROLEID");
            entity.Property(e => e.Rolename)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("ROLENAME");
        });

        modelBuilder.Entity<CRoom>(entity =>
        {
            entity.HasKey(e => e.Roomid).HasName("SYS_C008772");

            entity.ToTable("C_ROOMS");

            entity.Property(e => e.Roomid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROOMID");
            entity.Property(e => e.Hotelid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOTELID");
            entity.Property(e => e.Imagepath)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH");
            entity.Property(e => e.Isavailable)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ISAVAILABLE");
            entity.Property(e => e.PricePerNight)
                .HasColumnType("NUMBER")
                .HasColumnName("PRICE_PER_NIGHT");
            entity.Property(e => e.Roomcapacity)
                .HasColumnType("NUMBER")
                .HasColumnName("ROOMCAPACITY");
            entity.Property(e => e.Roomnumber)
                .HasColumnType("NUMBER")
                .HasColumnName("ROOMNUMBER");

            entity.HasOne(d => d.Hotel).WithMany(p => p.CRooms)
                .HasForeignKey(d => d.Hotelid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_HOTEL4");
        });

        modelBuilder.Entity<CRoomspagecontent>(entity =>
        {
            entity.HasKey(e => e.Roomspagecontentid).HasName("SYS_C008802");

            entity.ToTable("C_ROOMSPAGECONTENT");

            entity.Property(e => e.Roomspagecontentid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROOMSPAGECONTENTID");
            entity.Property(e => e.Footeremail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTEREMAIL");
            entity.Property(e => e.Footerlocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTERLOCATION");
            entity.Property(e => e.Footerphonenumber)
                .HasColumnType("NUMBER")
                .HasColumnName("FOOTERPHONENUMBER");
            entity.Property(e => e.ImagepathTop)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH_TOP");
            entity.Property(e => e.Pagename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAGENAME");
            entity.Property(e => e.Projectname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PROJECTNAME");
            entity.Property(e => e.Userloginid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERLOGINID");

            entity.HasOne(d => d.Userlogin).WithMany(p => p.CRoomspagecontents)
                .HasForeignKey(d => d.Userloginid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USERLOGIN5");
        });

        modelBuilder.Entity<CService>(entity =>
        {
            entity.HasKey(e => e.Serviceid).HasName("SYS_C008769");

            entity.ToTable("C_SERVICES");

            entity.Property(e => e.Serviceid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SERVICEID");
            entity.Property(e => e.Hotelid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOTELID");
            entity.Property(e => e.Servicename)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("SERVICENAME");
            entity.Property(e => e.Servicetext)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("SERVICETEXT");

            entity.HasOne(d => d.Hotel).WithMany(p => p.CServices)
                .HasForeignKey(d => d.Hotelid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_HOTEL3");
        });

        modelBuilder.Entity<CServicespagecontent>(entity =>
        {
            entity.HasKey(e => e.Servicespagecontentid).HasName("SYS_C008796");

            entity.ToTable("C_SERVICESPAGECONTENT");

            entity.Property(e => e.Servicespagecontentid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SERVICESPAGECONTENTID");
            entity.Property(e => e.Footeremail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTEREMAIL");
            entity.Property(e => e.Footerlocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTERLOCATION");
            entity.Property(e => e.Footerphonenumber)
                .HasColumnType("NUMBER")
                .HasColumnName("FOOTERPHONENUMBER");
            entity.Property(e => e.ImagepathTop)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH_TOP");
            entity.Property(e => e.Pagename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAGENAME");
            entity.Property(e => e.Projectname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PROJECTNAME");
            entity.Property(e => e.Userloginid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERLOGINID");

            entity.HasOne(d => d.Userlogin).WithMany(p => p.CServicespagecontents)
                .HasForeignKey(d => d.Userloginid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USERLOGIN3");
        });

        modelBuilder.Entity<CTeam>(entity =>
        {
            entity.HasKey(e => e.Teamid).HasName("SYS_C008788");

            entity.ToTable("C_TEAM");

            entity.Property(e => e.Teamid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TEAMID");
            entity.Property(e => e.Imagepath)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH");
            entity.Property(e => e.Position)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("POSITION");
            entity.Property(e => e.TeamMembername)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("TEAM_MEMBERNAME");
        });

        modelBuilder.Entity<CTeampagecontent>(entity =>
        {
            entity.HasKey(e => e.Teampagecontentid).HasName("SYS_C008808");

            entity.ToTable("C_TEAMPAGECONTENT");

            entity.Property(e => e.Teampagecontentid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TEAMPAGECONTENTID");
            entity.Property(e => e.Footeremail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTEREMAIL");
            entity.Property(e => e.Footerlocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTERLOCATION");
            entity.Property(e => e.Footerphonenumber)
                .HasColumnType("NUMBER")
                .HasColumnName("FOOTERPHONENUMBER");
            entity.Property(e => e.ImagepathTop)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH_TOP");
            entity.Property(e => e.Pagename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAGENAME");
            entity.Property(e => e.Projectname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PROJECTNAME");
            entity.Property(e => e.Userloginid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERLOGINID");

            entity.HasOne(d => d.Userlogin).WithMany(p => p.CTeampagecontents)
                .HasForeignKey(d => d.Userloginid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USERLOGIN7");
        });

        modelBuilder.Entity<CTestimonial>(entity =>
        {
            entity.HasKey(e => e.Testimonialid).HasName("SYS_C008782");

            entity.ToTable("C_TESTIMONIAL");

            entity.Property(e => e.Testimonialid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TESTIMONIALID");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("DATE")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.Hotelid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOTELID");
            entity.Property(e => e.Rating)
                .HasColumnType("NUMBER")
                .HasColumnName("RATING");
            entity.Property(e => e.TestimonialText)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("TESTIMONIAL_TEXT");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");
            entity.Property(e => e.STATUS)
               .HasMaxLength(25)
               .IsUnicode(false)
               .HasColumnName("STATUS");
            entity.Property(e => e.ACTIONS)
              .HasMaxLength(25)
              .IsUnicode(false)
              .HasColumnName("ACTIONS");

            entity.HasOne(d => d.Hotel).WithMany(p => p.CTestimonials)
                .HasForeignKey(d => d.Hotelid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_HOTEL43");

            entity.HasOne(d => d.User).WithMany(p => p.CTestimonials)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USER621");
        });

        modelBuilder.Entity<CTestimonialpagecontent>(entity =>
        {
            entity.HasKey(e => e.Testimonialpagecontentid).HasName("SYS_C008811");

            entity.ToTable("C_TESTIMONIALPAGECONTENT");

            entity.Property(e => e.Testimonialpagecontentid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TESTIMONIALPAGECONTENTID");
            entity.Property(e => e.Footeremail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTEREMAIL");
            entity.Property(e => e.Footerlocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FOOTERLOCATION");
            entity.Property(e => e.Footerphonenumber)
                .HasColumnType("NUMBER")
                .HasColumnName("FOOTERPHONENUMBER");
            entity.Property(e => e.ImagepathMiddle)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH_MIDDLE");
            entity.Property(e => e.ImagepathTop)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH_TOP");
            entity.Property(e => e.Pagename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAGENAME");
            entity.Property(e => e.Projectname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PROJECTNAME");
            entity.Property(e => e.Userloginid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERLOGINID");

            entity.HasOne(d => d.Userlogin).WithMany(p => p.CTestimonialpagecontents)
                .HasForeignKey(d => d.Userloginid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USERLOGIN8");
        });

        modelBuilder.Entity<CUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("SYS_C008760");

            entity.ToTable("C_USERS");

            entity.HasIndex(e => e.Email, "SYS_C008761").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Fname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("FNAME");
            entity.Property(e => e.Imagepath)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH");
            entity.Property(e => e.Lname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("LNAME");
            entity.Property(e => e.PhoneNumber)
                .HasPrecision(10)
                .HasColumnName("PHONE_NUMBER");
        });

        modelBuilder.Entity<CUserlogin>(entity =>
        {
            entity.HasKey(e => e.Userloginid).HasName("SYS_C008763");

            entity.ToTable("C_USERLOGIN");

            entity.Property(e => e.Userloginid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERLOGINID");
            entity.Property(e => e.Passwordd)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("PASSWORDD");
            entity.Property(e => e.Roleid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROLEID");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USERNAME");

            entity.HasOne(d => d.Role).WithMany(p => p.CUserlogins)
                .HasForeignKey(d => d.Roleid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_ROLE2");

            entity.HasOne(d => d.User).WithMany(p => p.CUserlogins)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USER3");
        });
        modelBuilder.HasSequence("ST_ID").IncrementsBy(2);
        modelBuilder.HasSequence("ST_ID2").IncrementsBy(2);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
