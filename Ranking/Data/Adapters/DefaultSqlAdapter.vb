Imports DataLibrary.DAO.Specialized.SQL

Namespace Data.Adapters
    ''' <summary>
    ''' Define um adaptador de fonte de dados que será usado para comunicação com o SQL Server
    ''' </summary>
    Public Class DefaultSqlAdapter
        Inherits SqlDatabaseAdapter
        Public Sub New()
            'O argumento deve ser igual ao nome da seção do arquivo Web.Config
            'onde estão as configurações de conexão
            MyBase.New("BancoSQLServer")
        End Sub
    End Class
End Namespace
