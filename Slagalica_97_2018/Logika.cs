using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Slagalica_97_2018
{
    public enum Polje
    {
        KARO,
        TREF,
        PIK,
        HERC,
        PRAZNO
    }

    public class Logika
    {
        public Polje[,] tabla;

        public int x;
        public int y;

        public int potezi;




        public Logika()
        {
            this.potezi = 0;
            this.x = 4;
            this.y = 4;

            //init();

        }

        public Polje znakTranslate(int num)
        {
            switch (num)
            {
                case 0:
                    return Polje.KARO;
                case 1:
                    return Polje.TREF;
                case 2:
                    return Polje.PIK;
                case 3:
                    return Polje.HERC;

                default:
                    return Polje.PRAZNO;
            }
        }


        public void init()
        {

            potezi = 0;

            tabla = new Polje[4, 4];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    tabla[i, j] = Polje.PRAZNO;
                }
            }

            Random random = new Random();

            int znak = 0;

            for(int i = 1; i < (x*y)+1; i++)
            {
                int randX;
                int randY;


                do
                {
                    randX = random.Next(0, 4);
                    randY = random.Next(0, 4);
                  
                }
                while (tabla[randX, randY] != Polje.PRAZNO);

                //Debug.WriteLine(znak);
                tabla[randX, randY] = znakTranslate(znak);

                if ((i % 4) == 0 && znak < 4)
                {
                    znak = znak + 1;

                }

            }

            int randomX = random.Next(0, 4);
            int randomY = random.Next(0, 4);

            tabla[randomX, randomY] = Polje.PRAZNO;
        }


        public bool odigrajPotez(int posX, int posY)
        {
            int prazX = 0;
            int prazY = 0;


            for(int i = 0; i < x; i++)
            {
                for(int j = 0; j < y; j++)
                {
                    if(tabla[i,j] == Polje.PRAZNO)
                    {
                        prazX = i;
                        prazY = j;
                        break;
                    }
                }
            }


            //Debug.WriteLine("PRAZX: " + prazX);
            //Debug.WriteLine("PRAZY: " + prazY);

            if (posX - 1 == prazX && posY == prazY)
            {

                //Debug.WriteLine("PRVIX: " + prazX + " " + " Y: " + prazY);
                tabla[prazX, prazY] = tabla[posX, posY];
                tabla[posX, posY] = Polje.PRAZNO;
                potezi++;
                return true;
            }
            else if(posX + 1 == prazX && posY == prazY)
            {

                //Debug.WriteLine("DRUGIX: " + prazX + " " + " " + prazY);
                tabla[prazX, prazY] = tabla[posX, posY];
                tabla[posX, posY] = Polje.PRAZNO;
                potezi++;
                return true;
            }
            else if(posX == prazX && prazY == posY + 1)
            {
                //Debug.WriteLine("TRECIX: " + prazX + " " + " " + prazY);
                tabla[prazX, prazY] = tabla[posX, posY];
                tabla[posX, posY] = Polje.PRAZNO;
                potezi++;
                return true;
            }
            else if(posX == prazX && prazY == posY - 1)
            {
                //Debug.WriteLine("CETVRTIX: " + prazX + " " + " " + prazY);
                tabla[prazX, prazY] = tabla[posX, posY];
                tabla[posX, posY] = Polje.PRAZNO;
                potezi++;
                return true;
            }
            else
            {
                return false;
            }




        }


        public bool proveraPobede()
        {
            int sumCounter = 0;
            int colCounter = 0;

            for (int j = 0; j < y; j++)
            {
                colCounter = 0;
                for (int i = 0; i < x; i++)
                {
                    

                    Polje startKolone = tabla[0, j];

                    if(startKolone == Polje.PRAZNO)
                    {
                        //Debug.WriteLine("IF ZA PRAZNO PRVO "+j);
                        //colCounter++;
                        startKolone = tabla[1, j];
                    }

                    if(tabla[i,j] == startKolone || tabla[i,j] == Polje.PRAZNO)
                    {
                        //Debug.WriteLine("ENTER IF "+j);
                        
                        colCounter++;

                        
                    }
                    else
                    {
                        break;
                    }
                   
                }
                //Debug.WriteLine("COL CTR:" + colCounter);
                if (colCounter == 4)
                    sumCounter += colCounter;
            }
            //Debug.WriteLine("\nSUM CTR: " + sumCounter);
            //Debug.WriteLine("\n");
            if (sumCounter == 16)
                return true;
            else
                sumCounter = 0;
                return false;

           
        }


        public System.Drawing.Bitmap dajSlikuZaPolje(Polje znak)
        {
            switch (znak)
            {
                case Polje.KARO:
                    //Debug.Write(" KARO FUNC ");
                    return Properties.Resources.karo;
                case Polje.TREF:
                    //Debug.Write(" TREF FUNC ");
                    return Properties.Resources.tref;
                case Polje.PIK:
                    //Debug.Write(" PIK FUNC ");
                    return Properties.Resources.pik;
                case Polje.HERC:
                    //Debug.Write(" HERC FUNC ");
                    return Properties.Resources.hearts;
                case Polje.PRAZNO:
                    return Properties.Resources.prazno;
                default:
                    return Properties.Resources.prazno;
            }
        }




        /*public void printTabla()
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Debug.Write(tabla[i,j]+" ");
                    //Debug.WriteLine("I: " + i + " J: " + j);
                }
                Debug.WriteLine("\n");
            }
        }*/





    }
}
