using System;

namespace Labo_TDZ_final02
{
    /**
    
    Classe acteur utilisée pour instantier tous les personnages du jeu, Ennemis mais aussi Joueur
    
    */
    class Acteur
    {
        public string Nom { get; set; }
        public int MaxHp { get; set; }
        public int Hp { get; set; }
        public int MaxArmure { get; set; }
        public int RegenArmure { get; set; }
        public int Armure { get; set; }
        public int Agilite { get; set; }
        public int Dommage { get; set; }
        public int TauxCritique { get; set; }
        public string Description { get; set; }

        public Acteur(string nom, int maxHp, int maxArmure, int regenArmure, int agilite, int dommage, string description)
        {
            this.Nom = nom;
            this.MaxHp = maxHp;
            this.Hp = maxHp;
            this.MaxArmure = maxArmure;
            this.Armure = maxArmure;
            this.RegenArmure = regenArmure;
            this.Agilite = agilite;
            this.Dommage = dommage;
            this.TauxCritique = 100 - Agilite / 2;
            this.Description = description;
        }

        /**
            Permet au joueur,ennemis d'attaquer une cible
            @param: Acteur defenseur l'instance qui subit l'attaque

            la méthode génère un nombre aléatoire comprit entre 1 et 100 dépendamment du nombre obtenu, la puissance de l'attaque va être modifiée
        */
        public void Attaquer(Acteur defenseur)
        {
            Random rng = new Random();
            int touche = rng.Next(1, 101);  //touche est généré aléatoirement un peu a la manière des jeux donjons et dragons, plus le nombre obtenu est important plus dommageFinal sera important
            int dommageFinal = this.Dommage;

            if (touche >= this.TauxCritique)
            {
                dommageFinal = (dommageFinal * 3 / 2);
                defenseur.Defendre(dommageFinal);
                Console.WriteLine($"{this.Nom} a porté un coup critique! {defenseur.Nom} reçoit {dommageFinal} points de dommage.");
            }
            else if (touche <= defenseur.Agilite)
            {
                Console.WriteLine($"{defenseur.Nom} a évité l'attaque.");
            }
            else
            {
                defenseur.Defendre(dommageFinal);
                Console.WriteLine($"{defenseur.Nom} reçoit {dommageFinal} points de dommage.");
            }
        }

        /**
        *  Cette méthode a pour but de correctement attribuer les dommages causés par l'attaquant sur la cible, pour cela on doit vérifier à combien est son armure
        * @param dommage : les degats recus par l'auteur de l'attaque
        *  si l'instance qui appelle la méthode a encore de l'armure, on retire son armure, si dommage est supérieur a armure, on retire des points de vie
        */
        public void Defendre(int dommage)
        {
            this.Armure -= dommage;
            if (this.Armure < 0)
            {
                this.Hp += this.Armure;  //+=  car ici this armure est négatif à cause du précédent calcul
                this.Armure = 0;
            }
            if (this.Armure + this.RegenArmure > this.MaxArmure)
            {
                this.Armure = this.MaxArmure;
            }
            else
            {
                this.Armure += this.RegenArmure;
            }

        }

        /**
            Cette méthode sert a controler la phase de combat, tant que cette méthode retourne vrai, le combat continue, les deux acteurs sont en vie.
        */
        public bool estVivant()
        {
            return !(this.Hp <= 0);
        }

        /**
            Cette méthode affiche à l'écran l'état des acteurs (points de vie armure)
        */
        public void AfficherEtat()
        {
            Console.WriteLine($"{this.Nom}, Hp : {this.Hp}, Armure : {this.Armure}");
        }
    }
}