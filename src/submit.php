<?php
usleep(50);

// Redirection vers une autre page
header("Location: index.php");

$host = getenv('DB_HOST');
$database = getenv('DB_DATABASE');
$username = getenv('DB_USERNAME');
$password = getenv('DB_PASSWORD');

// Connexion à la base de données
$conn = new mysqli($host, $username, $password, $database);

// Vérification de la connexion
if ($conn->connect_error) {
    die("La connexion a échoué : " . $conn->connect_error);
}

// Préparation de la requête SQL d'insertion
$sql = 'INSERT INTO players (username, email, password) VALUES (?, ?, ?)';
$stmt = $conn->prepare($sql);

// Liaison des valeurs aux paramètres de la requête
$stmt->bind_param('sss', $_POST["name"], $_POST["email"], $_POST["password"]);

// Exécution de la requête
if ($stmt->execute()) {
    echo "Nouvel enregistrement créé avec succès";
} else {
    echo "Erreur : " . $sql . "<br>" . $conn->error;
}

$conn->close();
