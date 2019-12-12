using System;
using System.Collections.Generic;
using System.Text;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Beobachter, welcher die Anzahl der generierten Zahlen in einem bestimmten Bereich zählt. 
    /// </summary>
    public class RangeObserver : BaseObserver
    {
        #region Properties

        /// <summary>
        /// Enthält die untere Schranke (inkl.)
        /// </summary>
        public int LowerRange { get; private set; }
        
        /// <summary>
        /// Enthält die obere Schranke (inkl.)
        /// </summary>
        public int UpperRange { get; private set; }

        /// <summary>
        /// Enthält die Anzahl der Zahlen, welche sich im Bereich befinden.
        /// </summary>
        public int NumbersInRange { get; private set; }

        /// <summary>
        /// Enthält die Anzahl der gesuchten Zahlen im Bereich.
        /// </summary>
        public int NumbersOfHitsToWaitFor { get; private set; }

        #endregion

        #region Constructors

        public RangeObserver(IObservable numberGenerator, int numberOfHitsToWaitFor, int lowerRange, int upperRange) : base(numberGenerator, int.MaxValue)
        {
            if (numberOfHitsToWaitFor <= 0)
            {
                throw new ArgumentException($"Argument {nameof(numberOfHitsToWaitFor)} ist <= 0!");
            }

            if (lowerRange > upperRange)
            {
                throw new ArgumentException($"Argument {nameof(lowerRange)} ist größer als Argument {nameof(upperRange)}");
            }
            
            NumbersOfHitsToWaitFor = numberOfHitsToWaitFor;
            LowerRange = lowerRange;
            UpperRange = upperRange;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{base.ToString()} => ");
            sb.Append($"{nameof(RangeObserver)} ");
            sb.Append($"[{nameof(LowerRange)}='{LowerRange}'");
            sb.Append($", {nameof(UpperRange)}='{UpperRange}'");
            sb.Append($", {nameof(NumbersInRange)}='{NumbersInRange}'");
            sb.Append($", {nameof(NumbersOfHitsToWaitFor)}='{NumbersOfHitsToWaitFor}']");

            return sb.ToString();
        }

        public override void OnNextNumber(int number)
        {
            if (number >= LowerRange && number <= UpperRange)
            {
                NumbersInRange++;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"   >> {this.GetType().Name}: Number is in range ('{LowerRange}'-'{UpperRange}')!");
                Console.ResetColor();
            }

            if (NumbersInRange >= NumbersOfHitsToWaitFor)
            {
                DetachFromNumberGenerator();
            }
            base.OnNextNumber(number);
        }


        protected override string GetTypeSpecificDetachText()
        {
            return $"Got '{NumbersInRange}' in the configured range";
        }

        public override string GetInfo()
        {
            return ($"{base.GetBaseInfo()} ====> There were '{NumbersInRange}' numbers between '{LowerRange}' and '{UpperRange}'");
        }

        #endregion
    }
}
