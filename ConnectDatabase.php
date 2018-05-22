<?php
	$servername = "localhost";
	$username = "vhost67430s1";
	$password = "2qFNxnT";
	$dbname = "vhost67430s1";
	// Create connection
	$conn = new mysqli($servername, $username, $password, $dbname);

	// Check connection
	if ($conn->connect_error) {
		die("Connection failed: " . $conn->connect_error);
	}
	
?>