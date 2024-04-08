<?php
session_start();

// Vérifie si l'utilisateur est connecté
if (!isset($_SESSION['email'])) {
    // Redirige vers la page de connexion si l'utilisateur n'est pas connecté
    header("Location: connexion.php");
    exit();
}

// Paramètres de connexion à la base de données
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

// Récupère l'identifiant de l'utilisateur à partir de la session
$email = $_SESSION['email'];

// Affiche les récompenses disponibles pour l'utilisateur
echo "Récompenses disponibles :<br>";

$sql = "SELECT * FROM rewards";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    while ($row = $result->fetch_assoc()) {
        echo "Nom: " . $row["reward_name"] . " - Montant: " . $row["amount"] . " points ";
        echo '<form action="claim.php" method="post">';
        echo '<input type="hidden" name="reward_id" value="' . $row["id"] . '">';
        echo '<input type="submit" name="claim" value="Réclamer">';
        echo '</form>';
    }
} else {
    echo "Aucune récompense disponible pour le moment.";
}

// Traitement de la réclamation de la récompense
if ($_SERVER["REQUEST_METHOD"] == "POST" && isset($_POST['claim'])) {
    // Récupère l'identifiant de la récompense
    $reward_id = $_POST['reward_id'];

    // Insère la récompense réclamée dans la base de données
    $sql_claim = "INSERT INTO user_rewards (user_id, reward_id) VALUES ((SELECT id FROM players WHERE email='$email'), '$reward_id')";
    if ($conn->query($sql_claim) === TRUE) {
        echo "Récompense réclamée avec succès!";
    } else {
        echo "Erreur lors de la réclamation de la récompense: " . $conn->error;
    }
}

// Ferme la connexion à la base de données
$conn->close();
?>
