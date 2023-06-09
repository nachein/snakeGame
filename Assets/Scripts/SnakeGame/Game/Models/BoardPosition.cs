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

        public override bool Equals(object otherObject)
        {
            if (!(otherObject is BoardPosition))
            {
                return false;
            }

            var otherBoardPosition = (BoardPosition) otherObject;
            return X == otherBoardPosition.X && Y == otherBoardPosition.Y;
        }
    }
}