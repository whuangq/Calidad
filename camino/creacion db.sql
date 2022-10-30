CREATE TABLE Caminante (correo varchar(255) NOT NULL, Nombre varchar(15) NOT NULL, Apellido varchar(20) NOT NULL, Contraseña varchar(255) NOT NULL, Sexo varchar(1) NOT NULL, Edad int NOT NULL, CodigoRegistro int NOT NULL, Tel int NOT NULL, PRIMARY KEY (correo));
CREATE TABLE Encuesta (EncuestaId int IDENTITY NOT NULL, Caminantecorreo varchar(255) NOT NULL, Servicioid int NOT NULL, Calificacion decimal(1, 1) NOT NULL, [Date] date NOT NULL, Comentario varchar(255) NOT NULL, Version int NOT NULL, PRIMARY KEY (EncuestaId));
CREATE TABLE Servicio (ServicioId int IDENTITY NOT NULL, Categoria varchar(30) NOT NULL, Descripcion varchar(255) NOT NULL, FotoUno varbinary(max) NOT NULL, FotoDos varbinary(max) NOT NULL, FotosTres varbinary(max) NOT NULL, PRIMARY KEY (ServicioId));
CREATE TABLE Evento (Id int IDENTITY NOT NULL, SitioSitioID int NOT NULL, Nombre varchar(15) NOT NULL, PRIMARY KEY (Id));
CREATE TABLE Sitio (SitioID int IDENTITY NOT NULL, Latitud decimal(6, 3) NOT NULL, Longitud decimal(6, 3) NOT NULL, Provincia varchar(20) NOT NULL, Canton varchar(15) NOT NULL, Distrito int NOT NULL, SitioNombre varchar(20) NOT NULL, Descripcion varchar(255) NOT NULL, FotoUno varbinary(max) NOT NULL, FotoDos varbinary(max) NOT NULL, FotoTres varbinary(max) NOT NULL, PRIMARY KEY (SitioID));
CREATE TABLE Trayecto (TrayectoID int IDENTITY NOT NULL, Inicio int NOT NULL, Final int NOT NULL, AltimetriaMin int NOT NULL, AltimetriaMax int NOT NULL, Distancia int NOT NULL, Descripcion varchar(255) NOT NULL, PRIMARY KEY (TrayectoID));
CREATE TABLE Proveedor (Cedula int IDENTITY NOT NULL, Nombre varchar(50) NOT NULL, NumTelefono int NOT NULL, PRIMARY KEY (Cedula));
CREATE TABLE Ruta (Id int IDENTITY NOT NULL, Inicio int NOT NULL, Final int NOT NULL, PRIMARY KEY (Id));
CREATE TABLE Sitio_Caminante (Caminantecorreo varchar(255) NOT NULL, SitioSitioID int NOT NULL, PRIMARY KEY (Caminantecorreo, SitioSitioID));
CREATE TABLE Trayecto_Servicio (TrayectoId int NOT NULL, Servicioid int NOT NULL, PRIMARY KEY (TrayectoId, Servicioid));
CREATE TABLE Proveedor_Servicio (Servicioid int NOT NULL, ProveedorCedula int NOT NULL, PRIMARY KEY (Servicioid, ProveedorCedula));
CREATE TABLE Servicio_Sitio (ServicioServicioId int NOT NULL, SitioSitioID int NOT NULL, PRIMARY KEY (ServicioServicioId, SitioSitioID));
CREATE TABLE Pregunta (Id int IDENTITY NOT NULL, Texto varchar(255) NOT NULL, Calificacion decimal(1, 1) NOT NULL, Comentario varchar(255) NULL, PRIMARY KEY (Id));
CREATE TABLE Pregunta_Encuesta (PreguntaId int NOT NULL, EncuestaId int NOT NULL, PRIMARY KEY (PreguntaId, EncuestaId));
CREATE TABLE Ruta_Trayecto (TrayectoTrayectoID int NOT NULL, RutaId int NOT NULL, PRIMARY KEY (TrayectoTrayectoID, RutaId));
CREATE TABLE Trayecto_Caminante (TrayectoTrayectoID int NOT NULL, Caminantecorreo varchar(255) NOT NULL, PRIMARY KEY (TrayectoTrayectoID, Caminantecorreo));
CREATE TABLE Proveedor_Ruta (ProveedorCedula int NOT NULL, RutaId int NOT NULL, PRIMARY KEY (ProveedorCedula, RutaId));
CREATE TABLE Evento_Caminante (EventoId int NOT NULL, Caminantecorreo varchar(255) NOT NULL, PRIMARY KEY (EventoId, Caminantecorreo));
ALTER TABLE Evento ADD CONSTRAINT FKEvento173602 FOREIGN KEY (SitioSitioID) REFERENCES Sitio (SitioID);
ALTER TABLE Encuesta ADD CONSTRAINT FKEncuesta214378 FOREIGN KEY (Caminantecorreo) REFERENCES Caminante (correo);
ALTER TABLE Sitio_Caminante ADD CONSTRAINT FKSitio_Cami66710 FOREIGN KEY (SitioSitioID) REFERENCES Sitio (SitioID);
ALTER TABLE Sitio_Caminante ADD CONSTRAINT FKSitio_Cami4234 FOREIGN KEY (Caminantecorreo) REFERENCES Caminante (correo);
ALTER TABLE Trayecto_Servicio ADD CONSTRAINT FKTrayecto_S167904 FOREIGN KEY (TrayectoId) REFERENCES Trayecto (TrayectoID);
ALTER TABLE Trayecto_Servicio ADD CONSTRAINT FKTrayecto_S290064 FOREIGN KEY (Servicioid) REFERENCES Servicio (ServicioId);
ALTER TABLE Encuesta ADD CONSTRAINT FKEncuesta996357 FOREIGN KEY (Servicioid) REFERENCES Servicio (ServicioId);
ALTER TABLE Proveedor_Servicio ADD CONSTRAINT FKProveedor_970398 FOREIGN KEY (ProveedorCedula) REFERENCES Proveedor (Cedula);
ALTER TABLE Proveedor_Servicio ADD CONSTRAINT FKProveedor_447360 FOREIGN KEY (Servicioid) REFERENCES Servicio (ServicioId);
ALTER TABLE Servicio_Sitio ADD CONSTRAINT FKServicio_S700136 FOREIGN KEY (ServicioServicioId) REFERENCES Servicio (ServicioId);
ALTER TABLE Servicio_Sitio ADD CONSTRAINT FKServicio_S891051 FOREIGN KEY (SitioSitioID) REFERENCES Sitio (SitioID);
ALTER TABLE Pregunta_Encuesta ADD CONSTRAINT FKPregunta_E583867 FOREIGN KEY (PreguntaId) REFERENCES Pregunta (Id);
ALTER TABLE Pregunta_Encuesta ADD CONSTRAINT FKPregunta_E624366 FOREIGN KEY (EncuestaId) REFERENCES Encuesta (EncuestaId);
ALTER TABLE Ruta_Trayecto ADD CONSTRAINT FKRuta_Traye763808 FOREIGN KEY (RutaId) REFERENCES Ruta (Id);
ALTER TABLE Ruta_Trayecto ADD CONSTRAINT FKRuta_Traye718619 FOREIGN KEY (TrayectoTrayectoID) REFERENCES Trayecto (TrayectoID);
ALTER TABLE Trayecto_Caminante ADD CONSTRAINT FKTrayecto_C101467 FOREIGN KEY (TrayectoTrayectoID) REFERENCES Trayecto (TrayectoID);
ALTER TABLE Trayecto_Caminante ADD CONSTRAINT FKTrayecto_C935550 FOREIGN KEY (Caminantecorreo) REFERENCES Caminante (correo);
ALTER TABLE Proveedor_Ruta ADD CONSTRAINT FKProveedor_922276 FOREIGN KEY (ProveedorCedula) REFERENCES Proveedor (Cedula);
ALTER TABLE Proveedor_Ruta ADD CONSTRAINT FKProveedor_236099 FOREIGN KEY (RutaId) REFERENCES Ruta (Id);
ALTER TABLE Evento_Caminante ADD CONSTRAINT FKEvento_Cam736452 FOREIGN KEY (EventoId) REFERENCES Evento (Id);
ALTER TABLE Evento_Caminante ADD CONSTRAINT FKEvento_Cam785018 FOREIGN KEY (Caminantecorreo) REFERENCES Caminante (correo);


