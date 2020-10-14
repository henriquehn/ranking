Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Ranking.Controllers
Imports Ranking.Data
Imports Ranking.DataModels

<TestClass()>
Public Class RankingControllerUnitTest
    Private randomGenerator As New Random()
    Private Const ElementCount As Integer = 10
    ''' <summary>
    ''' Cadastra alunos com pontuação aleatória entre 0 e 100
    ''' Antes de criar os novos elementos os elementos existentes serão apagados
    ''' Após criar os novos itens, eles serão lidos diretamente do banco de dados e
    ''' os elementos inseridos devem corresponder aos elementos lidos
    ''' A quantidade de elementos criada deve ser igual a ElementCount
    ''' </summary>
    <TestMethod()>
    Public Sub CreateRanking()
        'Cria os elementos de teste
        Dim testElements = GetTestElements()
        'Cria uma instância do controller
        Dim controller = New RankingController()
        'Esvazia o banco de dados
        DaoStore.ClearData(Of AlunoData)()
        'Envia os elementos de teste para o método Post
        For Each element In testElements
            controller.PostValue(element)
        Next
        'Lê os itens diretamente do banco de dados
        Dim result = DaoStore.List(Of AlunoData)
        'A quantidade de elementos de testes deve ser a mesma dos elementos lidos do banco
        Assert.AreEqual(testElements.Count, result.Count)
        'A quantidade de elementos lidos deve ser igual a ElementCount
        Assert.AreEqual(result.Count, ElementCount)
        'Percorre um a um os elementos de testes e os lidos do banco, comparando na mesma ordem
        For index = 0 To testElements.Count - 1
            'Os valores devem ser os mesmos em ambos os elementos
            Assert.AreEqual(testElements(index).Nome, result(index).Nome)
            Assert.AreEqual(testElements(index).Pontos, result(index).Pontos)
        Next
    End Sub

    ''' <summary>
    ''' Deve retornar todos os alunos ordenados pela pontuação do maior para o menor
    ''' Cada elemento deve ter o valor do campo Posicao correspondendo ao seu número ordinal na lista
    ''' </summary>
    <TestMethod()>
    Public Sub ListRanking()
        'Cria uma instância do controller
        Dim controller = New RankingController()
        'Chama o método GET
        Dim result = controller.GetValues()
        'O resultado não pode ser nulo
        Assert.IsNotNull(result)
        'O número de elementos lidos deve ser igual a ElementCount
        Assert.AreEqual(result.Count, ElementCount)
        'Cada elemento lido deve ter o atributo Posição igual à sua posição ordinal
        'Os pontos devem estar em ordem decrescente
        Dim lastValue = result(0).Pontos
        For index = 0 To result.Count - 1
            Assert.AreEqual(result(index).Posicao, index + 1)
            Assert.IsTrue(result(index).Pontos <= lastValue)
            lastValue = result(index).Pontos
        Next
    End Sub

    ''' <summary>
    ''' Cria uma lista preenchida com elementos gerados automaticamente
    ''' </summary>
    ''' <returns>Retorna um objeto List com elementos do tipo <c>RankingModel</c></returns>
    Private Function GetTestElements() As List(Of RankingModel)
        Dim testProducts As New List(Of RankingModel)
        For itemIndex As Integer = 1 To ElementCount
            testProducts.Add(New RankingModel With {.Nome = String.Format("Aluno{0}", itemIndex), .Pontos = randomGenerator.Next(100)})
        Next
        Return testProducts
    End Function
End Class