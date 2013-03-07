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

        'dictionary structure to store connected users list (this is what we will use in the end)
        'Private Shared ReadOnly usersDictionary As Dictionary(Of String, String) = New Dictionary(Of String, String)

        'using list for testing .. for now
        Private Shared usersList As List(Of String) = New List(Of String)

        'on connected event
        Public Overrides Function OnConnected() As Threading.Tasks.Task
            usersList.Add(Context.ConnectionId)
            Return Clients.All.joined(Context.ConnectionId)
        End Function

        'on disconnected event
        Public Overrides Function OnDisconnected() As Threading.Tasks.Task
            usersList.Remove(Context.ConnectionId)
            Return Clients.All.leave(Context.ConnectionId)
        End Function

        'update current connected clients list
        Public Sub Updateusers()
            Clients.All.getUsers(getListString(usersList))
        End Sub

        Public Sub Sendwelcome()
            Clients.Caller.welcome()
        End Sub

        Public Sub Send(ByVal user As String, ByVal message As String)
            Clients.All.sendMessage(HttpUtility.HtmlEncode(user), HttpUtility.HtmlEncode(message))
        End Sub

        Public Function getListString(ByVal usersList As List(Of String)) As String
            Dim builder As New StringBuilder

            builder.Append("<p><font color=blue>")

            Dim i As Integer
            For i = 0 To usersList.Count - 1
                builder.Append(usersList.Item(i)).Append("<hr>")
            Next i

            builder.Append("</font>")
            Return builder.ToString

        End Function

    End Class

End Namespace