ALTER TABLE dbo.Trayecto
ADD CONSTRAINT FK_Sitioid
FOREIGN KEY (Inicio) REFERENCES sitio(SitioID);

ALTER TABLE dbo.Trayecto
ADD CONSTRAINT FK_Sitioidfin
FOREIGN KEY (Final) REFERENCES sitio(SitioID);
CREATE TABLE Administrador (correo varchar(255) NOT NULL, Nombre varchar(15) NOT NULL, Apellido varchar(20) NOT NULL, Contraseña varchar(255) NOT NULL, Tel int NOT NULL, PRIMARY KEY (correo));
CREATE TABLE Solicitante (Correo varchar(255) NOT NULL, Nombre varchar(15) NOT NULL, Apellido varchar(15) NOT NULL, Sexo binary(1) NOT NULL, Edad int NOT NULL, Tel int NOT NULL, Solicitudid int NOT NULL, PRIMARY KEY (Correo));
CREATE TABLE Codigo (id int IDENTITY NOT NULL, SolicitanteCorreo varchar(255) NOT NULL, Valor int NOT NULL, PRIMARY KEY (id));
CREATE TABLE Solicitud (id int NOT NULL, Administradorcorreo varchar(255) NOT NULL, SolicitanteCorreo varchar(255) NOT NULL, PRIMARY KEY (id, Administradorcorreo, SolicitanteCorreo));
ALTER TABLE Codigo ADD CONSTRAINT FKCodigo487202 FOREIGN KEY (SolicitanteCorreo) REFERENCES Solicitante (Correo);
ALTER TABLE Solicitud ADD CONSTRAINT FKSolicitud50523 FOREIGN KEY (Administradorcorreo) REFERENCES Administrador (correo);
ALTER TABLE Solicitud ADD CONSTRAINT FKSolicitud171986 FOREIGN KEY (SolicitanteCorreo) REFERENCES Solicitante (Correo);



alter table Sitio
alter column Distrito varchar(20)
insert into [dbo].[Sitio] (Latitud, Longitud, Provincia, Canton, Distrito, SitioNombre, Descripcion, FotoUno, FotoDos, FotoTres)
values (0, 0, 'Limon', 'Siquirres','DistritoRandom','Parismina', 'Lugar de playas',0,0,0);

insert into [dbo].[Sitio] (Latitud, Longitud, Provincia, Canton, Distrito, SitioNombre, Descripcion, FotoUno, FotoDos, FotoTres)
values (0, 0, 'San Jose', 'San Jose','DistritoRandom','Ejemplo', 'Sitio final para ejemplo',0,0,0);

insert into trayecto (Inicio, Final, AltimetriaMin, AltimetriaMax, Distancia, Descripcion) values (1,2, 1000, 1300, 100, 'Trayecto de ejemplo') 