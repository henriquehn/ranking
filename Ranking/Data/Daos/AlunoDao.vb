Imports Ranking.Data.Adapters
Imports Ranking.DataModels
Imports Ranking.DefaultComparers

Namespace Data.Daos
    ''' <summary>
    ''' Define uma entidade DAO especializada no tipo <c>AlunoData</c>
    ''' </summary>
    Public Class AlunoDao
        Inherits BaseDao(Of AlunoData, DefaultSqlAdapter)

        ''' <summary>
        ''' Obtém todos os elementos da fonte de dados
        ''' </summary>
        ''' <returns>Retorna uma lista de elementos do tipo <c>AlunoData</c></returns>
        Protected Overrides Function ListAndSort() As List(Of AlunoData)
            Dim result = Me.Select(Of List(Of AlunoData))()
            result.Sort(AlunoDefaultComparer.Instance)
            Return result
        End Function
    End Class
End Namespace
