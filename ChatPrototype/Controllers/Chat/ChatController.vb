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

        Public Overrides Function OnConnected() As Threading.Tasks.Task
            Return Clients.All.joined(Context.ConnectionId)
        End Function

        Public Overrides Function OnDisconnected() As Threading.Tasks.Task
            Return Clients.All.leave(Context.ConnectionId)
        End Function

        Public Sub Sendwelcome()
            Clients.Caller.welcome()
        End Sub

        Public Sub Send(ByVal user As String, ByVal message As String)
            Clients.All.sendMessage(HttpUtility.HtmlEncode(user), HttpUtility.HtmlEncode(message))
        End Sub

    End Class

End Namespace