namespace BiuroRachunkowe.Models
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;
	using System.ComponentModel.DataAnnotations;

	public partial class OfficeModel : DbContext
	{
		public OfficeModel()
			: base("name=DefaultConnection")
		{
		}

		public virtual DbSet<InvoiceHeader> InvoiceHeader { get; set; }
		public virtual DbSet<InvoicePosition> InvoicePosition { get; set; }
		public virtual DbSet<SAD> SAD { get; set; }
		public virtual DbSet<SadPositions> SadPositions { get; set; }
		public virtual DbSet<SAD_Invoice> SAD_Invoice { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<InvoiceHeader>()
				.Property(e => e.InvoiceNumber)
				.IsUnicode(false);

			modelBuilder.Entity<InvoiceHeader>()
				.Property(e => e.InvoiceStatus)
				.IsFixedLength()
				.IsUnicode(false);

			modelBuilder.Entity<InvoiceHeader>()
				.Property(e => e.Voucher)
				.IsUnicode(false);

			modelBuilder.Entity<InvoiceHeader>()
				.Property(e => e.Remarks)
				.IsUnicode(false);

			modelBuilder.Entity<InvoiceHeader>()
				.Property(e => e.Currency)
				.IsUnicode(false);

			modelBuilder.Entity<InvoiceHeader>()
				.Property(e => e.ExchangeRate)
				.HasPrecision(10, 6);

			modelBuilder.Entity<InvoiceHeader>()
				.Property(e => e.InvoiceValue)
				.HasPrecision(14, 2);

			modelBuilder.Entity<InvoiceHeader>()
				.Property(e => e.TransportCost_)
				.HasPrecision(14, 2);

			modelBuilder.Entity<InvoiceHeader>()
				.Property(e => e.AdditionalCost)
				.HasPrecision(14, 2);

			modelBuilder.Entity<InvoiceHeader>()
				.Property(e => e.Supplier)
				.IsUnicode(false);

			modelBuilder.Entity<InvoiceHeader>()
				.Property(e => e.ShipFrom)
				.IsUnicode(false);

			modelBuilder.Entity<InvoiceHeader>()
				.Property(e => e.TypeOfTranosport)
				.IsUnicode(false);

			modelBuilder.Entity<InvoicePosition>()
				.Property(e => e.Itemnumber)
				.IsUnicode(false);

			modelBuilder.Entity<InvoicePosition>()
				.Property(e => e.Quantity)
				.HasPrecision(14, 3);

			modelBuilder.Entity<InvoicePosition>()
				.Property(e => e.Price)
				.HasPrecision(14, 6);

			modelBuilder.Entity<InvoicePosition>()
				.Property(e => e.UnitOfMeasure)
				.IsUnicode(false);

			modelBuilder.Entity<InvoicePosition>()
				.Property(e => e.Weight)
				.HasPrecision(12, 3);

			modelBuilder.Entity<InvoicePosition>()
				.Property(e => e.HSCode)
				.IsUnicode(false);

			modelBuilder.Entity<InvoicePosition>()
				.Property(e => e.AdditionalCost)
				.HasPrecision(14, 6);

			modelBuilder.Entity<InvoicePosition>()
				.Property(e => e.TransportsCost)
				.HasPrecision(14, 6);

			modelBuilder.Entity<InvoicePosition>()
				.Property(e => e.CountryOfOrigin)
				.IsUnicode(false);

			modelBuilder.Entity<SAD>()
				.Property(e => e.SadNumber)
				.IsUnicode(false);

			modelBuilder.Entity<SAD>()
				.Property(e => e.Currency)
				.IsUnicode(false);

			modelBuilder.Entity<SAD>()
				.Property(e => e.ExchangeRate)
				.HasPrecision(10, 6);

			modelBuilder.Entity<SAD>()
				.Property(e => e.SadStatus)
				.IsFixedLength()
				.IsUnicode(false);

			modelBuilder.Entity<SAD>()
				.Property(e => e.Paid)
				.IsFixedLength()
				.IsUnicode(false);

			modelBuilder.Entity<SAD>()
				.Property(e => e.Remarks)
				.IsUnicode(false);

			modelBuilder.Entity<SadPositions>()
				.Property(e => e.HSCode)
				.IsUnicode(false);

			modelBuilder.Entity<SadPositions>()
				.Property(e => e.CountryOfOrigin)
				.IsUnicode(false);

			modelBuilder.Entity<SadPositions>()
				.Property(e => e.Rate)
				.HasPrecision(7, 4);

			modelBuilder.Entity<SadPositions>()
				.Property(e => e.PositionValue)
				.HasPrecision(14, 2);

			modelBuilder.Entity<SadPositions>()
				.Property(e => e.DutyValue)
				.HasPrecision(14, 2);

			modelBuilder.Entity<SAD_Invoice>()
				.Property(e => e.CUSTOM_VALUE)
				.HasPrecision(14, 2);

			modelBuilder.Entity<SAD_Invoice>()
				.Property(e => e.DUTY_VALUE)
				.HasPrecision(14, 2);
		}
	}
	[Table("InvoiceHeader")]
	public partial class InvoiceHeader
	{
		public long Id { get; set; }

		[Required]
		[StringLength(50)]
		public string InvoiceNumber { get; set; }

		[Column(TypeName = "date")]
		public DateTime InvoiceDate { get; set; }

		[StringLength(1)]
		public string InvoiceStatus { get; set; }

		[StringLength(30)]
		public string Voucher { get; set; }

		[StringLength(255)]
		public string Remarks { get; set; }

		[Required]
		[StringLength(3)]
		public string Currency { get; set; }

		[Column(TypeName = "numeric")]
		public decimal ExchangeRate { get; set; }

		[Column(TypeName = "numeric")]
		public decimal InvoiceValue { get; set; }

		[Column("TransportCost ", TypeName = "numeric")]
		public decimal? TransportCost_ { get; set; }

		[Column(TypeName = "numeric")]
		public decimal? AdditionalCost { get; set; }

		[Required]
		[StringLength(11)]
		public string Supplier { get; set; }

		[StringLength(11)]
		public string ShipFrom { get; set; }

		[StringLength(15)]
		public string TypeOfTranosport { get; set; }
	}
	[Table("InvoicePosition")]
	public partial class InvoicePosition
	{
		public long InvoiceId { get; set; }

		public long Id { get; set; }

		public int? PositionNumber { get; set; }

		[Required]
		[StringLength(18)]
		public string Itemnumber { get; set; }

		[Column(TypeName = "numeric")]
		public decimal Quantity { get; set; }

		[Column(TypeName = "numeric")]
		public decimal Price { get; set; }

		[Required]
		[StringLength(4)]
		public string UnitOfMeasure { get; set; }

		[Column(TypeName = "numeric")]
		public decimal Weight { get; set; }

		[StringLength(13)]
		public string HSCode { get; set; }

		[Column(TypeName = "numeric")]
		public decimal? AdditionalCost { get; set; }

		[Column(TypeName = "numeric")]
		public decimal? TransportsCost { get; set; }

		[StringLength(50)]
		public string CountryOfOrigin { get; set; }
	}
	[Table("SAD")]
	public partial class SAD
	{
		public long Id { get; set; }

		[Required]
		[StringLength(25)]
		public string SadNumber { get; set; }

		[Column(TypeName = "date")]
		public DateTime SadDate { get; set; }

		[Required]
		[StringLength(3)]
		public string Currency { get; set; }

		[Column(TypeName = "numeric")]
		public decimal ExchangeRate { get; set; }

		[StringLength(1)]
		public string SadStatus { get; set; }

		[StringLength(1)]
		public string Paid { get; set; }

		[Column(TypeName = "date")]
		public DateTime? PaidDate { get; set; }

		[StringLength(255)]
		public string Remarks { get; set; }
	}
	public partial class SAD_Invoice
	{
		[Key]
		[Column(Order = 0)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public long ID_SAD { get; set; }

		[Key]
		[Column(Order = 1)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public long ID_SAD_POS { get; set; }

		[Key]
		[Column(Order = 2)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public long ID_INV { get; set; }

		[Key]
		[Column(Order = 3)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public long ID_INV_POS { get; set; }

		[Column(TypeName = "numeric")]
		public decimal? CUSTOM_VALUE { get; set; }

		[Column(TypeName = "numeric")]
		public decimal? DUTY_VALUE { get; set; }
	}
	public partial class SadPositions
	{
		public long? IdSad { get; set; }

		public long Id { get; set; }

		[Required]
		[StringLength(13)]
		public string HSCode { get; set; }

		[Required]
		[StringLength(3)]
		public string CountryOfOrigin { get; set; }

		[Column(TypeName = "numeric")]
		public decimal Rate { get; set; }

		[Column(TypeName = "numeric")]
		public decimal? PositionValue { get; set; }

		[Column(TypeName = "numeric")]
		public decimal? DutyValue { get; set; }
	}
}
