Imports Ranking.DataModels
Imports Ranking.DefaultComparers
Imports Ranking.Extensions
Imports Ranking.Interfaces

Namespace Data.Daos
    ''' <summary>
    ''' Define uma entidade DAO para tratamento do tipo <c>RankingModel</c>
    ''' </summary>
    Public Class RankingDao
        Implements IDaoBase
        Private Shared ReadOnly baseType As Type = GetType(RankingModel)

        ''' <summary>
        ''' Cria um novo item na fonte de dados
        ''' </summary>
        ''' <typeparam name="T">Tipo do item que será passado como argumento</typeparam>
        ''' <param name="item">Item que deverá ser criado na fonte de dados</param>
        ''' <returns>Retorna true caso o item seja criado e false caso contrário</returns>
        ''' <exception cref="InvalidCastException">Ocorre caso o tipo <c>T</c> seja incompatível com <c>RankingModel</c></exception>
        Public Function Create(Of T)(item As T) As Boolean Implements IDaoBase.Create
            Dim aluno = item.CastItem(Of RankingModel).AsAlunoData()
            Return DaoStore.Create(aluno)
        End Function

        ''' <summary>
        ''' Lista todos os itens da fonte de dados na ordem padrão
        ''' </summary>
        ''' <typeparam name="T">Tipo de item que deverá ser devolvido</typeparam>
        ''' <returns>Retorna um objeto do tipo <c>List(Of T)</c> contendo o resultado da consulta</returns>
        ''' <exception cref="InvalidCastException">Ocorre caso o tipo <c>T</c> seja incompatível com <c>RankingModel</c></exception>
        Public Function List(Of T)() As List(Of T) Implements IDaoBase.List
            Return ListAndSort(Of T)()
        End Function

        ''' <summary>
        ''' Lista todos os itens da fonte de dados na ordem definida pelo comparador fornecido
        ''' </summary>
        ''' <typeparam name="T">Tipo de item que deverá ser devolvido</typeparam>
        ''' <param name="comparer">Comparador que será usado para determinar a ordem dos itens</param>
        ''' <returns>Retorna um objeto do tipo <c>List(Of T)</c> contendo o resultado da consulta</returns>
        ''' <exception cref="InvalidCastException">Ocorre caso o tipo <c>T</c> seja incompatível com <c>RankingModel</c></exception>
        Public Function List(Of T)(comparer As IComparer(Of T)) As List(Of T) Implements IDaoBase.List
            Dim elements = DaoStore.List(Of AlunoData)
            Dim result = ConvertResult(Of T)(elements)
            result.Sort(comparer)
            SetPosition(result)
            Return result
        End Function

        ''' <summary>
        ''' Lista todos os itens da fonte de dados na definida por <c>AlunoRankingComparer</c>
        ''' </summary>
        ''' <typeparam name="T">Tipo de item que deverá ser devolvido</typeparam>
        ''' <returns>Retorna um objeto do tipo <c>List(Of T)</c> contendo o resultado da consulta</returns>
        ''' <exception cref="InvalidCastException">Ocorre caso o tipo <c>T</c> seja incompatível com <c>RankingModel</c></exception>
        Public Function ListAndSort(Of T)() As List(Of T) Implements IDaoBase.ListAndSort
            Dim elements = DaoStore.List(Of AlunoData)
            elements.Sort(AlunoRankingComparer.Instance)
            Dim newReult = ConvertResult(Of T)(elements)
            SetPosition(newReult)
            Return newReult
        End Function

        ''' <summary>
        ''' Define a propriedade Posicao do item confirme sua posição ordinal
        ''' </summary>
        ''' <typeparam name="T">Tipo de dados do item da lista</typeparam>
        ''' <param name="elements">Lista de objetos do tipo <c>T</c></param>
        Private Shared Sub SetPosition(Of T)(elements As List(Of T))
            Dim posicao As Integer = 1
            For Each element In elements
                element.CastItem(Of RankingModel).Posicao = posicao
                posicao += 1
            Next
        End Sub

        ''' <summary>
        ''' Converte uma lista de objetos do tipo <c>AlunoData</c> para uma lista do tipo T
        ''' </summary>
        ''' <typeparam name="T">Tipo dos elementos da lista de retorno</typeparam>
        ''' <param name="values">Lista de valores que serão convertidos</param>
        ''' <returns>Retorna uma lista de objetos do tipo <c>T</c></returns>
        ''' <exception cref="InvalidCastException">Ocorre caso o tipo <c>T</c> seja incompatível com <c>RankingModel</c></exception>
        Private Function ConvertResult(Of T)(values As List(Of AlunoData)) As List(Of T)
            Dim result As New List(Of RankingModel)
            For Each element In values
                result.Add(element.AsRankingModel())
            Next
            Return result.CastItem(Of List(Of T))
        End Function

        ''' <summary>
        ''' Determina se o objeto DAO pode ou não tratar o tipo de dado identificado pelo argumento <c>itemType</c>
        ''' </summary>
        ''' <param name="itemType">Um objeto do tipo <c>Type</c> correspondente ao tipo de dado que será testado</param>
        ''' <returns>Retorna True se o tipo fornecido puder ser tratado e false caso contrário</returns>
        Public Function CanHandle(itemType As Type) As Boolean Implements IDaoBase.CanHandle
            Return itemType.IsAssignableFrom(baseType)
        End Function

        ''' <summary>
        ''' Limpa todos os registros da fonte de dados correspondentes ao tipo <c>ItemType</c>
        ''' </summary>
        Public Sub ClearData() Implements IDaoBase.ClearData
            DaoStore.ClearData(Of AlunoData)()
        End Sub
    End Class
End Namespace
