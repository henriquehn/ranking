Imports System.Web.Http
Imports Ranking.Data
Imports Ranking.DataModels
Imports Ranking.Extensions

Namespace Controllers
    Public Class RankingController
        Inherits ApiController

        ' GET: api/Jogador
        Public Function GetValues() As IEnumerable(Of RankingModel)
            Try
                Return DaoStore.ListAndSort(Of RankingModel)
            Catch ex As NotSupportedException
                Throw
            Catch ex As Exception
                Throw New Exception("Operação inválida")
            End Try
        End Function

        ' POST: api/Jogador
        Public Sub PostValue(<FromBody()> ByVal value As RankingModel)
            Try
                If Not DaoStore.Create(value) Then
                    Throw New Exception("Operação inválida")
                End If
            Catch ex As NotSupportedException
                Throw
            Catch ex As Exception
                Throw New Exception("Operação inválida")
            End Try
        End Sub
    End Class
End Namespace