<?php
session_start();

// Détruire toutes les données de session
session_destroy();

// Rediriger vers la page de connexion ou une autre page de votre choix
header("Location: index.php");
exit();
