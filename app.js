const express = require('express');
const bodyParser = require('body-parser');
const mysql = require('mysql');

const app = express();
const port = 3000;

// Configuration de la connexion à la base de données MariaDB
const db = mysql.createConnection({
    host: '10.5.1.211', // Remplacez par l'adresse IP de votre VM MariaDB
    user: 'db1',
    password: 'db1',
    database: 'app_nulle'
});

// Connexion à la base de données
db.connect((err) => {
    if (err) {
        console.error('Erreur lors de la connexion à la base de données :', err.message);
    } else {
        console.log('Connexion à la base de données MariaDB réussie');
    }
});

// Middleware pour analyser les corps des requêtes en JSON
app.use(bodyParser.json());

app.get('/', (req, res) => {
    res.send('Bienvenue sur l\'API de votre application !');
});

// Endpoint pour récupérer les récompenses pour un joueur donné
app.get('/api/rewards/:username', (req, res) => {
    const username = req.params.username;

    const query = `
      SELECT r.*
      FROM rewards r
      INNER JOIN user_rewards ur ON r.id = ur.reward_id
      INNER JOIN players p ON ur.user_id = p.id
      WHERE p.username = ?;   
  `;

    db.query(query, [username], (err, rows) => {
        if (err) {
            console.error('Erreur lors de l\'exécution de la requête SQL :', err.message);
            res.status(500).send('Erreur lors de la récupération des récompenses');
            return;
        }

        if (rows.length > 0) {
            res.json(rows);
        } else {
            res.status(404).send('Aucune récompense trouvée pour ce joueur');
        }
    });
});

// Endpoint pour marquer une récompense comme récupérée
app.post('/api/rewards/claim/:username', (req, res) => {
    const username = req.params.username;

    // SQL pour mettre à jour la récompense comme récupérée pour le joueur spécifié
    const updateQuery = `
        UPDATE user_rewards
        SET claim_date = CURRENT_TIMESTAMP
        WHERE user_id = (
            SELECT id FROM players
            WHERE username = ?
        )
        AND claim_date IS NULL;`;

    // Exécutez la requête SQL
    db.query(updateQuery, [username], (error, results, fields) => {
        if (error) {
            console.error("Erreur lors de la mise à jour de la récompense:", error);
            res.status(500).json({ error: "Erreur lors de la mise à jour de la récompense" });
        } else {
            console.log("Récompense réclamée avec succès pour", username);
            res.status(200).json({ message: "Récompense réclamée avec succès pour " + username });
        }
    });
});

// Endpoint pour supprimer une récompense de la base de données
app.delete('/api/rewards/delete/:username', (req, res) => {
    const username = req.params.username;

    // SQL pour supprimer la récompense de la base de données pour le joueur spécifié
    const deleteQuery = `
        DELETE FROM user_rewards
        WHERE user_id = (
            SELECT id FROM players
            WHERE username = ?
        );`;

    // Exécutez la requête SQL
    db.query(deleteQuery, [username], (error, results, fields) => {
        if (error) {
            console.error("Erreur lors de la suppression de la récompense:", error);
            res.status(500).json({ error: "Erreur lors de la suppression de la récompense" });
        } else {
            console.log("Récompense supprimée avec succès pour", username);
            res.status(200).json({ message: "Récompense supprimée avec succès pour " + username });
        }
    });
});

// Écoute du port
app.listen(port, () => {
    console.log(`Server running on port ${port}`);
});