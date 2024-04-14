#!/bin/bash

if [[ $(id -u) -ne 0 ]] ; then
  echo "nop"
  exit 1
fi

# fw handling
systemctl enable firewalld
systemctl start firewalld
firewall-cmd --zone=public --remove-service=dhcpv6-client
firewall-cmd --zone=public --remove-service=cockpit
firewall-cmd --zone=public --remove-service=ssh
firewall-cmd --zone=public --add-service=ssh
firewall-cmd --zone=public --add-service=http
firewall-cmd --zone=public --add-port=3306/tcp
firewall-cmd --runtime-to-permanent
firewall-cmd --reload

#mariaDB
dnf install -y mariadb-server
systemctl enable --now mariadb

echo -e "\n[mysqld]" >> /etc/my.cnf.d/mariadb-server.cnf
echo "server-id=1" >> /etc/my.cnf.d/mariadb-server.cnf
echo "log-bin=mysql-bin" >> /etc/my.cnf.d/mariadb-server.cnf

mysql -u root <<EOF
CREATE DATABASE app_nulle;
CREATE USER 'db1'@'10.5.1.11' IDENTIFIED BY 'db1';
GRANT ALL PRIVILEGES ON app_nulle.* TO 'db1'@'10.5.1.11';
CREATE USER 'db1'@'%' IDENTIFIED BY 'db1';
GRANT ALL PRIVILEGES ON app_nulle.* TO 'db1'@'%';
FLUSH PRIVILEGES;

USE app_nulle;

CREATE TABLE players 
  (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL
  );

INSERT INTO players (username, email, password) VALUES
('joueur1', 'joueur1@example.com', 'joueur'),
('joueur2', 'joueur2@example.com', 'joueur');

CREATE TABLE rewards 
  (
    id INT AUTO_INCREMENT PRIMARY KEY,
    reward_name VARCHAR(100) NOT NULL,
    amount INT NOT NULL
  );

CREATE TABLE user_rewards (
    id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT NOT NULL,
    reward_id INT NOT NULL,
    claim_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES players(id),
    FOREIGN KEY (reward_id) REFERENCES rewards(id)
);


INSERT INTO rewards (reward_name, amount) VALUES
('botte du pourfendeur de feu', 100),
('armure du bourrin au cote amortie', 50),
('sabre mal programmer', 200);
EOF

systemctl restart mariadb