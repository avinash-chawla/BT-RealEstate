Create procedure sp_AddListing       
(       
		@Title nvarchar(max),
		@Address  nvarchar(max),
		@City  nvarchar(max),
		@State  nvarchar(max),
		@ZipCode  nvarchar(max),
		@Description  nvarchar(max),
		@Price decimal,
		@Bedrooms int,
		@Bathrooms int,
		@Garage int,
		@Sqft int,
		@LotSize decimal,
		@PhotoMain varchar,
		@Photo1  nvarchar(max) = 'N/A',
		@Photo2  nvarchar(max) = 'N/A',
		@Photo3  nvarchar(max) = 'N/A',
		@Photo4  nvarchar(max) = 'N/A',
		@Photo5  nvarchar(max) = 'N/A',
		@Photo6  nvarchar(max) = 'N/A',
		@IsPublished bit,
		@ListDate datetime,
		@RealtorId int
)      
As       
Begin       
    Insert into dbo.Listings (
		Title,
		Address,
		City,
		State,
		ZipCode,
		Description,
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
		RealtorId)
		
    Values (
		@Title,
		@Address,
		@City,
		@State,
		@ZipCode,
		@Description,
		@Price,
		@Bedrooms,
		@Bathrooms,
		@Garage,
		@Sqft,
		@LotSize,
		@PhotoMain,
		@Photo1,
		@Photo2,
		@Photo3,
		@Photo4,
		@Photo5,
		@Photo6,
		@IsPublished,
		@ListDate,
		@RealtorId)       
End