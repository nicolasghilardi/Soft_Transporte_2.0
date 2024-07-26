

use DB_Soft_Transporte
go

create proc SP_REGISTRARUSUARIO(
@Documento varchar (50),
@NombreCompleto varchar(100),
@Correo varchar (100),
@Clave varchar (100),
@IdRol int,
@Estado bit,
@IdUsuarioResultado int output,
@Mensaje varchar(500) output
)
as 
begin
	set @IdUsuarioResultado = 0
	set @Mensaje = ''

	if not exists (select *from USUARIO where Documento = @Documento)
	begin

		insert into USUARIO (Documento,NombreCompleto,Correo,Clave,IdRol,Estado)
		values (@Documento,@NombreCompleto,@Correo,@Clave,@IdRol,@Estado)

		set @IdUsuarioResultado = SCOPE_IDENTITY()
		

	end
	else
		set @Mensaje = 'No se puede repetir el Documento para más de un usuario'
end
go

create proc SP_EDITARUSUARIO(
@IdUsuario int,
@Documento varchar (50),
@NombreCompleto varchar(100),
@Correo varchar (100),
@Clave varchar (100),
@IdRol int,
@Estado bit,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as 
begin
	set @Respuesta = 0
	set @Mensaje = ''

	if not exists (select *from USUARIO where Documento = @Documento and IdUsuario != @IdUsuario)
	begin

		update USUARIO set
		Documento = @Documento,
		NombreCompleto = @NombreCompleto,
		Correo = @Correo,
		Clave = @Clave,
		IdRol = @IdRol,
		Estado = @Estado
		where IdUsuario = @IdUsuario

		set @Respuesta = 1
	end
	else
		set @Mensaje = 'No se puede repetir el Documento para más de un usuario'
end
go


create proc SP_ELIMINARUSUARIO(
@IdUsuario int,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as 
begin
	set @Respuesta = 0
	set @Mensaje = ''
	begin
		delete from USUARIO where IdUsuario = @IdUsuario
		set @Respuesta = 1
	end

end
go


--PROCEDIMIENTOS PARA CLIENTES

create proc SP_RegistrarCliente(
@nombreCliente varchar(100),
@telCliente varchar(50),
@direccion varchar (255),
@correo varchar (100),
@observacion varchar(255),
@Resultado int output,
@Mensaje varchar(500) output
)
as 
begin
	set @Resultado = 0
	declare @IDPERSONA int

	if not exists (select *from CLIENTES where nombreCliente = @nombreCliente)
	begin

		insert into CLIENTES (nombreCliente,telCliente,direccion,correo,observacion)
		values (@nombreCliente,@telCliente,@direccion,@correo,@observacion)

		set @Resultado = SCOPE_IDENTITY()	
	end
	else
		set @Mensaje = 'Ya existe un cliente con ese nombre.'
end
go


create proc SP_EditarCliente(
@idCliente int,
@nombreCliente varchar(100),
@telCliente varchar(50),
@direccion varchar (255),
@correo varchar (100),
@observacion varchar(255),
@Resultado bit output,
@Mensaje varchar(500) output
)
as 
begin
	set @Resultado = 0
	declare @IDPERSONA int
	
	if not exists (select *from CLIENTES where nombreCliente = @nombreCliente and idCliente != @idCliente)
	begin

		update CLIENTES set
		nombreCliente = @nombreCliente,
		telCliente = @telCliente,
		direccion = @direccion,
		correo = @correo,
		observacion = @observacion
		where IdCliente = @IdCliente

		set @Resultado = 1
	end
	else
		set @Mensaje = 'Ya existe un cliente con ese nombre.'
end
go

--PROCEDIMIENTOS CHOFERES

create proc SP_RegistrarChofer(
@nombreChofer varchar(100),
@documento varchar(50),
@vencLicencia date,
@vencLinti date,
@telefono varchar(50),
@antiguedad int,
@Resultado int output,
@Mensaje varchar(500) output
)
as 
begin
	set @Resultado = 0
	declare @IDPERSONA int

	if not exists (select *from CHOFERES where nombreChofer = @nombreChofer)
	begin

		insert into CHOFERES (nombreChofer,documento,vencLicencia,vencLinti,telefono,antiguedad)
		values (@nombreChofer,@documento,@vencLicencia,@vencLinti,@telefono,@antiguedad)

		set @Resultado = SCOPE_IDENTITY()	
	end
	else
		set @Mensaje = 'Ya existe un chofer con ese nombre.'
end
go


create proc SP_EditarChofer(
@idChofer int,
@nombreChofer varchar(100),
@documento varchar(50),
@vencLicencia date,
@vencLinti date,
@telefono varchar(50),
@antiguedad int,
@Resultado int output,
@Mensaje varchar(500) output
)
as 
begin
	set @Resultado = 0
	declare @IDPERSONA int
	
	if not exists (select *from CHOFERES where nombreChofer = @nombreChofer and idChofer != @idChofer)
	begin

		update CHOFERES set
		nombreChofer = @nombreChofer,
		documento = @documento,
		vencLicencia = @vencLicencia,
		vencLinti = @vencLinti,
		telefono = @telefono,
		antiguedad = @antiguedad
		where idChofer = @idChofer

		set @Resultado = 1
	end
	else
		set @Mensaje = 'Ya existe un chofer con ese nombre.'
end
go

--PROCEDIMIENTOS CAMIONES
create proc SP_RegistrarCamion(
@modelo varchar(50),
@patente varchar(20),
@capacidad decimal(10,2),
@vencTecnica date,
@vencRuta date,
@vencSeguro date,
@vencTazaFiscal date,
@cambioAceite date,
@Resultado int output,
@Mensaje varchar(500) output
)
as 
begin
	set @Resultado = 0
	declare @IDPERSONA int

	if not exists (select *from CAMIONES where patente = @patente)
	begin

		insert into CAMIONES (modelo,patente,capacidad,vencTecnica,vencRuta,vencSeguro,vencTazaFiscal,cambioAceite)
		values (@modelo,@patente,@capacidad,@vencTecnica,@vencRuta,@vencSeguro,@vencTazaFiscal,@cambioAceite)

		set @Resultado = SCOPE_IDENTITY()	
	end
	else
		set @Mensaje = 'Ya existe un camion con esa patente.'
end
go

create proc SP_EditarCamion(
@idCamion int,
@modelo varchar(50),
@patente varchar(20),
@capacidad decimal(10,2),
@vencTecnica date,
@vencRuta date,
@vencSeguro date,
@vencTazaFiscal date,
@cambioAceite date,
@Resultado int output,
@Mensaje varchar(500) output
)
as 
begin
	set @Resultado = 0
	declare @IDPERSONA int
	
	if not exists (select *from CAMIONES where patente = @patente and idCamion != @idCamion)
	begin

		update CAMIONES set
		modelo = @modelo,
		patente = @patente,
		capacidad = @capacidad,
		vencTecnica = @vencTecnica,
		vencRuta = @vencRuta,
		vencSeguro = @vencSeguro,
		vencTazaFiscal = @vencTazaFiscal,
		cambioAceite = @cambioAceite
		where idCamion = @idCamion

		set @Resultado = 1
	end
	else
		set @Mensaje = 'Ya existe un camion con esa patente.'
end
go

select idCamion,modelo,patente,capacidad,vencTecnica,vencRuta,vencSeguro,vencTazaFiscal,cambioAceite from CAMIONES


--PROCEDIMIENTOS VIAJES

create proc SP_RegistrarViaje(
@nroRendicion varchar(50),
@idCamion int,
@idChofer int,
@idCliente int,
@argEntregado decimal(10,2),
@chilEntregado decimal(10,2),
@gastosArg decimal(10,2),
@gastosChil decimal(10,2),
@kmSalida decimal(10,2),
@kmVuelta decimal(10,2),
@kmRecorrido decimal(10,2),
@combLts decimal(10,2),
@consumo decimal(10,2),
@restoArg decimal(10,2),
@restoChil decimal(10,2),
@Resultado int output,
@Mensaje varchar(500) output
)
as 
begin
	set @Resultado = 0

	insert into VIAJES (nroRendicion,idCamion,idChofer,idCliente,argEntregado,chilEntregado,gastosArg,gastosChil,kmSalida,kmVuelta,kmRecorrido,combLts,consumo,restoArg,restoChil)
	values (@nroRendicion,@idCamion,@idChofer,@idCliente,@argEntregado,@chilEntregado,@gastosArg,@gastosChil,@kmSalida,@kmVuelta,@kmRecorrido,@combLts,@consumo,@restoArg,@restoChil)

	set @Resultado = SCOPE_IDENTITY()	
	set @Mensaje = 'Viaje registrado exitosamente.'
end
go


create proc SP_EditarViaje(
@idViaje int,
@nroRendicion varchar(50),
@idCamion int,
@idChofer int,
@idCliente int,
@argEntregado decimal(10,2),
@chilEntregado decimal(10,2),
@gastosArg decimal(10,2),
@gastosChil decimal(10,2),
@kmSalida decimal(10,2),
@kmVuelta decimal(10,2),
@kmRecorrido decimal(10,2),
@combLts decimal(10,2),
@consumo decimal(10,2),
@restoArg decimal(10,2),
@restoChil decimal(10,2),
@Resultado int output,
@Mensaje varchar(500) output
)
as 
begin
	set @Resultado = 0
	set @Mensaje = ''
	
	if exists (select 1 from VIAJES where idViaje = @idViaje)
	begin

		update VIAJES set
		nroRendicion = @nroRendicion,
		idCamion = @idCamion,
		idChofer = @idChofer,
		idCliente = @idCliente,
		argEntregado = @argEntregado,
		chilentregado = @chilEntregado,
		gastosArg = @gastosArg,
		gastosChil = @gastosChil,
		kmSalida = @kmSalida,
		kmVuelta = @kmVuelta,
		kmRecorrido = @kmRecorrido,
		combLts = @combLts,
		consumo = @consumo,
		restoArg = @restoArg,
		restoChil = @restoChil
		where idViaje = @idViaje

		set @Resultado = 1
		set @Mensaje = 'El viaje fue actualizado correctamente.'
	end
	else
		set @Mensaje = 'No existe un viaje con ese ID'
end
go



create proc SP_RegistrarDetalleViaje(
@idViaje int,
@fecha date,
@destino varchar(100),
@vuelta varchar(100),
@rtoNumero varchar(100),
@micNro varchar(50),
@observacion varchar(100),
@demoras int,
@Resultado int output,
@Mensaje varchar(500) output
)
as
begin
	set @Resultado = 0

	insert into DETALLE_VIAJE (idViaje,fecha,destino,vuelta,rtoNumero,micNro,observacion,demoras)
	values (@idViaje,@fecha,@destino,@vuelta,@rtoNumero,@micNro,@observacion,@demoras)

	set @Resultado = SCOPE_IDENTITY()
	set @Mensaje = 'Detalle del viaje registrado correctamente.'
end
go

create proc SP_EditarDetalleViaje(
@idDetalleViaje int,
@idViaje int,
@fecha date,
@destino varchar(100),
@vuelta varchar(100),
@rtoNumero varchar(100),
@micNro varchar(50),
@observacion varchar(100),
@demoras int,
@Resultado int output,
@Mensaje varchar(500) output
)
as 
begin
	set @Resultado = 0
	set @Mensaje = ''
	
	if exists (select 1 from DETALLE_VIAJE where idDetalleViaje = @idDetalleViaje)
	begin

		update DETALLE_VIAJE set
		idViaje = @idViaje,
		fecha = @fecha,
		destino = @destino,
		vuelta = @vuelta,
		rtoNumero = @rtoNumero,
		micNro = @micNro,
		observacion = @observacion,
		demoras = @demoras

		where idDetalleViaje = @idDetalleViaje

		set @Resultado = 1
		set @Mensaje = 'El detalle del viaje fue actualizado correctamente.'
	end
	else
		set @Mensaje = 'No existe un detalle de viaje con ese ID'
end
go

-- PROCEDIMIENTO DE TABLA MANTENIMIENTO

create proc SP_RegistrarMantenimiento(
@idCamion int,
@fecha varchar(50),
@tipoMantenimiento varchar(100),
@descripcion varchar(255),
@kilometrajes varchar(100),
@proxMantenimiento varchar(100),
@costo decimal(10,2),
@partesReemplazadas varchar(255),
@comentarios varchar(255),
@estado varchar(100),
@Resultado int output,
@Mensaje varchar(500) output
)
as 
begin
	set @Resultado = 0
	

		insert into MANTENIMIENTO (idCamion, fecha,tipoMantenimiento,descripcion,kilometrajes,proxMantenimiento,costo,partesReemplazadas,comentarios,estado)
		values (@idCamion,@fecha,@tipoMantenimiento,@descripcion,@kilometrajes,@proxMantenimiento,@costo,@partesReemplazadas,@comentarios,@estado)

		set @Resultado = SCOPE_IDENTITY()
		set @Mensaje = 'Ya existe un mantenimiento con ese id.'
end
go

create proc SP_EditarMantenimiento(
@idMantenimiento int,
@idCamion int,
@fecha varchar(50),
@tipoMantenimiento varchar(100),
@descripcion varchar(255),
@kilometrajes varchar(100),
@proxMantenimiento varchar(100),
@costo decimal(10,2),
@partesReemplazadas varchar(255),
@comentarios varchar(255),
@estado varchar(100),
@Resultado int output,
@Mensaje varchar(500) output
)
as 
begin
	set @Resultado = 0
	set @Mensaje = ''
	
	if exists (select 1 from MANTENIMIENTO where idMantenimiento = @idMantenimiento)
	begin

		update MANTENIMIENTO set
		idCamion = @idCamion,
		fecha = @fecha,
		tipoMantenimiento = @tipoMantenimiento,
		descripcion = @descripcion,
		kilometrajes = @kilometrajes,
		proxMantenimiento = @proxMantenimiento,
		costo = @costo,
		partesReemplazadas = @partesReemplazadas,
		comentarios = @comentarios,
		estado = @estado
		where idMantenimiento = @idMantenimiento

		set @Resultado = 1
		set @Mensaje = 'El Mantenimiento fue actualizado correctamente.'
	end
	else
		set @Mensaje = 'No existe un viaje con ese ID'
end
go

select v.idViaje,
v.FechaRegistro, v.nroRendicion,
c.idCamion, c.patente,
ch.idChofer, ch.nombreChofer,
cl.idCliente, cl.nombreCliente,
v.argEntregado,v.chilEntregado,v.gastosArg,v.gastosChil,v.kmSalida,v.kmVuelta,v.kmRecorrido,v.combLts,v.consumo,v.restoArg,v.restoChil,convert(char(10), v.FechaRegistro, 103) as FechaRegistro
from VIAJES v
inner join CAMIONES c on v.idCamion = c.idCamion
inner join CHOFERES ch on v.idChofer = ch.idChofer
inner join CLIENTES cl on v.idCliente = cl.idCliente


select m.idMantenimiento,
c.idCamion, c.patente,
m.fecha, m.tipoMantenimiento,m.descripcion,
m.kilometrajes, m.proxMantenimiento, m.costo,
m.partesReemplazadas,m.comentarios,m.estado
from MANTENIMIENTO m
inner join CAMIONES c on m.idCamion = c.idCamion

insert into VIAJES(nroRendicion,idCamion,idChofer,idCliente,argEntregado,chilEntregado,gastosArg,gastosChil,kmSalida,kmVuelta,kmRecorrido,combLts,consumo,restoArg,restoChil)
values ('00001',2,2,1,'100','150','90','115','2000','2000','500','50','15','20','100')

insert into DETALLE_VIAJE(idViaje,fecha,destino,vuelta,rtoNumero,micNro,observacion,demoras)
values (2,'15/12/2020','Cordoba','San Martin','4951','4548','Cal','0')

insert into MANTENIMIENTO(idCamion,fecha,tipoMantenimiento,descripcion,kilometrajes,proxMantenimiento,costo,partesReemplazadas,comentarios,estado)
values (3,'15/12/2024','Caja de Cambio','Problemas con el 3er Cambio','19000','30000','200000','Caja de cambio completa','', 'Hecho')

SELECT dv.idDetalleViaje, dv.idViaje, dv.fecha, dv.destino, dv.vuelta,
dv.rtoNumero, dv.micNro, dv.observacion, dv.demoras, dv.FechaRegistro
FROM DETALLE_VIAJE dv
WHERE dv.idViaje = 2

select *from CAMIONES