$(function () {
	// Proxy created on the fly
	var chat = $.connection.chatHub;

	
	// Declare a function on the chat hub so the server can invoke it
	chat.addMessage = function (userName, message) {
		$('#messages').append('<li>' + userName + ' - ' + message + '</li>');
		$("#messages").prop({ scrollTop: $("#messages").prop("scrollHeight") });
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

	chat.controlChanged = function (controlId, value, connectionId, userName) {
		if ($.connection.hub.id != connectionId) {
			toastr.info('The control ' + controlId + ' has been changed by ' + userName + ' to be ' + value);
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

	$('#send-message').submit(function () {
		var command = $('#new-message').val();
		var orderId = $('#OrderId').val();
		chat.send(orderId, command);

		$('#new-message').val('');
		$('#new-message').focus();

		return false;
	});


	$('#new-message').val('');
	$('#new-message').focus();

	$('.notifyChange').on('change', function () {
		var orderId = $('#OrderId').val();

		chat.controlUpdated(orderId, $(this).prop('id'), $(this).val());
	});
});
