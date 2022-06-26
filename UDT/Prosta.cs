using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.Native)]
public struct Prosta : INullable
{

    public Prosta(List<double> args)
    {
        m_Null = false;

        x1 = args[0];
        y1 = args[1];
        x2 = args[2];
        y2 = args[3];
    }

    public double WyznaczDlugosc()
    {
        return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
    }

    public string WyznaczWspolrzedne()
    {
        return "(" + x1 + "/" + y1 + "/" + x2 + "/" + y2 + ")";
    }

    public double WyznaczObwod()
    {
        return 0.0;
    }

    public double WyznaczPole()
    {
        return 0.0;
    }

    public string Pole()
    {
        return "Pole prostej: 0.0";
    }

    public string Obwod()
    {
        return "Obwod prostej: 0.0";
    }

    public bool Validator()
    {
        return this.WyznaczDlugosc() != 0;
    }

    public override string ToString()
    {
        string returning_str = "Prosta o dlugosci " + Math.Round(this.WyznaczDlugosc(), 2).ToString()
                + " o wspolrzednych (" + this.x1.ToString()
                + ", " + this.y1.ToString() + ") oraz ("
                + this.x2.ToString()
                + ", " + this.y2.ToString() + ").";
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

    public static Prosta Null
    {
        get
        {
            Prosta h = new Prosta();
            h.m_Null = true;
            return h;
        }
    }

    public static Prosta Parse(SqlString s)
    {
        if (s.IsNull)
            return Null;

        string[] arguments = s.Value.Split("/".ToCharArray());

        if (arguments.Length != 4)
        {
            throw new ArgumentException("$Niepoprawna liczba argumentów! (wymagane 4)$");
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

        Prosta u = new Prosta(points);
        if (u.Validator() == false) throw new ArgumentException("$Nie da sie utworzyc prostej z podanych punktow!$");
        return u;
    }

    private bool m_Null;

    private double x1;
    private double x2;

    private double y1;
    private double y2;
}
