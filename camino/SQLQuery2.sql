select * from trayecto

select * from Sitio

insert into Sitio values(2.2,3,3,"San Jose", "San Jose", "Margaritas", "Generic", "Prueba insercion de sitio 1")

alter table Sitio
alter column Distrito varchar(20)
insert into [dbo].[Sitio] (Latitud, Longitud, Provincia, Canton, Distrito, SitioNombre, Descripcion, FotoUno, FotoDos, FotoTres)
values (0, 0, 'Limon', 'Siquirres','DistritoRandom','Parismina', 'Lugar de playas',0,0,0);


insert into [dbo].[Sitio] (Latitud, Longitud, Provincia, Canton, Distrito, SitioNombre, Descripcion, FotoUno, FotoDos, FotoTres)
values (0, 0, 'San Jose', 'San Jose','DistritoRandom','Ejemplo', 'Sitio final para ejemplo',0,0,0);

insert into trayecto (Inicio, Final, AltimetriaMin, AltimetriaMax, Distancia, Descripcion) values (1,2, 1000, 1300, 100, 'Trayecto de ejemplo') 