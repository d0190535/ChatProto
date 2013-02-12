@Code
    ViewData("Title") = "chat testing"
End Code

<script src="~/Scripts/jquery-1.9.1.js" type="text/javascript"></script>
<script src="~/Scripts/jquery.signalR-1.0.0-rc2.js" type="text/javascript"></script>
<script src="~/signalr/hubs" type="text/javascript"></script>

<label for="userid" >Your chat id:</label><input type="text" name="userid" id="userid" /><br />
<label for="message" >Your message:</label><input type="text" name="message" id="message" maxlength="100" />
<div id="chatWindow" style="width: 100%; height: 300px; overflow: scroll; border: 1px solid grey"></div>

<input type="button" id="broadcast" value="broadcast" />

<script type="text/javascript">
    $(function () {
        var myHub = $.connection.myHub;

        myHub.client.Add = function (msg) {
            alert("here");
        };

        $.connection.hub.logging = true
        $.connection.hub.start().done(function () {
            alert("Now connected!"); console.log("AHHHH");
        })
        .fail(function () { alert("Could not Connect!"); });

        $("#broadcast").click(function () {
            // Call the chat method on the server
            myHub.server.send("fuckyou");
        });
    });
</script>