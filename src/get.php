<?php
session_start();

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

if ($_SERVER["REQUEST_METHOD"] == "POST" && isset($_POST['login'])) {
    // Récupération des données du formulaire
    $email = $_POST['email'];
    $password = $_POST['password'];

    // Préparation de la requête SQL pour vérifier l'utilisateur
    $sql = 'SELECT * FROM players WHERE email = ? AND password = ?';
    $stmt = $conn->prepare($sql);

    // Liaison des valeurs aux paramètres de la requête
    $stmt->bind_param('ss', $email, $password);

    // Exécution de la requête
    $stmt->execute();

    // Récupération du résultat
    $result = $stmt->get_result();

    // Vérification si l'utilisateur existe
    if ($result->num_rows > 0) {
        // Utilisateur trouvé, démarrer la session et stocker les informations de l'utilisateur
        $_SESSION['email'] = $email;
        // Redirection vers une page de profil ou une autre page appropriée
        header("Location: profile.php");
        exit();
    } else {
        // Utilisateur non trouvé, afficher un message d'erreur ou rediriger vers la page de connexion
        echo "Aucun utilisateur trouvé avec ces identifiants.";
    }
}

// Fermeture de la connexion
$conn->close();
