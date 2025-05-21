-- phpMyAdmin SQL Dump
-- version 5.1.2
-- https://www.phpmyadmin.net/
--
-- Hôte : localhost:3306
-- Généré le : mer. 21 mai 2025 à 15:07
-- Version du serveur : 5.7.24
-- Version de PHP : 8.3.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `newsapi`
--

-- --------------------------------------------------------

--
-- Structure de la table `failed_login_attempts`
--

CREATE TABLE `failed_login_attempts` (
  `id` int(11) NOT NULL,
  `user_id` int(11) DEFAULT NULL,
  `attempt_date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `failed_login_attempts`
--

INSERT INTO `failed_login_attempts` (`id`, `user_id`, `attempt_date`) VALUES
(1, NULL, '2025-03-17 14:39:09'),
(2, 2, '2025-03-17 14:39:14'),
(3, 2, '2025-03-17 15:29:57'),
(4, NULL, '2025-04-15 19:53:15');

-- --------------------------------------------------------

--
-- Structure de la table `favorites`
--

CREATE TABLE `favorites` (
  `id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `title` text NOT NULL,
  `description` text,
  `url` text,
  `urlToImage` text,
  `publishedAt` datetime DEFAULT NULL,
  `source` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `favorites`
--

INSERT INTO `favorites` (`id`, `user_id`, `title`, `description`, `url`, `urlToImage`, `publishedAt`, `source`) VALUES
(2, 2, '« Bonjour c’est le livreur votre colis… » : nos conseils pour ne pas se faire avoir par ces SMS et pour les signaler', 'Des SMS prétendant venir de Mondial Relay envahissent nos téléphones. Pas de panique, on vous donne les clés pour éviter de vous faire avoir. Le scénario', 'https://www.frandroid.com/culture-tech/securite-applications/2525503_bonjour-cest-le-livreur-votre-colis-nos-conseils-pour-ne-pas-se-faire-avoir-par-ces-sms-et-pour-les-signaler', 'https://c0.lestechnophiles.com/images.frandroid.com/wp-content/uploads/2025/03/xiaomi-15-frandroid-p1066904-1200x800-1.jpg?resize=1600,900&key=1a29cd21&watermark', '2025-03-02 08:50:46', 'Unknown'),
(9, 2, 'Renault propose une nouvelle aide accessible à tous pour réduire le prix des voitures électriques', 'En plus du bonus écologique, un nouveau dispositif, les certificats d’économies d’énergie (CEE), va permettre aux clients d\'acheter une voiture électrique prix réduit. Le groupe Renault vient d\'intégrer ce dispositif sur ses véhicules électriques.', 'https://www.frandroid.com/marques/renault/2525089_renault-propose-une-nouvelle-aide-accessible-a-tous-pour-reduire-le-prix-des-voitures-electriques', 'https://c0.lestechnophiles.com/images.frandroid.com/wp-content/uploads/2025/03/r-dam-1628767-1200x675-1.jpg?resize=1600,900&key=6c33564e&watermark', '2025-03-02 06:01:00', 'Unknown'),
(10, 2, 'Donald Trump contre les médias, une guerre sans merci pour imposer le récit de sa présidence', 'Le président américain s’en prend à la presse traditionnelle et favorise les influenceurs conservateurs acquis à sa cause. La rencontre avec Zelensky l’a encore montré.', 'https://www.huffingtonpost.fr/international/article/donald-trump-contre-les-medias-une-guerre-sans-merci-pour-imposer-le-recit-de-sa-presidence-clx1_246760.html', 'https://focus.huffingtonpost.fr/2025/02/14/470/0/8256/4644/1820/1023/75/0/314df7f_sirius-fs-upload-1-1qasusdkxwee-1739568341639-000-36xt8h4.jpg', '2025-03-02 12:55:29', 'Unknown'),
(11, 2, 'La grande banque DBS va remplacer des milliers d’emplois par l’IA', 'La banque DBS, poids lourd financier à Singapour, a décidé de faire de la place à l’intelligence artificielle. Résultat : 4.000 postes temporaires et contractuels vont disparaître d’ici trois ans. Tout n’est certes pas complètement noir puisque la banque prév…', 'https://www.journaldugeek.com/2025/03/02/la-grande-banque-dbs-va-remplacer-des-milliers-demplois-par-lia/', 'https://www.journaldugeek.com/app/uploads/2025/02/dbs.jpg', '2025-03-02 11:02:02', 'Unknown'),
(12, 2, 'Selection Sunday bracket live tracker: March Madness bubble watch, bracketology ahead of NCAA tournament reveal - Yahoo Sports', 'Selection Sunday is finally here. Let the Madness begin.', 'https://sports.yahoo.com/college-basketball/live/selection-sunday-bracket-live-tracker-march-madness-bubble-watch-bracketology-ahead-of-ncaa-tournament-reveal-120018113.html', 'https://s.yimg.com/ny/api/res/1.2/ervkE1t6P8R1iDUyvCaWZQ--/YXBwaWQ9aGlnaGxhbmRlcjt3PTEyMDA7aD04MDA-/https://s.yimg.com/os/creatr-uploaded-images/2025-03/af8defe0-021c-11f0-bfbc-429c1faa2f47', '2025-03-16 12:45:37', 'Unknown');

-- --------------------------------------------------------

--
-- Structure de la table `user`
--

CREATE TABLE `user` (
  `id` int(11) NOT NULL,
  `pseudo` varchar(50) NOT NULL,
  `password` varchar(250) NOT NULL,
  `firstname` varchar(50) DEFAULT NULL,
  `lastname` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `user`
--

INSERT INTO `user` (`id`, `pseudo`, `password`, `firstname`, `lastname`) VALUES
(2, 'yy', '$2a$11$Qr9Kx9NxQ0CZU1nlK2m1ce11ZgKnzQ9LjYmNMAzZhpHn3/QVhvXsO', 'yy', 'yy'),
(3, 'admin', '$2a$11$ck4XNH.nNvB7bRXSLqcs7uj.OjzP2iLikdxgquuota8YHU5Pu7Hsu', 'ad', 'ad');

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `failed_login_attempts`
--
ALTER TABLE `failed_login_attempts`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_user_id` (`user_id`);

--
-- Index pour la table `favorites`
--
ALTER TABLE `favorites`
  ADD PRIMARY KEY (`id`),
  ADD KEY `user_id` (`user_id`);

--
-- Index pour la table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT pour les tables déchargées
--

--
-- AUTO_INCREMENT pour la table `failed_login_attempts`
--
ALTER TABLE `failed_login_attempts`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT pour la table `favorites`
--
ALTER TABLE `favorites`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT pour la table `user`
--
ALTER TABLE `user`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `failed_login_attempts`
--
ALTER TABLE `failed_login_attempts`
  ADD CONSTRAINT `fk_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`) ON DELETE SET NULL;

--
-- Contraintes pour la table `favorites`
--
ALTER TABLE `favorites`
  ADD CONSTRAINT `favorites_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
