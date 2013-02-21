@Code
    ViewData("Title") = "awesome uChatt"
End Code
<title>@ViewBag.Title</title>

<script src="~/Scripts/jquery-1.9.1.js" type="text/javascript"></script>
<script src="~/Scripts/jquery.signalR-1.0.0-rc2.js" type="text/javascript"></script>
<script src="~/signalr/hubs" type="text/javascript"></script>
<link rel="stylesheet" type="text/css" href="~/Scripts/style.css" />
<link rel="Stylesheet" type="text/css" href="~/Scripts/bootstrap.css" />

<label for="userid" >Your chat ID:</label><input type="text" name="userid" id="userid" /><br />
<label for="message" >Your message:</label><input type="text" name="message" id="message" maxlength="100" />
<input type="button" id="broadcast" value="Send" />
<div id="chatWindow" style="width: 100%; height: 300px; overflow: scroll; border: 1px solid grey"></div>

<!-- client's side logic -->
<script type="text/javascript">
    $(function () {
        var myHub = $.connection.myHub;
        var window = $("#chatWindow");

        myHub.client.welcome = function () {
            window.html(window.html() + "<i><font color=grey>Welcome to uChatt ~~</font></i><br/>");
            window.scrollTop(window[0].scrollHeight);
        };

        myHub.client.sendMessage = function (user, message) {
            var currTime = new Date();
            window.html(window.html() + "<font color=grey>[" + currTime.getHours() + ":" + currTime.getMinutes() + "]</font>" + "<b><font color=blue>" + user + "</b></font>: " + message + "<br/>");
            window.scrollTop(window[0].scrollHeight);
        };

        myHub.client.joined = function (user) {
            var currTime = new Date();
            window.html(window.html() + "<i><font color=green>"+user+" has joined the chat</font></i><br/>");
            window.scrollTop(window[0].scrollHeight);
        };

        myHub.client.leave = function (user) {
            var currTime = new Date();
            window.html(window.html() + "<i><font color=red>" + user + " has left the chat</font></i><br/>");
            window.scrollTop(window[0].scrollHeight);
        };

        // enable console logging for hub
        $.connection.hub.logging = true
        $.connection.hub.start().done(function () {
            myHub.server.sendwelcome();
        })
        .fail(function () {
            alert("Could not Connect!");
        });

        // calling server side
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