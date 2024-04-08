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


<h2>Connexion</h2>
  <form action="get.php" method="post">
    E-mail: <input type="text" name="email"><br>
    Mot de passe: <input type="password" name="password"><br>
    <input type="submit" name="login" value="Se connecter">
  </form>

</body>
</html>