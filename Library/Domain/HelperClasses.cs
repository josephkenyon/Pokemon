using Microsoft.Xna.Framework;

namespace Library.Domain
{
    public class Vector
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector()
        {
            X = 0;
            Y = 0;
        }

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vector(float x, float y)
        {
            Point point = new Vector2((float)x, (float)y).ToPoint();
            X = point.X;
            Y = point.Y;
        }

        public Vector(int value)
        {
            X = value;
            Y = value;
        }

        public Vector(float value)
        {
            Point point = new Vector2(value, value).ToPoint();
            X = point.X;
            Y = point.Y;
        }


        public Vector(Point point)
        {
            X = point.X;
            Y = point.Y;
        }

        public Vector(Vector2 vector2)
        {
            X = vector2.ToPoint().X;
            Y = vector2.ToPoint().Y;
        }

        public static Vector One => new Vector(1);
        public static Vector Zero => new Vector(0);

        public Point ToPoint() => new Point(X, Y);
        public Vector2 ToVector2() => ToPoint().ToVector2();

        public override bool Equals(object o)
        {
            if (o == null)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            return X;
        }
        public static bool operator ==(Vector a, Vector b) => a.X == b.X && a.Y == b.Y;
        public static bool operator !=(Vector a, Vector b) => a.X != b.X || a.Y != b.Y;
        public static Vector operator +(Vector a, Vector b) => new Vector { X = a.X + b.X, Y = a.Y + b.Y };
        public static Vector operator -(Vector a, Vector b) => new Vector { X = a.X - b.X, Y = a.Y - b.Y };
        public static Vector operator *(Vector a, Vector b) => new Vector { X = a.X * b.X, Y = a.Y * b.Y };
        public static Vector operator *(Vector a, float scaler) => new Vector(a.ToVector2() * scaler);
        public static Vector operator *(Vector a, int scaler) => new Vector { X = a.X * scaler, Y = a.Y * scaler };
        public static Vector operator /(Vector a, int scaler) => new Vector { X = a.X / scaler, Y = a.Y / scaler };
    }
}