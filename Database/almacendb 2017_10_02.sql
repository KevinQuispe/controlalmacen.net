USE [ALMACENBD]
GO
/****** Object:  Table [dbo].[Semana_Produccion_Insumo]    Script Date: 02/10/2017 17:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Semana_Produccion_Insumo](
	[cod_semana] [char](2) NOT NULL,
	[cod_insumo] [char](7) NOT NULL,
	[cod_almacen] [char](2) NOT NULL,
	[anio] [int] NOT NULL,
	[ruc] [char](13) NOT NULL,
	[codigoPT] [nvarchar](5) NOT NULL,
	[cantidadConsumo] [numeric](18, 6) NOT NULL,
	[cantidadConsumoReal] [numeric](18, 6) NOT NULL,
	[costo] [numeric](18, 6) NOT NULL,
 CONSTRAINT [PK_Semana_Produccion_Insumo] PRIMARY KEY CLUSTERED 
(
	[cod_semana] ASC,
	[cod_insumo] ASC,
	[cod_almacen] ASC,
	[anio] ASC,
	[ruc] ASC,
	[codigoPT] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[almacen]    Script Date: 02/10/2017 17:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[almacen](
	[cod_almacen] [char](2) NOT NULL,
	[descripcion] [nvarchar](80) NULL,
	[direccion] [nvarchar](80) NULL,
 CONSTRAINT [PK_almacen] PRIMARY KEY CLUSTERED 
(
	[cod_almacen] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[familia]    Script Date: 02/10/2017 17:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[familia](
	[cod_familia] [int] IDENTITY(1,1) NOT NULL,
	[fam_des] [nvarchar](80) NULL,
 CONSTRAINT [PK_Tabla2] PRIMARY KEY CLUSTERED 
(
	[cod_familia] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[unidad]    Script Date: 02/10/2017 17:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[unidad](
	[cod_unidad] [int] IDENTITY(1,1) NOT NULL,
	[uni_des] [nvarchar](80) NULL,
 CONSTRAINT [PK_unidad] PRIMARY KEY CLUSTERED 
(
	[cod_unidad] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[insumos]    Script Date: 02/10/2017 17:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[insumos](
	[cod_insumo] [char](7) NOT NULL,
	[nombre_insumo] [nvarchar](100) NULL,
	[cod_unidad] [int] NULL,
	[cod_familia] [int] NULL,
 CONSTRAINT [PK_insumos] PRIMARY KEY CLUSTERED 
(
	[cod_insumo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cliente]    Script Date: 02/10/2017 17:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cliente](
	[ruc] [char](13) NOT NULL,
	[razon_social] [text] NULL,
	[direccion] [text] NULL,
	[telefono] [text] NULL,
	[correo] [text] NULL,
	[rep_legal] [char](80) NULL,
 CONSTRAINT [PK_cliente] PRIMARY KEY CLUSTERED 
(
	[ruc] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[guiaCliente]    Script Date: 02/10/2017 17:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[guiaCliente](
	[numeroguia] [char](20) NOT NULL,
	[cod_almacen] [char](2) NOT NULL,
	[fecha] [date] NULL,
	[observacion] [text] NULL,
	[persona_recepcion] [text] NULL,
	[ruc] [char](13) NULL,
	[salidaodevolucion] [smallint] NOT NULL,
	[codalmacenrecep] [char](2) NULL,
	[fecha_traslado] [date] NULL,
	[punto_partida] [text] NULL,
	[punto_llegada] [text] NULL,
	[destinatario] [text] NULL,
	[ruc_cliente] [char](13) NULL,
	[config_vehicular] [nvarchar](50) NULL,
	[cert_mtc] [nvarchar](5) NULL,
	[licencia] [nvarchar](10) NULL,
	[otro_tipo_doc] [char](10) NULL,
 CONSTRAINT [PK_GuiaCliente_1] PRIMARY KEY CLUSTERED 
(
	[numeroguia] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stock_almacen]    Script Date: 02/10/2017 17:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stock_almacen](
	[cod_insumo] [char](7) NOT NULL,
	[cod_almacen] [char](2) NOT NULL,
	[cantidad] [float] NULL,
 CONSTRAINT [PK_stock_almacen] PRIMARY KEY CLUSTERED 
(
	[cod_insumo] ASC,
	[cod_almacen] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stock_cliente]    Script Date: 02/10/2017 17:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stock_cliente](
	[ruc] [char](13) NOT NULL,
	[cod_insumo] [char](7) NOT NULL,
	[cantidad] [float] NOT NULL,
	[costo] [numeric](10, 6) NULL,
 CONSTRAINT [PK_stock_cliente_1] PRIMARY KEY CLUSTERED 
(
	[ruc] ASC,
	[cod_insumo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[detalleGuia]    Script Date: 02/10/2017 17:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[detalleGuia](
	[cod_insumo] [char](7) NOT NULL,
	[numeroguia] [char](20) NOT NULL,
	[cantidad] [float] NULL,
 CONSTRAINT [PK_DetalleGuia] PRIMARY KEY CLUSTERED 
(
	[cod_insumo] ASC,
	[numeroguia] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Semana_Consumo_Produccion]    Script Date: 02/10/2017 17:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Semana_Consumo_Produccion](
	[ruc] [char](13) NOT NULL,
	[cod_semana] [char](2) NOT NULL,
	[anio] [int] NOT NULL,
	[cod_insumo] [char](7) NOT NULL,
	[Consumo] [numeric](18, 6) NOT NULL,
	[ConsumoReal] [numeric](18, 6) NOT NULL,
	[costo] [numeric](18, 6) NULL,
 CONSTRAINT [PK_Semana_Consumo_Produccion] PRIMARY KEY CLUSTERED 
(
	[ruc] ASC,
	[cod_semana] ASC,
	[anio] ASC,
	[cod_insumo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[semanas]    Script Date: 02/10/2017 17:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[semanas](
	[cod_semana] [char](2) NOT NULL,
	[anio] [int] NOT NULL,
	[fecha_inicio] [date] NULL,
	[fecha_fin] [date] NULL,
	[observacion] [text] NULL,
	[estado] [int] NULL,
 CONSTRAINT [PK_semanas] PRIMARY KEY CLUSTERED 
(
	[cod_semana] ASC,
	[anio] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Semana_Almacen]    Script Date: 02/10/2017 17:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Semana_Almacen](
	[codsemana] [char](2) NOT NULL,
	[codalmacen] [char](2) NOT NULL,
	[codinsumo] [char](7) NOT NULL,
	[anio] [int] NOT NULL,
	[saldoinicial] [numeric](18, 6) NULL,
	[entrega] [numeric](18, 6) NULL,
	[consumo_produccion] [numeric](18, 6) NULL,
	[saldofinal] [numeric](18, 6) NULL,
 CONSTRAINT [PK_Semana_Almacen] PRIMARY KEY CLUSTERED 
(
	[codsemana] ASC,
	[codalmacen] ASC,
	[codinsumo] ASC,
	[anio] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[lineaPT]    Script Date: 02/10/2017 17:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lineaPT](
	[cod_linea] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nvarchar](50) NULL,
 CONSTRAINT [PK_linea] PRIMARY KEY CLUSTERED 
(
	[cod_linea] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PTerminado]    Script Date: 02/10/2017 17:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PTerminado](
	[codigoPT] [nvarchar](5) NOT NULL,
	[descripcion] [nvarchar](100) NULL,
	[codLinea] [int] NULL,
 CONSTRAINT [PK__PTermina__2610718F52CE3E04] PRIMARY KEY CLUSTERED 
(
	[codigoPT] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConsumoPTerminado]    Script Date: 02/10/2017 17:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ConsumoPTerminado](
	[codigoPT] [nvarchar](5) NOT NULL,
	[cod_insumo] [char](7) NOT NULL,
	[cantidad] [numeric](18, 6) NOT NULL,
	[tipo] [bit] NOT NULL,
 CONSTRAINT [PK_ConsumoPTerminado] PRIMARY KEY CLUSTERED 
(
	[codigoPT] ASC,
	[cod_insumo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Semana_Produccion]    Script Date: 02/10/2017 17:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Semana_Produccion](
	[cod_semana] [char](2) NOT NULL,
	[codigoPT] [nvarchar](5) NOT NULL,
	[ruc] [char](13) NOT NULL,
	[anio] [int] NOT NULL,
	[cod_almacen] [char](2) NOT NULL,
	[CantidadCajas] [int] NOT NULL,
	[PaletizadoOno] [bit] NOT NULL,
	[cerroProducc] [bit] NULL,
 CONSTRAINT [PK_Semana_Produccion] PRIMARY KEY CLUSTERED 
(
	[cod_semana] ASC,
	[codigoPT] ASC,
	[ruc] ASC,
	[anio] ASC,
	[cod_almacen] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[pa_ListaDeFechasSemProducc]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_ListaDeFechasSemProducc]
		@codigoPT nvarchar(5),
		@codsem char(2),
		@codalm char(2),
		@anio int
AS
BEGIN
	SET NOCOUNT ON;
	select distinct fecha from Semana_Produccion
	where codigoPT=@codigoPT and cod_semana=@codsem and anio=@anio and cod_almacen=@codalm
	order by fecha
END
GO
/****** Object:  Table [dbo].[semana_insumo]    Script Date: 02/10/2017 17:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[semana_insumo](
	[cod_semana] [char](2) NOT NULL,
	[ruc] [char](13) NOT NULL,
	[cod_insumo] [char](7) NOT NULL,
	[anio] [int] NOT NULL,
	[saldo_inicial] [float] NULL,
	[entrega] [float] NULL,
	[consumo_produccion] [float] NULL,
	[saldo_final] [float] NULL,
 CONSTRAINT [PK_semana_insumo_1] PRIMARY KEY CLUSTERED 
(
	[cod_semana] ASC,
	[ruc] ASC,
	[cod_insumo] ASC,
	[anio] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[pa_consultarSaldoInicial]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_consultarSaldoInicial]
 @codSemana char(2),
 @ruc char(13)
as
declare @salIni float
declare @cantIsumo int
declare @ci int
if exists (select* from dbo.semana_insumo where ruc=@ruc)
set @cantIsumo=(select cod_insumo from semana_insumo where cod_semana=@codSemana)
begin
  set @ci=0;
    WHILE @ci <=@cantIsumo
      BEGIN;
            IF (@ci<@cantIsumo)
                  PRINT @cantIsumo
            SET @ci=@ci+1;
      END;
end
GO
/****** Object:  StoredProcedure [dbo].[pa_CerrarSemana]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_CerrarSemana]
 @codSem char(2),
 @anio integer
AS
BEGIN
	if exists (select * from semanas where cod_semana=@codSem and anio=@anio and estado=1)
	begin
		UPDATE semanas SET estado=2 WHERE cod_semana = @codSem 
		AND anio=@anio
	end
END
GO
/****** Object:  StoredProcedure [dbo].[pa_CerrarProduccion]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_CerrarProduccion]
@codsem char(2),
@anio int,
@ruc char(13)
AS
BEGIN
	SET NOCOUNT ON;
	update Semana_Produccion set cerroProducc=1
	where cod_semana=@codsem and anio=@anio and ruc=@ruc
END
GO
/****** Object:  StoredProcedure [dbo].[pa_buscarSemana]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pa_buscarSemana]
@codigo char(2)
as
select*from semanas where cod_semana=@codigo
GO
/****** Object:  StoredProcedure [dbo].[pa_ActualizarSemanaConsumProducc]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_ActualizarSemanaConsumProducc]
@ruc char(13),
@codsem char(2),
@anio int,
@codinsumo char(7),
@consreal numeric(18,6)
AS
BEGIN
	SET NOCOUNT ON;
	update Semana_Consumo_Produccion set ConsumoReal=@consreal
	where ruc=@ruc and cod_semana=@codsem and anio=	@anio and cod_insumo=@codinsumo
END
GO
/****** Object:  StoredProcedure [dbo].[pa_ActualizarConsumoRealProduccion]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_ActualizarConsumoRealProduccion]
@codins char(7),
@codalm char(2),
@codsem char(2),
@anio int,
@ruc char(13),
@codpt nvarchar(5),
@cantactual numeric(18,6)
AS
BEGIN
	SET NOCOUNT ON;
	update Semana_Produccion_Insumo set cantidadConsumoReal=@cantactual
	where cod_semana=@codsem and cod_insumo=@codins and cod_almacen=@codalm
	and anio=@anio and ruc=@ruc and codigoPT=@codpt
END
GO
/****** Object:  StoredProcedure [dbo].[pa_activarSemana]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_activarSemana]
 @codSem char(2),
 @anio integer,
 @codalm char(2)
as
begin 
declare @est int
declare @rpta int
if not exists (select * from semanas where estado=1)
begin
	if exists (select * from semanas where cod_semana=@codSem and anio=@anio)
	begin
		 UPDATE semanas SET estado=1 WHERE cod_semana = @codSem 
		 AND anio=@anio
		 set @rpta=1
	end
	else
	begin
		set @rpta=0
	end
end
else
begin
	set @rpta=2
end
select @rpta as respuesta
end
GO
/****** Object:  StoredProcedure [dbo].[pa_listaSctockClienteXProductor]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_listaSctockClienteXProductor]
@ruc char(13)
AS
BEGIN
	select sc.cod_insumo,ins.nombre_insumo,@ruc as ruc,sc.cantidad,sc.costo from stock_cliente as sc
	join insumos as ins on ins.cod_insumo=sc.cod_insumo
	where sc.ruc=@ruc
END
GO
/****** Object:  StoredProcedure [dbo].[pa_ListarSemanaConsumoProduccion]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_ListarSemanaConsumoProduccion]
@ruc char(13),
@codsem char(2),
@anio int
AS
BEGIN
	SET NOCOUNT ON;
	if not exists (	select scp.cod_insumo,ins.nombre_insumo,scp.Consumo,scp.ConsumoReal from Semana_Consumo_Produccion as scp 
	join insumos as ins on scp.cod_insumo=ins.cod_insumo where cod_semana=@codsem and anio=@anio and ruc=@ruc)
	begin
		insert into Semana_Consumo_Produccion(ruc,cod_semana,anio,cod_insumo,Consumo,ConsumoReal)
		select ruc,cod_semana,anio,cod_insumo,SUM(cantidadConsumo) as cantidadConsumo,SUM(cantidadConsumoReal) as cantidadConsumoReal from Semana_Produccion_Insumo
		where cod_semana=@codsem and anio=@anio and ruc=@ruc
		group by cod_semana,anio,ruc,cod_insumo
	end	
	select scp.cod_insumo,ins.nombre_insumo,scp.Consumo,scp.ConsumoReal from Semana_Consumo_Produccion as scp 
	join insumos as ins on scp.cod_insumo=ins.cod_insumo
	where cod_semana=@codsem and anio=@anio and ruc=@ruc
END
GO
/****** Object:  Table [dbo].[inventario_inicial]    Script Date: 02/10/2017 17:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[inventario_inicial](
	[cod_almacen] [char](2) NOT NULL,
	[Cod_insumo] [char](7) NOT NULL,
	[cant_actual] [float] NULL,
	[Fecha] [date] NULL,
	[costoinicial] [numeric](9, 5) NULL,
 CONSTRAINT [PK_inventario_incial_1] PRIMARY KEY CLUSTERED 
(
	[cod_almacen] ASC,
	[Cod_insumo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[pa_ListarInventarioInicial]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_ListarInventarioInicial]
@fecha date,
@codalm char(2)
AS
BEGIN
	SELECT inv.cod_almacen,inv.Fecha,inv.cod_insumo,ins.nombre_insumo,inv.cant_actual,inv.costoinicial FROM inventario_inicial as inv
	join insumos as ins on inv.Cod_insumo=ins.cod_insumo
	where inv.Fecha=@fecha and inv.cod_almacen=@codalm
END
GO
/****** Object:  StoredProcedure [dbo].[pa_listarInsumoConStock]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_listarInsumoConStock]
@codAlm char(2)
as
begin
select 
ins.cod_insumo,
ins.nombre_insumo,
sa.cantidad 
from 
stock_almacen sa join insumos ins
on sa.cod_insumo=ins.cod_insumo
where sa.cod_almacen=@codAlm and sa.cantidad>0
end
GO
/****** Object:  StoredProcedure [dbo].[pa_consultarStockSemana]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_consultarStockSemana]
@ruc char(13),
@sem char(2)
as
 select 
 ins.nombre_insumo,
 se.saldo_inicial,
 se.entrega,
 se.consumo_produccion,
(se.saldo_inicial+se.entrega)-(se.consumo_produccion) as saldoFin
 
 from dbo.semana_insumo as se
		join insumos as ins on ins.cod_insumo=se.cod_insumo
  where ruc=@ruc and cod_semana=@sem and se.saldo_inicial>0
GO
/****** Object:  StoredProcedure [dbo].[pa_consultarStock]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_consultarStock]
@codInsumo char(7),
@codAlmacen char(2)

as
begin 
select ins.cod_insumo,ins.nombre_insumo,sa.cantidad from stock_almacen sa inner join insumos ins
on sa.cod_insumo=ins.cod_insumo
where ins.cod_insumo=@codInsumo AND sa.cod_almacen=@codAlmacen
end
GO
/****** Object:  StoredProcedure [dbo].[pa_listarEntradaSalidaGuias]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pa_listarEntradaSalidaGuias]
@codAlm char(2),
@sd smallint
as
begin
	select 
	gc.fecha,
	gc.numeroguia,
	gc.persona_recepcion,
	gc.ruc,
	gc.observacion
	from  guiaCliente as gc 
	 where gc.cod_almacen=@codAlm and salidaodevolucion=@sd
end
GO
/****** Object:  StoredProcedure [dbo].[pa_listaguiacliente]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_listaguiacliente]
@codAlm char(2)
as
begin
	select top 20
	gc.fecha,
	gc.numeroguia,
	gc.persona_recepcion,
	gc.ruc,
	gc.observacion
	from  guiaCliente as gc 
	 where gc.cod_almacen=@codAlm order by fecha desc
end
GO
/****** Object:  StoredProcedure [dbo].[pa_ExistGuiaProductorSinVal]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_ExistGuiaProductorSinVal]
@numguia char(20)
AS
BEGIN
	SET NOCOUNT ON;
	declare @rpta int
	if exists (select * from guiaCliente where numeroguia=@numguia)
	begin
		set @rpta=1
	end
	else
	begin
		set @rpta=0
	end
	select @rpta as existe
END
GO
/****** Object:  StoredProcedure [dbo].[pa_eliminarGuiaCliente]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[pa_eliminarGuiaCliente]
@numGuia char(20),
@codAlma char(2)
as
declare @ca char(2)
declare @ng char(20)
select @ng = numeroguia from guiaCliente
where numeroguia=@numGuia

delete guiaCliente
where  numeroguia=@ng and cod_almacen=@codAlma
GO
/****** Object:  StoredProcedure [dbo].[pa_buscarGuiaCliente]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--PROCEDURE BUSCAR GUIA DE CLIENTE
CREATE procedure [dbo].[pa_buscarGuiaCliente]
@numGuia char(20),
@codalm char(2)
as
declare @ca char(2)
declare @ng char(20)

select @ng = numeroguia from guiaCliente
where numeroguia=@numGuia

select*from guiaCliente 
where numeroguia=@ng and cod_almacen=@codalm
GO
/****** Object:  StoredProcedure [dbo].[pa_ActualizarGuia]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_ActualizarGuia]
@numguia char(20),
@codalm char(2),
@fecha date,
@observacion text,
@personarecep text,
@ruc char(13)
AS
BEGIN
  update guiaCliente set observacion=@observacion, fecha=@fecha,ruc=@ruc,persona_recepcion=@personarecep
  where numeroguia=@numguia and cod_almacen=@codalm
END
GO
/****** Object:  UserDefinedFunction [dbo].[f_generarcodigo]    Script Date: 02/10/2017 17:24:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[f_generarcodigo]()
 returns char(20)
 as
 begin 
	declare @resultado char(20)
	declare  @codigo char(20)
	declare @agregado int
	if exists (select * from guiaCliente where salidaodevolucion=2)
	begin
		set @codigo=(select top 1 numeroguia from guiaCliente where salidaodevolucion=2 order by numeroguia desc)
		set @agregado=CONVERT(INT, SUBSTRING(@codigo,3,LEN(@codigo)-2))+1
		set @resultado='IM'+CONVERT(CHAR(6),@agregado)
	end
	else		
	begin
		set @resultado='IM1' 
	end
   return @resultado
 end
GO
/****** Object:  StoredProcedure [dbo].[pa_listarInsumGuiaTrasAlm]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	Listar insumos de la guía en el traslado de insumos entre almacen, pero también funciona en listar insumos de ccualquier guia Cliente
-- =============================================
CREATE PROCEDURE [dbo].[pa_listarInsumGuiaTrasAlm]
@numg char(20)
AS
BEGIN
	SET NOCOUNT ON;
	select det.cod_insumo,ins.nombre_insumo,det.cantidad from detalleGuia as det join insumos as ins on det.cod_insumo=ins.cod_insumo
	where numeroguia=@numg
END
GO
/****** Object:  StoredProcedure [dbo].[pa_ListarDetalleGuiav2]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_ListarDetalleGuiav2]
@numguia char(20)
AS
BEGIN
	SET NOCOUNT ON;
	select dg.cod_insumo,ins.nombre_insumo,dg.cantidad  from detalleGuia as dg join insumos as ins
	on dg.cod_insumo=ins.cod_insumo	where dg.numeroguia=@numguia
	order by cod_insumo
END
GO
/****** Object:  StoredProcedure [dbo].[pa_listaEntregaInsumo]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pa_listaEntregaInsumo]
@numGuia char(20)
as
declare @nguia char(20)
select @nguia =numeroguia from detalleGuia where numeroguia=@nguia
 select ins.nombre_insumo,ins.cod_unidad, dt.cantidad
  from insumos as ins,detalleGuia as dt 
	where numeroguia=@numGuia;
GO
/****** Object:  StoredProcedure [dbo].[pa_eliminarDetalleGuia]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pa_eliminarDetalleGuia]
@numguia char(20),
@codInsu char(7)
as
delete from detalleGuia where numeroguia=@numguia and cod_insumo=@codInsu
GO
/****** Object:  StoredProcedure [dbo].[pa_buscarDetalleGuia]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pa_buscarDetalleGuia]
@numGuia  char(20),
@codIns char(20)
as
select
 
 i.nombre_insumo,
 dg.numeroguia,
 dg.cantidad
 
 from  detalleGuia as dg
 join insumos as i on i.cod_insumo=dg.cod_insumo
 where dg.numeroguia=@numGuia and dg.cod_insumo=@codIns
GO
/****** Object:  StoredProcedure [dbo].[pa_AgregarInsumoaGuia]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_AgregarInsumoaGuia]
 @codInsumo char(7),
 @numGuia char(20),
 @cant float,
 @Fech datetime,
 @pesonaRec text,
 @observ text,
 @ruc char(13),
 @sod smallint,
 @codalm char(2)
AS
BEGIN
declare @rpta int=0
if((@cant<=(select top 1 cantidad from stock_almacen where cod_almacen=@codalm and cod_insumo=@codInsumo) AND @sod=1) OR @sod=2)
begin
	if not exists (select * from guiaCliente where numeroguia=@numGuia)
	begin
			 insert into GuiaCliente(numeroguia,fecha,persona_recepcion,observacion,cod_almacen,ruc,salidaodevolucion) 
			 values (@numGuia,@Fech,@pesonaRec,@observ,@codalm,@ruc,@sod)
	end
	insert into detalleGuia(cod_insumo,numeroguia,cantidad)
	values (@codInsumo,@numGuia,@cant)
	set @rpta=1	
end
else
begin
	set @rpta=2
end
select @rpta as respuesta
END
GO
/****** Object:  StoredProcedure [dbo].[pa_ActualizarDetalleGuia]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_ActualizarDetalleGuia] 
@numguia char(20),
@codinsu char(7),
@cantidad float,
@codalm char(2)
AS
BEGIN
	update detalleGuia set cantidad=@cantidad where cod_insumo=@codinsu and numeroguia=@numguia
	 and numeroguia=(select numeroguia from guiaCliente where numeroguia=@numguia and cod_almacen=@codalm)
END
GO
/****** Object:  StoredProcedure [dbo].[pa_ListInsumosProdTerminado]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_ListInsumosProdTerminado]
@codigo nvarchar(5)
AS
BEGIN
	SET NOCOUNT ON;
	select cpt.cod_insumo,ins.nombre_insumo,cpt.cantidad from ConsumoPTerminado as cpt join insumos as ins on cpt.cod_insumo=ins.cod_insumo
	where cpt.codigoPT=@codigo
END
GO
/****** Object:  StoredProcedure [dbo].[pa_ListarSemanaProduccionInsumo]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_ListarSemanaProduccionInsumo]
@codPT nvarchar(5),
@ruc char(13),
@codsem char(2),
@anio int,
@codalm char(2)
AS
BEGIN
	
	select spi.cod_insumo,ins.nombre_insumo,spi.codigoPT,spi.cantidadConsumo,spi.cantidadConsumoReal,spi.costo  
	from Semana_Produccion_Insumo as spi
	join ConsumoPTerminado as cpt on spi.codigoPT=cpt.codigoPT and spi.cod_insumo=cpt.cod_insumo 
	join insumos as ins on ins.cod_insumo=spi.cod_insumo
	where spi.codigoPT=@codPT and spi.ruc=@ruc
	and spi.cod_almacen=@codalm and cpt.tipo=0
END
GO
/****** Object:  StoredProcedure [dbo].[pa_ListarPorPaletizar]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_ListarPorPaletizar] 
@codPT nvarchar(5),
@ruc char(13),
@sem char(2),
@anio int,
@codalm char(2)
AS
BEGIN	
	SET NOCOUNT ON;
	select spi.cod_insumo,ins.nombre_insumo,spi.codigoPT,spi.cantidadConsumo,spi.cantidadConsumoReal,spi.costo  
	from Semana_Produccion_Insumo as spi join ConsumoPTerminado as cpt 
	on spi.codigoPT=cpt.codigoPT and spi.cod_insumo=cpt.cod_insumo 
	join insumos as ins on ins.cod_insumo=spi.cod_insumo
	where spi.codigoPT=@codPT and spi.ruc=@ruc
	and spi.cod_almacen=@codalm and cpt.tipo=1
	
END
GO
/****** Object:  StoredProcedure [dbo].[pa_listarDetalleProdTerm]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_listarDetalleProdTerm]
@codPT nvarchar(5)
as
select 
cp.cod_insumo,
ins.nombre_insumo,
cp.cantidad

from dbo.ConsumoPTerminado as cp 
	join insumos as ins on ins.cod_insumo=cp.cod_insumo
	 where codigoPT=@codPT
GO
/****** Object:  StoredProcedure [dbo].[pa_InsertarDetalleGuiaProductorv2]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_InsertarDetalleGuiaProductorv2]
@numguia char(20),
@codpt nvarchar(5),
@numcajas int,
@palet bit
AS
BEGIN
	SET NOCOUNT ON;
	-- Declaracion de variables para el cursor
	DECLARE @Id int,
		@codins char(7),
		@cantidad numeric(18,6)
	-- Declaración del cursor
	if(@palet=0)
	begin
		DECLARE insumus CURSOR FOR
		SELECT  cod_insumo, cantidad
		FROM ConsumoPTerminado where codigoPT=@codpt
		and tipo=0
	end
	else
	begin
		DECLARE insumus CURSOR FOR
		SELECT  cod_insumo, cantidad
		FROM ConsumoPTerminado where codigoPT=@codpt
	end

	OPEN insumus
	FETCH insumus INTO @codins,@cantidad
	WHILE (@@FETCH_STATUS = 0 )
	BEGIN	
			if exists (select * from detalleGuia where numeroguia=@numguia and cod_insumo=@codins)
			begin
				update detalleGuia set cantidad=cantidad+(@cantidad*@numcajas)
				where cod_insumo=@codins
			end
			else
			begin
				insert into detalleGuia(numeroguia,cod_insumo,cantidad)
				values(@numguia,@codins,@cantidad*@numcajas)
			end
			FETCH insumus INTO  @codins, @cantidad
	END
	CLOSE insumus
	DEALLOCATE insumus

END
GO
/****** Object:  StoredProcedure [dbo].[pa_AlcanzaStockCliente]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_AlcanzaStockCliente]
@ruc char(13),
@codpt nvarchar(5),
@numcajas int,
@tipo bit
AS
BEGIN
	SET NOCOUNT ON;
	declare @tabla table(
	codinsumo char(7),
	stockclient numeric(18,6),
	cantidadxcaja numeric(18,6)
	)
	if(@tipo=0)
	begin
		insert into @tabla(codinsumo,stockclient,cantidadxcaja)
		select cpt.cod_insumo,(select cantidad from stock_cliente where ruc=@ruc and cod_insumo=cpt.cod_insumo),(cpt.cantidad)*(@numcajas)
		from ConsumoPTerminado as cpt where cpt.codigoPT=@codpt and cpt.tipo=@tipo
	end
	else
	begin
		insert into @tabla(codinsumo,stockclient,cantidadxcaja)
		select cpt.cod_insumo,(select cantidad from stock_cliente where ruc=@ruc and cod_insumo=cpt.cod_insumo),(cpt.cantidad)*(@numcajas)
		from ConsumoPTerminado as cpt where cpt.codigoPT=@codpt
	end
	select t.codinsumo,ins.nombre_insumo,(t.cantidadxcaja-t.stockclient) as falta from @tabla as t 
	join insumos as ins on t.codinsumo=ins.cod_insumo
	where t.stockclient<t.cantidadxcaja
END
GO
/****** Object:  StoredProcedure [dbo].[pa_AlcanzaStockAlmacen]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_AlcanzaStockAlmacen]
@codpt nvarchar(5),
@codalm char(2),
@ncajas int,
@tipo bit
AS
BEGIN
	SET NOCOUNT ON;
	declare @tabla table(
	codinsumo char(7),
	stockclient numeric(18,6),
	cantidadxcaja numeric(18,6)
	)
	if(@tipo=0)
	begin
		insert into @tabla(codinsumo,stockclient,cantidadxcaja)
		select cpt.cod_insumo,(select cantidad from stock_almacen where cod_almacen=@codalm and cod_insumo=cpt.cod_insumo),(cpt.cantidad)*(@ncajas)
		from ConsumoPTerminado as cpt where cpt.codigoPT=@codpt and cpt.tipo=@tipo
	end
	else
	begin
		insert into @tabla(codinsumo,stockclient,cantidadxcaja)
		select cpt.cod_insumo,(select cantidad from stock_almacen where cod_almacen=@codalm and cod_insumo=cpt.cod_insumo),(cpt.cantidad)*(@ncajas)
		from ConsumoPTerminado as cpt where cpt.codigoPT=@codpt
	end
	select t.codinsumo,ins.nombre_insumo,(t.cantidadxcaja-t.stockclient) as falta from @tabla as t 
	join insumos as ins on t.codinsumo=ins.cod_insumo
	where t.stockclient<t.cantidadxcaja
END
GO
/****** Object:  StoredProcedure [dbo].[pa_ListarSemanaProduccionNroCajas]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_ListarSemanaProduccionNroCajas]
@codigoPT nvarchar(5),
@codsem char(2),
@codalm char(2),
@anio int
AS
BEGIN
	SET NOCOUNT ON;
	select c.ruc, c.razon_social,sp.CantidadCajas from Semana_Produccion as sp join cliente as c on c.ruc=sp.ruc
	where sp.cod_semana=@codsem and sp.codigoPT=@codigoPT and sp.cod_almacen=@codalm and sp.anio=@anio
END
GO
/****** Object:  StoredProcedure [dbo].[pa_ListarClientProduccAbierta]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_ListarClientProduccAbierta]
@codsem char(2),
@codalm char(2),
@anio int
AS
BEGIN
	SET NOCOUNT ON;	
	select distinct sp.ruc , CAST(c.razon_social as varchar(200)) as razon_social 
	from Semana_Produccion as sp join cliente as c on sp.ruc=c.ruc	
	where cod_semana=@codsem and anio=@anio and cod_almacen=@codalm and cerroProducc=0
END
GO
/****** Object:  StoredProcedure [dbo].[pa_listarClientes]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_listarClientes]
as
select*from cliente
GO
/****** Object:  StoredProcedure [dbo].[pa_filtrarCliente]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pa_filtrarCliente]
@ruc char(13)
as
begin 
	select*from cliente where ruc=@ruc
	
end
GO
/****** Object:  StoredProcedure [dbo].[pa_ExisteGuiaProductor]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_ExisteGuiaProductor]
@numg char(20),
@ruc char(13),
@Fech date,
@salidaodevol smallint=0
AS
BEGIN
declare @existe int=0
declare @existruc int=0
declare @fechvalida int=0
declare @rznsocial varchar(50)=null
declare @codeguia varchar(10)=null
if(@salidaodevol=1)
begin
	if exists (select * from guiaCliente where numeroguia=@numg)
	begin
		set @existe=1
	end
end
else
begin
	set @codeguia=(select dbo.f_generarcodigo())
end
if @ruc is not null
begin
	if exists (select * from cliente where ruc=@ruc)
	begin
		set @existruc=1
		set @rznsocial=(select razon_social from cliente where ruc=@ruc)
	end	
end
else
begin
	set @existruc=2
end
if (@Fech>=(select top 1 fecha_inicio from semanas  where estado=1) and @Fech<=(select top 1 fecha_fin from semanas  where estado=1))
begin
	set @fechvalida=1
end
select @existe as estado,@existruc as rucexiste,@fechvalida as fechavalida, @rznsocial as razonsocial,@codeguia as coddevolgenerado
END
GO
/****** Object:  StoredProcedure [dbo].[pa_eliminarClientes]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_eliminarClientes]
@ruc char(13) 
as
delete from cliente where ruc=@ruc
GO
/****** Object:  StoredProcedure [dbo].[pa_buscarClientes]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_buscarClientes]
@ruc char(13)
as
begin
select*from cliente where ruc=@ruc
end
GO
/****** Object:  StoredProcedure [dbo].[pa_actualizarCliente]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_actualizarCliente]
@ruc char(13),
@raz_social text,
@dire text,
@telef text,
@email text,
@rep_legal char(80)

as 
if exists (select* from cliente where ruc=@ruc)
begin

update  cliente set razon_social= @raz_social,direccion=@dire,telefono=@telef,correo=@email,rep_legal=@rep_legal
where ruc=@ruc
end
GO
/****** Object:  StoredProcedure [dbo].[pa_SoloAgregarInsumoaGuia]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pa_SoloAgregarInsumoaGuia]
@numguia char(20),
@codinsum char(7),
@cant float,
@codalm char(2)
AS
BEGIN
declare @sod smallint
set @sod=(select salidaodevolucion from guiaCliente where numeroguia=@numguia)
	if((@cant<=(select top 1 cantidad from stock_almacen where cod_almacen=@codalm and cod_insumo=@codinsum) AND @sod=1) OR @sod=2)
	begin
		insert into detalleGuia(cod_insumo,numeroguia,cantidad)
		values (@codinsum,@numguia,@cant)
	end
END
GO
/****** Object:  StoredProcedure [dbo].[pa_stockActualProductor]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_stockActualProductor]
@ruc char(13)

as
select
ins.cod_insumo,
ins.nombre_insumo,
sc.cantidad

 from stock_cliente as sc 
      join insumos as ins on ins.cod_insumo=sc.cod_insumo
 
 where sc.ruc=@ruc and sc.cantidad>0
GO
/****** Object:  StoredProcedure [dbo].[pa_SoloRegistrarInsumoProdTerm]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_SoloRegistrarInsumoProdTerm] 
@codigo nvarchar(5),
@codinsu char(7),
@cant numeric(18,6),
@tipo bit
AS
BEGIN
	SET NOCOUNT ON;
	insert into ConsumoPTerminado(codigoPT,cod_insumo,cantidad,tipo)
	values(@codigo,@codinsu,@cant,@tipo)
END
GO
/****** Object:  Table [dbo].[Guia_PTerminado]    Script Date: 02/10/2017 17:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Guia_PTerminado](
	[numguia] [char](20) NOT NULL,
	[codigoPT] [nvarchar](5) NOT NULL,
	[cantidadCajas] [int] NOT NULL,
	[paletiz] [bit] NOT NULL,
 CONSTRAINT [PK_Guia_PTerminado] PRIMARY KEY CLUSTERED 
(
	[numguia] ASC,
	[codigoPT] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[pa_RegistraGuiaPTerminado2]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_RegistraGuiaPTerminado2]
@numguia char(30),
@codpt nvarchar(5),
@cantcaj int,
@pal bit
AS
BEGIN
	SET NOCOUNT ON;
	declare @rpta int=0
	if not exists (select * from Guia_PTerminado where numguia=@numguia and codigoPT=@codpt)
	begin
		insert into Guia_PTerminado(numguia,codigoPT,cantidadCajas,paletiz)
		values(@numguia,@codPT,@cantcaj,@pal)
		set @rpta=1
	end
	else
	begin
		set @rpta=0
	end
	select @rpta as inserto
END
GO
/****** Object:  StoredProcedure [dbo].[pa_RegistraGuiaPTerminado]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_RegistraGuiaPTerminado]
@codalm char(2),
@numguia char(30),
@fech date,
@obs text,
@persrecep text,
@ruc char(13),
@codPT nvarchar(5),
@cantcaj int,
@pal bit
AS
BEGIN
	SET NOCOUNT ON;
	declare @rpta int=2
	insert into guiaCliente(numeroguia,cod_almacen,fecha,observacion,persona_recepcion,ruc,salidaodevolucion)
	values(@numguia,@codalm,@fech,@obs,@persrecep,@ruc,1)
	if exists (select * from guiaCliente where numeroguia=@numguia)
	begin
		if not exists (select * from Guia_PTerminado where numguia=@numguia and codigoPT=@codPT)
		begin
			insert into Guia_PTerminado(numguia,codigoPT,cantidadCajas,paletiz)
			values(@numguia,@codPT,@cantcaj,@pal)
			set @rpta=1
		end
		else
		begin
			set @rpta=0
		end
	end
	select @rpta as inserto
END
GO
/****** Object:  StoredProcedure [dbo].[pa_RegGuiaTrasladoAlmacen]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_RegGuiaTrasladoAlmacen]
@codalmEm char(2),
@codalmRe char(2),
@numguia char(20),
@codinsumo char(7),
@cantidad float
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @excedecantidad int=1
	declare @inserto int=0
	if(@cantidad>(select cantidad from stock_almacen where cod_almacen=@codalmEm and cod_insumo=@codinsumo))
	begin
		set @excedecantidad=1
	end
	else
	begin
		set @excedecantidad=0
	end
	if (@excedecantidad=0)
	begin
		if not exists (select * from guiaCliente where numeroguia=@numguia)
		begin
			insert into guiaCliente(numeroguia,cod_almacen,salidaodevolucion,codalmacenrecep)
			values(@numguia,@codalmEm,1,@codalmRe)
		end
		if exists (select * from guiaCliente where numeroguia=@numguia and cod_almacen=@codalmEm and codalmacenrecep=@codalmRe)
		begin
			if exists (select * from detalleGuia where numeroguia=@numguia and cod_insumo=@codinsumo)
			begin
				set @inserto=2
			end
			else
			begin
				insert into detalleGuia(cod_insumo,numeroguia,cantidad)
				values(@codinsumo,@numguia,@cantidad)
				set @inserto=1
			end
		end
	end
	select @excedecantidad as excede,@inserto as inserto
END
GO
/****** Object:  StoredProcedure [dbo].[pa_procesarSemanaInsumo]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_procesarSemanaInsumo]
@codSema char(2),
@ruc char(13),
@codAlm char(2)

as
--this cant es para la entrega de guias
declare @cantEntrega float
--this variable almacena el saldo inicial se debe consultar al sal anterior
declare @saldInicial float
declare @consProduccion float
--calcular toal sf=salIni+entrega-consumo
declare @saldoTotal float
declare @cantStock_Cliente float
set @saldInicial= (select saldo_inicial  from semana_insumo)
select @cantStock_Cliente=(select cantidad from stock_cliente where ruc=@ruc)
 
 set @cantEntrega=(select numeroguia COUNT  from guiaCliente where cod_almacen=@codAlm and ruc=@ruc)

begin
  if @saldInicial=0
   set  @saldInicial=@saldInicial+@cantStock_Cliente

    
end
GO
/****** Object:  StoredProcedure [dbo].[pa_MovimientosAlmacen]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_MovimientosAlmacen]
@numguia char(20)
AS
BEGIN
	SET NOCOUNT ON;
	declare @existeguia int=1
	if(exists (select * from guiaCliente where numeroguia=@numguia))
	begin
		set @existeguia=1
	end
	else
	begin
		set @existeguia=0
	end
	select @existeguia as existe
END
GO
/****** Object:  StoredProcedure [dbo].[pa_registrarInsumos]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[pa_registrarInsumos]
 
(
 @codInsumo char(7),
 @nombre_ins nchar(100),
 @codUni int,
 @codFam int
)
as
begin
 insert into dbo.insumos(
	 [cod_insumo],
	 [nombre_insumo],
	 [cod_unidad],
	 [cod_familia]
	)
 
 values (@codInsumo,@nombre_ins,@codUni,@codFam)
end
GO
/****** Object:  StoredProcedure [dbo].[pa_RegistrarInsumoInventarioInicial]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_RegistrarInsumoInventarioInicial]
@codinsumo char(7),
@codalm char(2),
@fecha date,
@cantact float,
@costoinicial numeric(9, 5)
AS
BEGIN
	INSERT INTO inventario_inicial(cod_almacen,Cod_insumo,cant_actual,Fecha,costoinicial)
	VALUES(@codalm,@codinsumo,@cantact,@fecha,@costoinicial)
END
GO
/****** Object:  StoredProcedure [dbo].[pa_registrarClientes]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[pa_registrarClientes](
 @ruc char(13),
 @razon_soc text,
 @direccion text,
 @telefono text,
 @rep_legal text,
 @email text

  
)
as
begin
 insert into cliente([ruc], [razon_social], [direccion], [telefono],[rep_legal],[correo]) 
 values (@ruc,@razon_soc, @direccion, @telefono, @rep_legal,@email)
end
GO
/****** Object:  StoredProcedure [dbo].[pa_RetornaGuiaPorNumeroGuia]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_RetornaGuiaPorNumeroGuia]
@numguia char(20),
@codAlm char(2)
AS
BEGIN
	select 
	gc.numeroguia,
	gc.fecha,
	gc.persona_recepcion,
	gc.observacion,
	gc.ruc
	from  guiaCliente as gc where gc.cod_almacen=@codAlm and numeroguia=@numguia
END
GO
/****** Object:  StoredProcedure [dbo].[pa_RetornaAnioCodSem]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_RetornaAnioCodSem] 
@fecha date
AS
BEGIN
	SET NOCOUNT ON;
	declare @codsem char(2) = null
	declare @anio int =0
	select @codsem=cod_semana,@anio=anio from semanas where @fecha between fecha_inicio and fecha_fin
	select @codsem as codseman, @anio as aniio
END
GO
/****** Object:  StoredProcedure [dbo].[pa_reportarMermas]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_reportarMermas]
 @ruc char(13)
 as
 select
  se.ruc,
  se.cod_insumo,
  ins.nombre_insumo,
  se.ConsumoReal-se.Consumo as merma,
  se.costo
  from Semana_Consumo_Produccion as se
  join insumos as ins on ins.cod_insumo=se.cod_insumo
  where se.ruc=@ruc and se.ConsumoReal-se.Consumo>0
GO
/****** Object:  StoredProcedure [dbo].[pa_regSemanaInsumoProduccion]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_regSemanaInsumoProduccion]
@codSema char(2),
@codPT char(5),
@ruc char(13),
@codalm char(2),
@cant int,
@anio int,
@paletizado bit
as
begin
 insert into dbo.Semana_Produccion
 (cod_semana,codigoPT,ruc,anio,cod_almacen,CantidadCajas,PaletizadoOno) 
 values (@codSema,@codPT,@ruc, @anio, @codalm,@cant,@paletizado)
end
GO
/****** Object:  StoredProcedure [dbo].[pa_RegistrarStockClienteInic]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_RegistrarStockClienteInic]
@ruc char(13),
@codinsumo char(7),
@cantidad float,
@costo numeric(10,6)
AS
BEGIN
declare @resultado int=0
if exists(select * from cliente where ruc=@ruc)
begin
	if not exists(select * from stock_cliente where ruc=@ruc and cod_insumo=@codinsumo)
	begin
		insert into stock_cliente(ruc,cod_insumo,cantidad,costo)
		values(@ruc,@codinsumo,@cantidad,@costo)
		set @resultado=1
	end
	else
	begin
		set @resultado=2
	end
end
else
begin
	set @resultado=0
end
select @resultado as resultado
END
GO
/****** Object:  StoredProcedure [dbo].[pa_registrarSemanas]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[pa_registrarSemanas] 
(
 @codSem char(2),
 @anio integer,
 @fech_inicio date,
 @fech_fin date,
 @observ text,
 @estado int
)

as
begin
 insert into dbo.semanas 
 ([cod_semana], 
 [anio],
 [fecha_inicio],
 [fecha_fin],
 [observacion],
 [estado]) values (@codsem,@anio,@fech_inicio,@fech_fin,@observ, @estado)
end
GO
/****** Object:  StoredProcedure [dbo].[pa_SemanaPorCerrar]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_SemanaPorCerrar]
AS
BEGIN
	select top 1 * from semanas where estado=1 
	order by fecha_fin desc
END
GO
/****** Object:  StoredProcedure [dbo].[pa_SemanaPorActivar]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,David Chiroque Peña>
-- Create date: < Date,6-12-2016>
-- =============================================
CREATE PROCEDURE [dbo].[pa_SemanaPorActivar]
AS
BEGIN
	declare @numceros int;
	SET @numceros=(SELECT COUNT(*) from semanas where estado=2);
	if(@numceros=0)
	begin
		SELECT top 1 * from semanas 
		where estado=0
		order by fecha_fin asc
	end
	Else
	begin
		SELECT top 1 *  from semanas 
		where estado=0 and fecha_fin > 
		(select top 1 (fecha_fin) from semanas
		where estado=2 
		order by fecha_fin desc)
		order by fecha_fin asc
	end
END
GO
/****** Object:  StoredProcedure [dbo].[pa_RetornaUnDetallePorGuia]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_RetornaUnDetallePorGuia]
@codins char(7),
@numguia char(20),
@codalm char(2)
AS
BEGIN
 select 
	 dg.cod_insumo,
	 ins.nombre_insumo,
	 dg.numeroguia,
	 dg.cantidad
	 from  detalleGuia as dg
	 join guiaCliente as gc on gc.numeroguia=dg.numeroguia  
	  join insumos as ins on ins.cod_insumo=dg.cod_insumo 
	 where dg.numeroguia=@numguia and gc.cod_almacen=@codalm and dg.cod_insumo=@codins
END
GO
/****** Object:  StoredProcedure [dbo].[pa_RetornaUltimaFecha]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_RetornaUltimaFecha]
AS
BEGIN
declare @fechaI date
declare @fechaF date
declare @existe int=0
if exists (select * from semanas)
begin
	set @fechaI=(select top 1 DATEADD(day,1,fecha_fin) from semanas order by fecha_fin desc)
	set @fechaF=(DATEADD(day,6,@fechaI))
	set @existe=1 
end
else
begin
	set @fechaI=null
	set @fechaF=null
	set @existe=0
end
select @existe as existe, @fechaI as fechaInic, @fechaF as FechaFinal
END
GO
/****** Object:  Table [dbo].[usuarios]    Script Date: 02/10/2017 17:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[usuarios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[loggin] [char](20) NOT NULL,
	[pass] [char](20) NULL,
	[nombres] [char](30) NULL,
	[apellidos] [char](30) NULL,
	[correo] [char](30) NULL,
	[telefono] [char](30) NULL,
 CONSTRAINT [PK_usuarios] PRIMARY KEY CLUSTERED 
(
	[loggin] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[pa_listarProdTipoProduccion]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_listarProdTipoProduccion]
@codTP nvarchar(5)
as 

select 
ins.nombre_insumo,
uni.uni_des,
cpt.cantidad


from ConsumoPTerminado as cpt 
 join insumos as ins on ins.cod_insumo=cpt.cod_insumo
 join unidad as uni on uni.cod_unidad=ins.cod_unidad

where codigoPT=@codTP
GO
/****** Object:  StoredProcedure [dbo].[pa_ListarInsumosGuiaProductor]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_ListarInsumosGuiaProductor]
@numguia char(20)
AS
begin
	select det.cod_insumo,ins.nombre_insumo,uni.uni_des,det.cantidad 
	from detalleGuia as det
	join insumos as ins on ins.cod_insumo=det.cod_insumo
	join unidad as uni on uni.cod_unidad=ins.cod_unidad
	where det.numeroguia=@numguia
end
GO
/****** Object:  StoredProcedure [dbo].[pa_listarDetalleGuia]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_listarDetalleGuia]
@numguia char(20),
@codAlmac char(2)
as
begin
 declare @costinicial numeric(9,5)
 select distinct
 dg.cod_insumo,
 ins.nombre_insumo,
 u.uni_des,
 dg.numeroguia,
 dg.cantidad, 
 (select costoinicial from inventario_inicial where cod_insumo=dg.cod_insumo) as costoinicial,
 (dg.cantidad)*(select costoinicial from inventario_inicial where cod_insumo=dg.cod_insumo) as total
 from  detalleGuia as dg
 join guiaCliente as gc on gc.numeroguia=dg.numeroguia
 join insumos as ins on ins.cod_insumo=dg.cod_insumo
 join unidad as u on u.cod_unidad=ins.cod_unidad
 where dg.numeroguia=@numguia and gc.cod_almacen=@codAlmac
end
GO
/****** Object:  Table [dbo].[proveedor]    Script Date: 02/10/2017 17:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[proveedor](
	[ruc] [char](13) NOT NULL,
	[razon_social] [nvarchar](70) NULL,
	[telefono] [nvarchar](70) NULL,
	[fax] [nvarchar](70) NULL,
	[rep_legal] [nvarchar](70) NULL,
	[correo] [nvarchar](70) NULL,
 CONSTRAINT [PK_proveedor] PRIMARY KEY CLUSTERED 
(
	[ruc] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[pa_SoloInsertInsumGuiaTransAlmacen]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_SoloInsertInsumGuiaTransAlmacen]
@numguia char(20),
@codinsumo char(7),
@cantidad float
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
END
GO
/****** Object:  StoredProcedure [dbo].[pa_SemanaProduccion]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_SemanaProduccion]
@codsem char(2),
@codalm char(2),
@anio int,
@ruc char(13),
@codigPT nvarchar(5),
@cantCajas int
AS
BEGIN
SET NOCOUNT ON;
END
GO
/****** Object:  Table [dbo].[Cambio_moneda]    Script Date: 02/10/2017 17:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cambio_moneda](
	[Fecha_Cambio] [date] NOT NULL,
	[Compra] [money] NULL,
	[Venta] [money] NULL,
 CONSTRAINT [PK_Cambio_moneda_1] PRIMARY KEY CLUSTERED 
(
	[Fecha_Cambio] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[pa_VerificarCierreProduccion]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_VerificarCierreProduccion]
@codsem char(2),
@anio int
AS
BEGIN
	SET NOCOUNT ON;
	select sp.ruc,c.razon_social,al.descripcion from Semana_Produccion as sp join almacen as al
	on sp.cod_almacen=al.cod_almacen join cliente as c on sp.ruc=c.ruc
	where sp.cod_semana=@codsem and sp.anio=@anio and sp.cerroProducc=0
END
GO
/****** Object:  StoredProcedure [dbo].[pa_listarInsumos]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_listarInsumos]

AS
select
i.cod_insumo,
i.nombre_insumo,
u.uni_des,
f.fam_des

 from  insumos as i
 join unidad as u on u.cod_unidad=i.cod_unidad
 join familia as f on f.cod_familia=i.cod_familia
GO
/****** Object:  StoredProcedure [dbo].[pa_RegistrarProductoTerminado]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_RegistrarProductoTerminado]
@descripcion nvarchar(100),
@codinsumo char(7),
@cant numeric(18,6),
@tipo bit
AS
BEGIN
declare @lastval int 
declare @i int
declare @codpt nvarchar(5)
set @lastval = (select MAX(cast(codigoPT as int)) from PTerminado) 
if @lastval is null 
	set @lastval = 0		
set @i = @lastval + 1	
set @codpt= replicate('0',5-(len(cast(@i as varchar))))+cast(@i as varchar)
INSERT INTO PTerminado(codigoPT,descripcion)
values(@codpt,@descripcion)
if exists (select * from PTerminado where codigoPT=@codpt and descripcion=@descripcion)
begin
	insert into ConsumoPTerminado(codigoPT,cod_insumo,cantidad,tipo)
	values(@codpt,@codinsumo,@cant,@tipo)	
end
else
begin
	set @codpt=null
end
select @codpt as codPTerm
END
GO
/****** Object:  StoredProcedure [dbo].[pa_listarProveedores]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pa_listarProveedores]
as
select*from proveedor
GO
/****** Object:  StoredProcedure [dbo].[pa_listarusuarios]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_listarusuarios]
as
select*from usuarios
GO
/****** Object:  StoredProcedure [dbo].[pa_insertarUsuarios]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[pa_insertarUsuarios]
(
 @log char(20),
 @pas char(20),
 @name char(30),
 @ape char(30),
 @email char(30),
 @phone char(30)
)
as
begin
 insert into dbo.usuarios ([loggin], [pass], [nombres], [apellidos], [correo],[telefono]) values (@log,@pas, @name, @ape, @email, @phone)
end
GO
/****** Object:  StoredProcedure [dbo].[pa_insertarNewCotizacion]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pa_insertarNewCotizacion]
@fecha date,
@comp money,
@vent money

as
insert into
 Cambio_moneda(
 Fecha_Cambio,
 Compra,Venta
 )
 
 values(@fecha,@comp,@vent);
GO
/****** Object:  StoredProcedure [dbo].[pa_listarCotizacion]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_listarCotizacion]
as
select*from Cambio_moneda
GO
/****** Object:  StoredProcedure [dbo].[pa_buscarUsuario]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_buscarUsuario]
@id int
as
select*from usuarios where id=@id
GO
/****** Object:  StoredProcedure [dbo].[pa_BuscartipoCambio]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_BuscartipoCambio]
@fecha date
as
select*from Cambio_moneda where Fecha_Cambio=@fecha
GO
/****** Object:  StoredProcedure [dbo].[pa_buscarProveedor]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_buscarProveedor]
@ruc char(13)
as
select* from proveedor
		 where ruc=@ruc
GO
/****** Object:  StoredProcedure [dbo].[pa_eliminarUsuario]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_eliminarUsuario]
@id int
as
delete usuarios from usuarios where id=@id
GO
/****** Object:  StoredProcedure [dbo].[pa_eliminarProveedor]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pa_eliminarProveedor]
@ruc char(13)
as
delete proveedor from proveedor where ruc=@ruc
GO
/****** Object:  StoredProcedure [dbo].[pa_actualizarUsuario]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_actualizarUsuario]
@id int,
@login char(20),
@pass char(20),
@nombres char(30),
@apellidos char(30),
@email char(30),
@telef char(30)
as
if exists (select* from usuarios where id=@id)
begin

update  usuarios 
set loggin= @login,pass=@pass,@nombres=@nombres,apellidos=@apellidos,correo=@email,telefono=@telef
where id=@id
end
GO
/****** Object:  Table [dbo].[compras]    Script Date: 02/10/2017 17:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[compras](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cod_compra] [varchar](10) NOT NULL,
	[numero] [char](20) NOT NULL,
	[ruc] [char](13) NULL,
	[fecha_emision] [date] NULL,
	[forma_pago] [char](2) NULL,
	[fecha_vencimiento] [date] NULL,
	[valor_venta] [money] NULL,
	[impuesto] [money] NULL,
	[numeroguia] [char](20) NOT NULL,
	[observacion] [text] NULL,
	[cod_almacen] [char](2) NOT NULL,
	[tipodecambio] [money] NOT NULL,
	[moneda] [int] NOT NULL,
	[compraoventa] [smallint] NOT NULL,
 CONSTRAINT [Unique_compras] PRIMARY KEY CLUSTERED 
(
	[cod_compra] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ_NUMERO] UNIQUE NONCLUSTERED 
(
	[numero] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[detalle_compra]    Script Date: 02/10/2017 17:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[detalle_compra](
	[cod_insumo] [char](7) NOT NULL,
	[cod_compra] [varchar](10) NOT NULL,
	[precio] [numeric](15, 6) NULL,
	[cantidad] [numeric](15, 6) NULL,
	[total] [numeric](15, 6) NULL,
	[observacion] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[pa_RegistrarInsumosCompraoVenta]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_RegistrarInsumosCompraoVenta]
@idcompra int,
@codIns char(7),
@precio numeric(15,6),
@cant numeric(15,6),
@observ text,
@cod_almacen char(2)
AS
BEGIN
declare @seregistra int=0
if(@idcompra=0)
begin
	set @seregistra=0
end
if exists (select * from detalle_compra where cod_compra=(select cod_compra from compras where id=@idcompra) and cod_insumo=@codIns)
begin
	set @seregistra=0
end
else
begin
	declare @codcompra varchar(10)
	set @codcompra=(select cod_compra from compras where id=@idcompra)
	insert into detalle_compra(cod_insumo,cod_compra,precio,cantidad,total,observacion)
	values(@codIns,@codcompra,@precio,@cant,@precio*@cant,@observ)
	set @seregistra=1
end
select @seregistra as siseregistro
END
GO
/****** Object:  StoredProcedure [dbo].[pa_listarDetalleCompras]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_listarDetalleCompras]
 @codalma char(2),
 @id int
as
begin
 select i.cod_insumo,i.nombre_insumo,unit.uni_des,
 dc.cantidad,dc.precio,dc.total,dc.observacion,c.cod_compra,c.id
 from  detalle_compra as dc
 join compras as c on dc.cod_compra=c.cod_compra
 join insumos as i on i.cod_insumo=dc.cod_insumo
 join unidad as unit on unit.cod_unidad=i.cod_unidad
 where c.cod_almacen=@codalma and c.id=@id 
 order by c.fecha_emision desc
end
GO
/****** Object:  StoredProcedure [dbo].[pa_eliminarDetalleCompra]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pa_eliminarDetalleCompra]
 @codalm char(2),
 @codIns varchar(7),
 @id int
as
begin
 delete from detalle_compra 
 where cod_insumo = @codIns 
 and cod_compra=(select cod_compra from compras
  where id=@id and cod_almacen=@codalm)
end
GO
/****** Object:  StoredProcedure [dbo].[pa_DetalleCompraPorEditar]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_DetalleCompraPorEditar]
 @codalma char(2),
 @id int,
 @codins char(7)
AS
BEGIN
	 select i.cod_insumo,i.nombre_insumo,dc.cantidad,dc.precio,dc.total,dc.observacion,c.cod_compra
	 from  detalle_compra as dc
	 join compras as c on dc.cod_compra=c.cod_compra
	 join insumos as i on i.cod_insumo=dc.cod_insumo
	 where c.cod_almacen=@codalma and c.id=@id and dc.cod_insumo=@codins
END
GO
/****** Object:  StoredProcedure [dbo].[pa_buscaDetalleCompra]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_buscaDetalleCompra]
@codalma char(2),
@codIns char(7),
@id int
as
select
 c.id,
 dc.cod_insumo,
 i.nombre_insumo,
 dc.cantidad,
 dc.precio,
 dc.total,
 dc.observacion
 
 from  detalle_compra as dc
 join compras as c on dc.cod_compra=c.cod_compra
 join insumos as i on i.cod_insumo=dc.cod_insumo
 where c.cod_almacen=@codalma and dc.cod_insumo=@codIns and c.id=@id
GO
/****** Object:  StoredProcedure [dbo].[pa_AsignarCostosAdicionales]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_AsignarCostosAdicionales]
@idcompra int,
@totalgc numeric(18,4)
AS
BEGIN
declare @codcompra varchar(10)
declare @totalpdc money
set @codcompra=(select cod_compra from compras where id=@idcompra)
set @totalpdc=(select SUM(precio) from detalle_compra where cod_compra=@codcompra)
update detalle_compra set precio=precio*(1+(@totalgc/(@totalpdc*cantidad))) where cod_compra=@codcompra

select * from detalle_compra
END
GO
/****** Object:  StoredProcedure [dbo].[pa_ActualizarDetalleCompra]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_ActualizarDetalleCompra]
@idcompra int,
@codins char(7),
@precio money,
@cantidad float,
@codalm char(2),
@observacion text
AS
BEGIN
  update detalle_compra set precio=@precio,cantidad=@cantidad,observacion=@observacion
where cod_insumo=@codins AND cod_compra=( select cod_compra from compras where id=@idcompra and cod_almacen=@codalm)
END
GO
/****** Object:  StoredProcedure [dbo].[pa_updateProveedor]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_updateProveedor]
@ruc char(13),
@raz_social nvarchar(70),
@telef nvarchar(70),
@fax nvarchar(70),
@rep_legal nvarchar(70),
@email nvarchar(70)
as 
if exists (select* from proveedor where ruc=@ruc)
begin

update  proveedor set razon_social= @raz_social,telefono=@telef,fax=@fax,rep_legal=@rep_legal,correo=@email
where ruc=@ruc
end
GO
/****** Object:  StoredProcedure [dbo].[pa_updateCotizacionDolar]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_updateCotizacionDolar]
@fecha date,
@compra money,
@venta money
as
	if exists (select* from Cambio_moneda where Fecha_Cambio=@fecha)
begin
	update Cambio_moneda set Compra=@compra,Venta=@venta where Fecha_Cambio=@fecha
end
else
begin
	print ' no found'
end
GO
/****** Object:  StoredProcedure [dbo].[pa_retornarTipoCambio]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pa_retornarTipoCambio]
@FECHA date	
AS
BEGIN
declare @compra money
declare @venta money
if not exists(select * from Cambio_moneda where Fecha_Cambio=@FECHA)
begin
	select top 1 @compra=Compra,@venta=Venta from Cambio_moneda where Fecha_Cambio<@FECHA order by Fecha_Cambio desc
	if (@venta is not null AND @compra is not null)
	begin
	  insert into Cambio_moneda(Fecha_Cambio,Compra,Venta)
	  values(@FECHA,@compra,@venta)
	end
end
select * from Cambio_moneda where Fecha_Cambio=@FECHA
END
GO
/****** Object:  StoredProcedure [dbo].[pa_registrarProveedor]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pa_registrarProveedor]
@ruc char(13),
@raz_social nvarchar(70),
@telefono nvarchar(70),
@fax nvarchar(70),
@rep_legal nvarchar(70),
@email nvarchar(70)

as
begin
 insert into proveedor([ruc], [razon_social], [telefono],[fax],[rep_legal],[correo]) 
 values (@ruc,@raz_social, @telefono, @fax, @rep_legal,@email)
end
GO
/****** Object:  StoredProcedure [dbo].[pa_registrarAlmacen]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[pa_registrarAlmacen]
(
 @codAlmac char(2),
 @desAlmac char(80),
 @dirAlmac char(80)
)
as
begin
 insert into dbo.almacen([cod_almacen], [descripcion], [direccion]) values (@codAlmac,@desAlmac, @dirAlmac)
end
GO
/****** Object:  Table [dbo].[pagos_compra]    Script Date: 02/10/2017 17:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pagos_compra](
	[cod_compra] [varchar](10) NOT NULL,
	[numero_pago] [char](20) NOT NULL,
	[observacion] [text] NOT NULL,
 CONSTRAINT [PK_pagos_compra] PRIMARY KEY CLUSTERED 
(
	[cod_compra] ASC,
	[numero_pago] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[pa_mostarSoloVentas]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pa_mostarSoloVentas]
@codalmacen char(2),
@movimiento smallint
AS
BEGIN
	select c.id,c.forma_pago, 
	c.numero,c.fecha_emision,
	c.fecha_vencimiento,c.observacion,
	p.razon_social,c.valor_venta,c.impuesto,
	 c.valor_venta+(c.valor_venta*c.impuesto) 
	as total from compras as c 
	JOIN proveedor as p on c.ruc=p.ruc
	WHERE c.cod_almacen=@codalmacen and c.compraoventa=@movimiento
END
GO
/****** Object:  Table [dbo].[Gastos_Compra]    Script Date: 02/10/2017 17:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Gastos_Compra](
	[cod_compra] [varchar](10) NOT NULL,
	[Cuenta] [varchar](8) NOT NULL,
	[Descripcion] [varchar](50) NULL,
	[Cantidad] [float] NULL,
	[Precio] [numeric](18, 3) NULL,
	[Total] [numeric](18, 3) NULL,
 CONSTRAINT [PK_Gastos_Compra] PRIMARY KEY CLUSTERED 
(
	[cod_compra] ASC,
	[Cuenta] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[pa_RetornaTotales]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_RetornaTotales]
@id int
AS
BEGIN
declare @codcompra varchar(10)
declare @total1 numeric(18,6)=0
declare @total2 numeric(18,6)=0
set @codcompra=(select cod_compra from compras where id=@id)
if exists (select * from detalle_compra where cod_compra=@codcompra)
begin
	set @total1=(select SUM(precio*cantidad) from detalle_compra where cod_compra=@codcompra)
end
if exists (select * from Gastos_Compra where cod_compra=@codcompra)
begin
	set @total2=(select SUM(Precio*Cantidad) from detalle_compra where cod_compra=@codcompra)
end
select @total1 as totalinsumos,@total2 as totalgastoscompra
END
GO
/****** Object:  UserDefinedFunction [dbo].[codgenerado]    Script Date: 02/10/2017 17:24:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[codgenerado] ()
returns varchar(10) 
as 

begin 
	declare @lastval int 
	declare @i int
	declare @ceros as varchar
	Set @ceros = '0000000000'
	set @lastval = (select MAX(cast(cod_compra as int)) from compras) 
	if @lastval is null 
		set @lastval = 0
		
	set @i = @lastval + 1
	
	return replicate('0',10-(len(cast(@i as varchar))))+cast(@i as varchar)
end
GO
/****** Object:  StoredProcedure [dbo].[pa_buscarcompra]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pa_buscarcompra]
@id int
as
declare @codalmacen char(2)
select @codalmacen= cod_almacen from compras where id=@id
print @codalmacen

select c.id, c.numero,
	c.fecha_emision,
	c.observacion,
	p.razon_social,c.valor_venta,
	c.impuesto, 
	c.valor_venta+(c.valor_venta*c.impuesto)
	as total from compras as c 
	JOIN proveedor as p on c.ruc=p.ruc
	WHERE c.cod_almacen=@codalmacen

select *from compras
GO
/****** Object:  StoredProcedure [dbo].[pa_ActualizarCompra]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_ActualizarCompra]
@id int,
@numero varchar(20),
@fecha_emision date,
@fecha_vencimiento date,
@valor_venta money,
@observacion text,
@cod_almacen char(2),
@impuesto money
AS
BEGIN
  UPDATE compras SET numero=@numero, fecha_emision=@fecha_emision,fecha_vencimiento=@fecha_vencimiento,valor_venta=@valor_venta,observacion=@observacion,impuesto=@impuesto
  WHERE id=@id AND cod_almacen=@cod_almacen
END
GO
/****** Object:  StoredProcedure [dbo].[pa_eliminarcompra]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pa_eliminarcompra]
@id int
as
delete compras 
where id=@id;
GO
/****** Object:  StoredProcedure [dbo].[pa_ListarCompras]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,Chiroque Peña David>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_ListarCompras] 
@codalmacen char(2)
AS
BEGIN
	select c.id,c.forma_pago, c.numero,c.fecha_emision,c.fecha_vencimiento,c.observacion,p.razon_social,c.valor_venta,c.impuesto, c.valor_venta+(c.valor_venta*c.impuesto) as total from compras as c 
	JOIN proveedor as p on c.ruc=p.ruc
	WHERE c.cod_almacen=@codalmacen
END
GO
/****** Object:  StoredProcedure [dbo].[pa_listaCodigoPterminado]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_listaCodigoPterminado]
as
begin
select 
p.codigoPT,
p.descripcion
 from PTerminado as p
end
GO
/****** Object:  StoredProcedure [dbo].[pa_ExistePTsegunDescrip]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_ExistePTsegunDescrip]
@descrip nvarchar(100)
AS
BEGIN
	SET NOCOUNT ON;
	declare @existe bit
	if exists (select * from PTerminado where descripcion=@descrip)
	begin
		set @existe = 1
	end
	else
	begin
		set @existe = 0 
	end
	select @existe as existe	
END
GO
/****** Object:  StoredProcedure [dbo].[pa_ExisteCompraGuia]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_ExisteCompraGuia]
@numero char(11),
@ruc char(13)
AS
BEGIN
declare @existe int=0
	if exists (select * from compras where numero=@numero)
	begin
		set @existe=1	
	end
	else
	begin
		set @existe=0
	end
	if not exists (select * from proveedor where ruc=@ruc) 
	begin
		set @existe=2
	end
	select @existe as estado
END
GO
/****** Object:  StoredProcedure [dbo].[pa_listaTiposProdTerminado]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pa_listaTiposProdTerminado]
as
select*from PTerminado
GO
/****** Object:  StoredProcedure [dbo].[pa_ListarProdTSegunLinea]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_ListarProdTSegunLinea]
@numlin int
AS
BEGIN
	SET NOCOUNT ON;
	select codigoPT,descripcion from PTerminado 
	where codLinea=@numlin
END
GO
/****** Object:  StoredProcedure [dbo].[pa_listarGastosCompras]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_listarGastosCompras]
@codAlmac char(2)
as

select *from Gastos_Compra where cod_compra in (select cod_compra from compras where cod_almacen=@codAlmac)
GO
/****** Object:  StoredProcedure [dbo].[pa_ListarGastosCompraPorCompra]    Script Date: 02/10/2017 17:24:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_ListarGastosCompraPorCompra] 
@id int
AS
BEGIN
	declare @codcompra varchar(10)
	set @codcompra=(select cod_compra from compras where id=@id)
	select * from Gastos_Compra where cod_compra=@codcompra
END
GO
/****** Object:  StoredProcedure [dbo].[pa_InsertarGastosCompra]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_InsertarGastosCompra] 
@id int,
@cuenta varchar(8),
@descrip varchar(50),
@cantidad float,
@precio numeric(18,3),
@total numeric(18,3)
AS
BEGIN
declare @cantTotal float
declare @codcompra varchar(10)
	set @cantTotal=@precio*@cantidad
	set @codcompra=(select cod_compra from compras where id=@id)
	insert into Gastos_Compra(cod_compra,Cuenta,Descripcion,Cantidad,Precio,Total)
	values(@codcompra,@cuenta,@descrip,@cantidad,@precio,@cantTotal)
END
GO
/****** Object:  StoredProcedure [dbo].[pa_insertarCompraoVentaRC]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<David chiroque>
-- Create date: <5-12-2016,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pa_insertarCompraoVentaRC]
	@numer char(20),
	@ruc char(13),
	@fechaem date,
	@formapago char(2),
	@fechavenc date,
	@valor_venta money,
	@impuesto money,
	@numguia char(20),
	@observacion text,	
	@cod_almacen char(2),
	@tipodecambio money,	
	@moneda int,
	@compoventa smallint
AS
BEGIN TRAN
	declare @IdentityOutput table (ID int)
	DECLARE @codcompra char(30)
	SET @codcompra=dbo.codgenerado() 
	INSERT INTO compras(cod_compra,numero,ruc,fecha_emision,forma_pago,fecha_vencimiento,valor_venta,impuesto,numeroguia,observacion,cod_almacen,tipodecambio,moneda,compraoventa)
	output inserted.id into @IdentityOutput
	values(@codcompra,@numer,@ruc,@fechaem,@formapago,@fechavenc,@valor_venta,@impuesto,@numguia,@observacion,@cod_almacen,@tipodecambio,@moneda,@compoventa)
	select ID from @IdentityOutput
IF(@@error <> 0)
BEGIN
  ROLLBACK TRAN
  RETURN
END
COMMIT TRAN
GO
/****** Object:  StoredProcedure [dbo].[pa_eliminarGastosCompras]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pa_eliminarGastosCompras]
@codCompra varchar(10),
@cuenta varchar(8)
as
delete  Gastos_Compra
	where cod_compra=@codCompra and Cuenta=@cuenta
GO
/****** Object:  StoredProcedure [dbo].[pa_editarGastosCompras]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pa_editarGastosCompras]
@codCompra varchar(10),
@cuenta varchar(8),
@descrip varchar(50),
@cant float,
@precio numeric(18,3),
@total numeric(18,3)
as
begin
    update Gastos_Compra 
    set Cuenta=@cuenta,Descripcion=@descrip,Cantidad=@cant,Precio=@precio,Total=@total
     WHERE  Cuenta=@cuenta and cod_compra=@codCompra 
end
GO
/****** Object:  StoredProcedure [dbo].[pa_busrcarGastosCompras]    Script Date: 02/10/2017 17:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pa_busrcarGastosCompras]
@codCompra varchar(10),
@cuenta varchar(8)
as
select*from Gastos_Compra where cod_compra=@codCompra and Cuenta=@cuenta
GO
/****** Object:  Default [DF_compras_moneda]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[compras] ADD  CONSTRAINT [DF_compras_moneda]  DEFAULT ((1)) FOR [moneda]
GO
/****** Object:  Check [ck_cantinicial_ii]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[inventario_inicial]  WITH CHECK ADD  CONSTRAINT [ck_cantinicial_ii] CHECK  (([cant_actual]>=(0)))
GO
ALTER TABLE [dbo].[inventario_inicial] CHECK CONSTRAINT [ck_cantinicial_ii]
GO
/****** Object:  Check [ck_cantidad]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[stock_almacen]  WITH NOCHECK ADD  CONSTRAINT [ck_cantidad] CHECK  (([cantidad]>=(0)))
GO
ALTER TABLE [dbo].[stock_almacen] NOCHECK CONSTRAINT [ck_cantidad]
GO
/****** Object:  Check [ck_cantidad_sc]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[stock_cliente]  WITH NOCHECK ADD  CONSTRAINT [ck_cantidad_sc] CHECK  (([cantidad]>=(0)))
GO
ALTER TABLE [dbo].[stock_cliente] NOCHECK CONSTRAINT [ck_cantidad_sc]
GO
/****** Object:  ForeignKey [FK_compras_almacen1]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[compras]  WITH CHECK ADD  CONSTRAINT [FK_compras_almacen1] FOREIGN KEY([cod_almacen])
REFERENCES [dbo].[almacen] ([cod_almacen])
GO
ALTER TABLE [dbo].[compras] CHECK CONSTRAINT [FK_compras_almacen1]
GO
/****** Object:  ForeignKey [FK_compras_proveedor]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[compras]  WITH CHECK ADD  CONSTRAINT [FK_compras_proveedor] FOREIGN KEY([ruc])
REFERENCES [dbo].[proveedor] ([ruc])
GO
ALTER TABLE [dbo].[compras] CHECK CONSTRAINT [FK_compras_proveedor]
GO
/****** Object:  ForeignKey [FK_Insumo_ConsumoPTerminado]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[ConsumoPTerminado]  WITH CHECK ADD  CONSTRAINT [FK_Insumo_ConsumoPTerminado] FOREIGN KEY([cod_insumo])
REFERENCES [dbo].[insumos] ([cod_insumo])
GO
ALTER TABLE [dbo].[ConsumoPTerminado] CHECK CONSTRAINT [FK_Insumo_ConsumoPTerminado]
GO
/****** Object:  ForeignKey [FK_PTerminado_ConsumoPTerminado]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[ConsumoPTerminado]  WITH CHECK ADD  CONSTRAINT [FK_PTerminado_ConsumoPTerminado] FOREIGN KEY([codigoPT])
REFERENCES [dbo].[PTerminado] ([codigoPT])
GO
ALTER TABLE [dbo].[ConsumoPTerminado] CHECK CONSTRAINT [FK_PTerminado_ConsumoPTerminado]
GO
/****** Object:  ForeignKey [FK_detalle_compra_compras1]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[detalle_compra]  WITH CHECK ADD  CONSTRAINT [FK_detalle_compra_compras1] FOREIGN KEY([cod_compra])
REFERENCES [dbo].[compras] ([cod_compra])
GO
ALTER TABLE [dbo].[detalle_compra] CHECK CONSTRAINT [FK_detalle_compra_compras1]
GO
/****** Object:  ForeignKey [FK_detalle_compra_insumos]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[detalle_compra]  WITH CHECK ADD  CONSTRAINT [FK_detalle_compra_insumos] FOREIGN KEY([cod_insumo])
REFERENCES [dbo].[insumos] ([cod_insumo])
GO
ALTER TABLE [dbo].[detalle_compra] CHECK CONSTRAINT [FK_detalle_compra_insumos]
GO
/****** Object:  ForeignKey [FK_detalleGuia_guiaCliente]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[detalleGuia]  WITH CHECK ADD  CONSTRAINT [FK_detalleGuia_guiaCliente] FOREIGN KEY([numeroguia])
REFERENCES [dbo].[guiaCliente] ([numeroguia])
GO
ALTER TABLE [dbo].[detalleGuia] CHECK CONSTRAINT [FK_detalleGuia_guiaCliente]
GO
/****** Object:  ForeignKey [FK_DetalleGuia_insumos]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[detalleGuia]  WITH CHECK ADD  CONSTRAINT [FK_DetalleGuia_insumos] FOREIGN KEY([cod_insumo])
REFERENCES [dbo].[insumos] ([cod_insumo])
GO
ALTER TABLE [dbo].[detalleGuia] CHECK CONSTRAINT [FK_DetalleGuia_insumos]
GO
/****** Object:  ForeignKey [FK_Gastos_Compra_compras]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[Gastos_Compra]  WITH CHECK ADD  CONSTRAINT [FK_Gastos_Compra_compras] FOREIGN KEY([cod_compra])
REFERENCES [dbo].[compras] ([cod_compra])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Gastos_Compra] CHECK CONSTRAINT [FK_Gastos_Compra_compras]
GO
/****** Object:  ForeignKey [FK_Guia_PTerminado_guiaCliente]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[Guia_PTerminado]  WITH CHECK ADD  CONSTRAINT [FK_Guia_PTerminado_guiaCliente] FOREIGN KEY([numguia])
REFERENCES [dbo].[guiaCliente] ([numeroguia])
GO
ALTER TABLE [dbo].[Guia_PTerminado] CHECK CONSTRAINT [FK_Guia_PTerminado_guiaCliente]
GO
/****** Object:  ForeignKey [FK_Guia_PTerminado_PTerminado]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[Guia_PTerminado]  WITH CHECK ADD  CONSTRAINT [FK_Guia_PTerminado_PTerminado] FOREIGN KEY([codigoPT])
REFERENCES [dbo].[PTerminado] ([codigoPT])
GO
ALTER TABLE [dbo].[Guia_PTerminado] CHECK CONSTRAINT [FK_Guia_PTerminado_PTerminado]
GO
/****** Object:  ForeignKey [FK_GuiaCliente_almacen]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[guiaCliente]  WITH CHECK ADD  CONSTRAINT [FK_GuiaCliente_almacen] FOREIGN KEY([cod_almacen])
REFERENCES [dbo].[almacen] ([cod_almacen])
GO
ALTER TABLE [dbo].[guiaCliente] CHECK CONSTRAINT [FK_GuiaCliente_almacen]
GO
/****** Object:  ForeignKey [FK_guiaCliente_almacen2]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[guiaCliente]  WITH CHECK ADD  CONSTRAINT [FK_guiaCliente_almacen2] FOREIGN KEY([codalmacenrecep])
REFERENCES [dbo].[almacen] ([cod_almacen])
GO
ALTER TABLE [dbo].[guiaCliente] CHECK CONSTRAINT [FK_guiaCliente_almacen2]
GO
/****** Object:  ForeignKey [FK_GuiaCliente_cliente]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[guiaCliente]  WITH CHECK ADD  CONSTRAINT [FK_GuiaCliente_cliente] FOREIGN KEY([ruc])
REFERENCES [dbo].[cliente] ([ruc])
GO
ALTER TABLE [dbo].[guiaCliente] CHECK CONSTRAINT [FK_GuiaCliente_cliente]
GO
/****** Object:  ForeignKey [FK_insumos_familia]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[insumos]  WITH CHECK ADD  CONSTRAINT [FK_insumos_familia] FOREIGN KEY([cod_familia])
REFERENCES [dbo].[familia] ([cod_familia])
GO
ALTER TABLE [dbo].[insumos] CHECK CONSTRAINT [FK_insumos_familia]
GO
/****** Object:  ForeignKey [FK_insumos_unidad]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[insumos]  WITH CHECK ADD  CONSTRAINT [FK_insumos_unidad] FOREIGN KEY([cod_unidad])
REFERENCES [dbo].[unidad] ([cod_unidad])
GO
ALTER TABLE [dbo].[insumos] CHECK CONSTRAINT [FK_insumos_unidad]
GO
/****** Object:  ForeignKey [FK_inventario_incial_almacen]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[inventario_inicial]  WITH CHECK ADD  CONSTRAINT [FK_inventario_incial_almacen] FOREIGN KEY([cod_almacen])
REFERENCES [dbo].[almacen] ([cod_almacen])
GO
ALTER TABLE [dbo].[inventario_inicial] CHECK CONSTRAINT [FK_inventario_incial_almacen]
GO
/****** Object:  ForeignKey [FK_inventario_incial_insumos]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[inventario_inicial]  WITH NOCHECK ADD  CONSTRAINT [FK_inventario_incial_insumos] FOREIGN KEY([Cod_insumo])
REFERENCES [dbo].[insumos] ([cod_insumo])
GO
ALTER TABLE [dbo].[inventario_inicial] CHECK CONSTRAINT [FK_inventario_incial_insumos]
GO
/****** Object:  ForeignKey [FK_pagos_compra_compras]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[pagos_compra]  WITH CHECK ADD  CONSTRAINT [FK_pagos_compra_compras] FOREIGN KEY([cod_compra])
REFERENCES [dbo].[compras] ([cod_compra])
GO
ALTER TABLE [dbo].[pagos_compra] CHECK CONSTRAINT [FK_pagos_compra_compras]
GO
/****** Object:  ForeignKey [FK_PTerminado_lineaPT2]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[PTerminado]  WITH CHECK ADD  CONSTRAINT [FK_PTerminado_lineaPT2] FOREIGN KEY([codLinea])
REFERENCES [dbo].[lineaPT] ([cod_linea])
GO
ALTER TABLE [dbo].[PTerminado] CHECK CONSTRAINT [FK_PTerminado_lineaPT2]
GO
/****** Object:  ForeignKey [FK_Semana_Almacen_semanas]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[Semana_Almacen]  WITH CHECK ADD  CONSTRAINT [FK_Semana_Almacen_semanas] FOREIGN KEY([codsemana], [anio])
REFERENCES [dbo].[semanas] ([cod_semana], [anio])
GO
ALTER TABLE [dbo].[Semana_Almacen] CHECK CONSTRAINT [FK_Semana_Almacen_semanas]
GO
/****** Object:  ForeignKey [FK_Semana_Consumo_Produccion_cliente]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[Semana_Consumo_Produccion]  WITH CHECK ADD  CONSTRAINT [FK_Semana_Consumo_Produccion_cliente] FOREIGN KEY([ruc])
REFERENCES [dbo].[cliente] ([ruc])
GO
ALTER TABLE [dbo].[Semana_Consumo_Produccion] CHECK CONSTRAINT [FK_Semana_Consumo_Produccion_cliente]
GO
/****** Object:  ForeignKey [FK_Semana_Consumo_Produccion_insumos]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[Semana_Consumo_Produccion]  WITH CHECK ADD  CONSTRAINT [FK_Semana_Consumo_Produccion_insumos] FOREIGN KEY([cod_insumo])
REFERENCES [dbo].[insumos] ([cod_insumo])
GO
ALTER TABLE [dbo].[Semana_Consumo_Produccion] CHECK CONSTRAINT [FK_Semana_Consumo_Produccion_insumos]
GO
/****** Object:  ForeignKey [FK_Semana_Consumo_Produccion_semanas]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[Semana_Consumo_Produccion]  WITH CHECK ADD  CONSTRAINT [FK_Semana_Consumo_Produccion_semanas] FOREIGN KEY([cod_semana], [anio])
REFERENCES [dbo].[semanas] ([cod_semana], [anio])
GO
ALTER TABLE [dbo].[Semana_Consumo_Produccion] CHECK CONSTRAINT [FK_Semana_Consumo_Produccion_semanas]
GO
/****** Object:  ForeignKey [FK_semana_insumo_cliente]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[semana_insumo]  WITH CHECK ADD  CONSTRAINT [FK_semana_insumo_cliente] FOREIGN KEY([ruc])
REFERENCES [dbo].[cliente] ([ruc])
GO
ALTER TABLE [dbo].[semana_insumo] CHECK CONSTRAINT [FK_semana_insumo_cliente]
GO
/****** Object:  ForeignKey [FK_semana_insumo_insumos]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[semana_insumo]  WITH CHECK ADD  CONSTRAINT [FK_semana_insumo_insumos] FOREIGN KEY([cod_insumo])
REFERENCES [dbo].[insumos] ([cod_insumo])
GO
ALTER TABLE [dbo].[semana_insumo] CHECK CONSTRAINT [FK_semana_insumo_insumos]
GO
/****** Object:  ForeignKey [FK_semana_insumo_semanas]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[semana_insumo]  WITH CHECK ADD  CONSTRAINT [FK_semana_insumo_semanas] FOREIGN KEY([cod_semana], [anio])
REFERENCES [dbo].[semanas] ([cod_semana], [anio])
GO
ALTER TABLE [dbo].[semana_insumo] CHECK CONSTRAINT [FK_semana_insumo_semanas]
GO
/****** Object:  ForeignKey [FK_Semana_Produccion_almacen]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[Semana_Produccion]  WITH CHECK ADD  CONSTRAINT [FK_Semana_Produccion_almacen] FOREIGN KEY([cod_almacen])
REFERENCES [dbo].[almacen] ([cod_almacen])
GO
ALTER TABLE [dbo].[Semana_Produccion] CHECK CONSTRAINT [FK_Semana_Produccion_almacen]
GO
/****** Object:  ForeignKey [FK_Semana-Produccion_almacen]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[Semana_Produccion]  WITH CHECK ADD  CONSTRAINT [FK_Semana-Produccion_almacen] FOREIGN KEY([cod_almacen])
REFERENCES [dbo].[almacen] ([cod_almacen])
GO
ALTER TABLE [dbo].[Semana_Produccion] CHECK CONSTRAINT [FK_Semana-Produccion_almacen]
GO
/****** Object:  ForeignKey [FK_Semana-Produccion_cliente]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[Semana_Produccion]  WITH CHECK ADD  CONSTRAINT [FK_Semana-Produccion_cliente] FOREIGN KEY([ruc])
REFERENCES [dbo].[cliente] ([ruc])
GO
ALTER TABLE [dbo].[Semana_Produccion] CHECK CONSTRAINT [FK_Semana-Produccion_cliente]
GO
/****** Object:  ForeignKey [FK_Semana-Produccion_PTerminado]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[Semana_Produccion]  WITH CHECK ADD  CONSTRAINT [FK_Semana-Produccion_PTerminado] FOREIGN KEY([codigoPT])
REFERENCES [dbo].[PTerminado] ([codigoPT])
GO
ALTER TABLE [dbo].[Semana_Produccion] CHECK CONSTRAINT [FK_Semana-Produccion_PTerminado]
GO
/****** Object:  ForeignKey [FK_Semana_Produccion_Insumo_Semana_Produccion]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[Semana_Produccion_Insumo]  WITH CHECK ADD  CONSTRAINT [FK_Semana_Produccion_Insumo_Semana_Produccion] FOREIGN KEY([cod_semana], [codigoPT], [ruc], [anio], [cod_almacen])
REFERENCES [dbo].[Semana_Produccion] ([cod_semana], [codigoPT], [ruc], [anio], [cod_almacen])
GO
ALTER TABLE [dbo].[Semana_Produccion_Insumo] CHECK CONSTRAINT [FK_Semana_Produccion_Insumo_Semana_Produccion]
GO
/****** Object:  ForeignKey [FK_stock_almacen_almacen]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[stock_almacen]  WITH NOCHECK ADD  CONSTRAINT [FK_stock_almacen_almacen] FOREIGN KEY([cod_almacen])
REFERENCES [dbo].[almacen] ([cod_almacen])
GO
ALTER TABLE [dbo].[stock_almacen] CHECK CONSTRAINT [FK_stock_almacen_almacen]
GO
/****** Object:  ForeignKey [FK_stock_almacen_insumos]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[stock_almacen]  WITH NOCHECK ADD  CONSTRAINT [FK_stock_almacen_insumos] FOREIGN KEY([cod_insumo])
REFERENCES [dbo].[insumos] ([cod_insumo])
GO
ALTER TABLE [dbo].[stock_almacen] CHECK CONSTRAINT [FK_stock_almacen_insumos]
GO
/****** Object:  ForeignKey [FK_stock_cliente_cliente1]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[stock_cliente]  WITH CHECK ADD  CONSTRAINT [FK_stock_cliente_cliente1] FOREIGN KEY([ruc])
REFERENCES [dbo].[cliente] ([ruc])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[stock_cliente] CHECK CONSTRAINT [FK_stock_cliente_cliente1]
GO
/****** Object:  ForeignKey [FK_stock_cliente_insumos]    Script Date: 02/10/2017 17:24:08 ******/
ALTER TABLE [dbo].[stock_cliente]  WITH CHECK ADD  CONSTRAINT [FK_stock_cliente_insumos] FOREIGN KEY([cod_insumo])
REFERENCES [dbo].[insumos] ([cod_insumo])
GO
ALTER TABLE [dbo].[stock_cliente] CHECK CONSTRAINT [FK_stock_cliente_insumos]
GO
