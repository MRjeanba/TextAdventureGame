/**
* Classe TourDeZork
* 
* Contrôle le jeu Tour De Zork
* Crée les ennemis en utilisant le tableau comme référence
* Tableau de référence généré à partir du fichier ennemis.txt
* Utilise les classes Acteur, GenerateurClasse et Niveau pour les tâches spécifiques
* 
* Création : 21-11- 25
* Par : Frédérik Taleb/Jean Baptiste Garibo
*
* Modification: 21-12-01
* Par : Frédérik Taleb/Jean Baptiste Garibo
*/
using System;
using System.IO;

namespace Labo_TDZ_final02
{
    class TourDeZork
    {
        public Acteur[] Ennemis { get; set; }
        public Acteur Joueur { get; set; }
        public Acteur Ennemi { get; set; }
        public Niveau Niveau { get; set; }
        public int EtatJeux { get; set; } = 0;
        public int Etage {get; set;}=0;

        /**
        * Constructeur qui :
        * 
        * Lit le fichier ennemis.txt
        * Crée un Acteur avec chaque ligne lue
        * Insère les acteurs créés dans le tableau des ennemis
        *
        */
        public TourDeZork()
        {
            // Initialiser le tableau des ennemis selon le nombre de lignes dans le fichier ennemis.txt
            StreamReader lecteur = new StreamReader(@"ennemis.txt");
            string [] ennemi = new string [6]; 
            this.Ennemis = new Acteur[4];
            this.Joueur = new Acteur("",0,0,0,0,0,"");
            this.Ennemi = new Acteur("",0,0,0,0,0,"");

            string infoEnnemi = "";
            int i =0;
            
            while (!lecteur.EndOfStream)
            {
                infoEnnemi = lecteur.ReadLine();
                ennemi = infoEnnemi.Split(';');
                this.Ennemis[i]=DecoderEnnemi(ennemi);
                i++;
            }
            lecteur.Close();
            this.Niveau = new Niveau();
            
            CreerEnnemi();

        }
        /**
        * Méthode qui affiche le menu principal du jeu
        */
        public void AfficherMenuPrincipal()
        {
            // Afficher le menu principal
            int choix =0;
            Console.WriteLine("Bienvenue à la tour de Zork, jusqu'où pourras-tu aller?\n  1.Nouvelle partie\n  2.Quitter");
            int.TryParse(Console.ReadLine(), out choix);
            while (choix!=1 && choix!=2)
            {
                Console.WriteLine("Veullez choisir un nombre entier entre 1 et 2. \n 1 = Nouvelle partie. \n 2= Quitter.");
                int.TryParse(Console.ReadLine(), out choix);
            }
            switch (choix)
            {
                case 1:
                    EtatJeux = 1;
                break;
                
                case 2:
                    EtatJeux = -1;
                break;

                default:
                break;
            }

            // Valider que la réponse au menu est un nombre entier et qu'il s'agit de 1 ou 2

            // Si la réponse est Quitter (2) assigner -1 à EtatJeux
            // Si la réponse est Nouvelle partie (1) assigner 1 à EtatJeux
        }
        /**
        * Méthode qui crée le personnage principal en utilisant 
        * une instance de la classe GenerateurClasse
        * Passe en mode exploration une fois le joueur créé
        */
        public void CreerPersonnage()
        {
            // Créer une instance de la Classe GenerateurClasse
            GenerateurClasse Generer = new GenerateurClasse();
            // Utiliser la méthode GenererClasse pour assigner à l'attribut joueur une instance de la classe Acteur
            this.Joueur = Generer.GenererClasse();
            // Assigner 2 à EtatJeux
            EtatJeux = 2;
        }
        /**
        * Méthode qui permet l'exploration d'un étage
        * Elle utilise Niveau et ses méthodes pour afficher la carte et déplacer le joueur
        *  tant que l'état du jeu est à 2
        */
        public void ExplorerNiveau()
        {
            // Tant que EtatJeux est à 2
            while (EtatJeux ==2)
            {
                Console.Clear();
                Console.WriteLine($" Étage numéro :{Etage}");
                this.Niveau.AfficherCarte();
                string direction = Console.ReadLine();
                this.Niveau.DeplacerJoueur(direction);
                if (this.Niveau.estCombat())
                {
                    EtatJeux = 3;
                }
                else if (this.Niveau.estSortie())
                {
                    EtatJeux =4;
                    Etage++;
                }
            }
            
        }
        /**
        * Méthode qui gère le combat entre le personnage et son ennemi
        * La logique est presque exactement la même que celle du dernier laboratoire sur le combat
        * On utilise une initiative aléatoire : agilite + nb entre 1 et 10
        * Quand le joueur gagne on revient à l'état d'exploration (2)
        * S'il perd on revient au menu principal (état 1)
        */
        public void Combattre()
        {
            // Initialiser les variables et instance pour l'initiative
            int initiativeJ=0;
            int initiativeE=0;
            Random random = new Random();
            Random random1 = new Random();
            // Tant que les 2 opposants sont vivants
            while (Joueur.estVivant() && Ennemi.estVivant())
            {
                Console.Clear();
                Console.WriteLine($"Vous allez affronter {Ennemi.Nom} \n Appuyez sur une touche pour continuer");
                Console.ReadKey();
                Joueur.AfficherEtat();
                Console.ReadKey();
                Ennemi.AfficherEtat();
                Console.ReadKey();
                int rng1 = random.Next(1,11);
                int rng2 = random1.Next(1,11);
                initiativeJ = Joueur.Agilite + rng1;
                initiativeE = Joueur.Agilite + rng2;
                if (initiativeJ > initiativeE)
                {
                    Joueur.Attaquer(Ennemi);
                    if (Ennemi.estVivant())
                    {
                        Ennemi.Attaquer(Joueur);
                    }
                }
                else if (initiativeE > initiativeJ)
                {
                    Ennemi.Attaquer(Joueur);
                    if (Joueur.estVivant())
                    {
                        Joueur.Attaquer(Ennemi);
                    }
                }
                else if (initiativeJ == initiativeE)
                {
                    Joueur.Attaquer(Ennemi);
                    Ennemi.Attaquer(Joueur);
                }
                Console.WriteLine("Veuillez appuyer sur une touche pour continuer le combat");
                Console.ReadKey();
            }
            
            if (Joueur.estVivant())
            {
                Console.Clear();
                Console.WriteLine("Bravo ! tu remportes la victoire !!! \n Appuyer sur une touche pour continuer !");
                Console.ReadKey();
                this.Niveau.DetruireEnnemi();
                EtatJeux = 2;
            }
            else if (!Joueur.estVivant())
            {
                Console.Clear();
                Console.WriteLine($"Malheureusement, vous êtes vaincu. Vous êtes parvenu jusqu'à l'étage {Etage} \n Veuillez appuyer sur une touche pour continuer");
                Console.ReadKey();
                this.Niveau = new Niveau();
                CreerEnnemi();
                EtatJeux = 0;
            }
            
        }
        /**
        * Méthode qui gère le changement de niveau et la création du nouvel ennemi pour ce niveau
        */
        public void ChangerNiveau()
        {
            // Nettoyer la console
            Console.Clear();
            Console.WriteLine("Vous allez changer de niveau, appuyer sur une touche pour continuer");
            Console.ReadKey();
            this.Niveau = new Niveau();
            this.Ennemi = new Acteur("",0,0,0,0,0,"");
            CreerEnnemi();
            EtatJeux = 2;
            
        }
        /**
        * Méthode qui utilise le tableau des modèles d'ennemi pour créer une nouvelle instance 
        * et l'assigner à l'attribut correspondant... Ennemi
        */
        private void CreerEnnemi()
        {
            // Utiliser un nombre aléatoire pour sélectionner une case du tableau Ennemis
            Random random = new Random();
            int rng = random.Next(0,4);
            this.Ennemi = new Acteur(Ennemis[rng].Nom,Ennemis[rng].Hp,Ennemis[rng].Armure,Ennemis[rng].RegenArmure,Ennemis[rng].Agilite,Ennemis[rng].Dommage,Ennemis[rng].Description);
            // Assigner une nouvelle instance d'Acteur à l'attribut Ennemi
            // Pour le constructeur de Acteur on copie les attributs de l'ennemi dans les paramètres
        }
        /**
        * Méthode qui transforme le tableau des stats d'ennemis en une instance de la classe Acteur
        * et retourne cette instance
        *
        * @return une instance de la classe Acteur
        */
        private Acteur DecoderEnnemi(string[] ennemi)
        {
            // Utiliser les cases du tableau pour remplire les paramètres 
            int hp = 0;
            int armure = 0;
            int regenarmure=0;
            int agilite =0;
            int dommage = 0;
            int.TryParse(ennemi[1], out hp);
            int.TryParse(ennemi[2], out armure);
            int.TryParse(ennemi[3], out regenarmure);
            int.TryParse(ennemi[4], out agilite);
            int.TryParse(ennemi[5], out dommage);
            Acteur Ennemi = new Acteur(ennemi[0],hp,armure,regenarmure,agilite,dommage,ennemi[6]);

            return Ennemi;
        }
    }
}