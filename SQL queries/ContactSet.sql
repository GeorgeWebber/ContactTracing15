Declare @RandomCaseId int


Declare @LowerLimitForCaseId int
Declare @UpperLimitForCaseId int

Set @LowerLimitForCaseId = 100001
Set @UpperLimitForCaseId = 200000

DECLARE @FromDate DATETIME2(0)
DECLARE @ToDate   DATETIME2(0)
DECLARE @RandomTracedDate DATETIME2(0)
DECLARE @RandomAddedDate   DATETIME2(0)

SET @FromDate = '2020-01-01 00:00:01' 
SET @ToDate = '2021-04-09 23:59:59'


Declare @Id int
Set @Id = 1

While @Id <= 100000
Begin 

   DECLARE @Seconds1 INT = DATEDIFF(SECOND, @FromDate, @ToDate)
   DECLARE @Random1 INT = ROUND(((@Seconds1-1) * RAND()), 0)
   

   SELECT @RandomTracedDate = DATEADD(SECOND, @Random1, @FromDate)

   DECLARE @Seconds2 INT = DATEDIFF(SECOND, @RandomTracedDate, @ToDate)
   DECLARE @Random2 INT = ROUND(((@Seconds2-1) * RAND()), 0)

   SELECT @RandomAddedDate = DATEADD(SECOND, @Random2, @RandomTracedDate)
   

   Select @RandomCaseId = Round(((@UpperLimitForCaseId - @LowerLimitForCaseId) * Rand()) + @LowerLimitForCaseId, 0)


   Insert Into Contacts values ('Forename - ' + CAST(@Id as nvarchar(10)), 'Surname - ' + CAST(@Id as nvarchar(10)), @RandomCaseId, 
   'Email - ' + CAST(@Id as nvarchar(10)), @RandomAddedDate, 'Phone - ' + CAST(@Id as nvarchar(10)), @RandomTracedDate, null)
   Print @Id
   Set @Id = @Id + 1
End