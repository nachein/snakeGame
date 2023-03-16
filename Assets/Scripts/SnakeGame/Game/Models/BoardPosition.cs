namespace SnakeGame.Game.Models
{
    public class BoardPosition
    {
        public BoardPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X;
        public int Y;

        public static BoardPosition operator+ (BoardPosition a, BoardPosition b)
        {
            return new BoardPosition(a.X + b.X, a.Y + b.Y);
        }
    }
}