<?php
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

<html>
<body>
  <h1>Page d'accueil</h1>
    <p>Bienvenue sur notre site!</p>
      <a href="connexion.php">Se connecter</a>
      <a href="inscription.php">S'inscrire</a>
      <a href="claim.php">Claim ton item !</a>
</body>
</html>