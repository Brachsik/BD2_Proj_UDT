using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.Native)]
public struct Trojkat : INullable
{

    public Trojkat(List<double> args)
    {
        m_Null = false;

        x1 = args[0];
        y1 = args[1];
        x2 = args[2];
        y2 = args[3];
        x3 = args[4];
        y3 = args[5];

        bok_a = .0;
        bok_b = .0;
        bok_c = .0;

        this.WyznaczBoki();
    }

    public void WyznaczBoki()
    {
        this.bok_a = this.WyznaczDlugosc(this.x1, this.y1, this.x2, this.y2);
        this.bok_b = this.WyznaczDlugosc(this.x2, this.y2, this.x3, this.y3);
        this.bok_c = this.WyznaczDlugosc(this.x3, this.y3, this.x1, this.y1);
    }

    public double WyznaczDlugosc(double x1, double y1, double x2, double y2)
    {
        return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
    }

    public string WyznaczWspolrzedne()
    {
        return "(" + x1 + "/" + y1 + "/" + x2 + "/" + y2 + "/" + x3 + "/" + y3 + ")";
    }

    public double WyznaczObwod()
    {
        return Math.Round(this.bok_a + this.bok_b + this.bok_c, 2);
    }

    public double WyznaczPole()
    {
        double p = 0.5 * this.WyznaczObwod();
        return Math.Round(Math.Sqrt(p * (p - this.bok_a) * (p - this.bok_b) * (p - this.bok_c)), 2);
    }

    public string Pole()
    {
        return "Pole trojkata: " + WyznaczPole().ToString();
    }

    public string Obwod()
    {
        return "Obwod trojkata: " + WyznaczObwod().ToString();
    }

    public bool Validator()
    {
        return (this.bok_a + this.bok_b) > this.bok_c
                && (this.bok_a + this.bok_c) > this.bok_b
                && (this.bok_c + this.bok_b) > this.bok_a;
    }


    public override string ToString()
    {
        string returning_str = "Trojkat o wspolrzednych (" + this.x1.ToString()
                + ", " + this.y1.ToString() + "), ("
                + this.x2.ToString()
                + ", " + this.y2.ToString() + ") oraz ("
                + this.x3.ToString()
                + ", " + this.y3.ToString() + ")."
                + "\nDlugosc bokow: "+bok_a.ToString()+", "+bok_b.ToString()+", "+bok_c.ToString();
        if (bok_a == bok_b && bok_a == bok_c) returning_str += "\nJest to trojkat rownoboczny";
        else if (bok_a==bok_b || bok_b==bok_c || bok_c == bok_a) returning_str += "\nJest to trojkat rownoramienny";

        return returning_str;
    }

    public bool IsNull
    {
        get
        {
            // Put your code here
            return m_Null;
        }
    }

    public static Trojkat Null
    {
        get
        {
            Trojkat h = new Trojkat();
            h.m_Null = true;
            return h;
        }
    }

    public static Trojkat Parse(SqlString s)
    {
        if (s.IsNull)
            return Null;

        string[] arguments = s.Value.Split("/".ToCharArray());

        if (arguments.Length != 6)
        {
            throw new ArgumentException("$Niepoprawna liczba argumentów! (wymagane 6)$");
        }

        List<double> points = new List<double>();

        try
        {
            foreach (var x in arguments)
            {
                points.Add(double.Parse(x));
            }
        }
        catch
        {
            throw new ArgumentException("$Niepoprawny typ danych!$");
        }

        Trojkat u = new Trojkat(points);
        if (u.Validator() == false) throw new ArgumentException("$Nie da sie utworzyc trojkata z podanych punktow!$");
        return u;
    }

    private bool m_Null;

    private double bok_a;
    private double bok_b;
    private double bok_c;

    private double x1;
    private double x2;
    private double x3;

    private double y1;
    private double y2;
    private double y3;
}
