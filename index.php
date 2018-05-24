<?php
require 'ConnectDatabase.php';

$UsernameExist = false;
if (isset($_POST['UserReg']) && isset($_POST['PassReg'])){
//Registreeri
	$User = $_POST['UserReg'];
	$Pass = password_hash($_POST['PassReg'], PASSWORD_DEFAULT);
	
	$sql = "SELECT Id, User, Pass FROM Account";
	$result = $conn->query($sql);
	if ($result->num_rows > 0) {
		while($row = $result->fetch_assoc()) {
			if($User == $row["User"]){
				$UsernameExist = true;
			}
		}
	}
	if($UsernameExist)
		echo "Kasutaja $User on juba olemas";
	else
	{
		$sql = "INSERT INTO Account (User, Pass) VALUES ('$User','$Pass')";

		if ($conn->query($sql) === TRUE) {
			echo "Correct";
		} else {
			echo "Error: " . $sql . "<br>" . $conn->error;
		}
		
	}
}
else if (isset($_POST['UserLogin']) && isset($_POST['PassLogin'])){
//Logi sisse
	$User = $_POST['UserLogin'];
	$Pass = $_POST['PassLogin'];
	$PasswordVerfy = false;
	
	$sql = "SELECT Id, User, Pass FROM Account";
	$result = $conn->query($sql);
	if ($result->num_rows > 0) {
		while($row = $result->fetch_assoc()) {
			if($User == $row["User"]){
				if(password_verify($Pass,$row["Pass"]))
					$PasswordVerfy = true;
			}
		}
	}
	if($PasswordVerfy)
		echo "Correct";
	else
		echo "Kasutaja nimi või parool on vale";
}
else if (isset($_POST['AccountName'])){
//Get Account id
	$sql = "SELECT Id, User FROM Account";
	$result = $conn->query($sql);
	if ($result->num_rows > 0) {
		while($row = $result->fetch_assoc()) {
			if($_POST['AccountName'] == $row["User"]){
				echo $row["Id"];					
			}
		}
	}
}
else if (isset($_POST['AccountID']) && isset($_POST['Name']) && isset($_POST['MaleFamale']) && isset($_POST['Age']) && isset($_POST['Description'])){
//AccountSettings
	$AccountID = $_POST['AccountID'];
	$Name = $_POST['Name'];
	$MaleFamale = $_POST['MaleFamale'];
	$Age = $_POST['Age'];
	$Description = $_POST['Description'];
	
	$sql = "
	INSERT INTO Account_Settings 
			(Account_id, Nickname, MaleFamale, Age, Description) 
	VALUES ('$AccountID','$Name','$MaleFamale','$Age','$Description')";
	
	if ($conn->query($sql) === TRUE) {
			echo "Correct";
		} else {
			echo "Error: " . $sql . "<br>" . $conn->error;
		}
	
}
else if (isset($_POST['AccountID']) && isset($_POST['Latitude']) && isset($_POST['Longitude'])){
//AccountLocation
$AccountID = $_POST['AccountID'];
$Latitude = $_POST['Latitude'];
$Longitude = $_POST['Longitude'];
$IdRow;
	$sql = "
			SELECT Account_id
			FROM Account_Location";
			
	$result = $conn->query($sql);
	
	if ($result->num_rows > 0) 
	{ //check row exist or not
		$i = 0;
		while($row = $result->fetch_assoc()) 
		{
			$IdRow[$i] = $row["Account_id"];
			$i++;
		}
	}
	else 
	  echo "0 results ". $conn->error;
  
	if(in_array($AccountID, $IdRow))
	{ // Update row if exist
		$sql = "UPDATE Account_Location
				SET Latitude = '$Latitude', Longitude = '$Longitude' 
				WHERE Account_id = $AccountID";
		if ($conn->query($sql) === TRUE) 
			echo "Correct";
		else
			echo "Error: " . $sql . "<br>" . $conn->error;
	}
	else
	{ // insert new row
		$sql = "
		INSERT INTO Account_Location
				(Account_id, Latitude, Longitude) 
		VALUES ('$AccountID','$Latitude','$Longitude')";
		
		if ($conn->query($sql) === TRUE) 
			echo "Correct";
		else
			echo "Error: " . $sql . "<br>" . $conn->error;
	}
}
else if (isset($_POST['AccountID'])){
//AccountSettingsView
	$AccountID = $_POST['AccountID'];
	$sql = "SELECT Account.User, Account_Settings.Nickname, Account_Settings.MaleFamale,
				   Account_Settings.Age ,Account_Settings.Description
			FROM   Account, Account_Settings
			WHERE  Account.Id=$AccountID AND Account_Settings.Account_id=$AccountID";
			
	$result = $conn->query($sql);
	if ($result->num_rows > 0) 
		{
			// output data of each row
			while($row = $result->fetch_assoc()) 
			{
				echo "".$row["User"]."╙". $row["Nickname"]."♂". $row["MaleFamale"]."┼".$row["Age"]."╗".$row["Description"]."╒";
			}
		}
		else 
			echo "0 results ". $conn->error;
}
else if(isset($_POST['GetAllAccLocation'])){
//Get all accounts location
	if($_POST['GetAllAccLocation'])
	{
		$sql = "SELECT Account.User, Account_Location.Latitude, Account_Location.Longitude
				FROM Account, Account_Location
				WHERE Account.Id=Account_Location.Account_id";
		$result = $conn->query($sql);

		if ($result->num_rows > 0) 
		{
			// output data of each row
			while($row = $result->fetch_assoc()) 
			{
				echo "".$row["User"]."╙". $row["Latitude"]."♂". $row["Longitude"]."┼╗";
			}
		}
		else 
			echo "0 results ". $conn->error;;
	}
}
else{
	/*
	//ALL ACCOUNTS
	$sql = "SELECT Id, User, Pass FROM Account";
	$result = $conn->query($sql);

	if ($result->num_rows > 0) {
		// output data of each row
		while($row = $result->fetch_assoc()) {
			echo "Id: " . $row["Id"]. " - Name: " . $row["User"]. " " . $row["Pass"]. "<br>";
		}
	} else {
		echo "0 results";
	}
	*/
		
	/*
	// ALL ACCOUNTs + ACCOUNT SETTINGS result
	$sql = "SELECT Account.User, Account_Settings.Nickname 
			FROM Account, Account_Settings
			WHERE Account.Id=Account_Settings.Account_id";
	$result = $conn->query($sql);

	if ($result->num_rows > 0) {
		// output data of each row
		while($row = $result->fetch_assoc()) {
			echo "User: " . $row["User"]. " ----- Name: " . $row["Nickname"]. "<br>";
		}
	} else {
		echo "0 results ". $conn->error;;
	}
	*/

}
$conn->close();
//tomorrow > tomorrow
// Save geoloc with all data. +
// When click on geoloc show account id and all data. +

//tomorrow > tomorrow > tomorrow
// When press on chat. ->>
// Database create table with (you id + He id) in string. ->>
// Like '2'+ '4' = 24 = Chat_24. all chat info goes into this table
?>
