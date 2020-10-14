Imports Ranking.DataModels

Namespace DefaultComparers
    ''' <summary>
    ''' Singletron que implementa um comparador padrão de alunos
    ''' </summary>
    ''' <remarks>
    ''' O critério de comparação é o nome (alfabeticamente) seguido pelos pontos (em ordem crescente)
    ''' Elementos nulos têm precedência sobre elementos não nulos
    ''' </remarks>
    Public Class AlunoDefaultComparer
        Implements IComparer(Of AlunoData)
        Public Shared ReadOnly Instance As New AlunoDefaultComparer

        Private Sub New()

        End Sub

        Public Function Compare(x As AlunoData, y As AlunoData) As Integer Implements IComparer(Of AlunoData).Compare
            Dim result As Integer = 0
            Select Case True
                Case x Is Nothing
                    result = If(y Is Nothing, 0, -1)
                Case y Is Nothing
                    result = 1
                Case x.Nome Is Nothing
                    result = If(y.Nome Is Nothing, 0, -1)
                Case y.Nome Is Nothing
                    result = 1
                Case Else
                    result = StringComparer.InvariantCultureIgnoreCase.Compare(x.Nome, y.Nome)
                    If result = 0 Then
                        Select Case True
                            Case x.Pontos Is Nothing
                                result = If(y.Pontos Is Nothing, 0, -1)
                            Case y.Pontos Is Nothing
                                result = 1
                            Case Else
                                result = x.Pontos.Value.CompareTo(y.Pontos.Value)
                        End Select
                    End If
            End Select
            Return result
        End Function
    End Class
End Namespace
