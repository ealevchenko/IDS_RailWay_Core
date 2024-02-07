using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

public partial class KrrPaCntRailwayContext : DbContext
{
    public KrrPaCntRailwayContext()
    {
    }

    public KrrPaCntRailwayContext(DbContextOptions<KrrPaCntRailwayContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ArrivalCar> ArrivalCars { get; set; }

    public virtual DbSet<ArrivalSostav> ArrivalSostavs { get; set; }

    public virtual DbSet<ArrivalUzContPay> ArrivalUzContPays { get; set; }

    public virtual DbSet<ArrivalUzDocument> ArrivalUzDocuments { get; set; }

    public virtual DbSet<ArrivalUzDocumentAct> ArrivalUzDocumentActs { get; set; }

    public virtual DbSet<ArrivalUzDocumentDoc> ArrivalUzDocumentDocs { get; set; }

    public virtual DbSet<ArrivalUzDocumentPay> ArrivalUzDocumentPays { get; set; }

    public virtual DbSet<ArrivalUzVagon> ArrivalUzVagons { get; set; }

    public virtual DbSet<ArrivalUzVagonAct> ArrivalUzVagonActs { get; set; }

    public virtual DbSet<ArrivalUzVagonCont> ArrivalUzVagonConts { get; set; }

    public virtual DbSet<ArrivalUzVagonPay> ArrivalUzVagonPays { get; set; }

    public virtual DbSet<CardsWagon> CardsWagons { get; set; }

    public virtual DbSet<CardsWagonsRepair> CardsWagonsRepairs { get; set; }

    public virtual DbSet<DirectoryBankRate> DirectoryBankRates { get; set; }

    public virtual DbSet<DirectoryBorderCheckpoint> DirectoryBorderCheckpoints { get; set; }

    public virtual DbSet<DirectoryCargo> DirectoryCargos { get; set; }

    public virtual DbSet<DirectoryCargoEtsng> DirectoryCargoEtsngs { get; set; }

    public virtual DbSet<DirectoryCargoGng> DirectoryCargoGngs { get; set; }

    public virtual DbSet<DirectoryCargoGroup> DirectoryCargoGroups { get; set; }

    public virtual DbSet<DirectoryCargoOutGroup> DirectoryCargoOutGroups { get; set; }

    public virtual DbSet<DirectoryCarsKi> DirectoryCarsKis { get; set; }

    public virtual DbSet<DirectoryCertificationDatum> DirectoryCertificationData { get; set; }

    public virtual DbSet<DirectoryCommercialCondition> DirectoryCommercialConditions { get; set; }

    public virtual DbSet<DirectoryConditionArrival> DirectoryConditionArrivals { get; set; }

    public virtual DbSet<DirectoryConsignee> DirectoryConsignees { get; set; }

    public virtual DbSet<DirectoryCountry> DirectoryCountrys { get; set; }

    public virtual DbSet<DirectoryCurrency> DirectoryCurrencies { get; set; }

    public virtual DbSet<DirectoryDepo> DirectoryDepos { get; set; }

    public virtual DbSet<DirectoryDetentionReturn> DirectoryDetentionReturns { get; set; }

    public virtual DbSet<DirectoryDivision> DirectoryDivisions { get; set; }

    public virtual DbSet<DirectoryExchangeRate> DirectoryExchangeRates { get; set; }

    public virtual DbSet<DirectoryExternalStation> DirectoryExternalStations { get; set; }

    public virtual DbSet<DirectoryGenusWagon> DirectoryGenusWagons { get; set; }

    public virtual DbSet<DirectoryHazardClass> DirectoryHazardClasses { get; set; }

    public virtual DbSet<DirectoryInlandRailway> DirectoryInlandRailways { get; set; }

    public virtual DbSet<DirectoryLessorsWagon> DirectoryLessorsWagons { get; set; }

    public virtual DbSet<DirectoryLimitingLoading> DirectoryLimitingLoadings { get; set; }

    public virtual DbSet<DirectoryLocomotive> DirectoryLocomotives { get; set; }

    public virtual DbSet<DirectoryLocomotiveStatus> DirectoryLocomotiveStatuses { get; set; }

    public virtual DbSet<DirectoryModelsWagon> DirectoryModelsWagons { get; set; }

    public virtual DbSet<DirectoryOperatorsWagon> DirectoryOperatorsWagons { get; set; }

    public virtual DbSet<DirectoryOperatorsWagonsAmkr> DirectoryOperatorsWagonsAmkrs { get; set; }

    public virtual DbSet<DirectoryOperatorsWagonsGroup> DirectoryOperatorsWagonsGroups { get; set; }

    public virtual DbSet<DirectoryOuterWay> DirectoryOuterWays { get; set; }

    public virtual DbSet<DirectoryOwnersWagon> DirectoryOwnersWagons { get; set; }

    public virtual DbSet<DirectoryParkWay> DirectoryParkWays { get; set; }

    public virtual DbSet<DirectoryPayerArrival> DirectoryPayerArrivals { get; set; }

    public virtual DbSet<DirectoryPayerArrivalOld> DirectoryPayerArrivalOlds { get; set; }

    public virtual DbSet<DirectoryPayerSender> DirectoryPayerSenders { get; set; }

    public virtual DbSet<DirectoryPoligonTravelWagon> DirectoryPoligonTravelWagons { get; set; }

    public virtual DbSet<DirectoryRailway> DirectoryRailways { get; set; }

    public virtual DbSet<DirectoryReasonDiscrepancy> DirectoryReasonDiscrepancies { get; set; }

    public virtual DbSet<DirectoryShipper> DirectoryShippers { get; set; }

    public virtual DbSet<DirectorySpecialCondition> DirectorySpecialConditions { get; set; }

    public virtual DbSet<DirectoryStation> DirectoryStations { get; set; }

    public virtual DbSet<DirectoryTypeDivision> DirectoryTypeDivisions { get; set; }

    public virtual DbSet<DirectoryTypeOwnerShip> DirectoryTypeOwnerShips { get; set; }

    public virtual DbSet<DirectoryTypeWagon> DirectoryTypeWagons { get; set; }

    public virtual DbSet<DirectoryTypesRepairsWagon> DirectoryTypesRepairsWagons { get; set; }

    public virtual DbSet<DirectoryWagon> DirectoryWagons { get; set; }

    public virtual DbSet<DirectoryWagonLoadingStatus> DirectoryWagonLoadingStatuses { get; set; }

    public virtual DbSet<DirectoryWagonManufacturer> DirectoryWagonManufacturers { get; set; }

    public virtual DbSet<DirectoryWagonOperation> DirectoryWagonOperations { get; set; }

    public virtual DbSet<DirectoryWagonsCondition> DirectoryWagonsConditions { get; set; }

    public virtual DbSet<DirectoryWagonsRent> DirectoryWagonsRents { get; set; }

    public virtual DbSet<DirectoryWay> DirectoryWays { get; set; }

    public virtual DbSet<InstructionalLetter> InstructionalLetters { get; set; }

    public virtual DbSet<InstructionalLettersWagon> InstructionalLettersWagons { get; set; }

    public virtual DbSet<OutgoingCar> OutgoingCars { get; set; }

    public virtual DbSet<OutgoingDetentionReturn> OutgoingDetentionReturns { get; set; }

    public virtual DbSet<OutgoingSostav> OutgoingSostavs { get; set; }

    public virtual DbSet<OutgoingUzContPay> OutgoingUzContPays { get; set; }

    public virtual DbSet<OutgoingUzDocument> OutgoingUzDocuments { get; set; }

    public virtual DbSet<OutgoingUzDocumentPay> OutgoingUzDocumentPays { get; set; }

    public virtual DbSet<OutgoingUzVagon> OutgoingUzVagons { get; set; }

    public virtual DbSet<OutgoingUzVagonAct> OutgoingUzVagonActs { get; set; }

    public virtual DbSet<OutgoingUzVagonCont> OutgoingUzVagonConts { get; set; }

    public virtual DbSet<OutgoingUzVagonPay> OutgoingUzVagonPays { get; set; }

    public virtual DbSet<ParkStateStation> ParkStateStations { get; set; }

    public virtual DbSet<ParkStateWagon> ParkStateWagons { get; set; }

    public virtual DbSet<ParkStateWay> ParkStateWays { get; set; }

    public virtual DbSet<ParksListWagon> ParksListWagons { get; set; }

    public virtual DbSet<ParksWagon> ParksWagons { get; set; }

    public virtual DbSet<SapincomingSupply> SapincomingSupplies { get; set; }

    public virtual DbSet<SapoutgoingSupply> SapoutgoingSupplies { get; set; }

    public virtual DbSet<Setting> Settings { get; set; }

    public virtual DbSet<UsageFeePeriod> UsageFeePeriods { get; set; }

    public virtual DbSet<UzDoc> UzDocs { get; set; }

    public virtual DbSet<UzDocOut> UzDocOuts { get; set; }

    public virtual DbSet<WagonInternalMovement> WagonInternalMovements { get; set; }

    public virtual DbSet<WagonInternalOperation> WagonInternalOperations { get; set; }

    public virtual DbSet<WagonInternalRoute> WagonInternalRoutes { get; set; }

    public virtual DbSet<WagonUsageFee> WagonUsageFees { get; set; }

    public virtual DbSet<WagonsMotionSignal> WagonsMotionSignals { get; set; }

    public virtual DbSet<WebAccess> WebAccesses { get; set; }

    public virtual DbSet<WebView> WebViews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=krr-sql-paclx03;initial catalog=KRR-PA-CNT-Railway;integrated security=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArrivalCar>(entity =>
        {
            entity.HasOne(d => d.IdArrivalNavigation).WithMany(p => p.ArrivalCars).HasConstraintName("FK_ArrivalCars_ArrivalSostav");

            entity.HasOne(d => d.IdArrivalUzVagonNavigation).WithMany(p => p.ArrivalCars).HasConstraintName("FK_ArrivalCars_Arrival_UZ_Vagon");

            entity.HasOne(d => d.NumDocNavigation).WithMany(p => p.ArrivalCars).HasConstraintName("FK_ArrivalCars_UZ_DOC");
        });

        modelBuilder.Entity<ArrivalSostav>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ArrivalSostav1");

            entity.HasOne(d => d.IdStationFromNavigation).WithMany(p => p.ArrivalSostavIdStationFromNavigations).HasConstraintName("FK_ArrivalSostav_Directory_Station");

            entity.HasOne(d => d.IdStationOnNavigation).WithMany(p => p.ArrivalSostavIdStationOnNavigations).HasConstraintName("FK_ArrivalSostav_Directory_Station1");

            entity.HasOne(d => d.IdWayNavigation).WithMany(p => p.ArrivalSostavs).HasConstraintName("FK_ArrivalSostav_Directory_Ways");
        });

        modelBuilder.Entity<ArrivalUzContPay>(entity =>
        {
            entity.HasOne(d => d.IdContNavigation).WithMany(p => p.ArrivalUzContPays)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Arrival_UZ_Cont_Pay_Arrival_UZ_Vagon_Cont");
        });

        modelBuilder.Entity<ArrivalUzDocument>(entity =>
        {
            entity.HasOne(d => d.CodeBorderCheckpointNavigation).WithMany(p => p.ArrivalUzDocuments).HasConstraintName("FK_Arrival_UZ_Document_Directory_BorderCheckpoint");

            entity.HasOne(d => d.CodeConsigneeNavigation).WithMany(p => p.ArrivalUzDocuments).HasConstraintName("FK_Arrival_UZ_Document_Directory_Consignee");

            entity.HasOne(d => d.CodePayerSenderNavigation).WithMany(p => p.ArrivalUzDocuments).HasConstraintName("FK_Arrival_UZ_Document_Directory_PayerSender");

            entity.HasOne(d => d.CodeShipperNavigation).WithMany(p => p.ArrivalUzDocuments).HasConstraintName("FK_Arrival_UZ_Document_Directory_Shipper");

            entity.HasOne(d => d.CodeStnFromNavigation).WithMany(p => p.ArrivalUzDocumentCodeStnFromNavigations).HasConstraintName("FK_Arrival_UZ_Document_Directory_ExternalStation");

            entity.HasOne(d => d.CodeStnToNavigation).WithMany(p => p.ArrivalUzDocumentCodeStnToNavigations).HasConstraintName("FK_Arrival_UZ_Document_Directory_ExternalStation1");

            entity.HasOne(d => d.IdDocUzNavigation).WithMany(p => p.ArrivalUzDocuments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Arrival_UZ_Document_UZ_DOC");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasConstraintName("FK_Arrival_UZ_Document_Arrival_UZ_Document");
        });

        modelBuilder.Entity<ArrivalUzDocumentAct>(entity =>
        {
            entity.HasOne(d => d.IdDocumentNavigation).WithMany(p => p.ArrivalUzDocumentActs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Arrival_UZ_Document_Acts_Arrival_UZ_Document");
        });

        modelBuilder.Entity<ArrivalUzDocumentDoc>(entity =>
        {
            entity.Property(e => e.Doc).IsFixedLength();

            entity.HasOne(d => d.IdDocumentNavigation).WithMany(p => p.ArrivalUzDocumentDocs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Arrival_UZ_Document_Docs_Arrival_UZ_Document");
        });

        modelBuilder.Entity<ArrivalUzDocumentPay>(entity =>
        {
            entity.HasOne(d => d.IdDocumentNavigation).WithMany(p => p.ArrivalUzDocumentPays)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Arrival_UZ_Document_Pay_Arrival_UZ_Document");
        });

        modelBuilder.Entity<ArrivalUzVagon>(entity =>
        {
            entity.Property(e => e.Danger).IsFixedLength();
            entity.Property(e => e.DangerKod).IsFixedLength();

            entity.HasOne(d => d.DangerNavigation).WithMany(p => p.ArrivalUzVagons).HasConstraintName("FK_Arrival_UZ_Vagon_Directory_HazardClass");

            entity.HasOne(d => d.IdArrivalNavigation).WithMany(p => p.ArrivalUzVagons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Arrival_UZ_Vagon_ArrivalSostav");

            entity.HasOne(d => d.IdCargoNavigation).WithMany(p => p.ArrivalUzVagons).HasConstraintName("FK_Arrival_UZ_Vagon_Directory_Cargo");

            entity.HasOne(d => d.IdCargoGngNavigation).WithMany(p => p.ArrivalUzVagons).HasConstraintName("FK_Arrival_UZ_Vagon_Directory_CargoGNG");

            entity.HasOne(d => d.IdCertificationDataNavigation).WithMany(p => p.ArrivalUzVagons).HasConstraintName("FK_Arrival_UZ_Vagon_Directory_CertificationData");

            entity.HasOne(d => d.IdCommercialConditionNavigation).WithMany(p => p.ArrivalUzVagons).HasConstraintName("FK_Arrival_UZ_Vagon_Directory_CommercialCondition");

            entity.HasOne(d => d.IdConditionNavigation).WithMany(p => p.ArrivalUzVagons).HasConstraintName("FK_Arrival_UZ_Vagon_Directory_ConditionArrival");

            entity.HasOne(d => d.IdCountrysNavigation).WithMany(p => p.ArrivalUzVagons).HasConstraintName("FK_Arrival_UZ_Vagon_Directory_Countrys");

            entity.HasOne(d => d.IdDivisionOnAmkrNavigation).WithMany(p => p.ArrivalUzVagons).HasConstraintName("FK_Arrival_UZ_Vagon_Directory_Divisions");

            entity.HasOne(d => d.IdDocumentNavigation).WithMany(p => p.ArrivalUzVagons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Arrival_UZ_Vagon_Arrival_UZ_Document");

            entity.HasOne(d => d.IdGenusNavigation).WithMany(p => p.ArrivalUzVagons).HasConstraintName("FK_Arrival_UZ_Vagon_Directory_GenusWagons");

            entity.HasOne(d => d.IdOwnerNavigation).WithMany(p => p.ArrivalUzVagons).HasConstraintName("FK_Arrival_UZ_Vagon_Directory_OwnersWagons");

            entity.HasOne(d => d.IdStationOnAmkrNavigation).WithMany(p => p.ArrivalUzVagons).HasConstraintName("FK_Arrival_UZ_Vagon_Directory_Station");

            entity.HasOne(d => d.IdTypeNavigation).WithMany(p => p.ArrivalUzVagons).HasConstraintName("FK_Arrival_UZ_Vagon_Directory_TypeWagons");

            entity.HasOne(d => d.IdTypeOwnershipNavigation).WithMany(p => p.ArrivalUzVagons).HasConstraintName("FK_Arrival_UZ_Vagon_Directory_TypeOwnerShip");

            entity.HasOne(d => d.IdWagonsRentArrivalNavigation).WithMany(p => p.ArrivalUzVagons).HasConstraintName("FK_Arrival_UZ_Vagon_Directory_WagonsRent");

            entity.HasOne(d => d.NumNavigation).WithMany(p => p.ArrivalUzVagons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Arrival_UZ_Vagon_Directory_Wagons");
        });

        modelBuilder.Entity<ArrivalUzVagonAct>(entity =>
        {
            entity.HasOne(d => d.IdVagonNavigation).WithMany(p => p.ArrivalUzVagonActs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Arrival_UZ_Vagon_Acts_Arrival_UZ_Vagon");
        });

        modelBuilder.Entity<ArrivalUzVagonCont>(entity =>
        {
            entity.HasOne(d => d.IdCargoNavigation).WithMany(p => p.ArrivalUzVagonConts).HasConstraintName("FK_Arrival_UZ_Vagon_Cont_Directory_Cargo");

            entity.HasOne(d => d.IdCargoGngNavigation).WithMany(p => p.ArrivalUzVagonConts).HasConstraintName("FK_Arrival_UZ_Vagon_Cont_Directory_CargoGNG");

            entity.HasOne(d => d.IdVagonNavigation).WithMany(p => p.ArrivalUzVagonConts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Arrival_UZ_Vagon_Cont_Arrival_UZ_Vagon");
        });

        modelBuilder.Entity<ArrivalUzVagonPay>(entity =>
        {
            entity.HasOne(d => d.IdVagonNavigation).WithMany(p => p.ArrivalUzVagonPays)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Arrival_UZ_Vagon_Pay_Arrival_UZ_Vagon");
        });

        modelBuilder.Entity<CardsWagon>(entity =>
        {
            entity.Property(e => e.Num).ValueGeneratedNever();

            entity.HasOne(d => d.CodeModelWagonNavigation).WithMany(p => p.CardsWagons).HasConstraintName("FK_CardsWagons_Directory_ModelsWagons");

            entity.HasOne(d => d.IdGenusWagonNavigation).WithMany(p => p.CardsWagons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CardsWagons_Directory_GenusWagons");

            entity.HasOne(d => d.IdLessorWagonNavigation).WithMany(p => p.CardsWagons).HasConstraintName("FK_CardsWagons_Directory_LessorsWagons");

            entity.HasOne(d => d.IdOperatorWagonNavigation).WithMany(p => p.CardsWagons).HasConstraintName("FK_CardsWagons_Directory_OperatorsWagons");

            entity.HasOne(d => d.IdOwnerWagonNavigation).WithMany(p => p.CardsWagons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CardsWagons_Directory_OwnersWagons");

            entity.HasOne(d => d.IdPoligonTravelWagonNavigation).WithMany(p => p.CardsWagons).HasConstraintName("FK_CardsWagons_Directory_PoligonTravelWagons");

            entity.HasOne(d => d.IdSpecialConditionsNavigation).WithMany(p => p.CardsWagons).HasConstraintName("FK_CardsWagons_Directory_SpecialConditions");

            entity.HasOne(d => d.IdTypeOwnershipNavigation).WithMany(p => p.CardsWagons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CardsWagons_Directory_TypeOwnerShip");

            entity.HasOne(d => d.IdTypeRepairsNavigation).WithMany(p => p.CardsWagons).HasConstraintName("FK_CardsWagons_Directory_TypesRepairsWagons");

            entity.HasOne(d => d.IdTypeWagonNavigation).WithMany(p => p.CardsWagons).HasConstraintName("FK_CardsWagons_Directory_TypeWagons");

            entity.HasOne(d => d.IdWagonManufacturerNavigation).WithMany(p => p.CardsWagons).HasConstraintName("FK_CardsWagons_Directory_WagonManufacturers");
        });

        modelBuilder.Entity<CardsWagonsRepair>(entity =>
        {
            entity.HasOne(d => d.CodeDepoNavigation).WithMany(p => p.CardsWagonsRepairs).HasConstraintName("FK_CardsWagonsRepairs_Directory_DEPO");

            entity.HasOne(d => d.IdTypeRepairWagonNavigation).WithMany(p => p.CardsWagonsRepairs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CardsWagonsRepairs_Directory_TypesRepairsWagons");

            entity.HasOne(d => d.IdWagonsConditionNavigation).WithMany(p => p.CardsWagonsRepairs).HasConstraintName("FK_CardsWagonsRepairs_Directory_WagonsCondition");

            entity.HasOne(d => d.NumNavigation).WithMany(p => p.CardsWagonsRepairs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CardsWagonsRepairs_CardsWagons");
        });

        modelBuilder.Entity<DirectoryBorderCheckpoint>(entity =>
        {
            entity.Property(e => e.Code).ValueGeneratedNever();

            entity.HasOne(d => d.CodeInlandrailwayNavigation).WithMany(p => p.DirectoryBorderCheckpoints)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Directory_BorderCheckpoint_Directory_InlandRailway");
        });

        modelBuilder.Entity<DirectoryCargo>(entity =>
        {
            entity.HasOne(d => d.IdCargoEtsngNavigation).WithMany(p => p.DirectoryCargos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Directory_Cargo_Directory_CargoETSNG");

            entity.HasOne(d => d.IdGroupNavigation).WithMany(p => p.DirectoryCargos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Directory_Cargo_Directory_CargoGroup");

            entity.HasOne(d => d.IdOutGroupNavigation).WithMany(p => p.DirectoryCargos).HasConstraintName("FK_Directory_Cargo_Directory_CargoOutGroup");
        });

        modelBuilder.Entity<DirectoryCarsKi>(entity =>
        {
            entity.HasKey(e => e.Num).HasName("PK_IDS_Directory_Cars_KIS");

            entity.Property(e => e.Num).ValueGeneratedNever();

            entity.HasOne(d => d.IdGenusNavigation).WithMany(p => p.DirectoryCarsKis).HasConstraintName("FK_Directory_Cars_KIS_Directory_GenusWagons");

            entity.HasOne(d => d.IdLimitingNavigation).WithMany(p => p.DirectoryCarsKis).HasConstraintName("FK_Directory_Cars_KIS_Directory_LimitingLoading");

            entity.HasOne(d => d.IdOperatorNavigation).WithMany(p => p.DirectoryCarsKis).HasConstraintName("FK_Directory_Cars_KIS_Directory_OperatorsWagons");
        });

        modelBuilder.Entity<DirectoryConsignee>(entity =>
        {
            entity.Property(e => e.Code).ValueGeneratedNever();
        });

        modelBuilder.Entity<DirectoryCurrency>(entity =>
        {
            entity.Property(e => e.CodeCc).IsFixedLength();
        });

        modelBuilder.Entity<DirectoryDepo>(entity =>
        {
            entity.Property(e => e.Code).ValueGeneratedNever();
        });

        modelBuilder.Entity<DirectoryDivision>(entity =>
        {
            entity.HasOne(d => d.IdTypeDevisionNavigation).WithMany(p => p.DirectoryDivisions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Directory_Divisions_Directory_TypeDivision");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasConstraintName("FK_Directory_Divisions_Directory_Divisions");
        });

        modelBuilder.Entity<DirectoryExchangeRate>(entity =>
        {
            entity.Property(e => e.Re).ValueGeneratedNever();
        });

        modelBuilder.Entity<DirectoryExternalStation>(entity =>
        {
            entity.Property(e => e.Code).ValueGeneratedNever();

            entity.HasOne(d => d.CodeInlandrailwayNavigation).WithMany(p => p.DirectoryExternalStations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Directory_ExternalStation_Directory_InlandRailway");
        });

        modelBuilder.Entity<DirectoryHazardClass>(entity =>
        {
            entity.Property(e => e.Code).IsFixedLength();
        });

        modelBuilder.Entity<DirectoryInlandRailway>(entity =>
        {
            entity.Property(e => e.Code).ValueGeneratedNever();

            entity.HasOne(d => d.CodeRailwayNavigation).WithMany(p => p.DirectoryInlandRailways)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Directory_InlandRailway_Directory_Railway");
        });

        modelBuilder.Entity<DirectoryLocomotive>(entity =>
        {
            entity.HasOne(d => d.IdLocomotiveStatusNavigation).WithMany(p => p.DirectoryLocomotives)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Directory_Locomotive_Directory_LocomotiveStatus");
        });

        modelBuilder.Entity<DirectoryOperatorsWagon>(entity =>
        {
            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasConstraintName("FK_Directory_OperatorsWagons_Directory_OperatorsWagons");
        });

        modelBuilder.Entity<DirectoryOuterWay>(entity =>
        {
            entity.HasOne(d => d.IdParkFromNavigation).WithMany(p => p.DirectoryOuterWayIdParkFromNavigations).HasConstraintName("FK_Directory_OuterWays_Directory_ParkWays");

            entity.HasOne(d => d.IdParkOnNavigation).WithMany(p => p.DirectoryOuterWayIdParkOnNavigations).HasConstraintName("FK_Directory_OuterWays_Directory_ParkWays1");

            entity.HasOne(d => d.IdStationFromNavigation).WithMany(p => p.DirectoryOuterWayIdStationFromNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Directory_OuterWays_Directory_Station1");

            entity.HasOne(d => d.IdStationOnNavigation).WithMany(p => p.DirectoryOuterWayIdStationOnNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Directory_OuterWays_Directory_Station");

            entity.HasOne(d => d.IdWayFromNavigation).WithMany(p => p.DirectoryOuterWayIdWayFromNavigations).HasConstraintName("FK_Directory_OuterWays_Directory_Ways");

            entity.HasOne(d => d.IdWayOnNavigation).WithMany(p => p.DirectoryOuterWayIdWayOnNavigations).HasConstraintName("FK_Directory_OuterWays_Directory_Ways1");
        });

        modelBuilder.Entity<DirectoryRailway>(entity =>
        {
            entity.Property(e => e.Code).ValueGeneratedNever();

            entity.HasOne(d => d.IdCountrysNavigation).WithMany(p => p.DirectoryRailways)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Directory_Railway_Directory_Countrys");
        });

        modelBuilder.Entity<DirectoryShipper>(entity =>
        {
            entity.Property(e => e.Code).ValueGeneratedNever();
        });

        modelBuilder.Entity<DirectoryWagon>(entity =>
        {
            entity.Property(e => e.Num).ValueGeneratedNever();
            entity.Property(e => e.NewConstruction).IsFixedLength();

            entity.HasOne(d => d.IdCountrysNavigation).WithMany(p => p.DirectoryWagons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Directory_Wagons_Directory_Countrys");

            entity.HasOne(d => d.IdGenusNavigation).WithMany(p => p.DirectoryWagons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Directory_Wagons_Directory_GenusWagons");

            entity.HasOne(d => d.IdOperatorNavigation).WithMany(p => p.DirectoryWagons).HasConstraintName("FK_Directory_Wagons_Directory_OperatorsWagons");

            entity.HasOne(d => d.IdOwnerNavigation).WithMany(p => p.DirectoryWagons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Directory_Wagons_Directory_OwnersWagons");

            entity.HasOne(d => d.IdTypeOwnershipNavigation).WithMany(p => p.DirectoryWagons).HasConstraintName("FK_Directory_Wagons_Directory_TypeOwnerShip");
        });

        modelBuilder.Entity<DirectoryWagonsRent>(entity =>
        {
            entity.HasOne(d => d.IdLimitingNavigation).WithMany(p => p.DirectoryWagonsRents).HasConstraintName("FK_Directory_WagonsRent_Directory_LimitingLoading");

            entity.HasOne(d => d.IdOperatorNavigation).WithMany(p => p.DirectoryWagonsRents).HasConstraintName("FK_Directory_WagonsRent_Directory_OperatorsWagons");

            entity.HasOne(d => d.NumNavigation).WithMany(p => p.DirectoryWagonsRents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Directory_WagonsRent_Directory_Wagons");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasConstraintName("FK_Directory_WagonsRent_Directory_WagonsRent");
        });

        modelBuilder.Entity<DirectoryWay>(entity =>
        {
            entity.HasOne(d => d.IdDevisionNavigation).WithMany(p => p.DirectoryWays).HasConstraintName("FK_Directory_Ways_Directory_Divisions");

            entity.HasOne(d => d.IdParkNavigation).WithMany(p => p.DirectoryWays)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Directory_Ways_Directory_ParkWays");

            entity.HasOne(d => d.IdStationNavigation).WithMany(p => p.DirectoryWays)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Directory_Ways_Directory_Station");
        });

        modelBuilder.Entity<InstructionalLettersWagon>(entity =>
        {
            entity.HasOne(d => d.IdInstructionalLettersNavigation).WithMany(p => p.InstructionalLettersWagons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InstructionalLettersWagon_InstructionalLetters");
        });

        modelBuilder.Entity<OutgoingCar>(entity =>
        {
            entity.Property(e => e.VagonnikUser).IsFixedLength();

            entity.HasOne(d => d.IdOutgoingNavigation).WithMany(p => p.OutgoingCars).HasConstraintName("FK_OutgoingCars_OutgoingSostav");

            entity.HasOne(d => d.IdOutgoingDetentionNavigation).WithMany(p => p.OutgoingCarIdOutgoingDetentionNavigations).HasConstraintName("FK_OutgoingCars_OutgoingDetentionReturn1");

            entity.HasOne(d => d.IdOutgoingReturnStartNavigation).WithMany(p => p.OutgoingCarIdOutgoingReturnStartNavigations).HasConstraintName("FK_OutgoingCars_OutgoingDetentionReturn2");

            entity.HasOne(d => d.IdOutgoingReturnStopNavigation).WithMany(p => p.OutgoingCarIdOutgoingReturnStopNavigations).HasConstraintName("FK_OutgoingCars_OutgoingDetentionReturn3");

            entity.HasOne(d => d.IdOutgoingUzVagonNavigation).WithMany(p => p.OutgoingCars).HasConstraintName("FK_OutgoingCars_Outgoing_UZ_Vagon");

            entity.HasOne(d => d.IdReasonDiscrepancyAmkrNavigation).WithMany(p => p.OutgoingCarIdReasonDiscrepancyAmkrNavigations).HasConstraintName("FK_OutgoingCars_Directory_Reason_Discrepancy");

            entity.HasOne(d => d.IdReasonDiscrepancyUzNavigation).WithMany(p => p.OutgoingCarIdReasonDiscrepancyUzNavigations).HasConstraintName("FK_OutgoingCars_Directory_Reason_Discrepancy1");

            entity.HasOne(d => d.NumDocNavigation).WithMany(p => p.OutgoingCars).HasConstraintName("FK_OutgoingCars_UZ_DOC_OUT");
        });

        modelBuilder.Entity<OutgoingDetentionReturn>(entity =>
        {
            entity.HasOne(d => d.IdDetentionReturnNavigation).WithMany(p => p.OutgoingDetentionReturns)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OutgoingDetentionReturn_Directory_DetentionReturn");
        });

        modelBuilder.Entity<OutgoingSostav>(entity =>
        {
            entity.HasOne(d => d.IdStationFromNavigation).WithMany(p => p.OutgoingSostavIdStationFromNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OutgoingSostav_Directory_Station");

            entity.HasOne(d => d.IdStationOnNavigation).WithMany(p => p.OutgoingSostavIdStationOnNavigations).HasConstraintName("FK_OutgoingSostav_Directory_Station1");

            entity.HasOne(d => d.IdWayFromNavigation).WithMany(p => p.OutgoingSostavs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OutgoingSostav_Directory_Ways");
        });

        modelBuilder.Entity<OutgoingUzContPay>(entity =>
        {
            entity.HasOne(d => d.IdContNavigation).WithMany(p => p.OutgoingUzContPays)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Outgoing_UZ_Cont_Pay_Outgoing_UZ_Vagon_Cont");
        });

        modelBuilder.Entity<OutgoingUzDocument>(entity =>
        {
            entity.HasOne(d => d.CodeBorderCheckpointNavigation).WithMany(p => p.OutgoingUzDocuments).HasConstraintName("FK_Outgoing_UZ_Document_Directory_BorderCheckpoint");

            entity.HasOne(d => d.CodeConsigneeNavigation).WithMany(p => p.OutgoingUzDocuments).HasConstraintName("FK_Outgoing_UZ_Document_Directory_Shipper1");

            entity.HasOne(d => d.CodePayerNavigation).WithMany(p => p.OutgoingUzDocuments).HasConstraintName("FK_Outgoing_UZ_Document_Directory_PayerSender");

            entity.HasOne(d => d.CodeShipperNavigation).WithMany(p => p.OutgoingUzDocuments).HasConstraintName("FK_Outgoing_UZ_Document_Directory_Consignee");

            entity.HasOne(d => d.CodeStnFromNavigation).WithMany(p => p.OutgoingUzDocumentCodeStnFromNavigations).HasConstraintName("FK_Outgoing_UZ_Document_Directory_ExternalStation");

            entity.HasOne(d => d.CodeStnToNavigation).WithMany(p => p.OutgoingUzDocumentCodeStnToNavigations).HasConstraintName("FK_Outgoing_UZ_Document_Directory_ExternalStation1");
        });

        modelBuilder.Entity<OutgoingUzDocumentPay>(entity =>
        {
            entity.HasOne(d => d.IdDocumentNavigation).WithMany(p => p.OutgoingUzDocumentPays)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Outgoing_UZ_Document_Pay_Outgoing_UZ_Document");
        });

        modelBuilder.Entity<OutgoingUzVagon>(entity =>
        {
            entity.HasOne(d => d.CodeStnToNavigation).WithMany(p => p.OutgoingUzVagons).HasConstraintName("FK_Outgoing_UZ_Vagon_Directory_ExternalStation");

            entity.HasOne(d => d.IdCargoNavigation).WithMany(p => p.OutgoingUzVagons).HasConstraintName("FK_Outgoing_UZ_Vagon_Directory_Cargo");

            entity.HasOne(d => d.IdCargoGngNavigation).WithMany(p => p.OutgoingUzVagons).HasConstraintName("FK_Outgoing_UZ_Vagon_Directory_CargoGNG");

            entity.HasOne(d => d.IdConditionNavigation).WithMany(p => p.OutgoingUzVagons).HasConstraintName("FK_Outgoing_UZ_Vagon_Directory_ConditionArrival");

            entity.HasOne(d => d.IdCountrysNavigation).WithMany(p => p.OutgoingUzVagons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Outgoing_UZ_Vagon_Directory_Countrys");

            entity.HasOne(d => d.IdDivisionNavigation).WithMany(p => p.OutgoingUzVagons).HasConstraintName("FK_Outgoing_UZ_Vagon_Directory_Divisions");

            entity.HasOne(d => d.IdDocumentNavigation).WithMany(p => p.OutgoingUzVagons).HasConstraintName("FK_Outgoing_UZ_Vagon_Outgoing_UZ_Document");

            entity.HasOne(d => d.IdGenusNavigation).WithMany(p => p.OutgoingUzVagons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Outgoing_UZ_Vagon_Directory_GenusWagons");

            entity.HasOne(d => d.IdOutgoingNavigation).WithMany(p => p.OutgoingUzVagons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Outgoing_UZ_Vagon_OutgoingSostav");

            entity.HasOne(d => d.IdOwnerNavigation).WithMany(p => p.OutgoingUzVagons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Outgoing_UZ_Vagon_Directory_OwnersWagons");

            entity.HasOne(d => d.IdWagonsRentArrivalNavigation).WithMany(p => p.OutgoingUzVagonIdWagonsRentArrivalNavigations).HasConstraintName("FK_Outgoing_UZ_Vagon_Directory_WagonsRent");

            entity.HasOne(d => d.IdWagonsRentOutgoingNavigation).WithMany(p => p.OutgoingUzVagonIdWagonsRentOutgoingNavigations).HasConstraintName("FK_Outgoing_UZ_Vagon_Directory_WagonsRent1");
        });

        modelBuilder.Entity<OutgoingUzVagonAct>(entity =>
        {
            entity.HasOne(d => d.IdVagonNavigation).WithMany(p => p.OutgoingUzVagonActs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Outgoing_UZ_Vagon_Acts_Outgoing_UZ_Vagon");
        });

        modelBuilder.Entity<OutgoingUzVagonCont>(entity =>
        {
            entity.HasOne(d => d.IdCargoNavigation).WithMany(p => p.OutgoingUzVagonConts).HasConstraintName("FK_Outgoing_UZ_Vagon_Cont_Directory_Cargo");

            entity.HasOne(d => d.IdCargoGngNavigation).WithMany(p => p.OutgoingUzVagonConts).HasConstraintName("FK_Outgoing_UZ_Vagon_Cont_Directory_CargoGNG");

            entity.HasOne(d => d.IdVagonNavigation).WithMany(p => p.OutgoingUzVagonConts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Outgoing_UZ_Vagon_Cont_Outgoing_UZ_Vagon");
        });

        modelBuilder.Entity<OutgoingUzVagonPay>(entity =>
        {
            entity.HasOne(d => d.IdVagonNavigation).WithMany(p => p.OutgoingUzVagonPays)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Outgoing_UZ_Vagon_Pay_Outgoing_UZ_Vagon");
        });

        modelBuilder.Entity<ParkStateStation>(entity =>
        {
            entity.HasOne(d => d.IdStationNavigation).WithMany(p => p.ParkStateStations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParkState_Station_Directory_Station");
        });

        modelBuilder.Entity<ParkStateWagon>(entity =>
        {
            entity.HasOne(d => d.IdParkStateWayNavigation).WithMany(p => p.ParkStateWagons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParkState_Wagon_ParkState_Way");
        });

        modelBuilder.Entity<ParkStateWay>(entity =>
        {
            entity.HasOne(d => d.IdParkStateStationNavigation).WithMany(p => p.ParkStateWays)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParkState_Way_ParkState_Station");

            entity.HasOne(d => d.IdWayNavigation).WithMany(p => p.ParkStateWays)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParkState_Way_Directory_Ways");
        });

        modelBuilder.Entity<ParksListWagon>(entity =>
        {
            entity.HasOne(d => d.IdParkWagonNavigation).WithMany(p => p.ParksListWagons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParksListWagons_ParksWagons");

            entity.HasOne(d => d.NumNavigation).WithMany(p => p.ParksListWagons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParksListWagons_CardsWagons");
        });

        modelBuilder.Entity<SapincomingSupply>(entity =>
        {
            entity.HasOne(d => d.IdArrivalCarNavigation).WithMany(p => p.SapincomingSupplies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SAPIncomingSupply_ArrivalCars");
        });

        modelBuilder.Entity<SapoutgoingSupply>(entity =>
        {
            entity.Property(e => e.Abtnr).IsFixedLength();
            entity.Property(e => e.KunnrAg).IsFixedLength();
            entity.Property(e => e.Stawn).IsFixedLength();
            entity.Property(e => e.Vbeln).IsFixedLength();
            entity.Property(e => e.Zcrossstat).IsFixedLength();
            entity.Property(e => e.Zendstat).IsFixedLength();
            entity.Property(e => e.Zzplatel).IsFixedLength();

            entity.HasOne(d => d.IdOutgoingCarNavigation).WithMany(p => p.SapoutgoingSupplies).HasConstraintName("FK_SAPOutgoingSupply_OutgoingCars");
        });

        modelBuilder.Entity<UsageFeePeriod>(entity =>
        {
            entity.HasOne(d => d.IdCurrencyNavigation).WithMany(p => p.UsageFeePeriodIdCurrencyNavigations).HasConstraintName("FK_Usage_Fee_Period_Directory_Currency2");

            entity.HasOne(d => d.IdCurrencyDerailmentNavigation).WithMany(p => p.UsageFeePeriodIdCurrencyDerailmentNavigations).HasConstraintName("FK_Usage_Fee_Period_Directory_Currency3");

            entity.HasOne(d => d.IdGenusNavigation).WithMany(p => p.UsageFeePeriods)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usage_Fee_Period_Directory_GenusWagons");

            entity.HasOne(d => d.IdOperatorNavigation).WithMany(p => p.UsageFeePeriods)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usage_Fee_Period_Directory_OperatorsWagons");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasConstraintName("FK_Usage_Fee_Period_Usage_Fee_Period");
        });

        modelBuilder.Entity<WagonInternalMovement>(entity =>
        {
            entity.HasOne(d => d.IdOuterWayNavigation).WithMany(p => p.WagonInternalMovements).HasConstraintName("FK_WagonInternalMovement_Directory_OuterWays");

            entity.HasOne(d => d.IdStationNavigation).WithMany(p => p.WagonInternalMovements)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WagonInternalMovement_Directory_Station");

            entity.HasOne(d => d.IdWagonInternalRoutesNavigation).WithMany(p => p.WagonInternalMovements)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WagonInternalMovement_WagonInternalRoutes");

            entity.HasOne(d => d.IdWayNavigation).WithMany(p => p.WagonInternalMovements)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WagonInternalMovement_Directory_Ways");

            entity.HasOne(d => d.IdWioNavigation).WithMany(p => p.WagonInternalMovements).HasConstraintName("FK_WagonInternalMovement_WagonInternalOperation");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasConstraintName("FK_WagonInternalMovement_WagonInternalMovement");
        });

        modelBuilder.Entity<WagonInternalOperation>(entity =>
        {
            entity.HasOne(d => d.IdConditionNavigation).WithMany(p => p.WagonInternalOperations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WagonInternalOperation_Directory_ConditionArrival");

            entity.HasOne(d => d.IdLoadingStatusNavigation).WithMany(p => p.WagonInternalOperations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WagonInternalOperation_Directory_WagonLoadingStatus");

            entity.HasOne(d => d.IdOperationNavigation).WithMany(p => p.WagonInternalOperations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WagonInternalOperation_Directory_WagonOperations");

            entity.HasOne(d => d.IdWagonInternalRoutesNavigation).WithMany(p => p.WagonInternalOperations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WagonInternalOperation_WagonInternalRoutes");

            entity.HasOne(d => d.Locomotive1Navigation).WithMany(p => p.WagonInternalOperationLocomotive1Navigations).HasConstraintName("FK_WagonInternalOperation_Directory_Locomotive");

            entity.HasOne(d => d.Locomotive2Navigation).WithMany(p => p.WagonInternalOperationLocomotive2Navigations).HasConstraintName("FK_WagonInternalOperation_Directory_Locomotive1");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasConstraintName("FK_WagonInternalOperation_WagonInternalOperation");
        });

        modelBuilder.Entity<WagonInternalRoute>(entity =>
        {
            entity.Property(e => e.HighlightColor).IsFixedLength();

            entity.HasOne(d => d.IdArrivalCarNavigation).WithMany(p => p.WagonInternalRoutes).HasConstraintName("FK_WagonInternalRoutes_ArrivalCars");

            entity.HasOne(d => d.IdOutgoingCarNavigation).WithMany(p => p.WagonInternalRoutes).HasConstraintName("FK_WagonInternalRoutes_OutgoingCars");

            entity.HasOne(d => d.IdSapIncomingSupplyNavigation).WithMany(p => p.WagonInternalRoutes).HasConstraintName("FK_WagonInternalRoutes_SAPIncomingSupply");

            entity.HasOne(d => d.IdSapOutboundSupplyNavigation).WithMany(p => p.WagonInternalRoutes).HasConstraintName("FK_WagonInternalRoutes_SAPOutgoingSupply");

            entity.HasOne(d => d.IdUsageFeeNavigation).WithMany(p => p.WagonInternalRoutes).HasConstraintName("FK_WagonInternalRoutes_WagonUsageFee");

            entity.HasOne(d => d.NumNavigation).WithMany(p => p.WagonInternalRoutes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WagonInternalRoutes_Directory_Wagons");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasConstraintName("FK_WagonInternalRoutes_WagonInternalRoutes");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
