CREATE VIEW [dbo].[vw_ProductMaster] AS
	SELECT prd.*,ISNULL(case when Isnull(prd.DiscountId,'')='' then prd.Price else 	FLOOR(prd.Price-(prd.Price* selling.Value)/100)	end,0)  as SellingPrice
	FROM tblProductMaster prd
	left join tblLookupMaster selling on selling.Id=prd.DiscountId
	where prd.IsDelete=0
