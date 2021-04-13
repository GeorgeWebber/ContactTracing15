Create procedure spGetTestingCentreById
                                    @TestingCentreId int
                                    as
                                    Begin
                                        Select * from TestingCentres
                                        where TestingCentreId = @TestingCentreId
                                    End
