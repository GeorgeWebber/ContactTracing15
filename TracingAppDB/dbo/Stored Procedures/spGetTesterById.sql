Create procedure spGetTesterById
                                    @TesterId int
                                    as
                                    Begin
                                        Select * from Testers
                                        where TesterId = @TesterId
                                    End
