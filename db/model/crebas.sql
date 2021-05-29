/*==============================================================*/
/* Database name:  AutoLot                                      */
/* DBMS name:      Microsoft SQL Server 2017                    */
/* Created on:     5/29/2021 2:13:35 PM                         */
/*==============================================================*/


drop database AutoLot
go

/*==============================================================*/
/* Database: AutoLot                                            */
/*==============================================================*/
create database AutoLot
go

use AutoLot
go

/*==============================================================*/
/* Table: CreditRisk                                            */
/*==============================================================*/
create table CreditRisk (
   Id                   int                  identity,
   CustomerId           int                  not null,
   FirstName            varchar(50)          not null,
   LastName             varchar(50)          not null,
   TimeStamp            timestamp            null,
   constraint PK_CreditRisk primary key (Id)
)
go

/*==============================================================*/
/* Index: IX_FK_Customer_CreditRisk                             */
/*==============================================================*/




create nonclustered index IX_FK_Customer_CreditRisk on CreditRisk (CustomerId ASC)
go

/*==============================================================*/
/* Table: Customer                                              */
/*==============================================================*/
create table Customer (
   Id                   int                  identity,
   FirstName            varchar(50)          not null,
   LastName             varchar(50)          not null,
   TimeStamp            timestamp            null,
   constraint PK_Customer primary key (Id)
)
go

/*==============================================================*/
/* Table: Inventory                                             */
/*==============================================================*/
create table Inventory (
   Id                   int                  identity,
   MakeId               int                  not null,
   Color                varchar(50)          not null,
   PetName              varchar(50)          not null,
   TimeStamp            timestamp            null,
   constraint PK_Inventory primary key (Id)
)
go

/*==============================================================*/
/* Index: IX_FK_Make_Inventory                                  */
/*==============================================================*/




create nonclustered index IX_FK_Make_Inventory on Inventory (MakeId ASC)
go

/*==============================================================*/
/* Table: Make                                                  */
/*==============================================================*/
create table Make (
   Id                   int                  identity,
   Name                 varchar(50)          not null,
   TimeStamp            timestamp            null,
   constraint PK_Make primary key (Id)
)
go

/*==============================================================*/
/* Table: "Order"                                               */
/*==============================================================*/
create table "Order" (
   Id                   int                  identity,
   CarId                int                  not null,
   CustomerId           int                  not null,
   TimeStamp            timestamp            null,
   constraint PK_Order primary key (Id)
)
go

/*==============================================================*/
/* Index: IX_FK_Inventory_Order                                 */
/*==============================================================*/




create nonclustered index IX_FK_Inventory_Order on "Order" (CarId ASC)
go

/*==============================================================*/
/* Index: IX_FK_Customer_Order                                  */
/*==============================================================*/




create nonclustered index IX_FK_Customer_Order on "Order" (CustomerId ASC,
  CarId ASC)
go

alter table CreditRisk
   add constraint FK_Customer_CreditRisk foreign key (CustomerId)
      references Customer (Id)
go

alter table Inventory
   add constraint FK_Make_Inventory foreign key (MakeId)
      references Make (Id)
go

alter table "Order"
   add constraint FK_Customer_Order foreign key (CustomerId)
      references Customer (Id)
go

alter table "Order"
   add constraint FK_Inventory_Order foreign key (CarId)
      references Inventory (Id)
go

