
create database DB_Soft_Transporte
go

use DB_Soft_Transporte
go

create table CAMIONES (
idCamion int primary key identity(1,1) not null,
modelo varchar(50),
patente varchar(20),
capacidad decimal(10,2),
vencTecnica date,
vencRuta date,
vencSeguro date,
vencTazaFiscal date,
cambioAceite date,
FechaRegistro datetime default getdate() 
)
go

create table CHOFERES (
idChofer int primary key identity(1,1) not null,
nombreChofer varchar(100),
documento varchar(50),
vencLicencia date,
vencLinti date,
telefono varchar(50),
antiguedad int,
FechaRegistro datetime default getdate() 
)
go

--EXEC sp_rename 'CLIENTES.nombre', 'nombreCliente', 'COLUMN';
--GO

select idChofer,nombre,documento,vencLicencia,vencLinti,telefono,antiguedad from CHOFERES

create table CLIENTES(
idCliente int primary key identity(1,1) not null,
nombreCliente varchar(100),
telCliente varchar(50),
direccion varchar(255),
correo varchar(100),
observacion varchar(255),
FechaRegistro datetime default getdate() 
)
go


create table VIAJES(
idViaje int primary key identity(1,1) not null,
nroRendicion varchar(50),
idCamion int,
idChofer int,
idCliente int,
argEntregado decimal(10,2),
chilEntregado decimal(10,2),
gastosArg decimal(10,2),
gastosChil decimal(10,2),
kmSalida decimal(10,2),
kmVuelta decimal(10,2),
kmRecorrido decimal(10,2),
combLts decimal(10,2),
consumo decimal(10,2),
restoArg decimal(10,2),
restoChil decimal(10,2),
FechaRegistro datetime default getdate(),
constraint FK_Viajes_Camiones foreign key (idCamion) references CAMIONES(idCamion) on delete cascade,
constraint FK_Viajes_Choferes foreign key (idChofer) references CHOFERES(idChofer) on delete set null,
constraint FK_Viajes_Cliente foreign key (idCliente) references CLIENTES(idCliente) on delete set null
)
go

create index idx_viajes_idCliente on VIAJES(idCliente);
go


create table DETALLE_VIAJE(
idDetalleViaje int primary key identity(1,1) not null,
idViaje int,
fecha date,
destino varchar(100),
vuelta varchar(100),
rtoNumero varchar(100),
micNro varchar(50),
observacion varchar(100),
demoras int,
FechaRegistro datetime default getdate(),
constraint FK_DetalleViaje_Viajes foreign key (idViaje) references VIAJES(idViaje) on delete cascade
)
go

create index idx_detalleviaje_fecha on DETALLE_VIAJE(fecha);
go


create table MANTENIMIENTO(
idMantenimiento int primary key identity(1,1) not null,
idCamion int references CAMIONES(idCamion),
fecha varchar(50),
tipoMantenimiento varchar(100),
descripcion varchar(255),
kilometrajes varchar(100),
proxMantenimiento varchar(100),
costo decimal(10,2),
partesReemplazadas varchar(255),
comentarios varchar(255),
estado varchar(100),
FechaRegistro datetime default getdate() 
)
go

create table ROL(
IdRol int primary key identity,
Descripcion varchar (50),
FechaRegistro datetime default getdate() 
)
go

create table PERMISO(
IdPermiso int primary key identity,
IdRol int references ROL(IdRol),
NombreMenu varchar (100),
FechaRegistro datetime default getdate() 
)
go

create table USUARIO(
IdUsuario int primary key identity,
Documento varchar (50),
NombreCompleto varchar (150),
Correo varchar (50),
Clave varchar (50),
IdRol int references ROL(IdRol),
Estado bit,
FechaRegistro datetime default getdate() 
)
go

create table NEGOCIO(
IdNegocio int primary key,
Nombre varchar(60),
CUIT varchar(60),
Direccion varchar (60),
Logo varbinary(max) NULL
)
go

insert into NEGOCIO (IdNegocio,Nombre,CUIT,Direccion)
values (1, 'Los Pamperos', '000000', 'Direccion de Prueba')

insert into ROL (Descripcion)
values ('ADMINISTRADOR')

insert into ROL (Descripcion)
values ('EMPLEADO')

insert into USUARIO(Documento,NombreCompleto,Correo,Clave,IdRol,Estado)
values ('37486938', 'Nicolas', 'info@nicolasghilardi.com.ar', 'Admin',1,1)

insert into CLIENTES(nombreCliente,telCliente,direccion,correo,observacion)
values ('Nicolas Ghilardi', '2612113736', 'Hamlet 19, Malaga, Espana', 'nghilardi.93@gmail.com', 'SEO de NG Informatica')

select *from NEGOCIO