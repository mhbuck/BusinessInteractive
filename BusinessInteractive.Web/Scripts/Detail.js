$(function () {
	// Proxy created on the fly
	var chat = $.connection.chatHub;

	
	// Declare a function on the chat hub so the server can invoke it
	chat.addMessage = function (message) {
		$('#messages').append('<li>' + message + '</li>');
	};

	chat.newPersonJoinedOrLeft = function (count)
	{
		$('#ChatWithOthers').toggleClass('btn-success', count > 1);
		if (count > 1) {
			$('#ChatWithOthers').text('Users (' + count + ') online');
		}
		else {
			$('#ChatWithOthers').text('No Other Users online');
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