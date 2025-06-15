using NUnit.Framework;
using FractionStruct;


namespace FractionStruct.UnitTests
{
    [TestFixture]
    public class FractionTests
    {
        [Test]
        public void ConstructorTest()
        {
            var fraction = new Fraction(3, 4);
            Assert.That(fraction.P, Is.EqualTo(3));
            Assert.That(fraction.Q, Is.EqualTo(4));
        }

        [Test]
        public void QSet_NonPositiveValue_ArgumentException()
        {
            var fraction = new Fraction();
            Assert.That(() => fraction.Q = 0, Throws.ArgumentException);
            Assert.That(() => fraction.Q = -5, Throws.ArgumentException);
        }

        [Test]
        public void ValueTest()
        {
            var fraction1 = new Fraction(1, 2);
            Assert.That(fraction1.Value, Is.EqualTo(0.5).Within(1e-13));

            var fraction2 = new Fraction(-3, 4);
            Assert.That(fraction2.Value, Is.EqualTo(-0.75).Within(1e-13));

            var fraction3 = new Fraction(0, 1);
            Assert.That(fraction3.Value, Is.EqualTo(0).Within(1e-13));
        }

        [TestCase(1, 2, "1/2")]
        [TestCase(-3, 4, "-3/4")]
        [TestCase(0, 1, "0/1")]
        public void ToStringTest(int p, int q, string result)
        {
            var fraction = new Fraction(p, q);
            Assert.That(fraction.ToString(), Is.EqualTo(result));
        }

        [TestCase(1, 2, 2, 4, true)]
        [TestCase(1, 2, 3, 6, true)]
        [TestCase(1, 2, 1, 3, false)]
        [TestCase(-1, 2, -2, 4, true)]
        public void Equals_TwoFractions_ExpectedResult(int p1, int q1, int p2, int q2, bool result)
        {
            var fraction1 = new Fraction(p1, q1);
            var fraction2 = new Fraction(p2, q2);
            Assert.That(fraction1.Equals(fraction2), Is.EqualTo(result));
        }

        [Test]
        public void Equals_WrongArgument_ArgumentException()
        {
            var fraction = new Fraction(1, 2);
            var obj = new object();
            Assert.That(() => fraction.Equals(obj), Throws.ArgumentException);
        }

        [Test]
        public void GetHashCodeTest()
        {
            var x = new Fraction(1, 2);
            var y = new Fraction(2, 4);
            var z = new Fraction(1, 3);

            Assert.That(x.GetHashCode(), Is.EqualTo(y.GetHashCode()));
            Assert.That(x.GetHashCode(), Is.Not.EqualTo(z.GetHashCode()));
        }

        [Test]
        public void ComparisonTest()
        {
            var x = new Fraction(1, 2);
            var y = new Fraction(2, 4);
            var z = new Fraction(1, 3);

            Assert.That(x == y, Is.True);
            Assert.That(x != y, Is.False);
            Assert.That(x == z, Is.False);
            Assert.That(x != z, Is.True);
        }

        [TestCase(1, 2, 1, 3, 5, 6)]
        [TestCase(1, 2, -1, 4, 1, 4)]
        [TestCase(-1, 3, -1, 6, -1, 2)]
        public void AdditionTest(int p1, int q1, int p2, int q2, int resultP, int resultQ)
        {
            var fraction1 = new Fraction(p1, q1);
            var fraction2 = new Fraction(p2, q2);
            var result = new Fraction(resultP, resultQ);

            Assert.That(fraction1 + fraction2, Is.EqualTo(result));
        }

        [TestCase(1, 2, 1, 3, 1, 6)]
        [TestCase(1, 2, -1, 4, 3, 4)]
        [TestCase(-1, 3, -1, 6, -1, 6)]
        public void SubtractionTest(int p1, int q1, int p2, int q2, int resultP, int resultQ)
        {
            var fraction1 = new Fraction(p1, q1);
            var fraction2 = new Fraction(p2, q2);
            var result = new Fraction(resultP, resultQ);

            Assert.That(fraction1 - fraction2, Is.EqualTo(result));
        }

        [TestCase(1, 2, 2, 3, 1, 3)]
        [TestCase(3, 4, -2, 5, -3, 10)]
        [TestCase(-1, 3, -3, 4, 1, 4)]
        public void MultiplicationTest(int p1, int q1, int p2, int q2, int resultP, int resultQ)
        {
            var fraction1 = new Fraction(p1, q1);
            var fraction2 = new Fraction(p2, q2);
            var result = new Fraction(resultP, resultQ);

            Assert.That(fraction1 * fraction2, Is.EqualTo(result));
        }

        [TestCase(1, 2, 2, 3, 3, 4)]
        [TestCase(3, 4, -2, 5, -15, 8)]
        [TestCase(-1, 3, -3, 4, 4, 9)]
        public void DivisionTest(int p1, int q1, int p2, int q2, int resultP, int resultQ)
        {
            var fraction1 = new Fraction(p1, q1);
            var fraction2 = new Fraction(p2, q2);
            var result = new Fraction(resultP, resultQ);

            Assert.That(fraction1 / fraction2, Is.EqualTo(result));
        }

        [Test]
        public void Division_ByZero_DivideByZeroException()
        {
            var fraction1 = new Fraction(1, 2);
            var fraction2 = new Fraction(0, 1);

            Assert.That(() => fraction1 / fraction2, Throws.TypeOf<DivideByZeroException>());
        }
    }
}