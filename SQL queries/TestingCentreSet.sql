Declare @Id int
Set @Id = 1

While @Id <= 100
Begin 
   Insert Into TestingCentres values ('Testing Centre - ' + CAST(@Id as nvarchar(10)),
              'Postcode - ' + CAST(@Id as nvarchar(10)))
   Print @Id
   Set @Id = @Id + 1
End