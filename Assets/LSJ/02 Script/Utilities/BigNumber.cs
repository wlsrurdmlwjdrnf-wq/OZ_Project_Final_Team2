using System;
using UnityEngine;

[System.Serializable]
public class BigNumber : IComparable<BigNumber>
{
    public double mantissa; // 1.0 ~ <10.0 ¹üÀ§·Î Á¤±ÔÈ­
    public long exponent;

    // »ý¼ºÀÚµé
    public BigNumber() : this(0, 0) { }

    public BigNumber(double value)
    {
        Set(value);
    }

    public BigNumber(double m, long e)
    {
        mantissa = m;
        exponent = e;
        Normalize();
    }

    private void Set(double value)
    {
        if (value == 0)
        {
            mantissa = 0;
            exponent = 0;
            return;
        }

        exponent = (long)Math.Floor(Math.Log10(Math.Abs(value)));
        mantissa = value / Math.Pow(10, exponent);
        Normalize();
    }

    private void Normalize()
    {
        if (mantissa == 0)
        {
            exponent = 0;
            return;
        }

        while (mantissa >= 10.0)
        {
            mantissa /= 10.0;
            exponent++;
        }
        while (mantissa < 1.0 && mantissa > 0)
        {
            mantissa *= 10.0;
            exponent--;
        }
    }

    // µ¡¼À
    public static BigNumber operator +(BigNumber a, BigNumber b)
    {
        if (a.exponent > b.exponent)
        {
            double diff = Math.Pow(10, a.exponent - b.exponent);
            return new BigNumber(a.mantissa + b.mantissa / diff, a.exponent);
        }
        else if (b.exponent > a.exponent)
        {
            double diff = Math.Pow(10, b.exponent - a.exponent);
            return new BigNumber(b.mantissa + a.mantissa / diff, b.exponent);
        }
        else
        {
            return new BigNumber(a.mantissa + b.mantissa, a.exponent);
        }
    }

    // »¬¼À
    public static BigNumber operator -(BigNumber a, BigNumber b)
    {
        return a + Negate(b);
    }

    private static BigNumber Negate(BigNumber n)
    {
        if (n.mantissa == 0) return new BigNumber(0);
        return new BigNumber(-n.mantissa, n.exponent);
    }

    // °ö¼À
    public static BigNumber operator *(BigNumber a, BigNumber b)
    {
        return new BigNumber(a.mantissa * b.mantissa, a.exponent + b.exponent);
    }

    // ºñ±³
    public int CompareTo(BigNumber other)
    {
        if (exponent != other.exponent) return exponent.CompareTo(other.exponent);
        return mantissa.CompareTo(other.mantissa);
    }

    public static bool operator >(BigNumber a, BigNumber b) => a.CompareTo(b) > 0;
    public static bool operator <(BigNumber a, BigNumber b) => a.CompareTo(b) < 0;
    public static bool operator >=(BigNumber a, BigNumber b) => a.CompareTo(b) >= 0;
    public static bool operator <=(BigNumber a, BigNumber b) => a.CompareTo(b) <= 0;

    public static bool operator ==(BigNumber a, BigNumber b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (a is null || b is null) return false;
        return a.exponent == b.exponent && Math.Abs(a.mantissa - b.mantissa) < 1e-10;
    }

    public static bool operator !=(BigNumber a, BigNumber b) => !(a == b);

    public override bool Equals(object obj)
    {
        return obj is BigNumber other && this == other;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(mantissa, exponent);
    }
}