USE [GENERAL_PRUEBAS]
GO
/****** Object:  StoredProcedure [dbo].[VID_SP_FE_IN_ED_PE_Genera08D]    Script Date: 26/11/2018 12:57:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[VID_SP_FE_IN_ED_PE_Genera08D]
 @DocEntry Int ,@TipoDoc  Varchar(10), @ObjType VarChar(10)
AS
BEGIN
	SELECT CAST(ROW_NUMBER() OVER(ORDER BY T0.LineaOrden, T0.LineaOrden2) AS INT)	[Id]
			          ,T0.[quantity]	[Cantidad]
					  ,T0.[unit]		[UnidadMedida]
					  ,T0.[price] * T0.[quantity]	[Suma]
					  ,T0.[price] * T0.[quantity]	[TotalVenta]
					  ,T0.[price]		[PrecioUnitario]
					  ,'01'				[TipoPrecio]
					  --,ISNULL((SELECT Rate FROM OSTC WHERE Code = T0.TaxCode),0.0)		[Impuesto]
					  ,T0.Gravadas		[Impuesto]					
					  ,T0.[exemptType]	[TipoImpuesto]
					  ,0.0				[ImpuestoSelectivo]
					  ,0.0				[OtroImpuesto]
					  ,ISNULL(T0.[description],'') [Descripcion]
					  ,T0.[code]		[CodigoItem]
					  ,T0.[price]		[PrecioReferencial]
				 FROM VID_VW_FE_PE_OINV_D T0
				WHERE T0.DocEntry = @DocEntry
				  AND T0.ObjType = @ObjType
				ORDER BY  T0.[LineaOrden], T0.[LineaOrden2]
END

