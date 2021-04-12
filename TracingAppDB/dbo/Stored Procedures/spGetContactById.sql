Create procedure spGetContactById
                                    @ContactId int
                                    as
                                    Begin
                                        Select * from Contacts
                                        where ContactId = @ContactId
                                    End
