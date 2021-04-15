Declare @RandomTesterId int
Declare @RandomPrice int
Declare @RandomEdition int

Declare @LowerLimitForTesterId int
Declare @UpperLimitForTesterId int

Set @LowerLimitForTesterId = 1
Set @UpperLimitForTesterId = 1000

DECLARE @FromDate DATETIME2(0)
DECLARE @ToDate   DATETIME2(0)
DECLARE @RandomTestDate DATETIME2(0)
DECLARE @RandomAddedDate   DATETIME2(0)

SET @FromDate = '2020-01-01 00:00:01' 
SET @ToDate = '2021-04-09 23:59:59'


Declare @Id int
Set @Id = 1

While @Id <= 100000
Begin 

   DECLARE @Seconds1 INT = DATEDIFF(SECOND, @FromDate, @ToDate)
   DECLARE @Random1 INT = ROUND(((@Seconds1-1) * RAND()), 0)
   

   SELECT @RandomTestDate = DATEADD(SECOND, @Random1, @FromDate)

   DECLARE @Seconds2 INT = DATEDIFF(SECOND, @RandomTestDate, @ToDate)
   DECLARE @Random2 INT = ROUND(((@Seconds2-1) * RAND()), 0)

   SELECT @RandomAddedDate = DATEADD(SECOND, @Random2, @RandomTestDate)
   

   Select @RandomTesterId = Round(((@UpperLimitForTesterId - @LowerLimitForTesterId) * Rand()) + @LowerLimitForTesterId, 0)


   Insert Into Cases values ('Forename - ' + CAST(@Id as nvarchar(10)), 'Surname - ' + CAST(@Id as nvarchar(10)), @RandomTesterId, 
   'Phone number - ' + CAST(@Id as nvarchar(10)), @RandomTestDate, @RandomAddedDate, 'Postcode - ' + CAST(@Id as nvarchar(10)), 'FALSE', 'Email - ' + CAST(@Id as nvarchar(10)),
   'Phone2 - ' + CAST(@Id as nvarchar(10)), null, null, null)
   Print @Id
   Set @Id = @Id + 1
End