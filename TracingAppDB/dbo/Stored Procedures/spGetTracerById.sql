Create procedure spGetTracerById
                                    @TracerId int
                                    as
                                    Begin
                                        Select * from Tracers
                                        where TracerId = @TracerId
                                    End
