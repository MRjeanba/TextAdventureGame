using System;

namespace Labo_TDZ_final02
{
    class Niveau
    {
        public string[,] Carte { get; set; }
        public Position2D PosJoueur { get; set; }
        public Position2D PosEnnemi { get; set; }
        public Position2D PosSortie { get; set; }

        public Niveau()
        {
            Carte = new string[,] {
                    {"#","#","#","#","#","#","#"},
                    {"#"," "," "," "," "," ","#"},
                    {"#"," "," "," "," "," ","#"},
                    {"#"," "," "," "," "," ","#"},
                    {"#"," "," "," "," "," ","#"},
                    {"#"," "," "," "," "," ","#"},
                    {"#","#","#","#","#","#","#"}
                };

            this.PosJoueur = new Position2D(1, 1);
            this.PosSortie = new Position2D(this.Carte.GetLength(1) - 2, this.Carte.GetLength(0) - 2);
            PlacerEnnemi();
            this.Carte[this.PosJoueur.posY, this.PosJoueur.posX] = "J";
            this.Carte[this.PosSortie.posY, this.PosSortie.posX] = "S";
        }

        public void AfficherCarte()
        {
            for (int i = 0; i < this.Carte.GetLength(0); i++)
            {
                for (int j = 0; j < this.Carte.GetLength(1); j++)
                {
                    if (this.Carte[i, j] == "J")
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write($" {this.Carte[i, j]}");
                    }
                    else if (this.Carte[i, j] == "S")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($" {this.Carte[i, j]}");
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.Write($" {this.Carte[i, j]}");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("A: Gauche  D: Droite  W: Haut  S: Bas");
        }

        public void DeplacerJoueur(string direction)
        {
            direction = direction.ToUpper();
            switch (direction)
            {
                case "A":
                    DeplacerGauche();
                    break;
                case "D":
                    DeplacerDroite();
                    break;
                case "W":
                    DeplacerHaut();
                    break;
                case "S":
                    DeplacerBas();
                    break;
            }
            this.Carte[this.PosJoueur.posY, this.PosJoueur.posX] = "J";
        }

        public void DetruireEnnemi()
        {
            this.PosEnnemi.Deplacer(-1, -1);
        }

        public bool estSortie()
        {
            return PosJoueur.Equals(PosSortie);
        }

        public bool estCombat()
        {
            return PosJoueur.Equals(PosEnnemi);
        }

        private void PlacerEnnemi()
        {
            Random rng = new Random();
            Position2D position = new Position2D(1, 1);
            while (position.Equals(this.PosJoueur) || position.Equals(this.PosSortie))
            {
                position.posX = rng.Next(1, this.Carte.GetLength(1) - 1);
                position.posY = rng.Next(1, this.Carte.GetLength(0) - 1);
            }

            this.PosEnnemi = position;
        }

        private void DeplacerGauche()
        {
            if (this.PosJoueur.posX > 1)
            {
                this.Carte[this.PosJoueur.posY, this.PosJoueur.posX] = " ";
                this.PosJoueur.posX--;
            }
        }

        private void DeplacerDroite()
        {
            if (this.PosJoueur.posX < this.Carte.GetLength(1) - 2)
            {
                this.Carte[this.PosJoueur.posY, this.PosJoueur.posX] = " ";
                this.PosJoueur.posX++;
            }
        }

        private void DeplacerHaut()
        {
            if (this.PosJoueur.posY > 1)
            {
                this.Carte[this.PosJoueur.posY, this.PosJoueur.posX] = " ";
                this.PosJoueur.posY--;
            }
        }

        private void DeplacerBas()
        {
            if (this.PosJoueur.posY < this.Carte.GetLength(0) - 2)
            {
                this.Carte[this.PosJoueur.posY, this.PosJoueur.posX] = " ";
                this.PosJoueur.posY++;
            }
        }
    }
}