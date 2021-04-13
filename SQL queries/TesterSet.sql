Declare @RandomTestingCentreId int

Declare @LowerLimitForTestingCentreId int
Declare @UpperLimitForTestingCentreId int

Set @LowerLimitForTestingCentreId = 1
Set @UpperLimitForTestingCentreId = 100

Declare @Id int
Set @Id = 1

While @Id <= 1000
Begin 

   Select @RandomTestingCentreId = Round(((@UpperLimitForTestingCentreId - @LowerLimitForTestingCentreId) * Rand()) + @LowerLimitForTestingCentreId, 0)

   Insert Into Testers values ('Tester - ' + CAST(@Id as nvarchar(10)), @RandomTestingCentreId)
   Print @Id
   Set @Id = @Id + 1
End