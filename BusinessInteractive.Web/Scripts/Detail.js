$(function () {
	// Proxy created on the fly
	var chat = $.connection.chatHub;

	
	// Declare a function on the chat hub so the server can invoke it
	chat.addMessage = function (message) {
		$('#messages').append('<li>' + message + '</li>');
	};

	chat.newPersonJoined = function (personJoined)
	{
		if (personJoined !== $.connection.hub.id) {
			alert(personJoined);
		}
	}

	/*
	$("#broadcast").click(function () {
		// Call the chat method on the server
		chat.send($('#msg').val());
	});
	*/
	// Start the connection
	$.connection.hub.start(
		function () {
			var orderId = $('#OrderId').val();
			chat.startViewingOrder(orderId);
		});
});