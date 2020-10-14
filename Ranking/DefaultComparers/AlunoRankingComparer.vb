Imports Ranking.DataModels

Namespace DefaultComparers
    ''' <summary>
    ''' Singletron que implementa um comparador de alunos com base na pontuação
    ''' </summary>
    ''' <remarks>
    ''' O critério de comparação é a pontuação(em ordem decrescente) seguido pelo o nome (alfabeticamente)
    ''' Elementos não nulos têm precedência sobre elementos nulos
    ''' </remarks>
    Public Class AlunoRankingComparer
        Implements IComparer(Of AlunoData)
        Public Shared ReadOnly Instance As New AlunoRankingComparer

        Private Sub New()

        End Sub

        Public Function Compare(x As AlunoData, y As AlunoData) As Integer Implements IComparer(Of AlunoData).Compare
            Dim result As Integer = 0
            Select Case True
                Case x Is Nothing
                    result = If(y Is Nothing, 0, 1)
                Case y Is Nothing
                    result = -1
                Case x.Pontos Is Nothing
                    result = If(y.Pontos Is Nothing, 0, 1)
                Case y.Pontos Is Nothing
                    result = -1
                Case Else
                    result = -x.Pontos.Value.CompareTo(y.Pontos.Value)
                    If result = 0 Then
                        result = StringComparer.InvariantCultureIgnoreCase.Compare(x.Nome, y.Nome)
                    End If
            End Select
            Return result
        End Function
    End Class
End Namespace
