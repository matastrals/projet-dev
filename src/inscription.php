<?php
// Vérification si le formulaire est soumis
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    // Vérification si le bouton d'inscription est soumis
    if (isset($_POST['register'])) {
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

        // Préparation de la requête SQL d'insertion
        $sql = 'INSERT INTO players (username, email, password) VALUES (?, ?, ?)';
        $stmt = $conn->prepare($sql);

        // Liaison des valeurs aux paramètres de la requête
        $stmt->bind_param('sss', $_POST["name"], $_POST["email"], $_POST["password"]);

        // Exécution de la requête
        if ($stmt->execute()) {
            // Démarrage de la session et définir une variable de session pour l'email de l'utilisateur
            session_start();
            $_SESSION['email'] = $_POST['email'];

            // Redirection vers la page de profil
            header("Location: profile.php");
            exit();
        } else {
            echo "Erreur : " . $sql . "<br>" . $conn->error;
        }

        $conn->close();
    }
}
?>

<!DOCTYPE html>
<html lang="fr">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Page d'inscription</title>
  <link rel="stylesheet" href="style.css">
         <a href="index.php" style="display: inline-block;
                                     padding: 10px 20px;
                                     background-color: #9f9f9f;
                                     color: white;
                                     text-decoration: none;
                                     border-radius: 5px;"
        >
            Retour
        </a>
</head>
<body>
  <div class="inscription">
    <h2>Inscription</h2>
    <form action="<?php echo $_SERVER['PHP_SELF']; ?>" method="post">
      Nom d'utilisateur: <input type="text" name="name"><br>
      E-mail: <input type="text" name="email"><br>
      Mot de passe: <input type="password" name="password"><br>
      <input type="submit" name="register" value="S'inscrire">
    </form>
  </div>
</body>
</html>
