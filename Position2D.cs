namespace Labo_TDZ_final02
{
    class Position2D
    {
        public int posX { get; set; }
        public int posY { get; set; }

        public Position2D(int x, int y)
        {
            this.posX = x;
            this.posY = y;
        }

        public void Deplacer(int x, int y)
        {
            this.posX = x;
            this.posY = y;
        }

        public override bool Equals(object obj)
        {
            return (obj is Position2D) && ((Position2D)obj).posX == this.posX && ((Position2D)obj).posY == this.posY;
        }
        
        public override int GetHashCode()
        {
            return this.posX + this.posY;
        }
    }
}