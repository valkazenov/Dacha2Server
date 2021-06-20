create table BasementData(
	Id int identity(1,1) not null,
		constraint pk_BasementData primary key clustered(Id),
	Temperature double precision default 0 not null,
	Humidity double precision default 0 not null,
	FanWorking int default 0 not null,
	FanSeconds int default 0 not null,
	BoilerWorking int default 0 not null,
	BoilerAmperage double precision default 0 not null,
	SupplyVoltage int default 0 not null,
	LastSyncTime int default 0 not null);
GO
insert into BasementData(Temperature) values(0);
go
create table WateringData(
	Id int identity(1,1) not null,
		constraint pk_WateringData primary key clustered(Id),
	Working int default 0 not null,
	Zone int default 0 not null,
	StartTime int default 0 not null,
	StopTime int default 0 not null,
	TankLevel int default 0 not null,
	OutsideTemperature double precision default 0 not null,
	OutsideHumidity double precision default 0 not null,
	RainPercent int default 0 not null,
	SupplyVoltage int default 0 not null,
	CurrentTime int default 0 not null,
	LastSyncTime int default 0 not null);
GO
insert into WateringData(Working) values(0);
go
create table BasementSettings(
	Id int identity(1,1) not null,
		constraint pk_BasementSettings primary key clustered(Id),
	BoilerEnabled int default 0 not null,
	FanWorkMode int default 0 not null,
	FanDisableInterval int default 0 not null,
	FanEnableInterval int default 0 not null,
	Applied int default 0 not null);
go
insert into BasementSettings(BoilerEnabled,FanWorkMode,FanDisableInterval,FanEnableInterval,Applied) values(0,0,2,3,1);
go
create table WateringSettings(
	Id int identity(1,1) not null,
		constraint pk_WateringSettings primary key clustered(Id),
	ZoneMode1 int default 0 not null,
	ZoneMode2 int default 0 not null,
	ZoneMode3 int default 0 not null,
	ZoneMode4 int default 0 not null,
	MinRainPercent int default 0 not null,
	Applied int default 0 not null);
go
insert into WateringSettings(ZoneMode1,ZoneMode2,ZoneMode3,ZoneMode4,MinRainPercent,Applied) values(1,0,0,0,15,1);
go
create table WateringTuneSettings(
	Id int identity(1,1) not null,
		constraint pk_WateringTuneSettings primary key clustered(Id),
	ZoneNumber int default 0 not null,
	WeekDay int default 0 not null,
	StartHour int default 0 not null,
	StartMinute int default 0 not null,
	StopHour int default 0 not null,
	StopMinute int default 0 not null,
	Applied int default 0 not null);
go
create index WateringTuneSettings_Zone on WateringTuneSettings(ZoneNumber);
go
