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
<!DOCTYPE html>
<html lang="fr">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <a href="index.php" style="display: inline-block;
                                     padding: 10px 20px;
                                     background-color: #9f9f9f;
                                     color: white;
                                     text-decoration: none;
                                     border-radius: 5px;"
        >
            Retour
        </a>
  <title>Page de connexion</title>
  <link rel="stylesheet" href="style.css">
<body>

<div class="connexion">
<h2>Connexion</h2>
  <form action="get.php" method="post">
    E-mail: <input type="text" name="email"><br>
    Mot de passe: <input type="password" name="password"><br>
    <input type="submit" name="login" value="Se connecter">
  </form>
  </div>
</body>
</html>