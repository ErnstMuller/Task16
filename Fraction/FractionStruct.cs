using System;

namespace FractionStruct
{
    public struct Fraction
    {
        public int P { get; set; }
        private int q;

        public int Q
        {
            get => q;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Знаменатель должен быть положительным числом");
                q = value;
            }
        }

        public double Value => (double)P / Q;

        public Fraction(int p, int q) : this()
        {
            P = p;
            Q = q;
        }

        public override string ToString() => $"{P}/{Q}";

        public override bool Equals(object obj)
        {
            if (obj is Fraction other)
            {
                return P * other.Q == other.P * Q;
            }
            throw new ArgumentException("Объект для сравнения не является дробью");
        }

        public override int GetHashCode()
        {
            int gcd = GCD(Math.Abs(P), Math.Abs(Q));
            int simplifiedP = P / gcd;
            int simplifiedQ = Q / gcd;

            unchecked
            {
                int hash = 17;
                const int prime = 23;
                hash = hash * prime + simplifiedP.GetHashCode();
                hash = hash * prime + simplifiedQ.GetHashCode();
                return hash;
            }
        }

        private static int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public static bool operator ==(Fraction left, Fraction right) => left.Equals(right);
        public static bool operator !=(Fraction left, Fraction right) => !left.Equals(right);

        public static Fraction operator +(Fraction left, Fraction right)
        {
            int newP = left.P * right.Q + right.P * left.Q;
            int newQ = left.Q * right.Q;
            return new Fraction(newP, newQ);
        }

        public static Fraction operator -(Fraction left, Fraction right)
        {
            int newP = left.P * right.Q - right.P * left.Q;
            int newQ = left.Q * right.Q;
            return new Fraction(newP, newQ);
        }

        public static Fraction operator *(Fraction left, Fraction right)
        {
            int newP = left.P * right.P;
            int newQ = left.Q * right.Q;
            return new Fraction(newP, newQ);
        }

        public static Fraction operator /(Fraction left, Fraction right)
        {
            if (right.P == 0)
                throw new DivideByZeroException("Деление на нулевую дробь невозможно");

            int newP = left.P * right.Q;
            int newQ = left.Q * right.P;

            if (newQ < 0)
            {
                newP = -newP;
                newQ = -newQ;
            }

            return new Fraction(newP, newQ);
        }
    }
}