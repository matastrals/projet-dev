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
?>
<!DOCTYPE html>
<html lang="fr">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="style.css">
    <div class="retour">
        <a href="profile.php" style="display: inline-block;
                                     padding: 10px 20px;
                                     background-color: #9f9f9f;
                                     color: white;
                                     text-decoration: none;
                                     border-radius: 5px;"
        >
            Retour
        </a>
    <title>Récompenses disponibles</title>
    <!-- Ajoutez le lien vers votre fichier CSS -->
    <style>
        /* Ajoutez ici des styles CSS supplémentaires si nécessaire */
    </style>
</head>
<body>
    <div class="rewards-container">
        <h2>Récompenses disponibles :</h2>
        <!-- Boucle pour afficher les récompenses -->
        <?php
        // Récupère l'identifiant de l'utilisateur à partir de la session
        $email = $_SESSION['email'];

        // Affiche les récompenses disponibles pour l'utilisateur

        $sql = "SELECT * FROM rewards";
        $result = $conn->query($sql);



        if ($result->num_rows > 0) {
            while ($row = $result->fetch_assoc()) {
                echo '<form action="claim.php" method="post" class="reward-form">';
                echo '<p class="reward-name">Nom: ' . $row["reward_name"] . '</p>';
                echo '<p class="reward-amount">Montant: ' . $row["amount"] . ' points</p>';
                echo '<input type="hidden" name="reward_id" value="' . $row["id"] . '">';
                echo '<input type="submit" name="claim" value="Réclamer" class="claim-button">';
                echo '</form>';
            }
        } else {
            echo '<p class="no-rewards">Aucune récompense disponible pour le moment.</p>';
        }
        

            // Traitement de la réclamation de la récompense
    if ($_SERVER["REQUEST_METHOD"] == "POST" && isset($_POST['claim'])) {
        // Récupère l'identifiant de la récompense
        $reward_id = $_POST['reward_id'];

        // Vérifier si l'utilisateur a déjà réclamé cette récompense
        $sql_check_claim = "SELECT * FROM user_rewards WHERE user_id = (SELECT id FROM players WHERE email='$email') AND reward_id = '$reward_id'";
        $result_check_claim = $conn->query($sql_check_claim);
        if ($result_check_claim->num_rows > 0) {
            // L'utilisateur a déjà réclamé cette récompense
            echo "Vous avez déjà réclamé cette récompense.";
        } else {
            // Insérer la récompense réclamée dans la base de données
            $sql_claim = "INSERT INTO user_rewards (user_id, reward_id) VALUES ((SELECT id FROM players WHERE email='$email'), '$reward_id')";
            if ($conn->query($sql_claim) === TRUE) {
                echo "Récompense réclamée avec succès!";
            } else {
                echo "Erreur lors de la réclamation de la récompense: " . $conn->error;
            }
        }
    }
        $conn->close();
        ?>

    </div>

</body>

</html>