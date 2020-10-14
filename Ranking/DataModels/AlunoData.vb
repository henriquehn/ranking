Imports DataLibrary.DAO.Attributes.ItemAttributes

Namespace DataModels
    ''' <summary>
    ''' Define um modelo de dados para manipulação da tabela Alunos
    ''' </summary>
    <SourceTable("Alunos", False)>
    Public Class AlunoData
        <FieldBehaviour(FieldIndexType.PrimaryIndex, FieldEditMode.AutoIncrementField)>
        Public Property ID As Integer?
        Public Property Nome As String
        Public Property Pontos As Integer?
    End Class
End Namespace
