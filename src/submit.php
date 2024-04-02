<?php
$host = getenv('DB_HOST');
$database = getenv('DB_DATABASE');
$username = getenv('DB_USERNAME');
$password = getenv('DB_PASSWORD');

// Connexion à la base de données
$conn = new mysqli($host, $username, $password, $database);

// Vérification de la connexion
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

// Préparation de la requête SQL d'insertion
$sql = 'INSERT INTO players (username, email) VALUES (?, ?)';
$stmt = $conn->prepare($sql);

// Liaison des valeurs aux paramètres de la requête
$stmt->bind_param('ss', $_POST["name"], $_POST["email"]);

// Exécution de la requête
if ($stmt->execute()) {
    echo "New record created successfully";
} else {
    echo "Error: " . $sql . "<br>" . $conn->error;
}

// Fermeture de la connexion
$conn->close();
