using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace Zadanie2
{
    class Program
    {
        static ulong[] Tab = {100913, 1009139, 10091401, 100914061, 1009140611, 10091406133, 100914061337, 1009140613399};
        static ulong Counter = 0;
        const int TimeIterationAmount = 10;
        static List<ulong> Primes;
        static ulong PrimesLenght = 0;
        static StreamWriter OutputFile = new StreamWriter("output.csv");

        static ulong GeneratePrimes(ulong n)
        {
            if (PrimesLenght <= n)
            {
                ulong NSqrt = 0;

                while ((NSqrt + 1) * (NSqrt + 1) < n)
                {
                    NSqrt++;
                }

                bool[] Sieve = new bool[NSqrt];
                for (ulong i = 0; i < NSqrt; i++)
                {
                    Sieve[i] = true;
                }

                Sieve[0] = false;

                for (ulong i = 2; i <= NSqrt; i++)
                {
                    for (ulong j = 2 * i; j <= NSqrt; j += i)
                    {
                        Sieve[j - 1] = false;
                    }
                }

                Primes = new List<ulong>();
                for (ulong i = 0; i < NSqrt; i++)
                {
                    if (Sieve[i])
                    {
                        Primes.Add(i + 1);
                    }
                }
            }
            return n;
        }

        static bool PrimePrimitive(ulong n)
        {
            if (n < 2) return false;
            else if (n < 4) return true;
            else if (n % 2 == 0) return false;
            else
            {
                ulong m = (n >> 1); // n/2;
                for (ulong i = 3; i < m; i += 2)
                {
                    if (n % i == 0) return false;
                }
            }
            return true;
        }

        static bool PrimeGood(ulong n)
        {
            if (n < 2) return false;
            else if (n < 4) return true;
            else if (n % 2 == 0) return false;
            else
            {
                for (ulong i = 3; i * i <= n; i += 2)
                {
                    if (n % i == 0) return false;
                }
            }
            return true;
        }

        static bool PrimeOptimal(ulong n)
        {
            if (n < 2) return false;
            else if (n < 4) return true;
            else
            {
                foreach (ulong Prime in Primes)
                {
                    if (n * n < Prime)
                    {
                        if (n % Prime == 0) return false;
                    }
                    else
                    {
                        break;
                    }

                }
            }
            return true;
        }

        static void PrimePrimitiveTime(ulong n)
        {
            decimal ElapsedSeconds;
            long ElapsedTime = 0;
            long MinTime = long.MaxValue;
            long MaxTime = long.MinValue;
            long IterationElapsedTime;
            for (int i = 0; i < TimeIterationAmount + 2; i++)
            {
                long StartingTime = Stopwatch.GetTimestamp(); //Czas startowy iteracji
                PrimePrimitive(n);
                long EndingTime = Stopwatch.GetTimestamp(); //Czas końcowy iteracji
                IterationElapsedTime = EndingTime - StartingTime; //Czas trwania iteracji
                ElapsedTime += IterationElapsedTime; //Dodanie czasu iteracji do czasu łącznego
                if (IterationElapsedTime < MinTime) MinTime = IterationElapsedTime; //Jeśli czas iteracji jest krótszy od dotychczasowego najkrótszego czasu ustawienie go jako nowego najkrótszego czasu
                if (IterationElapsedTime > MaxTime) MaxTime = IterationElapsedTime; //Jeśli czas iteracji jest dłuższy od dotychczasowego najdłuższego czasu ustawienie go jako nowego najdłuższego czasu

            }
            ElapsedTime -= (MinTime + MaxTime); //Odjęcie czasu najkrótszej i najdłuższej iteracji
            ElapsedSeconds = (decimal)(ElapsedTime * (1.0 / (TimeIterationAmount * Stopwatch.Frequency))); //Podzielenie przez liczbę iteracji pomnożoną przez częstotliwość zegara 
            OutputFile.Write(ElapsedSeconds.ToString() + ";");
        }

        static bool PrimePrimitiveInstrumentalization(ulong n)
        {
            Counter = 0;
            if (n < 2) return false;
            else if (n < 4) return true;
            else if (n % 2 == 0)
            {
                Counter++;
                return false;
            } 
            else
            {
                Counter++;
                ulong m = (n >> 1); // n/2;
                for (ulong i = 3; i < m; i += 2)
                {
                    Counter++;
                    if (n % i == 0) return false;
                }
            }
            OutputFile.Write(Counter + ";");
            return true;
        }

        static void PrimeGoodTime(ulong n)
        {
            decimal ElapsedSeconds;
            long ElapsedTime = 0;
            long MinTime = long.MaxValue;
            long MaxTime = long.MinValue;
            long IterationElapsedTime;
            for (int i = 0; i < TimeIterationAmount + 2; i++)
            {
                long StartingTime = Stopwatch.GetTimestamp(); //Czas startowy iteracji
                PrimeGood(n);
                long EndingTime = Stopwatch.GetTimestamp(); //Czas końcowy iteracji
                IterationElapsedTime = EndingTime - StartingTime; //Czas trwania iteracji
                ElapsedTime += IterationElapsedTime; //Dodanie czasu iteracji do czasu łącznego
                if (IterationElapsedTime < MinTime) MinTime = IterationElapsedTime; //Jeśli czas iteracji jest krótszy od dotychczasowego najkrótszego czasu ustawienie go jako nowego najkrótszego czasu
                if (IterationElapsedTime > MaxTime) MaxTime = IterationElapsedTime; //Jeśli czas iteracji jest dłuższy od dotychczasowego najdłuższego czasu ustawienie go jako nowego najdłuższego czasu

            }
            ElapsedTime -= (MinTime + MaxTime); //Odjęcie czasu najkrótszej i najdłuższej iteracji
            ElapsedSeconds = (decimal)(ElapsedTime * (1.0 / (TimeIterationAmount * Stopwatch.Frequency))); //Podzielenie przez liczbę iteracji pomnożoną przez częstotliwość zegara 
            OutputFile.Write(ElapsedSeconds.ToString() + ";");
        }

        static bool PrimeGoodInstrumentalization(ulong n)
        {
            Counter = 0;

            if (n < 2) return false;
            else if (n < 4) return true;
            else if (n % 2 == 0) 
            {
                Counter++;
                return false;
            }
            else
            {
                Counter++;
                for (ulong i = 3; i * i <= n; i += 2)
                {
                    Counter++;
                    if (n % i == 0) return false;
                }
            }
            OutputFile.Write(Counter + ";");
            return true;
        }

        static void PrimeOptimalTime(ulong n)
        {
            decimal ElapsedSeconds;
            long ElapsedTime = 0;
            long MinTime = long.MaxValue;
            long MaxTime = long.MinValue;
            long IterationElapsedTime;
            for (int i = 0; i < TimeIterationAmount + 2; i++)
            {
                long StartingTime = Stopwatch.GetTimestamp(); //Czas startowy iteracji
                PrimeOptimal(n);
                long EndingTime = Stopwatch.GetTimestamp(); //Czas końcowy iteracji
                IterationElapsedTime = EndingTime - StartingTime; //Czas trwania iteracji
                ElapsedTime += IterationElapsedTime; //Dodanie czasu iteracji do czasu łącznego
                if (IterationElapsedTime < MinTime) MinTime = IterationElapsedTime; //Jeśli czas iteracji jest krótszy od dotychczasowego najkrótszego czasu ustawienie go jako nowego najkrótszego czasu
                if (IterationElapsedTime > MaxTime) MaxTime = IterationElapsedTime; //Jeśli czas iteracji jest dłuższy od dotychczasowego najdłuższego czasu ustawienie go jako nowego najdłuższego czasu

            }
            ElapsedTime -= (MinTime + MaxTime); //Odjęcie czasu najkrótszej i najdłuższej iteracji
            ElapsedSeconds = (decimal)(ElapsedTime * (1.0 / (TimeIterationAmount * Stopwatch.Frequency))); //Podzielenie przez liczbę iteracji pomnożoną przez częstotliwość zegara 
            OutputFile.Write(ElapsedSeconds.ToString() + ";");
        }

        static bool PrimeOptimalInstrumentalization(ulong n)
        {
            Counter = 0;

            if (n < 2) return false;
            else if (n < 4) return true;
            else
            {
                foreach (ulong Prime in Primes)
                {
                    if (Prime * Prime <= n)
                    {
                        Counter++;
                        if (n % Prime == 0) return false;
                    }
                    else
                    {
                        break;
                    }

                }
            }
            OutputFile.Write(Counter + ";");
            return true;
        }

        static void Main(string[] args)
        {
            OutputFile.Write("Number;");
            OutputFile.Write("PrimitiveInstrumentalization;");
            OutputFile.Write("GoodInstrumentalization;");
            OutputFile.Write("OptimalInstrumentalization;");
            OutputFile.Write("PrimitiveTime;");
            OutputFile.Write("GoodTime;");
            OutputFile.Write("OptimalTime;");
            OutputFile.Write("\n");
            PrimesLenght = GeneratePrimes(1009140613399);

            foreach (ulong TestPrime in Tab)
            {
                Console.WriteLine(TestPrime);
                OutputFile.Write(TestPrime+";");

                if (TestPrime <= 10091406133)
                {
                    PrimePrimitiveInstrumentalization(TestPrime);
                }
                else
                {
                    OutputFile.Write(";");
                }

                PrimeGoodInstrumentalization(TestPrime);
                PrimeOptimalInstrumentalization(TestPrime);

                if (TestPrime <= 10091406133)
                {
                    PrimePrimitiveTime(TestPrime);
                }
                else
                {
                    OutputFile.Write(";");
                }

                PrimeGoodTime(TestPrime);
                PrimeOptimalTime(TestPrime);
                OutputFile.Write("\n");
            }

            OutputFile.Close();
        }
    }
}
