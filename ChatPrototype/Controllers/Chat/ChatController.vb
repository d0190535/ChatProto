Imports Microsoft.AspNet.SignalR
Imports Microsoft.AspNet.SignalR.Hub
Imports Microsoft.AspNet.SignalR.Hubs

Namespace ChatPrototype
    Public Class ChatController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Chat

        Function Index() As ActionResult

            Return View()
        End Function

    End Class

    <HubName("myHub")>
    Public Class myHub
        Inherits Hub

        Public Sub Sendwelcome()
            Clients.All.welcome()
        End Sub

        Public Sub Send(ByVal user As String, ByVal message As String)
            Dim text As String = HttpUtility.HtmlEncode(message)
            Clients.All.sendMessage(user, text)
        End Sub

    End Class

End Namespace