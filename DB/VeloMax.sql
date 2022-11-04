-- environnement
drop database if exists VeloMax;
create database if not exists VeloMax;
use VeloMax;
set sql_safe_updates=0;

-- tables
CREATE TABLE commande (id_commande int NOT NULL auto_increment, date_commande datetime, date_livraison datetime, id_individu varchar(50), id_boutique varchar(50), id_adresse int, primary key(id_commande));  
CREATE TABLE bicyclette (id_bicyclette int NOT NULL, nom_bicyclette varchar(50), grandeur varchar(50), prix_bicyclette int, ligne_produit enum("VTT","Vélo de course", "Classique", "BMX"), date_intro_bicyclette datetime, date_discontinuation_bicyclette datetime, stock_bicyclette int);  
CREATE TABLE fidelio (num_program enum("1 Fidélio", "2 Fidélio Or", "3 Fidélio Platine", "4 Fidélio Max"), cout int, duree varchar(50), rabais int);  
CREATE TABLE boutique (id_boutique varchar(50) NOT NULL, nom_contact_boutique varchar(50), remise int, tel_boutique varchar(50), courriel_boutique varchar(50), id_adresse int not null);  
CREATE TABLE piece_detachee (id_piece varchar(50) NOT NULL, description varchar(50), date_intro_piece datetime, date_discontinuation_piece datetime, stock_piece int);  
CREATE TABLE individu (id_individu varchar(50) NOT NULL, nom_individu varchar(50), prenom_individu varchar(50), tel_individu varchar(50), courriel_individu varchar(50), id_adresse int not null); 
CREATE TABLE fournisseur (siret varchar(50) NOT NULL, nom_entreprise varchar(50), contact_fournisseur varchar(50), note varchar(50), id_adresse int not null);  
CREATE TABLE adresse (id_adresse int NOT NULL auto_increment, rue varchar(50), ville varchar(50), code_postal int, region varchar(50), primary key(id_adresse));  
CREATE TABLE assemble (id_bicyclette int NOT NULL, id_piece varchar(50) NOT NULL);  
CREATE TABLE fournit (id_piece varchar(50) NOT NULL, siret varchar(50) NOT NULL, num_produit_catalogue varchar(50), prix_piece int, delai_approv int);  
CREATE TABLE achat_piece (id_piece varchar(50) NOT NULL, siret varchar(50) not null, id_commande int NOT NULL, quantite_piece int);  
CREATE TABLE achat_velo (id_bicyclette int NOT NULL, id_commande int NOT NULL, quantite_velo int);  
CREATE TABLE adhere (id_individu varchar(50) NOT NULL, num_program enum("1 Fidélio", "2 Fidélio Or", "3 Fidélio Platine", "4 Fidélio Max") NOT NULL, date_paiement_adhesion datetime);
ALTER TABLE bicyclette ADD CONSTRAINT PK_bicyclette PRIMARY KEY (id_bicyclette);  
ALTER TABLE fidelio ADD CONSTRAINT PK_fidelio PRIMARY KEY (num_program);  
ALTER TABLE boutique ADD CONSTRAINT PK_boutique PRIMARY KEY (id_boutique);  
ALTER TABLE piece_detachee ADD CONSTRAINT PK_piece_detachee PRIMARY KEY (id_piece);  
ALTER TABLE individu ADD CONSTRAINT PK_individu PRIMARY KEY (id_individu);  
ALTER TABLE fournisseur ADD CONSTRAINT PK_fournisseur PRIMARY KEY (siret);  
ALTER TABLE assemble ADD CONSTRAINT PK_assemble PRIMARY KEY (id_bicyclette, id_piece);  
ALTER TABLE fournit ADD CONSTRAINT PK_fournit PRIMARY KEY (id_piece, siret);  
ALTER TABLE achat_piece ADD CONSTRAINT PK_achat_piece PRIMARY KEY (id_piece, siret, id_commande);  
ALTER TABLE achat_velo ADD CONSTRAINT PK_achat_velo PRIMARY KEY (id_bicyclette, id_commande);  
ALTER TABLE adhere ADD CONSTRAINT PK_adhere PRIMARY KEY (id_individu, num_program);  
ALTER TABLE commande ADD CONSTRAINT FK_commande_id_individu FOREIGN KEY (id_individu) REFERENCES individu (id_individu) ON DELETE CASCADE;  
ALTER TABLE commande ADD CONSTRAINT FK_commande_id_boutique FOREIGN KEY (id_boutique) REFERENCES boutique (id_boutique) ON DELETE CASCADE;  
ALTER TABLE individu ADD CONSTRAINT FK_individu_id_adresse FOREIGN KEY (id_adresse) REFERENCES adresse (id_adresse) ON DELETE CASCADE;  
ALTER TABLE boutique ADD CONSTRAINT FK_boutique_id_adresse FOREIGN KEY (id_adresse) REFERENCES adresse (id_adresse) ON DELETE CASCADE;  
ALTER TABLE fournisseur ADD CONSTRAINT FK_fournisseur_id_adresse FOREIGN KEY (id_adresse) REFERENCES adresse (id_adresse) ON DELETE CASCADE;  
ALTER TABLE assemble ADD CONSTRAINT FK_assemble_id_bicyclette FOREIGN KEY (id_bicyclette) REFERENCES bicyclette (id_bicyclette)ON DELETE CASCADE;  
ALTER TABLE assemble ADD CONSTRAINT FK_assemble_id_piece FOREIGN KEY (id_piece) REFERENCES piece_detachee (id_piece)ON DELETE CASCADE;  
ALTER TABLE fournit ADD CONSTRAINT FK_fournit_id_piece FOREIGN KEY (id_piece) REFERENCES piece_detachee (id_piece)ON DELETE CASCADE; 
ALTER TABLE fournit ADD CONSTRAINT FK_fournit_siret FOREIGN KEY (siret) REFERENCES fournisseur (siret)ON DELETE CASCADE;  
ALTER TABLE achat_piece ADD CONSTRAINT FK_achat_piece_id_piece FOREIGN KEY (id_piece) REFERENCES piece_detachee (id_piece)ON DELETE CASCADE;  
ALTER TABLE achat_piece ADD CONSTRAINT FK_achat_piece_siret FOREIGN KEY (siret) REFERENCES fournisseur (siret)ON DELETE CASCADE; 
ALTER TABLE achat_piece ADD CONSTRAINT FK_achat_piece_id_commande FOREIGN KEY (id_commande) REFERENCES commande (id_commande)ON DELETE CASCADE;  
ALTER TABLE achat_velo ADD CONSTRAINT FK_achat_velo_id_bicyclette FOREIGN KEY (id_bicyclette) REFERENCES bicyclette (id_bicyclette)ON DELETE CASCADE;  
ALTER TABLE achat_velo ADD CONSTRAINT FK_achat_velo_id_commande FOREIGN KEY (id_commande) REFERENCES commande (id_commande)ON DELETE CASCADE; 
ALTER TABLE adhere ADD CONSTRAINT FK_adhere_id_individu FOREIGN KEY (id_individu) REFERENCES individu (id_individu)ON DELETE CASCADE;    


