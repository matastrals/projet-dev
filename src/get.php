<?php
$host = getenv('DB_HOST');
$database = getenv('DB_DATABASE');
$username = getenv('DB_USERNAME');
$password = getenv('DB_PASSWORD');

$conn = new mysqli($host, $username, $password, $database);

$sql = 'SELECT * FROM players WHERE username = ?';
$results = $conn->prepare($sql);
$results->bind_param('s', $_POST["name"]);
$results->execute();
$results = $results->get_result();

if ($results->num_rows === 0) {
  printf("No results for user %s", $_POST["name"]);
} else {
  foreach ($results as $row) {
      printf("User %s found! Email address: %s\n", $row["username"], $row["email"]);
  }
}
