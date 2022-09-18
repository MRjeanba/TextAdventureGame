/**
* Laboratoire final pour la tour de zork
* 
* Nous cérons la classe finale qui contrôle le jeux
* Elle contient un attribut essentiel qui permet au programme principal d'appeler les bonnes méthodes
* EtatJeux nous permet de savoir ce que nous sommes en train de faire :
* -1 : Quitter le programme
*  0 : Afficher le menu principal
*  1 : Créer le personnage du joueur
*  2 : Explorer le niveau
*  3 : Combattre un ennemi
*  4 : Changer de niveau
* 
* Création : 21-11- 25
* Par : Frédérik Taleb
*
* Modification: 21-12-01
* Par : Frédérik Taleb
*/

namespace Labo_TDZ_final02
{
    class Program
    {
        static void Main(string[] args)
        {
            TourDeZork tdz = new TourDeZork();

            while (tdz.EtatJeux >= 0)
            {
                switch (tdz.EtatJeux)
                {
                    case 1:
                        tdz.CreerPersonnage();
                        break;
                    case 2:
                        tdz.ExplorerNiveau();
                        break;
                    case 3:
                        tdz.Combattre();
                        break;
                    case 4:
                        tdz.ChangerNiveau();
                        break;
                    default:
                        tdz.AfficherMenuPrincipal();
                        break;
                }
            }
        }
    }
}
