--DROP VIEW VID_VW_FE_PE_ORIN_E
CREATE VIEW VID_VW_FE_PE_ORIN_E
AS

	SELECT IFNULL(M0."DocCurrCod",'PEN')			"currency"
	      ,T0."TaxDate"								"date"
		  ,T0."U_BPP_MDSD"							"series"
		  ,T0."U_BPP_MDSD" || '-' || T0."U_BPP_MDCD"		"externalFolio"
		  ,V0."SlpName"								"sellerCode"
		  ,T0."LicTradNum"							"code"
		  ,T0."CardName"							"name"
		  ,T0."U_BPP_MDTN"							"noteType"
		  ,IFNULL(T12."StreetB",'') || ', ' || IFNULL(T12."BlockB",'') || ', ' || IFNULL(T12."CityB",'')	"address"
		  ,IFNULL(C2."U_BPP_CODI",'')					"municipality"
		  ,IFNULL(C1."FirstName",'')					"contact"
		  ,IFNULL(C0."Phone1",'')						"phone"
		  ,IFNULL(C0."E_Mail",'')						"email"
		  ,T0."DocDueDate"							"expirationDate"
		  ,T0."DocDueDate"							"datePayment"
		  ,CASE WHEN T0."DocTotalFC" > 0 THEN T0."DocTotalFC"
			    ELSE T0."DocTotal"
		   END										"amountPayment"
		  ,T5."PymntGroup"							"descriptionPayment"
		  ,''										"estimateNumber"
		  ,''										"account"
		  ,''										"reference"
		  ,''										"project"
		  ,''										"certificateNumber"
		  ,''										"contactP"
		  ,''										"gloss"
		  ,IFNULL(C0."U_BPP_BPTD",'')				"identityDocumentType"
		  ,T0."DocType"
		  ,T0."DocEntry"
		  ,T0."ObjType"
		  ,CASE WHEN T0."DocCur" <> A0."MainCurncy" THEN T0."DiscSumFC"
		        ELSE T0."DiscSum"
		   END										"DiscSum"
		  ,CASE WHEN T0."DocCur" <> A0."MainCurncy" THEN T0."VatSumFC"
		        ELSE T0."VatSum"
		   END										"VatSum"
		  ,M0."CurrName"							"CurrName"
		  ,CASE WHEN T0."DocCur" <> A0."MainCurncy" THEN (T0."DocTotal" - T0."VatSumFC")
		        ELSE (T0."DocTotal" - T0."VatSum")
		   END										 "Gravadas"
	  FROM "ORIN" T0
	  JOIN "OCTG" T5 ON T5."GroupNum" = T0."GroupNum"
	  JOIN "RIN12" T12 ON T12."DocEntry" = T0."DocEntry"
	  JOIN "OCRD" C0 ON C0."CardCode" = T0."CardCode"
	  JOIN "OSLP" V0 ON V0."SlpCode" = T0."SlpCode"
	  JOIN "OUSR" U0 ON U0."INTERNAL_K" = T0."UserSign"
	  LEFT JOIN "OCRN" M0 ON M0."CurrCode" = T0."DocCur"
	  LEFT JOIN "OCPR" C1 ON C1."CardCode" = T0."CardCode"
	                   AND C1."CntctCode" = T0."CntctCode"
	  LEFT JOIN "CRD1" C2 ON C2."CardCode" = T0."CardCode"
	                   AND C2."Address" = T0."PayToCode"
					   AND C2."AdresType" = 'B'
	  JOIN "NNM1" N0 ON N0."Series" = T0."Series"
	              --AND N0.ObjectCode = T0.ObjType
	  , "OADM" A0
	 WHERE UPPER(LEFT(N0."BeginStr",1)) = 'E'
	   AND T0."CANCELED" = 'N';