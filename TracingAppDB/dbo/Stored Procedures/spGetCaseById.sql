Create procedure spGetCaseById
                                    @CaseId int
                                    as
                                    Begin
                                        Select * from Cases
                                        where CaseId = @CaseId
                                    End
