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

// Afficher les informations de l'utilisateur
echo "Bienvenue sur votre profil, $email !<br>";

// Lien pour se déconnecter
echo '<a href="connexion.php">Se déconnecter</a><br>';

echo '<a href="inscription.php">Inscription</a><br>';

echo '<a href="claim.php">Claim ton item !</a><br>';
