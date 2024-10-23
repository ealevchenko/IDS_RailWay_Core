using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EFIDS.Models;

public partial class KrrPaCntRailwayArchiveContext : DbContext
{
    public KrrPaCntRailwayArchiveContext()
    {
    }

    public KrrPaCntRailwayArchiveContext(DbContextOptions<KrrPaCntRailwayArchiveContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FilingWagon> FilingWagons { get; set; }

    public virtual DbSet<View1> View1s { get; set; }

    public virtual DbSet<View2> View2s { get; set; }

    public virtual DbSet<View3> View3s { get; set; }

    public virtual DbSet<WagonOfWay> WagonOfWays { get; set; }

    public virtual DbSet<ВыборкаВнешнихПутей> ВыборкаВнешнихПутейs { get; set; }

    public virtual DbSet<ВыборкаДляТестаПереносаВагонов> ВыборкаДляТестаПереносаВагоновs { get; set; }

    public virtual DbSet<ВыборкаПрибытий> ВыборкаПрибытийs { get; set; }

    public virtual DbSet<ПолучениеВагонов> ПолучениеВагоновs { get; set; }

    public virtual DbSet<Пятница> Пятницаs { get; set; }

    public virtual DbSet<ТестНовогоОтчета> ТестНовогоОтчетаs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=krr-sql-paclx02;Initial Catalog=KRR-PA-CNT-Railway-Archive;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<View1>(entity =>
        {
            entity.ToView("View_1");
        });

        modelBuilder.Entity<View2>(entity =>
        {
            entity.ToView("View_2");
        });

        modelBuilder.Entity<View3>(entity =>
        {
            entity.ToView("View_3");
        });

        modelBuilder.Entity<WagonOfWay>(entity =>
        {
            entity.Property(e => e.SapOutgoingSupplyBorderCheckpointCode).IsFixedLength();
            entity.Property(e => e.SapOutgoingSupplyCargoCode).IsFixedLength();
            entity.Property(e => e.SapOutgoingSupplyDestinationStationCode).IsFixedLength();
            entity.Property(e => e.SapOutgoingSupplyNum).IsFixedLength();
            entity.Property(e => e.SapOutgoingSupplyPayerCode).IsFixedLength();
            entity.Property(e => e.SapOutgoingSupplyShipperCode).IsFixedLength();
            entity.Property(e => e.SapOutgoingSupplyWarehouseCode).IsFixedLength();
            entity.Property(e => e.WirHighlightColor).IsFixedLength();
        });

        modelBuilder.Entity<ВыборкаВнешнихПутей>(entity =>
        {
            entity.ToView("Выборка внешних путей");
        });

        modelBuilder.Entity<ВыборкаДляТестаПереносаВагонов>(entity =>
        {
            entity.ToView("Выборка для теста переноса вагонов");
        });

        modelBuilder.Entity<ВыборкаПрибытий>(entity =>
        {
            entity.ToView("Выборка прибытий");
        });

        modelBuilder.Entity<ПолучениеВагонов>(entity =>
        {
            entity.ToView("Получение вагонов");
        });

        modelBuilder.Entity<Пятница>(entity =>
        {
            entity.ToView("пятница");
        });

        modelBuilder.Entity<ТестНовогоОтчета>(entity =>
        {
            entity.ToView("тест нового отчета");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
