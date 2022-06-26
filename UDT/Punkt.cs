using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.Native)]
public struct Punkt : INullable
{

    public Punkt(List<double> args)
    {
        m_Null = false;

        wspolrz_x1 = args[0];
        wspolrz_y1 = args[1];
    }

    public string WyznaczWspolrzedne()
    {
        return "("+wspolrz_x1 + "/" + wspolrz_y1 + ")";
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
        return "Pole punktu: 0.0";
    }

    public string Obwod()
    {
        return "Obwod punktu: 0.0";
    }

    public override string ToString()
    {
        string returning_str = "Punkt o wspolrzednych (" + this.wspolrz_x1.ToString()
                + ", " + this.wspolrz_y1.ToString() + ").";
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

    public static Punkt Null
    {
        get
        {
            Punkt h = new Punkt();
            h.m_Null = true;
            return h;
        }
    }

    public static Punkt Parse(SqlString s)
    {
        if (s.IsNull)
            return Null;
        string[] arguments = s.Value.Split("/".ToCharArray());

        if (arguments.Length != 2)
        {
            throw new ArgumentException("$Niepoprawna liczba argumentów! (wymagane 2)$");
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
        Punkt u = new Punkt(points);
        return u;
    }

    private bool m_Null;

    private double wspolrz_x1;
    private double wspolrz_y1;
}
