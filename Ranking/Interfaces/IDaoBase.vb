Namespace Interfaces
    ''' <summary>
    ''' Define uma interface padrão para todos os DAOs do sistema
    ''' </summary>
    Public Interface IDaoBase
        ''' <summary>
        ''' Cria um novo item na fonte de dados
        ''' </summary>
        ''' <typeparam name="T">Tipo do item</typeparam>
        ''' <param name="item">Item que será armazenado na fonte de dados</param>
        ''' <returns>Retorna true se o item for inserido com sucesso e false caso contrário</returns>
        ''' <exception cref="InvalidCastException">Ocorre caso o tipo <c>T</c> seja incompatível com o tipo de dado suportado pela instância concreta</exception>
        Function Create(Of T)(item As T) As Boolean
        ''' <summary>
        ''' Lista todos os itens da fonte de dados na ordem padrão
        ''' </summary>
        ''' <typeparam name="T">Tipo de item</typeparam>
        ''' <returns>Retorna uma lista de objetos do tipo <c>T</c></returns>
        ''' <exception cref="InvalidCastException">Ocorre caso o tipo <c>T</c> seja incompatível com o tipo de dado suportado pela instância concreta</exception>
        Function List(Of T)() As List(Of T)
        ''' <summary>
        ''' Lista todos os itens da fonte de dados na ordem especificada pelo comparador fornecido
        ''' </summary>
        ''' <typeparam name="T">Tipo de item</typeparam>
        ''' <param name="comparer"></param>
        ''' <returns>Retorna uma lista de objetos do tipo <c>T</c></returns>
        ''' <exception cref="InvalidCastException">Ocorre caso o tipo <c>T</c> seja incompatível com o tipo de dado suportado pela instância concreta</exception>
        Function List(Of T)(comparer As IComparer(Of T)) As List(Of T)
        ''' <summary>
        ''' Lista todos os itens da fonte de dados na ordem definida pela implementação de DAO
        ''' </summary>
        ''' <typeparam name="T">Tipo de item</typeparam>
        ''' <returns>Retorna uma lista de objetos do tipo <c>T</c></returns>
        ''' <exception cref="InvalidCastException">Ocorre caso o tipo <c>T</c> seja incompatível com o tipo de dado suportado pela instância concreta</exception>
        Function ListAndSort(Of T)() As List(Of T)
        ''' <summary>
        ''' Determina se o objeto DAO pode ou não tratar o tipo de dado identificado pelo argumento <c>itemType</c>
        ''' </summary>
        ''' <param name="itemType">Um objeto do tipo <c>Type</c> correspondente ao tipo de dado que será testado</param>
        ''' <returns>Retorna True se o tipo fornecido puder ser tratado e false caso contrário</returns>
        Function CanHandle(itemType As Type) As Boolean
        ''' <summary>
        ''' Limpa todos os registros da fonte de dados correspondentes ao tipo <c>ItemType</c>
        ''' </summary>
        Sub ClearData()
    End Interface
End Namespace
