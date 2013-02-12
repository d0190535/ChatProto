@Code
    ViewData("Title") = "chat testing"
End Code

<script src="~/Scripts/jquery-1.9.1.js" type="text/javascript"></script>
<script src="~/Scripts/jquery.signalR-1.0.0-rc2.js" type="text/javascript"></script>
<script src="~/signalr/hubs" type="text/javascript"></script>
<link rel="Stylesheet" type="text/css" href="../../Scripts/bootstrap.css" />
<link rel="Stylesheet" type="text/css" href="../../Scripts/style.css" />

<label for="userid" >Your chat id:</label><input type="text" name="userid" id="userid" /><br />
<label for="message" >Your message:</label><input type="text" name="message" id="message" maxlength="100" />
<input type="button" id="broadcast" value="Send" />
<div id="chatWindow" style="width: 100%; height: 300px; overflow: scroll; border: 1px solid grey"></div>

<script type="text/javascript">
    $(function () {
        var myHub = $.connection.myHub;

        myHub.client.Add = function (user, msg) {
            var window = $("#chatWindow");
            window.html(window.html() + "<b>" + user + "</b>: " + msg + "<br />");
        };

        $.connection.hub.logging = true
        $.connection.hub.start().done(function () {
            alert("You are now connected and ready to chat!");
        })
        .fail(function () {
            alert("Could not Connect!");
        });

        $(document).keypress(function (e) {
            if (e.which == 13) {
                myHub.server.send($("#userid").val(), $("#message").val());
                $("#message").val("");
            }
        });

        $("#broadcast").click(function () {
            myHub.server.send($("#userid").val(), $("#message").val());
            $("#message").val("");
        });
    });
</script>