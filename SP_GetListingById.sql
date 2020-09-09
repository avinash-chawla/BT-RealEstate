Create procedure sp_GetListingById 
(      
    @ListingId int  
)      
As       
Begin       
    SELECT 
		l.Id,
		Title,
		Address,
		City,
		State,
		ZipCode,
		l.Description,
		Price,
		Bedrooms,
		Bathrooms,
		Garage,
		Sqft,
		LotSize,
		PhotoMain,
		Photo1,
		Photo2,
		Photo3,
		Photo4,
		Photo5,
		Photo6,
		IsPublished,
		ListDate,
		RealtorId,
		r.Id as R_Id,
		r.Description as R_Description,
		r.Email as R_Email,
		r.HireDate as R_HireDate,
		r.Image as R_Image,
		r.IsMvp as R_IsMvp,
		r.Name as R_Name,
		r.Phone as R_Phone
	
	FROM dbo.Listings as l 
	Join dbo.Realtors as r
	on r.Id = l.RealtorId
	WHERE l.Id= @ListingId
End  