using System;
using System.Collections.Generic;
using System.Linq;

namespace Casas
{
    class Program
    {
        private const int CantidadCasas = 8;
        static void Main(string[] args)
        {

            bool diasCorrectos = false;
            string dias, casas;
            int valorAnterior = 0;
            int valorSiguiente = 0;
            List<int> casasArray = new List<int>();
            Console.WriteLine("Ejercicio Casas - Kevin Perez");
            Console.WriteLine("===============================");
            Console.WriteLine("   ");

            Console.WriteLine("Por favor digite el numero de días a ejecutar");
            dias = Console.ReadLine();
            Console.WriteLine("Por favor digite los valores para cada case separados por ,");
            casas = Console.ReadLine();

            try
            {
                casasArray = casas.Split(',').Select(Int32.Parse).Where(a => a == 0 || a == 1).ToList();
                Console.WriteLine("Casas " + string.Join(" ", casasArray));
            }
            catch (Exception)
            {
                Console.WriteLine("Digíte un número de casas válido");
            }

            if (!int.TryParse(dias, out int diasMal))
            {
                Console.WriteLine("Digíte un número de días válido");
            }

            int diasInt = int.Parse(dias);

            if (diasInt < 0 || casasArray.Count > 8)
            {
                Console.WriteLine("Parametros no validos");
            }
            else
            {
                Console.WriteLine("   ");
                Console.WriteLine("Entrada " + string.Join(" ", casasArray));

                for (int i = 0; i < diasInt; i++)
                {
                    valorAnterior = 0;
                    valorSiguiente = 0;

                    for (int k = 0; k < casasArray.Count; k++)
                    {
                        if (k < (casasArray.Count - 1))
                        {
                            valorSiguiente = k + 1;
                        }

                        if (valorSiguiente == valorAnterior)
                        {
                            valorAnterior = casasArray[k];
                            casasArray[k] = 0;

                        }
                        else
                        {

                            valorAnterior = casasArray[k];
                            casasArray[k] = 1;
                        }


                    }//fin for casas

                    Console.WriteLine("Salida  " + string.Join(" ", casasArray));

                }//fin for dias


            }


        }
    }
}
