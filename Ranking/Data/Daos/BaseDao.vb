Imports DataLibrary.DAO.Specialized.SQL
Imports DataLibrary.Interfaces
Imports Ranking.Extensions
Imports Ranking.Interfaces

Namespace Data.Daos
    ''' <summary>
    ''' Classe abstrata que define a estrutura básica da entidade DAO
    ''' </summary>
    ''' <typeparam name="ItemType">Tipo de item</typeparam>
    ''' <typeparam name="DatabaseAdapterType">Tipo de adaptador de banco de dados</typeparam>
    Public MustInherit Class BaseDao(Of ItemType As {Class, New}, DatabaseAdapterType As {IDatabaseAdapter, New})
        Inherits SqlDao(Of ItemType, DatabaseAdapterType)
        Implements IDaoBase
        Protected Shared ReadOnly baseType As Type = GetType(ItemType)

        ''' <summary>
        ''' Lista todos os itens da fonte de dados na ordem definida pela subclasse
        ''' </summary>
        ''' <returns>Retorna um objeto do tipo <c>List(Of T)</c> contendo o resultado da consulta</returns>
        ''' <exception cref="InvalidCastException">Ocorre caso o tipo <c>T</c> seja incompatível com <c>ItemType</c></exception>
        Protected MustOverride Function ListAndSort() As List(Of ItemType)

        ''' <summary>
        ''' Cria um novo item na fonte de dados
        ''' </summary>
        ''' <typeparam name="T">Tipo do item que será passado como argumento</typeparam>
        ''' <param name="item">Item que deverá ser criado na fonte de dados</param>
        ''' <returns>Retorna true caso o item seja criado e false caso contrário</returns>
        ''' <exception cref="InvalidCastException">Ocorre caso o tipo <c>T</c> seja incompatível com <c>ItemType</c></exception>
        Public Overridable Function Create(Of T)(item As T) As Boolean Implements IDaoBase.Create
            Return Me.Insert((item.CastItem(Of T))) > 0
        End Function

        ''' <summary>
        ''' Lista todos os itens da fonte de dados na ordem padrão
        ''' </summary>
        ''' <typeparam name="T">Tipo de item que deverá ser devolvido</typeparam>
        ''' <returns>Retorna um objeto do tipo <c>List(Of T)</c> contendo o resultado da consulta</returns>
        ''' <exception cref="InvalidCastException">Ocorre caso o tipo <c>T</c> seja incompatível com <c>ItemType</c></exception>
        Public Overridable Function List(Of T)() As List(Of T) Implements IDaoBase.List
            Dim result = Me.Select(Of List(Of ItemType))().CastItem(Of List(Of T))
            Return result
        End Function

        ''' <summary>
        ''' Lista todos os itens da fonte de dados na ordem definida pela subclasse
        ''' </summary>
        ''' <typeparam name="T">Tipo de item que deverá ser devolvido</typeparam>
        ''' <returns>Retorna um objeto do tipo <c>List(Of T)</c> contendo o resultado da consulta</returns>
        ''' <exception cref="InvalidCastException">Ocorre caso o tipo <c>T</c> seja incompatível com <c>ItemType</c></exception>
        Public Overridable Function ListAndSort(Of T)() As List(Of T) Implements IDaoBase.ListAndSort
            Dim result = Me.ListAndSort().CastItem(Of List(Of T))
            Return result
        End Function

        ''' <summary>
        ''' Determina se o objeto DAO pode ou não tratar o tipo de dado identificado pelo argumento <c>itemType</c>
        ''' </summary>
        ''' <param name="itemType">Um objeto do tipo <c>Type</c> correspondente ao tipo de dado que será testado</param>
        ''' <returns>Retorna True se o tipo fornecido puder ser tratado e false caso contrário</returns>
        Public Overridable Function CanHandle(itemType As Type) As Boolean Implements IDaoBase.CanHandle
            Return itemType.IsAssignableFrom(baseType)
        End Function

        ''' <summary>
        ''' Lista todos os itens da fonte de dados na ordem definida pelo comparador fornecido
        ''' </summary>
        ''' <typeparam name="T">Tipo de item que deverá ser devolvido</typeparam>
        ''' <param name="comparer">Comparador que será usado para determinar a ordem dos itens</param>
        ''' <returns>Retorna um objeto do tipo <c>List(Of T)</c> contendo o resultado da consulta</returns>
        ''' <exception cref="InvalidCastException">Ocorre caso o tipo <c>T</c> seja incompatível com <c>ItemType</c></exception>
        Public Overridable Function List(Of T)(comparer As IComparer(Of T)) As List(Of T) Implements IDaoBase.List
            Dim result = Me.Select(Of List(Of ItemType))().CastItem(Of List(Of T))
            result.Sort(comparer)
            Return result
        End Function

        ''' <summary>
        ''' Limpa todos os registros da fonte de dados correspondentes ao tipo <c>ItemType</c>
        ''' </summary>
        Public Sub ClearData() Implements IDaoBase.ClearData
            MyBase.DeleteAll()
        End Sub
    End Class
End Namespace
