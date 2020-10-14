Imports Ranking.Data.Daos
Imports Ranking.DataModels
Imports Ranking.Interfaces

Namespace Data
    ''' <summary>
    ''' Entidade responsável por instanciar e manter os DAOs disponíveis o sistema
    ''' </summary>
    Public Class DaoStore
        Private Shared ReadOnly Daos As New Dictionary(Of Type, IDaoBase)

        ''' <summary>
        ''' Instancia todos os DAOs suportados
        ''' </summary>
        Shared Sub New()
            Daos.Add(GetType(AlunoData), New AlunoDao)
            Daos.Add(GetType(RankingModel), New RankingDao)
        End Sub

        ''' <summary>
        ''' Retorna um DAO compatível com o tipo passado como argumento
        ''' </summary>
        ''' <typeparam name="T">Tipo de dado com o qual o DAO deverá ser compatível</typeparam>
        ''' <returns>Retorna um objeto que implementa a interface <c>IDaoBase</c></returns>
        ''' <exception cref="NotSupportedException">Ocorre quando não existe um DAO compatível com o tipo de dado fornecido</exception>
        Private Shared Function ForType(Of T)() As IDaoBase
            Dim daoType As Type = GetType(T)
            If Daos.ContainsKey(daoType) Then
                Dim result = Daos(daoType)

                If result.CanHandle(daoType) Then
                    Return result
                Else
                    Throw New NotSupportedException(String.Format("Não existe nenhum DAO com suporte para o tipo de dados {0}", daoType.Name))
                End If
            Else
                Throw New NotSupportedException(String.Format("Não existe nenhum DAO com suporte para o tipo de dados {0}", daoType.Name))
            End If
        End Function

        ''' <summary>
        ''' Insere o objeto <c>item</c> fornecido na fonte de dados
        ''' </summary>
        ''' <typeparam name="T">Tipo de dado do objeto que será criado</typeparam>
        ''' <param name="item">Item que será inserido</param>
        ''' <returns>Retorna true se o item for inserido na fonte de dados e false caso contrário</returns>
        ''' <exception cref="NotSupportedException">Ocorre quando não existe um DAO compatível com o tipo de dado fornecido</exception>
        Public Shared Function Create(Of T)(item As T) As Boolean
            Dim currentDao = ForType(Of T)()
            Return currentDao.Create(item)
        End Function

        ''' <summary>
        ''' Lista todos os elementos do tipo <c>T</c> a partir da fonte de dados na ordem padrão definida pelo DAO
        ''' </summary>
        ''' <typeparam name="T">Tipo de dado do objeto que será devolvido</typeparam>
        ''' <returns>Retorna uma lista de itens do tipo <c>T</c></returns>
        ''' <exception cref="NotSupportedException">Ocorre quando não existe um DAO compatível com o tipo de dado fornecido</exception>
        Public Shared Function List(Of T)() As List(Of T)
            Dim currentDao = ForType(Of T)()
            Return currentDao.List(Of T)
        End Function

        ''' <summary>
        ''' Lista todos os elementos do tipo <c>T</c> a partir da fonte de dados em uma ordem específica, confirme o DAO utilizado
        ''' </summary>
        ''' <typeparam name="T">Tipo de dado do objeto que será devolvido</typeparam>
        ''' <returns>Retorna uma lista de itens do tipo <c>T</c></returns>
        ''' <exception cref="NotSupportedException">Ocorre quando não existe um DAO compatível com o tipo de dado fornecido</exception>
        Public Shared Function ListAndSort(Of T)() As List(Of T)
            Dim currentDao = ForType(Of T)()
            Return currentDao.ListAndSort(Of T)
        End Function

        ''' <summary>
        ''' Método criado apenas para a propósito de testes
        ''' </summary>
        Public Shared Sub ClearData(Of T)()
            Dim currentDao = ForType(Of T)()
            currentDao.ClearData()
        End Sub
    End Class
End Namespace

