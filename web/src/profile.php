<?php
session_start();

// Vérifier si l'utilisateur est connecté
if (!isset($_SESSION['email'])) {
    // Rediriger vers la page de connexion si l'utilisateur n'est pas connecté
    header("Location: login.php");
    exit();
}

// Récupérer les informations de l'utilisateur à partir de la session
$email = $_SESSION['email'];
?>

<!DOCTYPE html>
<html lang="fr">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="style.css">
    <title>Votre Profil</title>
</head>
<body>
    <div class='acceuil'>
    <h1>Bienvenue sur votre profil, <?php echo $email; ?>!</h1>
    <!-- Liens pour se déconnecter, s'inscrire et réclamer un item -->
    <a href="logout.php">Se déconnecter</a><br>
    <a href="claim.php">Claim ton item !</a><br>
    </div>
</body>
</html>