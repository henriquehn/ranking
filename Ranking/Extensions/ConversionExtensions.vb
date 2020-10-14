Imports System.Runtime.CompilerServices
Imports Ranking.DataModels

Namespace Extensions
    Public Module ConversionExtensions
        <Extension>
        Public Function CastItem(Of T)(item As Object) As T
            Return DirectCast(item, T)
        End Function

        <Extension>
        Public Function AsAlunoData(item As RankingModel) As AlunoData
            Dim result As New AlunoData With {.Nome = item?.Nome, .Pontos = item?.Pontos}
            Return result
        End Function

        <Extension>
        Public Function AsRankingModel(item As AlunoData) As RankingModel
            Dim result As New RankingModel With {.Nome = If(item?.Nome, ""), .Pontos = If(item?.Pontos, 0)}
            Return result
        End Function
    End Module
End Namespace
