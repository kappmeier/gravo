''' <summary>
''' Single factory to generate DAO objects from passed database connections.
''' </summary>
Public Module DaoFactory

    Public Function Dictionary(ByVal db As IDataBaseOperation) as IDictionaryDao
        Return New DictionaryDao(db)
    End Function

    Public Function Groups(ByVal db As IDataBaseOperation) as IGroupsDao
        Return New GroupsDao(db)
    End Function

    Public Function Group(ByVal db As IDataBaseOperation) as IGroupDao
        Return New GroupDao(db)
    End Function

    Public Function Cards(ByVal db As IDataBaseOperation) as ICardsDao
        Return New CardsDao(db)
    End Function

    Public Function Management(ByVal db As IDataBaseOperation) as IManagementDao
        Return New ManagementDao(db)
    End Function

    Public Function Properties(ByVal db As IDataBaseOperation) as IPropertiesDao
           Return New PropertiesDao(db)
    End Function
End Module
