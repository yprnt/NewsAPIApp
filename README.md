# 📰 NewsAPIApp

**NewsAPIApp** est une application C# avec WPF permettant de récupérer et d'afficher des articles selon leur nom ou catégorie. Il y a un système de favoris et *prochainement* un système pour lire l'article directement depuis l'application.

## 📂 Structure du projet

- `NewsAPIApp/` : Contient le code source principal de l'application.
- `newsapi.sql` : Script SQL pour la création de la base de données utilisée par l'application.
- `NewsAPIApp.sln` : Fichier de solution Visual Studio pour le projet.
- `README.md` : Ce fichier, fournissant une vue d'ensemble du projet.
- `LICENSE.txt` : Licence du projet (GPL-3.0).

## ⚙️ Fonctionnalités

- Récupération des actualités en temps réel via l'API de NewsAPI.org.
- Affichage des titres, descriptions et liens des articles.
- Filtrage des actualités par mots-clés, sources ou catégories.
- Stockage des résultats dans une base de données locale pour une consultation ultérieure.

## 🚀 Installation

1. **Cloner le dépôt :**

       ```bash 
       git clone https://github.com/yprnt/NewsAPIApp.git
       ```
   
3. **Ouvrir le projet :**

   - Ouvrez le fichier `NewsAPIApp.sln` avec Visual Studio.

4. **Configurer la base de données :**

   - Exécutez le script `newsapi.sql` pour créer la base de données nécessaire.

5. **Obtenir une clé API :**

   - Inscrivez-vous sur [NewsAPI.org](https://newsapi.org/) pour obtenir une clé API gratuite.

6. **Configurer la clé API :**

   - Ajoutez votre clé API dans le fichier `ArticlesVM.cs`.

7. **Exécuter l'application :**

   - Compilez et exécutez le projet depuis Visual Studio.

## 🔑 Connexion rapide

### Compte Utilisateur
- **Login**: yy
- **Mot de passe**: yy

### Compte Administrateur
- **Login**: admin
- **Mot de passe**: admin


## 🖥️ Technologies utilisées

- **Langage :** C#
- **Framework :** .NET
- **API :** [NewsAPI.org](https://newsapi.org/)
- **Base de données :** MySQL (ou autre, selon le script `newsapi.sql`)
- **IDE :** Visual Studio

## 📸 Aperçu

*Des captures d'écrans sont à venir ...*

## 📄 Licence

Ce projet est sous licence [GPL-3.0](https://www.gnu.org/licenses/gpl-3.0.html). Voir le fichier `LICENSE.txt` pour plus d’informations.

## 🙌 Remerciements

- [NewsAPI.org](https://newsapi.org/) pour fournir une API d’actualités accessible et facile à utiliser.
