# Tennis Stats API - Technical Test L'Atelier

Cette API simple permet de gérer et de consulter les statistiques des joueurs de tennis. Elle a été conçue en suivant une approche **Clean Architecture** simplifiée pour garantir la lisibilité, la testabilité et la maintenabilité.

## 🏗️ Architecture du Projet

Le code est organisé dans le dossier `src/` selon les couches suivantes :

- **TennisStats.Domain** : Le cœur de l'application (Entités et Interfaces de dépôt).
- **TennisStats.Application** : Logique métier (Services), DTOs et gestion des exceptions personnalisées.
- **TennisStats.Infrastructure** : Persistance des données (Lecture du fichier JSON `headtohead.json`).
- **TennisStats.Api** : Point d'entrée de l'application (Contrôleurs, Middlewares de gestion d'erreurs).

Les tests unitaires se trouvent dans le dossier `tests/`.

## 🚀 Fonctionnalités

- **GET /Players** : Liste des joueurs triés par rang (du meilleur au moins bon).
- **GET /Players/{id}** : Détails d'un joueur par son identifiant unique.
- **GET /Players/stats** : Statistiques globales :
  - Pays avec le meilleur ratio de victoires.
  - IMC (BMI) moyen des joueurs.
  - Médiane de la taille des joueurs.
- **POST /Players** : Ajout d'un nouveau joueur.

## 🛠️ Installation et Lancement

### Prérequis
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Lancer l'API
Depuis la racine du projet, exécutez la commande suivante :
```bash
dotnet run --project src/TennisStats.Api/TennisStats.Api.csproj
```
L'API sera disponible sur `http://localhost:5297` (ou le port indiqué dans votre terminal).

### Accéder à la documentation (Swagger)
Une fois l'application lancée, vous pouvez tester les endpoints via l'interface Swagger :
[http://localhost:5297/swagger](http://localhost:5297/swagger)

## 🧪 Tests Unitaires

Pour exécuter les tests unitaires et vérifier la logique métier :
```bash
dotnet test
```

## ☁️ Déploiement

Le projet est prêt à être déployé sur n'importe quel service Cloud (Azure, AWS, Render, Heroku) supportant .NET 8. Une configuration Docker peut être ajoutée facilement pour une portabilité totale.
# TennisStatsApi
