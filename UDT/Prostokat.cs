using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.Native)]
public struct Prostokat : INullable
{

    public Prostokat(List<double> args)
    {
        m_Null = false;

        x1 = args[0];
        y1 = args[1];
        x2 = args[2];
        y2 = args[3];
        x3 = args[4];
        y3 = args[5];
        x4 = args[6];
        y4 = args[7];

        bok_a = .0;
        bok_b = .0;
        bok_c = .0;
        bok_d = .0;

        this.WyznaczBoki();
    }

    public void WyznaczBoki()
    {
        bok_a = this.WyznaczDlugosc(x1, y1, x2, y2);
        bok_b = this.WyznaczDlugosc(x2, y2, x3, y3);
        bok_c = this.WyznaczDlugosc(x3, y3, x4, y4);
        bok_d = this.WyznaczDlugosc(x4, y4, x1, y1);
    }

    public string WyznaczWspolrzedne()
    {
        return "(" + x1 + "/" + y1 + "/" + x2 + "/" + y2 +"/"+ x3 + "/" + y3 + "/" + x4 + "/" + y4 + ")";
    }

    public double WyznaczDlugosc(double x1, double y1, double x2, double y2)
    {
        return Math.Round(Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2)), 2);
    }

    public bool Validator()
    {
        return bok_a == bok_c
            && bok_b == bok_d
            && this.WyznaczPole() != 0;
    }

    public double WyznaczObwod()
    {
        return Math.Round(2 * bok_a + 2 * bok_b, 2);
    }

    public double WyznaczPole()
    {
        return Math.Round(bok_a * bok_b, 2);
    }

    public string Pole()
    {
        return "Pole prostokata: " + WyznaczPole().ToString();
    }

    public string Obwod()
    {
        return "Obwod prostokata: " + WyznaczObwod().ToString();
    }

    public override string ToString()
    {
        string returning_str = "Prostokat o wspolrzednych (" + this.x1.ToString()
                + ", " + this.y1.ToString() + "), ("
                + this.x2.ToString()
                + ", " + this.y2.ToString() + "), ("
                + this.x3.ToString()
                + ", " + this.y3.ToString() + ") oraz ("
                + this.x4.ToString()
                + ", " + this.y4.ToString() + ")."
                +"\nDlugosci bokow: "+bok_a.ToString()+", "+bok_b.ToString();
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

    public static Prostokat Null
    {
        get
        {
            Prostokat h = new Prostokat();
            h.m_Null = true;
            return h;
        }
    }

    public static Prostokat Parse(SqlString s)
    {
        if (s.IsNull)
            return Null;

        string[] arguments = s.Value.Split("/".ToCharArray());

        if (arguments.Length != 8)
        {
            throw new ArgumentException("$Niepoprawna liczba argumentów! (wymagane 8)$");
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

        Prostokat u = new Prostokat(points);
        if (u.Validator() == false) throw new ArgumentException("$Nie da sie utworzyc prostokatu z podanych punktow!$");
        return u;
    }

    private bool m_Null;

    private double bok_a;
    private double bok_b;
    private double bok_c;
    private double bok_d;

    private double x1;
    private double x2;
    private double x3;
    private double x4;
    private double y1;
    private double y2;
    private double y3;
    private double y4;
}