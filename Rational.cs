using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace FactorioRateCalculator
{
    internal readonly struct Rational<T>(T enumerator, T denominator) : INumber<Rational<T>> where T : struct, IBinaryInteger<T>, ISignedNumber<T>
    {
        public T Denominator { get; } = denominator != T.Zero ? T.Abs(denominator) : throw new ArgumentException($"The {nameof(Denominator)} field of type {nameof(Rational<T>)} cannot be {T.Zero}.");

        public T Enumerator { get; } = T.Abs(enumerator);

        public bool isNegative { get; } = T.IsNegative(enumerator) ^ T.IsNegative(denominator);

        public bool IsSimplified => Utils.GreatestCommonDenominator(Enumerator, Denominator) == T.One;

        public Rational<T> Simplified
        {
            get
            {
                T gcd = Utils.GreatestCommonDenominator(Enumerator, Denominator);
                return gcd == T.One ? this : new Rational<T>(Enumerator / gcd, !isNegative ? Denominator / gcd : -Denominator / gcd);
            }
        }

        public T ValueI => Enumerator / Denominator;

        public Half ValueH => CalculateValue<Half>(Enumerator, Denominator);

        public float ValueF => CalculateValue<float>(Enumerator, Denominator);

        public double ValueD => CalculateValue<double>(Enumerator, Denominator);

        public decimal ValueM => CalculateValue<decimal>(Enumerator, Denominator);

        public static Rational<T> One => new Rational<T>(T.One, T.One);

        public static int Radix => 2;

        public static Rational<T> Zero => new Rational<T>(T.Zero, T.One);

        public static Rational<T> AdditiveIdentity => Zero;

        public static Rational<T> MultiplicativeIdentity => One;

        public static Rational<T> Abs(Rational<T> value) => value >= Zero ? value : -value;

        static TValue CalculateValue<TValue>(T enumerator, T denominator) where TValue : IFloatingPoint<TValue> => checked(TValue.CreateChecked(enumerator) / TValue.CreateChecked(denominator));

        public static (Rational<T> a, Rational<T> b) BringToCommonDenominator(Rational<T> a, Rational<T> b, bool simplifyFirst = true)
        {
            if (simplifyFirst)
            {
                a = a.Simplified;
                b = b.Simplified;
            }
            T commonDenominator = Utils.SmallestCommonMultiple(a.Denominator, b.Denominator);
            return (
                a.RecalculateWithNewDenominator(commonDenominator, false),
                b.RecalculateWithNewDenominator(commonDenominator, false)
            );
        }

        public static bool IsCanonical(Rational<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsComplexNumber(Rational<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsEvenInteger(Rational<T> value) => IsInteger(value) && value.Enumerator / value.Denominator % (T.One + T.One) == T.Zero;

        public static bool IsFinite(Rational<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsImaginaryNumber(Rational<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsInfinity(Rational<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsInteger(Rational<T> value) => value.Enumerator % value.Denominator == T.Zero;

        public static bool IsNaN(Rational<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsNegative(Rational<T> value) => value.isNegative;

        public static bool IsNegativeInfinity(Rational<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsNormal(Rational<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsOddInteger(Rational<T> value) => IsInteger(value) && value.Enumerator / value.Denominator % (T.One + T.One) == T.One;

        public static bool IsPositive(Rational<T> value) => !value.isNegative;

        public static bool IsPositiveInfinity(Rational<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsRealNumber(Rational<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsSubnormal(Rational<T> value)
        {
            throw new NotImplementedException();
        }

        public static bool IsZero(Rational<T> value) => value.Enumerator == T.Zero;

        public static Rational<T> MaxMagnitude(Rational<T> x, Rational<T> y)
        {
            throw new NotImplementedException();
        }

        public static Rational<T> MaxMagnitudeNumber(Rational<T> x, Rational<T> y)
        {
            throw new NotImplementedException();
        }

        public static Rational<T> MinMagnitude(Rational<T> x, Rational<T> y)
        {
            throw new NotImplementedException();
        }

        public static Rational<T> MinMagnitudeNumber(Rational<T> x, Rational<T> y)
        {
            throw new NotImplementedException();
        }

        public static Rational<T> Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static Rational<T> Parse(string s, NumberStyles style, IFormatProvider? provider) => Parse(s.AsSpan(), style, provider);

        public static Rational<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static Rational<T> Parse(string s, IFormatProvider? provider) => Parse(s.AsSpan(), provider);

        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out Rational<T> result)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out Rational<T> result)
        {
            if (s is not null)
            {
                return TryParse(s.AsSpan(), style, provider, out result);
            }
            else
            {
                result = default;
                return false;
            }
        }

        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Rational<T> result)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Rational<T> result)
        {
            if (s is not null)
            {
                return TryParse(s.AsSpan(), provider, out result);
            }
            else
            {
                result = default;
                return false;
            }
        }

        public int CompareTo(object? obj) => obj is not null
            ? obj is Rational<T> r
                ? CompareTo(r)
                : throw new InvalidOperationException($"Cannot compare objects of type {obj.GetType().Name} and {nameof(Rational<T>)}.")
            : 1
        ;

        public int CompareTo(Rational<T> other) => this != other
            ? this > other
                ? 1
                : -1
            : 0
        ;

        public bool Equals(Rational<T> other) => this == other;

        public override bool Equals([NotNullWhen(true)] object? obj) => obj is not null && obj is Rational<T> r && Equals(r);

        public override int GetHashCode() => HashCode.Combine(Enumerator, Denominator, isNegative);

        public string ToString(string? format, IFormatProvider? formatProvider) => ToString();

        public override string ToString() => $"{Enumerator}/{Denominator}";

        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertFromChecked<TOther>(TOther value, out Rational<T> result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertFromSaturating<TOther>(TOther value, out Rational<T> result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertFromTruncating<TOther>(TOther value, out Rational<T> result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertToChecked<TOther>(Rational<T> value, out TOther result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertToSaturating<TOther>(Rational<T> value, out TOther result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertToTruncating<TOther>(Rational<T> value, out TOther result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static Rational<T> operator +(Rational<T> value) => value;

        public static Rational<T> operator +(Rational<T> left, Rational<T> right)
#if true
            => new Rational<T>(
                (!left.isNegative ? left.Enumerator : -left.Enumerator) * right.Denominator +
                (!right.isNegative ? right.Enumerator : -right.Enumerator) * left.Denominator,
                left.Denominator * right.Denominator
            )
#if true
            .Simplified
#endif
        ;
#else
        {
            left = left.Simplified;
            right = right.Simplified;
            T commonDenominator = Utils.SmallestCommonMultiple(left.Denominator, right.Denominator);
            return new Rational<T>(
                (!left.isNegative ? left.Enumerator : -left.Enumerator) * (left.Denominator == commonDenominator ? commonDenominator : commonDenominator / left.Denominator) +
                (!right.isNegative ? right.Enumerator : -right.Enumerator) * (right.Denominator == commonDenominator ? commonDenominator : commonDenominator / right.Denominator),
                commonDenominator
            );
        }
#endif

        public static Rational<T> operator -(Rational<T> value) => new Rational<T>(value.Enumerator, value.isNegative ? value.Denominator : -value.Denominator);

        public static Rational<T> operator -(Rational<T> left, Rational<T> right)
#if true
            => new Rational<T>(
                (!left.isNegative ? left.Enumerator : -left.Enumerator) * right.Denominator -
                (!right.isNegative ? right.Enumerator : -right.Enumerator) * left.Denominator,
                left.Denominator * right.Denominator
            )
#if true
            .Simplified
#endif
        ;
#else
        {
            left = left.Simplified;
            right = right.Simplified;
            T commonDenominator = Utils.SmallestCommonMultiple(left.Denominator, right.Denominator);
            return new Rational<T>(
                (!left.isNegative ? left.Enumerator : -left.Enumerator) * (left.Denominator == commonDenominator ? commonDenominator : commonDenominator / left.Denominator) -
                (!right.isNegative ? right.Enumerator : -right.Enumerator) * (right.Denominator == commonDenominator ? commonDenominator : commonDenominator / right.Denominator),
                commonDenominator
            );
        }
#endif

        public static Rational<T> operator ++(Rational<T> value) => value + T.One;

        public static Rational<T> operator --(Rational<T> value) => value - T.One;

        public static Rational<T> operator *(Rational<T> left, Rational<T> right) => new Rational<T>(left.Enumerator * right.Enumerator, left.Denominator * (left.isNegative ^ right.isNegative ? -right.Denominator : right.Denominator))
#if true
            .Simplified
#endif
        ;

        public static Rational<T> operator /(Rational<T> left, Rational<T> right) => new Rational<T>(left.Enumerator * right.Denominator, left.Denominator * (left.isNegative ^ right.isNegative ? -right.Enumerator : right.Enumerator))
#if true
            .Simplified
#endif
        ;

        public static Rational<T> operator %(Rational<T> left, Rational<T> right)
        {
            (Rational<T> a, Rational<T> b) = BringToCommonDenominator(left, right);
            return new Rational<T>(a.Enumerator % b.Enumerator, !a.isNegative ? a.Denominator : -a.Denominator);
        }

        public static bool operator ==(Rational<T> left, Rational<T> right) => left.Enumerator == right.Enumerator && left.Denominator == right.Denominator ? true : left.Enumerator * right.Denominator == right.Enumerator * left.Denominator;

        public static bool operator !=(Rational<T> left, Rational<T> right) => !(left == right);

        public static bool operator <(Rational<T> left, Rational<T> right)
        {
            (Rational<T> a, Rational<T> b) = BringToCommonDenominator(left, right);
            return a.Enumerator < b.Enumerator;
        }

        public static bool operator >(Rational<T> left, Rational<T> right)
        {
            (Rational<T> a, Rational<T> b) = BringToCommonDenominator(left, right);
            return a.Enumerator > b.Enumerator;
        }

        public static bool operator <=(Rational<T> left, Rational<T> right)
        {
            (Rational<T> a, Rational<T> b) = BringToCommonDenominator(left, right);
            return a.Enumerator <= b.Enumerator;
        }

        public static bool operator >=(Rational<T> left, Rational<T> right)
        {
            (Rational<T> a, Rational<T> b) = BringToCommonDenominator(left, right);
            return a.Enumerator >= b.Enumerator;
        }

        public static implicit operator Rational<T>(T value) => new Rational<T>(value, T.One);

        public static implicit operator T(Rational<T> value) => value.ValueI;

        public static implicit operator Half(Rational<T> value) => value.ValueH;

        public static implicit operator float(Rational<T> value) => value.ValueF;

        public static implicit operator double(Rational<T> value) => value.ValueD;

        public static implicit operator decimal(Rational<T> value) => value.ValueM;
    }
}
