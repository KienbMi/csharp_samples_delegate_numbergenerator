
using NumberGenerator.Logic;
using System;

namespace NumberGenerator.Ui
{
    class Program
    {
        static void Main()
        {
            // Zufallszahlengenerator erstelltn
            RandomNumberGenerator numberGenerator = new RandomNumberGenerator(250);

            // Beobachter erstellen
            BaseObserver baseObserver = new BaseObserver(numberGenerator, 10);
            StatisticsObserver statisticsObserver = new StatisticsObserver(numberGenerator, 20);
            RangeObserver rangeObserver = new RangeObserver(numberGenerator, 5, 200, 300);
            QuickTippObserver quickTippObserver = new QuickTippObserver(numberGenerator);

            Console.WriteLine($"{numberGenerator.ToString()}");
            numberGenerator.StartNumberGeneration();

            // Nummerngenerierung starten
            // Resultat ausgeben
            Console.WriteLine("----------Result------------");
            Console.WriteLine(statisticsObserver.GetInfo());
            Console.WriteLine(rangeObserver.GetInfo());
            Console.WriteLine(quickTippObserver.GetInfo());
            Console.WriteLine("----------------------------");
            Console.WriteLine("Drücken Sie eine beliebige Taste . . .");
            Console.ReadKey();
        }
    }
}
