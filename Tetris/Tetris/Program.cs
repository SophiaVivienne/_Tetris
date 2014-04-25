using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    #region Blockok:
    public class Block
    {

        public List<int[,]> Blockok;
        public Random r;

        public Block()
        {
            r = new Random(DateTime.Now.Millisecond);

            Blockok = new List<int[,]>();
            Blockok.Add(new int[3, 2] { { 1, 0 } , { 1, 1 } , { 0, 1 } });
            Blockok.Add(new int[3, 2] { { 0, 2 } , { 2, 2 } , { 2, 0 } });
            Blockok.Add(new int[3, 2] { { 5, 5 }, { 0, 5 }, { 0, 5 } });
            Blockok.Add(new int[3, 2] { { 4, 4 }, { 4, 0 }, { 4, 0 } });
            Blockok.Add(new int[4, 1] { {3}, {3}, {3}, {3} });
            Blockok.Add(new int[2, 2] { { 6, 6 }, { 6, 6 } });

        }
        public int[,] RandomBlock()
        {
            return Blockok[r.Next(Blockok.Count)];
        }
    }
    #endregion

    #region Jatekmenet:
    public class Jatek{

    public int[,] Jatekter;
    public int X;
    public int Y;
    public int[,] MozgoBlock = null;
    public Block RandBlockGen = new Block();
    public int Pontszam = 0;
    public int Rekord = 0;
    public enum Gomb
    {
        Le,
        Fel,
        Jobbra,
        Balra
    }


    #region Lepes:
    public void Lepes()
    {
        if (Elfer(MozgoBlock, X, Y + 1))
        {
            Y++;
        }
        else
        {
            Jatekter = AlloBlockok(MozgoBlock, Jatekter, X, Y);
            Kovetkezo();
        }
        int Sorok = EllenorzesSorok();
        /*if (VanSor != null)
        {
            VanSor(Sorok);
        }*/
    }
    #endregion

    #region Elfer:
    private bool Elfer(int[,] Block, int x, int y)
    {
        int[,] masolat = (int[,])Jatekter.Clone();

        if (x + Block.GetUpperBound(1) <= masolat.GetUpperBound(1) && y + Block.GetUpperBound(0) <= masolat.GetUpperBound(0))
        {
            for (int i = 0; i <= Block.GetUpperBound(1); i++)
            {
                for (int j = 0; j <= Block.GetUpperBound(0); j++)
                {
                    if (Block[j, i] != 0)
                    {
                        if (masolat[y + j, x + i] != 0)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        return false;
    }
    #endregion

    #region AlloBlockok:
    private int[,] AlloBlockok(int[,] Block, int[,] mezo, int x, int y)
    {
        if (x + Block.GetUpperBound(1) <= mezo.GetUpperBound(1) && y + Block.GetUpperBound(0) <= mezo.GetUpperBound(0))
        {
            for (int i = 0; i <= Block.GetUpperBound(1); i++)
            {
                for (int j = 0; j <= Block.GetUpperBound(0); j++)
                {
                    if (Block[j, i] != 0)
                    {
                        mezo[y + j, x + i] = Block[j, i];
                    }
                }
            }
        }
        /*if (AlloBlock != null)
        {
            AlloBlock();
        }*/
        return mezo;
    }
    #endregion

    #region Kovetkezo:
    public void Kovetkezo()
    {
        X = Jatekter.GetUpperBound(1) / 2;
        Y = 0;
        MozgoBlock = RandBlockGen.RandomBlock();
        if (!Elfer(MozgoBlock, X, Y + 1))
        {
            Vege();
        }
    }
    #endregion

    #region EllenorzesSorok:
    private int EllenorzesSorok()
    {
        for (int i = 0; i < Jatekter.GetUpperBound(0) + 1; i++)
        {
            bool vanTeljesSor = true;
            for (int j = 0; j < Jatekter.GetUpperBound(1) + 1; j++)
            {
                vanTeljesSor = vanTeljesSor && Jatekter[i, j] != 0;
            }
            if (vanTeljesSor)
            {
                SorTorles(i--);
                return EllenorzesSorok() + 1;
            }
        }
        return 0;
    }
    #endregion

    #region SorTorles:
    private void SorTorles(int index)
    {
        for (int i = index; i > 0; i--)
        {
            for (int j = 0; j <= Jatekter.GetUpperBound(1); j++)
            {
                Jatekter[i, j] = Jatekter[i - 1, j];
            }
        }
        for (int j = 0; j <= Jatekter.GetUpperBound(1); j++)
        {
            Jatekter[0, j] = 0;
        }
    }
    #endregion

    #region VanSor:
    #endregion

    #region Gombnyomas:
    public void Gombnyomas(Gomb k)
    {
        switch (k)
        {
            case Gomb.Le:
                Lepes();
                break;
            case Gomb.Fel:
                while (Elfer(MozgoBlock, X, Y + 1))
                {
                    Lepes();
                }
                Lepes();
                break;
            case Gomb.Jobbra:
                if (X < Jatekter.GetUpperBound(0) - MozgoBlock.GetUpperBound(0) && Elfer(MozgoBlock, X + 1, Y))
                {
                    X++;
                }
                break;
            case Gomb.Balra:
                if (X > 0 && Elfer(MozgoBlock, X - 1, Y))
                {
                    X--;
                }
                break;
        }
    }
    #endregion

    #region Vege:
    public void Vege()
    {
        Console.Clear();
        if (Pontszam > Rekord)
        {
            Rekord = Pontszam;
            Console.WriteLine("Az új rekord: " + Rekord);
        }
        else
        {
            Console.WriteLine("A pontszám: " + Pontszam);
        }
    }
    #endregion

    }
    #endregion
}
