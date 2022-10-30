SELECT  Sitio.direccion ,[dbo].[Sitio].Latitud, [dbo].[Sitio].Longitud FROM [dbo].[Sitio] 
JOIN Trayecto ON [dbo].[Trayecto].Inicio = [dbo].[Sitio].SitioID

SELECT  Sitio.direccion, [dbo].[Sitio].Latitud, [dbo].[Sitio].Longitud FROM [dbo].[Sitio] 
JOIN Trayecto ON [dbo].[Trayecto].Final = [dbo].[Sitio].SitioID

ALTER TABLE sitio
ADD direccion varchar(30)

select * from Sitio

insert into sitio (Latitud, Longitud, Provincia, Canton, Distrito, SitioNombre, Descripcion, direccion )
values (9.431547,-84.164933, 'Puntarenas', 'Quepos', 'Quepos', 'Paseo del mar', 'Inicio de trayecto en puntarenas','Paseo del mar, Quepos')

insert into sitio 
values (9.431547,-84.164933, 'Puntarenas', 'Quepos', 'Quepos', 'Paseo del mar', 'Inicio de trayecto en puntarenas', 0x00000000, 0x00000000, 0x00000000, 'Paseo del mar, Quepos')

insert into sitio 
values (9.683713,-84.040846, 'San Jose', 'San Pablo', 'San Pablo', 'Parque Central', 'Parque central de San Pablo', 0x00000000, 0x00000000, 0x00000000, 'parque central san jose, san pablo')

insert into sitio 
values (9.684587, -84.040441, 'San Jose', 'San Pablo', 'San Pablo', 'Iglesia', 'Iglesia de san pablo', 0x00000000, 0x00000000, 0x00000000, 'Iglesia San Pablo Apóstol, San José, San Pablo')

insert into sitio 
values (9.705778, -84.0175351, 'San Jose', 'San Pablo', 'Santa Cruz', 'Cerro', 'Cerro la roca', 0x00000000, 0x00000000, 0x00000000, 'Cerro la Roca, de, San José, Santa Cruz')

alter table sitio
alter column
direccion varchar(80)

alter table Sitio
alter column
Longitud Decimal(8,6)


alter table Sitio
alter column
Latitud Decimal(8,6)

delete from sitio where sitioid = 10


select * from trayecto
select * from Sitio

insert into Trayecto
values (6, 11, 25, 5500, 50, 'Quepos a San Pablo' )