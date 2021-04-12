Create procedure spGetTracingCentreById
                                    @TracingCentreId int
                                    as
                                    Begin
                                        Select * from TracingCentress
                                        where TracingCentreId = @TracingCentreId
                                    End
