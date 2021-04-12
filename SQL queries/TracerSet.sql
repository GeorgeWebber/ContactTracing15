Declare @RandomTracingCentreId int

Declare @LowerLimitForTracingCentreId int
Declare @UpperLimitForTracingCentreId int

Set @LowerLimitForTracingCentreId = 1
Set @UpperLimitForTracingCentreId = 100

Declare @Id int
Set @Id = 1

While @Id <= 1000
Begin 

   Select @RandomTracingCentreId = Round(((@UpperLimitForTracingCentreId - @LowerLimitForTracingCentreId) * Rand()) + @LowerLimitForTracingCentreId, 0)

   Insert Into Tracers values ('Tracer - ' + CAST(@Id as nvarchar(10)), @RandomTracingCentreId)
   Print @Id
   Set @Id = @Id + 1
End