Imports System.Xml.Serialization
Imports Newtonsoft.Json

Namespace DataModels
    ''' <summary>
    ''' Define um modelo de memória para os elementos do hanking
    ''' </summary>
    Public Class RankingModel
        <JsonProperty(PropertyName:="posicao")>
        Public Property Posicao As Integer
        <JsonProperty(PropertyName:="nome")>
        Public Property Nome As String
        <JsonProperty(PropertyName:="pontos")>
        Public Property Pontos As Integer
    End Class
End Namespace
