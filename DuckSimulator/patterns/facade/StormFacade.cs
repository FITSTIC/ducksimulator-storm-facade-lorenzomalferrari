using System.Collections.Generic;
using System;

/// <summary>
/// Interfaccia del sistema Stormo.
/// Uno stormo viene riempito di papere e poi viene fatto migrare verso una direzione.
/// </summary>
public interface IStorm<Tduck> where Tduck : Duck, new()
{
    /// <summary>
    /// Lista delle papere che compongono lo stormo
    /// </summary>
    /// <returns></returns>
    List<Tduck> Ducks { get; }
    /// <summary>
    /// Riempie lo stormo
    /// </summary>
    /// <param name="nDucks">numero di papere che compongono lo stormo</param>
    void FillStorm(int nDucks);
    /// <summary>
    /// Posizione corrente sulle cordinate X dello stormo
    /// </summary>
    /// <returns></returns>
    double PositionX { get; }
    /// <summary>
    /// Posizione corrente sulle cordinate Y dello stormo
    /// </summary>
    /// <returns></returns>
    double PositionY { get; }
    /// <summary>
    /// Contatore totale dello spazio percorso dallo stormo durante la migrazione.
    /// </summary>
    /// <returns></returns>
    double TotalDistance { get; }
    /// <summary>
    /// Fa migrare lo stormo verso una direzione
    /// </summary>
    /// <param name="dir">Direzione verso la quale tutto lo stormo sta migrando</param>
    /// <param name="distance">Distanza che deve percorrere lo stormo</param>
    void Migrate(Direction dir, double distance);
    /// <summary>
    /// Distanza in linea d'aria dal punto di partenza.
    /// </summary>
    /// <returns></returns>
    double LineDistanceFromStart { get; }
}

public class Storm<Tduck> : IStorm<Tduck> where Tduck : Duck, new()
{
    private List<Tduck> papere;
    // pos su asse X e Y + distanza totale
    private double posX, posY, totDist;
    //costruttore
    public Storm()
    {
        papere = new List<Tduck>();
    }
    public List<Tduck> Ducks => papere;

    public double PositionX => Math.Round(posX, 1);

    public double PositionY => Math.Round(posY, 1);

    public double LineDistanceFromStart
    {
        get
        {
            //Genero un numero con la virgola e successivamente arrotondo le cifre decimali
            double x = Math.Sqrt((PositionX * PositionX) + (PositionY * PositionY));
            return Math.Round(x, 2);
        }
    }

    public double TotalDistance => Math.Round(totDist, 1);

    public void FillStorm(int nDucks)
    {
        if (nDucks < 1)
        {
            throw new ArgumentException("Parametro d'ingresso non valido.");
        }

        for (int i = 0; i < nDucks; i++)
        {
            papere.Add(new Tduck());
        }
    }

    public void Migrate(Direction dir, double distance)
    {
        if (papere == null || papere.Count == 0)
        {
            throw new ArgumentException("ATTENZIONE: LO STORMO VA RIEMPITO.");
        }

        switch (dir)
        {
            case Direction.NORD:
                posY += distance;
                break;
            case Direction.SUD:
                posY += distance * -1;
                break;
            case Direction.EST:
                posX += distance;
                break;
            case Direction.OVEST:
                posX += distance * -1;
                break;
            default:
                throw new Exception("ATTENZIONE: DIREZIONE NON VALIDA!");
        }

        totDist += distance;

        foreach (Tduck p in papere)
        {
            p.Fly(distance);
            (p as Duck).CurrentDirection = dir;
        }
    }
}