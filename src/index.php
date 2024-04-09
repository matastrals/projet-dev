<?php
session_start();

// Vérifier si l'utilisateur est connecté
if (isset($_SESSION['logged_in']) && $_SESSION['logged_in'] === true) {
    $login_link = "connexion.php"; // Lien vers la page de déconnexion
    $login_text = "Se déconnecter";
} else {
    $login_link = "connexion.php"; // Lien vers la page de connexion
    $login_text = "Se connecter";
}

// Traitement du formulaire de connexion et d'inscription
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    if (isset($_POST['register'])) {
        // Inscription
        header("Location: submit.php");
        exit();
    } elseif (isset($_POST['login'])) {
        // Connexion
        header("Location: get.php");
        exit();
    }
}
?>

<!DOCTYPE html>
<html lang="fr">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Page d'accueil</title>
  <link rel="stylesheet" href="style.css">
</head>
<body>
  <div class='acceuil'>
    <h1>Page d'accueil</h1>
    <p>Bienvenue sur notre site!</p>
    <a href="<?php echo $login_link; ?>"><?php echo $login_text; ?></a> <!-- Afficher le lien de connexion/déconnexion -->
    <a href="inscription.php">S'inscrire</a>
    <a href="claim.php">Claim ton item !</a>
  </div>
</body>
</html>
