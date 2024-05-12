# Projet Dev

## Installation
En premier lieu, il faut installer le projet avec la commande :
```git clone https://github.com/matastrals/projet-dev```
Ensuite, installez vagrant (vous pouvez suivre ce lien https://developer.hashicorp.com/vagrant/install?product_intent=vagrant).
#### - Partie web
Lorsque vous êtes à la racine du projet, tapez la commande 
```cd web/``` puis ```vagrant up```. 
Ensuite, il faut revenir à la racine du projet avec ```cd ..``` et de la il faut faire :
```npm install express```
```npm install body-parser```
```npm install mysql```
Et pour finir ```node app.js``` pour lancer l'application.

#### - Partie gameprog

Il vous faut unity 2022.3.16f1. Il faut aussi unity hub.
De la vous pouvez ajouter un projet pour cela vous ouvrez unity hub, vous faites "Add" et vous sélectionnez le dossier "gameprog".
Ensuite vous lancez le projet.

## Description

Le principe est simple, vous avez un jeu vidéo qui est relier à un site web grâce à une api. Vous pouvez récupérer des objets sur le site et les avoirs dans le jeu. 
Vous pouvez vous inscrire sur le site et vous connectez.
Tandis que sur le jeu, vous pouvez héberger votre partie pour pouvoir communiquer via un chat dans le jeu avec vos amis dans le même réseau local.

## Comment faire

Vous pouvez désormais vous rendre sur le site à l'addresse suivante : http://10.5.1.11/
Vous pouvez aussi accéder à l'api à cette addresse :
http://127.0.0.1:3000/
Sur le site, vous pouvez vous inscrire et claim votre item.
Vous pouvez voir vos objets claim sur l'api : 
http://127.0.0.1:3000/api/rewards/"nom d'utilisateur"

Ensuite vous pouvez récupérez vos objets sur le jeu en entrant votre nom d'utilisateur dans la section prévu à cette effet et en cliquant sur "claim items". Les items se supprimeront de l'api après les avoirs claims.

Dans le jeu, vous avez la possibilité de communiquez avec vos amis avec un chat et pour se faire il faut qu'une machine héberge et que les autres s'y connecte :
 - La personne qui héberge le serveur doit mettre son ip et le port qu'elle souhaite utiliser. Il faut après cliquer sur "host".
 - Les personnes qui rejoignent doivent mettre l'ip de la machine qui héberge et son port. Ensuite, il faut cliquer sur "join".

Pour plus de simplicité, il est conseiller de désactiver le pare-feu pour ne pas avoir de problème réseau.
Ensuite, il suffit de lancer le jeu et vous pouvez communiquer.
Vous êtes obliger de mettre un pseudonyme pour lancer le jeu.

#### Les touches : 
z, q, s, d : Se déplacer
e : Intéragir
i : Ouvre l'inventaire
Entrée : Ouvre le chat
Échap : Ouvre le menu
Tab : Ouvre le tableau de score
