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
  <h2>Inscription</h2>
  <form action="submit.php" method="post">
    Nom d'utilisateur: <input type="text" name="name"><br>
    E-mail: <input type="text" name="email"><br>
    Mot de passe: <input type="password" name="password"><br>
    <input type="submit" name="register" value="S'inscrire">
  </form>
</body>
</html>