-- Peuplement des tables

-- Table bicyclette 
insert into bicyclette (id_bicyclette, nom_bicyclette, grandeur, prix_bicyclette, ligne_produit, date_intro_bicyclette, date_discontinuation_bicyclette, stock_bicyclette) values (101,"Kilimandjaro","Adultes",569,"VTT","2000-10-10","2025-10-10", 5);
insert into bicyclette (id_bicyclette, nom_bicyclette, grandeur, prix_bicyclette, ligne_produit, date_intro_bicyclette, date_discontinuation_bicyclette, stock_bicyclette) values (102,"NorthPole","Adultes",329,"VTT","2000-01-12","2023-10-10", 5);
insert into bicyclette (id_bicyclette, nom_bicyclette, grandeur, prix_bicyclette, ligne_produit, date_intro_bicyclette, date_discontinuation_bicyclette, stock_bicyclette) values (103,"MontBlanc","Jeunes",399,"VTT","2000-05-05","2024-08-10", 5);
insert into bicyclette (id_bicyclette, nom_bicyclette, grandeur, prix_bicyclette, ligne_produit, date_intro_bicyclette, date_discontinuation_bicyclette, stock_bicyclette) values (104,"Hooligan","Jeunes",199,"VTT","2000-12-10","2025-01-10", 5);
insert into bicyclette (id_bicyclette, nom_bicyclette, grandeur, prix_bicyclette, ligne_produit, date_intro_bicyclette, date_discontinuation_bicyclette, stock_bicyclette) values (105,"Orléans","Hommes",299,"Vélo de course","2000-10-10","2025-05-10", 5);
insert into bicyclette (id_bicyclette, nom_bicyclette, grandeur, prix_bicyclette, ligne_produit, date_intro_bicyclette, date_discontinuation_bicyclette, stock_bicyclette) values (106,"Orléans","Dames",229,"Vélo de course","2000-10-10","2022-10-10", 5);
insert into bicyclette (id_bicyclette, nom_bicyclette, grandeur, prix_bicyclette, ligne_produit, date_intro_bicyclette, date_discontinuation_bicyclette, stock_bicyclette) values (107,"BlueJay","Hommes",349,"Vélo de course","2000-10-10","2022-11-10", 5);
insert into bicyclette (id_bicyclette, nom_bicyclette, grandeur, prix_bicyclette, ligne_produit, date_intro_bicyclette, date_discontinuation_bicyclette, stock_bicyclette) values (108,"BlueJay","Dames",349,"Vélo de course","2000-10-10","2025-10-10", 5);
insert into bicyclette (id_bicyclette, nom_bicyclette, grandeur, prix_bicyclette, ligne_produit, date_intro_bicyclette, date_discontinuation_bicyclette, stock_bicyclette) values (109,"Trail Explorer","Filles",129,"Classique","2000-10-10","2025-10-10", 5);
insert into bicyclette (id_bicyclette, nom_bicyclette, grandeur, prix_bicyclette, ligne_produit, date_intro_bicyclette, date_discontinuation_bicyclette, stock_bicyclette) values (110,"Trail Explorer","Garçons",129,"Classique","2000-10-10","2025-10-10", 5);
insert into bicyclette (id_bicyclette, nom_bicyclette, grandeur, prix_bicyclette, ligne_produit, date_intro_bicyclette, date_discontinuation_bicyclette, stock_bicyclette) values (111,"Night Hawk","Jeunes",189,"Classique","2000-10-10","2025-10-10", 5);
insert into bicyclette (id_bicyclette, nom_bicyclette, grandeur, prix_bicyclette, ligne_produit, date_intro_bicyclette, date_discontinuation_bicyclette, stock_bicyclette) values (112,"Tierra Verde","Hommes",199,"Classique","2000-10-10","2025-10-10", 5);
insert into bicyclette (id_bicyclette, nom_bicyclette, grandeur, prix_bicyclette, ligne_produit, date_intro_bicyclette, date_discontinuation_bicyclette, stock_bicyclette) values (113,"Tierra Verde","Dames",199,"Classique","2000-10-10","2025-10-10", 5);
insert into bicyclette (id_bicyclette, nom_bicyclette, grandeur, prix_bicyclette, ligne_produit, date_intro_bicyclette, date_discontinuation_bicyclette, stock_bicyclette) values (114,"Mud Zinger I","Jeunes",279,"BMX","2000-10-10","2025-10-10", 5);
insert into bicyclette (id_bicyclette, nom_bicyclette, grandeur, prix_bicyclette, ligne_produit, date_intro_bicyclette, date_discontinuation_bicyclette, stock_bicyclette) values (115,"Mud Zinger II","Adultes",359,"BMX","2000-10-10","2025-10-10", 5);

-- Table piece_detachee
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("C32", "Cadre", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("C34", "Cadre", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("C76", "Cadre", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("C43", "Cadre", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("C44f", "Cadre", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("C43f", "Cadre", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("C01", "Cadre", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("C02", "Cadre", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("C15", "Cadre", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("C87", "Cadre", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("C87f", "Cadre", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("C25", "Cadre", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("C26", "Cadre", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("G7", "Guidon", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("G9", "Guidon", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("G12", "Guidon", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("F3", "Freins", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("F9", "Freins", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("S88", "Selle", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("S37", "Selle", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("S35", "Selle", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("S02", "Selle", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("S03", "Selle", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("S36", "Selle", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("S34", "Selle", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("S87", "Selle", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("DV133", "Dérailleur avant", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("DV17", "Dérailleur avant", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("DV87", "Dérailleur avant", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("DV57", "Dérailleur avant", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("DV15", "Dérailleur avant", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("DV41", "Dérailleur avant", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("DV132", "Dérailleur avant", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("DR56", "Dérailleur arrière", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("DR87", "Dérailleur arrière", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("DR86", "Dérailleur arrière", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("DR23", "Dérailleur arrière", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("DR76", "Dérailleur arrière", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("DR52", "Dérailleur arrière", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("RV45", "Roue avant", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("RV48", "Roue avant", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("RV12", "Roue avant", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("RV19", "Roue avant", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("RV1", "Roue avant", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("RV11", "Roue avant", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("RV44", "Roue avant", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("RR46", "Roue arrière", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("RR47", "Roue arrière", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("RR32", "Roue arrière", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("RR18", "Roue arrière", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("RR2", "Roue arrière", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("RR12", "Roue arrière", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("R02", "Réflecteurs", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("R09", "Réflecteurs", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("R10", "Réflecteurs", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("P12", "Pédalier", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("P34", "Pédalier", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("P1", "Pédalier", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("P15", "Pédalier", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("O2", "Ordinateur", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("O4", "Ordinateur", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("S01", "Panier", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("S05", "Panier", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("S74", "Panier", "2000-01-01", "2025-05-05", 15);
insert into piece_detachee (id_piece, description, date_intro_piece, date_discontinuation_piece, stock_piece) values ("S73", "Panier", "2000-01-01", "2025-05-05", 15);


-- table assemble
insert into assemble (id_bicyclette, id_piece) values (101, "C32");
insert into assemble (id_bicyclette, id_piece) values (101, "G7");
insert into assemble (id_bicyclette, id_piece) values (101, "F3");
insert into assemble (id_bicyclette, id_piece) values (101, "S88");
insert into assemble (id_bicyclette, id_piece) values (101, "DV133");
insert into assemble (id_bicyclette, id_piece) values (101, "DR56");
insert into assemble (id_bicyclette, id_piece) values (101, "RV45");
insert into assemble (id_bicyclette, id_piece) values (101, "RR46");
insert into assemble (id_bicyclette, id_piece) values (101, "P12");
insert into assemble (id_bicyclette, id_piece) values (101, "O2");

insert into assemble (id_bicyclette, id_piece) values (102, "C34");
insert into assemble (id_bicyclette, id_piece) values (102, "G7");
insert into assemble (id_bicyclette, id_piece) values (102, "F3");
insert into assemble (id_bicyclette, id_piece) values (102, "S88");
insert into assemble (id_bicyclette, id_piece) values (102, "DV17");
insert into assemble (id_bicyclette, id_piece) values (102, "DR87");
insert into assemble (id_bicyclette, id_piece) values (102, "RV48");
insert into assemble (id_bicyclette, id_piece) values (102, "RR47");
insert into assemble (id_bicyclette, id_piece) values (102, "P12");

insert into assemble (id_bicyclette, id_piece) values (103, "C76");
insert into assemble (id_bicyclette, id_piece) values (103, "G7");
insert into assemble (id_bicyclette, id_piece) values (103, "F3");
insert into assemble (id_bicyclette, id_piece) values (103, "S88");
insert into assemble (id_bicyclette, id_piece) values (103, "DV17");
insert into assemble (id_bicyclette, id_piece) values (103, "DR56");
insert into assemble (id_bicyclette, id_piece) values (103, "RV45");
insert into assemble (id_bicyclette, id_piece) values (103, "RR46");
insert into assemble (id_bicyclette, id_piece) values (103, "P12");
insert into assemble (id_bicyclette, id_piece) values (103, "O2");

insert into assemble (id_bicyclette, id_piece) values (104, "C76");
insert into assemble (id_bicyclette, id_piece) values (104, "G7");
insert into assemble (id_bicyclette, id_piece) values (104, "F3");
insert into assemble (id_bicyclette, id_piece) values (104, "S88");
insert into assemble (id_bicyclette, id_piece) values (104, "DV87");
insert into assemble (id_bicyclette, id_piece) values (104, "DR86");
insert into assemble (id_bicyclette, id_piece) values (104, "RV48");
insert into assemble (id_bicyclette, id_piece) values (104, "RR32");
insert into assemble (id_bicyclette, id_piece) values (104, "P12");

insert into assemble (id_bicyclette, id_piece) values (105, "C43");
insert into assemble (id_bicyclette, id_piece) values (105, "G9");
insert into assemble (id_bicyclette, id_piece) values (105, "F9");
insert into assemble (id_bicyclette, id_piece) values (105, "S37");
insert into assemble (id_bicyclette, id_piece) values (105, "DV57");
insert into assemble (id_bicyclette, id_piece) values (105, "DR86");
insert into assemble (id_bicyclette, id_piece) values (105, "RV19");
insert into assemble (id_bicyclette, id_piece) values (105, "RR18");
insert into assemble (id_bicyclette, id_piece) values (105, "R02");
insert into assemble (id_bicyclette, id_piece) values (105, "P34");

insert into assemble (id_bicyclette, id_piece) values (106, "C44f");
insert into assemble (id_bicyclette, id_piece) values (106, "G9");
insert into assemble (id_bicyclette, id_piece) values (106, "F9");
insert into assemble (id_bicyclette, id_piece) values (106, "S35");
insert into assemble (id_bicyclette, id_piece) values (106, "DV57");
insert into assemble (id_bicyclette, id_piece) values (106, "DR86");
insert into assemble (id_bicyclette, id_piece) values (106, "RV19");
insert into assemble (id_bicyclette, id_piece) values (106, "RR18");
insert into assemble (id_bicyclette, id_piece) values (106, "R02");
insert into assemble (id_bicyclette, id_piece) values (106, "P34");

insert into assemble (id_bicyclette, id_piece) values (107, "C43");
insert into assemble (id_bicyclette, id_piece) values (107, "G9");
insert into assemble (id_bicyclette, id_piece) values (107, "F9");
insert into assemble (id_bicyclette, id_piece) values (107, "S37");
insert into assemble (id_bicyclette, id_piece) values (107, "DV57");
insert into assemble (id_bicyclette, id_piece) values (107, "DR87");
insert into assemble (id_bicyclette, id_piece) values (107, "RV19");
insert into assemble (id_bicyclette, id_piece) values (107, "RR18");
insert into assemble (id_bicyclette, id_piece) values (107, "R02");
insert into assemble (id_bicyclette, id_piece) values (107, "P34");
insert into assemble (id_bicyclette, id_piece) values (107, "O4");

insert into assemble (id_bicyclette, id_piece) values (108, "C43f");
insert into assemble (id_bicyclette, id_piece) values (108, "G9");
insert into assemble (id_bicyclette, id_piece) values (108, "F9");
insert into assemble (id_bicyclette, id_piece) values (108, "S35");
insert into assemble (id_bicyclette, id_piece) values (108, "DV57");
insert into assemble (id_bicyclette, id_piece) values (108, "DR87");
insert into assemble (id_bicyclette, id_piece) values (108, "RV19");
insert into assemble (id_bicyclette, id_piece) values (108, "RR18");
insert into assemble (id_bicyclette, id_piece) values (108, "R02");
insert into assemble (id_bicyclette, id_piece) values (108, "P34");
insert into assemble (id_bicyclette, id_piece) values (108, "O4");

insert into assemble (id_bicyclette, id_piece) values (109, "C01");
insert into assemble (id_bicyclette, id_piece) values (109, "G12");
insert into assemble (id_bicyclette, id_piece) values (109, "S02");
insert into assemble (id_bicyclette, id_piece) values (109, "RV1");
insert into assemble (id_bicyclette, id_piece) values (109, "RR2");
insert into assemble (id_bicyclette, id_piece) values (109, "R09");
insert into assemble (id_bicyclette, id_piece) values (109, "P1");
insert into assemble (id_bicyclette, id_piece) values (109, "S01");

insert into assemble (id_bicyclette, id_piece) values (110, "C02");
insert into assemble (id_bicyclette, id_piece) values (110, "G12");
insert into assemble (id_bicyclette, id_piece) values (110, "S03");
insert into assemble (id_bicyclette, id_piece) values (110, "RV1");
insert into assemble (id_bicyclette, id_piece) values (110, "RR2");
insert into assemble (id_bicyclette, id_piece) values (110, "R09");
insert into assemble (id_bicyclette, id_piece) values (110, "P1");
insert into assemble (id_bicyclette, id_piece) values (110, "S05");

insert into assemble (id_bicyclette, id_piece) values (111, "C15");
insert into assemble (id_bicyclette, id_piece) values (111, "G12");
insert into assemble (id_bicyclette, id_piece) values (111, "F9");
insert into assemble (id_bicyclette, id_piece) values (111, "S36");
insert into assemble (id_bicyclette, id_piece) values (111, "DV15");
insert into assemble (id_bicyclette, id_piece) values (111, "DR23");
insert into assemble (id_bicyclette, id_piece) values (111, "RV11");
insert into assemble (id_bicyclette, id_piece) values (111, "RR12");
insert into assemble (id_bicyclette, id_piece) values (111, "R10");
insert into assemble (id_bicyclette, id_piece) values (111, "P15");
insert into assemble (id_bicyclette, id_piece) values (111, "S74");

insert into assemble (id_bicyclette, id_piece) values (112, "C87");
insert into assemble (id_bicyclette, id_piece) values (112, "G12");
insert into assemble (id_bicyclette, id_piece) values (112, "F9");
insert into assemble (id_bicyclette, id_piece) values (112, "S36");
insert into assemble (id_bicyclette, id_piece) values (112, "DV41");
insert into assemble (id_bicyclette, id_piece) values (112, "DR76");
insert into assemble (id_bicyclette, id_piece) values (112, "RV11");
insert into assemble (id_bicyclette, id_piece) values (112, "RR12");
insert into assemble (id_bicyclette, id_piece) values (112, "R10");
insert into assemble (id_bicyclette, id_piece) values (112, "P15");
insert into assemble (id_bicyclette, id_piece) values (112, "S74");

insert into assemble (id_bicyclette, id_piece) values (113, "C87f");
insert into assemble (id_bicyclette, id_piece) values (113, "G12");
insert into assemble (id_bicyclette, id_piece) values (113, "F9");
insert into assemble (id_bicyclette, id_piece) values (113, "S34");
insert into assemble (id_bicyclette, id_piece) values (113, "DV41");
insert into assemble (id_bicyclette, id_piece) values (113, "DR76");
insert into assemble (id_bicyclette, id_piece) values (113, "RV11");
insert into assemble (id_bicyclette, id_piece) values (113, "RR12");
insert into assemble (id_bicyclette, id_piece) values (113, "R10");
insert into assemble (id_bicyclette, id_piece) values (113, "P15");
insert into assemble (id_bicyclette, id_piece) values (113, "S73");

insert into assemble (id_bicyclette, id_piece) values (114, "C25");
insert into assemble (id_bicyclette, id_piece) values (114, "G7");
insert into assemble (id_bicyclette, id_piece) values (114, "F3");
insert into assemble (id_bicyclette, id_piece) values (114, "S87");
insert into assemble (id_bicyclette, id_piece) values (114, "DV132");
insert into assemble (id_bicyclette, id_piece) values (114, "DR52");
insert into assemble (id_bicyclette, id_piece) values (114, "RV44");
insert into assemble (id_bicyclette, id_piece) values (114, "RR47");
insert into assemble (id_bicyclette, id_piece) values (114, "P12");

insert into assemble (id_bicyclette, id_piece) values (115, "C26");
insert into assemble (id_bicyclette, id_piece) values (115, "G7");
insert into assemble (id_bicyclette, id_piece) values (115, "F3");
insert into assemble (id_bicyclette, id_piece) values (115, "S87");
insert into assemble (id_bicyclette, id_piece) values (115, "DV133");
insert into assemble (id_bicyclette, id_piece) values (115, "DR52");
insert into assemble (id_bicyclette, id_piece) values (115, "RV44");
insert into assemble (id_bicyclette, id_piece) values (115, "RR47");
insert into assemble (id_bicyclette, id_piece) values (115, "P12");


-- Table fidelio
insert into fidelio (num_program, cout, duree, rabais) values ("1 Fidélio", 15, "1 an", 5);
insert into fidelio (num_program, cout, duree, rabais) values ("2 Fidélio Or", 25, "2 ans", 8);
insert into fidelio (num_program, cout, duree, rabais) values ("3 Fidélio Platine", 60, "2 ans", 10);
insert into fidelio (num_program, cout, duree, rabais) values ("4 Fidélio Max", 100, "3 ans", 12);

-- table adresse (rue, ville, code_postal, région)
insert into adresse (id_adresse, rue, ville, code_postal, region) values (1, "1 Boulevard des protagonistes", "Paris", 75001, "Ile-de-France");
insert into adresse (id_adresse, rue, ville, code_postal, region) values (2, "6 rue des merveilles", "Paris", 75002, "Ile-de-France"); 
insert into adresse (id_adresse, rue, ville, code_postal, region) values (3, "5 rue des rêves", "Créteil", "94000", "Ile-de-France"); 
insert into adresse (id_adresse, rue, ville, code_postal, region) values (4, "2 rue Alexis de Tocqueville", "Versailles", 78000, "Ile-de-France");
insert into adresse (id_adresse, rue, ville, code_postal, region) values (5, "1 rue de l'océan", "Marseille", 13000, "Province-Alpes-Côte d'Azur");
insert into adresse (id_adresse, rue, ville, code_postal, region) values (6, "3 rue du cheval de Troie", "Dijon", 21000, "Bourgogne"); 
insert into adresse (id_adresse, rue, ville, code_postal, region) values (7, "5 Boulevard du Banh Mi", "Bondy", 93140, "Ile-de-France");
insert into adresse (id_adresse, rue, ville, code_postal, region) values (8, "10 rue du dollar", "Paris", 75016, "Ile-de-France"); 
insert into adresse (id_adresse, rue, ville, code_postal, region) values (9, "La rue du titan Assaillant", "Brest", 29200, "Bretagne"); 
insert into adresse (id_adresse, rue, ville, code_postal, region) values (10, "5 rue de l'entreprise", "Paris", 75005, "Ile-de-France");
insert into adresse (id_adresse, rue, ville, code_postal, region) values (11, "1 rue de l'entreprise", "Paris", 75001, "Ile-de-France"); 
insert into adresse (id_adresse, rue, ville, code_postal, region) values (12, "2 rue de l'entreprise", "Paris", 75002, "Ile-de-France");
insert into adresse (id_adresse, rue, ville, code_postal, region) values (13, "3 rue de l'entreprise", "Paris", 75003, "Ile-de-France"); 
insert into adresse (id_adresse, rue, ville, code_postal, region) values (14, "4 rue de l'entreprise", "Paris", 75004, "Ile-de-France"); 
insert into adresse (id_adresse, rue, ville, code_postal, region) values (15, "5 rue de l'entreprise", "Paris", 75005, "Ile-de-France");

-- table individu 
insert into individu (id_individu, nom_individu, prenom_individu, tel_individu, courriel_individu, id_adresse) values ("I01", "LIU", "Céline", "0123456789", "celine@liu.fr", 1);
insert into individu (id_individu, nom_individu, prenom_individu, tel_individu, courriel_individu, id_adresse) values ("I02", "NEANG", "Sandrine","7856321490","sandrine@neang.fr", 2);
insert into individu (id_individu, nom_individu, prenom_individu, tel_individu, courriel_individu, id_adresse) values ("I03", "LY", "Jade","9854123607", "jade@ly.fr", 3);
insert into individu (id_individu, nom_individu, prenom_individu, tel_individu, courriel_individu, id_adresse) values ("I04", "MAKARISON", "Jolyane", "7894561230", "jolyane@makarison.fr", 4);
insert into individu (id_individu, nom_individu, prenom_individu, tel_individu, courriel_individu, id_adresse) values ("I05", "LLINARES", "Laura", "0128456789", "laura@llinares.fr", 5);

-- table adhère 
insert into adhere (id_individu, num_program, date_paiement_adhesion) values ("I01", "1 Fidélio", "2022-01-01");
insert into adhere (id_individu, num_program, date_paiement_adhesion) values ("I02", "2 Fidélio Or", "2022-01-01");


-- table boutique 
insert into boutique (id_boutique, nom_contact_boutique, remise, tel_boutique, courriel_boutique, id_adresse) values ("B01", "Elodie LAY", 8,"1659487320","elodie.lay@boutique.fr", 6);
insert into boutique (id_boutique, nom_contact_boutique, remise, tel_boutique, courriel_boutique, id_adresse) values ("B02", "Héléna NGUYEN", 10, "2589467130","helena.nguyen@boutique.fr", 7);
insert into boutique (id_boutique, nom_contact_boutique, remise, tel_boutique, courriel_boutique, id_adresse) values ("B03", "Vincent NAZZARENO", 20, "8462951370","vincent.nazzareno@boutique.fr", 8);
insert into boutique (id_boutique, nom_contact_boutique, remise, tel_boutique, courriel_boutique, id_adresse) values ("B04", "Mikasa ACKERMAN", 5, "3684125970","mikasa.ackerman@boutique.fr", 9);
insert into boutique (id_boutique, nom_contact_boutique, remise, tel_boutique, courriel_boutique, id_adresse) values ("B05", "Eren YEAGER", 12, "7849563210","eren.yeager@boutique.fr", 10);


-- table fournisseur
insert into fournisseur (siret, nom_entreprise, contact_fournisseur, note, id_adresse) values ("F01", "Dépanneur EXPRESS", "8527419630", "5", 11); 
insert into fournisseur (siret, nom_entreprise, contact_fournisseur, note, id_adresse) values ("F02", "Pièces par Milliers", "9516234870" , "2", 12); 
insert into fournisseur (siret, nom_entreprise, contact_fournisseur, note, id_adresse) values ("F03", "Conforama", "8529637410", "3", 13);
insert into fournisseur (siret, nom_entreprise, contact_fournisseur, note, id_adresse) values ("F04", "Décathlon", "3216549870", "4", 14);
insert into fournisseur (siret, nom_entreprise, contact_fournisseur, note, id_adresse) values ("F05", "Carrefour", "7856941203", "1", 15);


-- table fournit
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("C32", "F01", "F01_1", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("C32", "F02", "F02_1", 10, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("C34", "F03", "F03_1", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("C76", "F04", "F04_1", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("C43", "F05", "F05_1", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("C44f", "F01", "F01_1", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("C43f", "F01", "F01_1", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("C01", "F02", "F02_1", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("C02", "F02", "F02_1", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("C15", "F04", "F04_1", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("C87", "F05", "F05_1", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("C87f", "F02", "F02_1", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("C25", "F03", "F03_1", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("C26", "F03", "F03_1", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("G7", "F04", "F04_2", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("G9", "F04", "F04_2", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("G12", "F05", "F05_2", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("F3", "F02", "F02_3", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("F9", "F03", "F03_3", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("DV133", "F01", "F01_4", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("DV17", "F04", "F04_4", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("DV87", "F01", "F01_4", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("DV57", "F01", "F01_4", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("DV15", "F02", "F02_4", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("DV41", "F02", "F02_4", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("DV132", "F03", "F03_4", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("RV45", "F05", "F05_5", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("RV48", "F05", "F05_5", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("RV12", "F05", "F05_5", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("RV19", "F05", "F05_5", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("RV1", "F04", "F04_5", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("RV11", "F01", "F01_5", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("RV44", "F01", "F01_5", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("RR46", "F02", "F02_6", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("RR47", "F02", "F02_6", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("RR32", "F03", "F03_6", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("RR18", "F03", "F03_6", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("RR2", "F01", "F01_6", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("RR12", "F02", "F02_6", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("R02", "F04", "F04_7", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("R09", "F05", "F05_7", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("R10", "F04", "F04_7", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("P12", "F02", "F02_8", 10, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("P34", "F01", "F01_8", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("P1", "F03", "F03_8", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("P15", "F01", "F01_8", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("O2", "F03", "F03_9", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("O2", "F02", "F02_9", 1, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("O4", "F01", "F01_9", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("S01", "F02", "F02_10", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("S05", "F05", "F05_10", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("S74", "F04", "F04_10", 5, 5);
insert into fournit (id_piece, siret, num_produit_catalogue, prix_piece, delai_approv) values ("S73", "F01", "F01_10", 5, 5);

-- table commande
-- commandes des individus
insert into commande (id_commande, date_commande, date_livraison, id_individu, id_boutique, id_adresse) values (1, "2021-05-01", "2021-05-02", "I01", null, 1);
insert into commande (id_commande, date_commande, date_livraison, id_individu, id_boutique, id_adresse) values (2, "2021-07-01", "2021-07-02", "I02", null, 2);
insert into commande (id_commande, date_commande, date_livraison, id_individu, id_boutique, id_adresse) values (3, "2021-02-01", "2021-02-02", "I03", null, 3);
insert into commande (id_commande, date_commande, date_livraison, id_individu, id_boutique, id_adresse) values (4, "2021-09-01", "2021-09-02", "I04", null, 4);
insert into commande (id_commande, date_commande, date_livraison, id_individu, id_boutique, id_adresse) values (5, "2021-01-01", "2021-01-02", "I05", null, 5);
-- commandes des boutiques
insert into commande (id_commande, date_commande, date_livraison, id_individu, id_boutique, id_adresse) values (6, "2021-03-04", "2021-03-05", NULL, "B01", 6);
insert into commande (id_commande, date_commande, date_livraison, id_individu, id_boutique, id_adresse) values (7, "2021-05-05", "2021-05-06", NULL, "B02", 7);
insert into commande (id_commande, date_commande, date_livraison, id_individu, id_boutique, id_adresse) values (8, "2021-08-02", "2021-08-03", NULL, "B03", 8);
insert into commande (id_commande, date_commande, date_livraison, id_individu, id_boutique, id_adresse) values (9, "2021-04-05", "2021-04-06", NULL, "B04", 9);
insert into commande (id_commande, date_commande, date_livraison, id_individu, id_boutique, id_adresse) values (10, "2021-01-08", "2021-01-09", NULL, "B05", 10);


-- table achat_piece
-- individus
insert into achat_piece (id_piece, siret, id_commande, quantite_piece) values ("P12", "F02", 1, 5);
insert into achat_piece (id_piece, siret, id_commande, quantite_piece) values ("P12", "F02",2, 4);
insert into achat_piece (id_piece, siret, id_commande, quantite_piece) values ("O2", "F03", 2, 8);
insert into achat_piece (id_piece, siret, id_commande, quantite_piece) values ("O2", "F02", 2, 1);
insert into achat_piece (id_piece, siret, id_commande, quantite_piece) values ("F3", "F02",4, 1);
-- boutiques
insert into achat_piece (id_piece, siret, id_commande, quantite_piece) values ("C34", "F03", 6, 1);
insert into achat_piece (id_piece, siret, id_commande, quantite_piece) values ("O4", "F01", 7, 1);
insert into achat_piece (id_piece, siret, id_commande, quantite_piece) values ("O2", "F03", 7, 1);
insert into achat_piece (id_piece, siret, id_commande, quantite_piece) values ("G9", "F04",7, 1);
insert into achat_piece (id_piece, siret, id_commande, quantite_piece) values ("DV133", "F01", 8, 1);

-- table achat_velo
-- individus
insert into achat_velo (id_bicyclette, id_commande, quantite_velo) values (101, 2, 1);
insert into achat_velo (id_bicyclette, id_commande, quantite_velo) values (102, 3, 1);
insert into achat_velo (id_bicyclette, id_commande, quantite_velo) values (103, 5, 1);
-- boutiques
insert into achat_velo (id_bicyclette, id_commande, quantite_velo) values (104, 7, 1);
insert into achat_velo (id_bicyclette, id_commande, quantite_velo) values (105, 7, 1);
insert into achat_velo (id_bicyclette, id_commande, quantite_velo) values (106, 8, 1);
insert into achat_velo (id_bicyclette, id_commande, quantite_velo) values (107, 9, 1);
insert into achat_velo (id_bicyclette, id_commande, quantite_velo) values (108, 9, 1);
insert into achat_velo (id_bicyclette, id_commande, quantite_velo) values (109, 9, 1);
insert into achat_velo (id_bicyclette, id_commande, quantite_velo) values (110, 9, 1);
insert into achat_velo (id_bicyclette, id_commande, quantite_velo) values (111, 10, 1);
insert into achat_velo (id_bicyclette, id_commande, quantite_velo) values (112, 10, 1);
insert into achat_velo (id_bicyclette, id_commande, quantite_velo) values (113, 10, 1);
insert into achat_velo (id_bicyclette, id_commande, quantite_velo) values (114, 10, 1);
insert into achat_velo (id_bicyclette, id_commande, quantite_velo) values (115, 10, 2);

-- Export XML & Json
-- select DISTINCT id_individu, nom_individu, prenom_individu, tel_individu, courriel_individu, id_adresse, num_program from individu natural join adhere;
-- select distinct siret, nom_entreprise, contact_fournisseur, note, id_adresse from fournisseur natural join piece_detachee where stock_piece < 20;

--  Gestion du stock, connaissance à tout moment du stoc des pièces et des vélos
-- stock par pièce
-- select id_piece,sum(stock_piece) from piece_detachee group by id_piece;
-- stock par fournisseur
-- select id_piece,stock_piece,siret from piece_detachee natural join fournit order by id_piece;
-- stock par vélo
-- select id_bicyclette, sum(stock_bicyclette) from bicyclette group by id_bicyclette;
-- stock par categorie de vélo
-- select ligne_produit,sum(stock_bicyclette) from bicyclette group by ligne_produit;
-- stock par marque de vélo
-- select nom_bicyclette, sum(stock_bicyclette) from bicyclette group by nom_bicyclette;
-- stock par description de pièce
-- select description, sum(stock_piece) from piece_detachee group by description;

-- Module Statistique
-- 1) rapport des quantités vendues de chaque item qui se trouve dans l'inventaire de VéloMax
-- select id_bicyclette as modèle,quantite_velo as quantité, prix_bicyclette as prix_unite, sum(quantite_velo*prix_bicyclette) as gainTotalparVeloMax from achat_velo natural join bicyclette group by id_bicyclette union select id_piece, sum(quantite_piece), prix_piece, sum(quantite_piece*prix_piece) from achat_piece natural join fournit group by id_piece,siret;

-- 2) liste des membres pour chaque programme d'adhésion et 3) afficher la liste d'expiration des adhésions
-- programme 1 Fidélio
-- select num_program,id_individu,nom_individu,prenom_individu,date_paiement_adhesion,date_add(date_paiement_adhesion, interval 1 YEAR) as date_expiration_adhesion from adhere natural join individu where num_program = '1 Fidélio' union
-- select num_program,id_individu,nom_individu,prenom_individu,date_paiement_adhesion,date_add(date_paiement_adhesion, interval 2 YEAR) as date_expiration_adhesion from adhere natural join individu where num_program = '2 Fidélio Or' union
-- select num_program,id_individu,nom_individu,prenom_individu,date_paiement_adhesion,date_add(date_paiement_adhesion, interval 2 YEAR) as date_expiration_adhesion from adhere natural join individu where num_program = '3 Fidélio Platine' union
-- select num_program,id_individu,nom_individu,prenom_individu,date_paiement_adhesion,date_add(date_paiement_adhesion, interval 3 YEAR) as date_expiration_adhesion from adhere natural join individu where num_program = '4 Fidélio Max';

-- 4)
-- les best clients (individus et boutiques) en fonction des quantités vendues en nombre de pièces vendues
-- select id_individu,nom_individu, prenom_individu,sum(quantite_piece),count(id_piece) from achat_piece natural join commande natural join individu where id_individu is not null group by id_individu order by sum(quantite_piece) desc;
-- select id_boutique,sum(quantite_piece),count(id_piece) from achat_piece natural join commande where id_boutique is not null group by id_boutique order by sum(quantite_piece) desc;
-- les best clients (individus et boutiques) en cumul en euros pour les pièces
-- select id_individu as id_client,id_commande,sum(prix_piece*quantite_piece) as prixTotalPiecesEn€,count(id_piece) as nombre_pieces from commande natural join achat_piece natural join fournit where id_individu is not null group by id_commande union
-- select id_boutique,id_commande,sum(prix_piece*quantite_piece),count(id_piece) from commande natural join achat_piece natural join fournit where id_boutique is not null group by id_commande;
-- les best clients (individus et boutiques) en cumul en euros pour les vélos
-- select id_individu as id_client,id_commande,sum(prix_bicyclette*quantite_velo) as prixTotalVeloEn€, count(id_bicyclette) as nombre_velos from commande natural join achat_velo natural join bicyclette where id_individu is not null group by id_commande union
-- select id_boutique,id_commande,sum(prix_bicyclette*quantite_velo), count(id_bicyclette) from commande natural join achat_velo natural join bicyclette where id_boutique is not null group by id_commande;

-- 5) Analyse des commandes
-- moyenne du nombre de pièces par commande
-- select id_commande, avg(quantite_piece) from commande natural join achat_piece natural join fournit group by id_commande;
-- moyenne du nombre de vélos par commande
-- select id_commande,avg(quantite_velo) from commande natural join achat_velo natural join bicyclette group by id_commande;