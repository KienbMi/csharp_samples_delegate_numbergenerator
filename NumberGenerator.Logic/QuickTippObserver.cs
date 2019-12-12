using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Beobachter, welcher auf einen vollständigen Quick-Tipp wartet: 6 unterschiedliche Zahlen zw. 1 und 45.
    /// </summary>
    public class QuickTippObserver : RangeObserver
    {
        #region Fields

        #endregion

        #region Properties

        public List<int> QuickTippNumbers { get; private set; }

        #endregion

        #region Constructor

        public QuickTippObserver(IObservable numberGenerator) : base(numberGenerator, 6, 1, 45)
        {
            QuickTippNumbers = new List<int>();
        }

        #endregion

        #region Methods

        public override void OnNextNumber(int number)
        {
            if (number >= LowerRange && number <= UpperRange)
            {
                if (QuickTippNumbers.Contains(number) == false)
                {
                    QuickTippNumbers.Add(number);
                }
            }

            base.OnNextNumber(number);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{base.ToString()} => ");
            sb.Append($"{nameof(QuickTippObserver)} ");
            sb.Append($"[{nameof(CountOfNumbersReceived)}='{CountOfNumbersReceived}'");
            for (int i = 0; i < QuickTippNumbers.Count; i++)
            {
                sb.Append($", Number_{i + 1}='{QuickTippNumbers[i]}'");
            }
            sb.Append($"]");

            return sb.ToString();
        }

        protected override string GetTypeSpecificDetachText()
        {
            return $"Got a full Quick -Tipp";
        }

        public override string GetInfo()
        {
            StringBuilder sb = new StringBuilder();
            QuickTippNumbers.Sort();

            for (int i = 0; i < QuickTippNumbers.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(", ");
                }
                    
                sb.Append($"{QuickTippNumbers[i]}");
            }

            return ($"{base.GetBaseInfo()} ====> Quick-Tipp is {sb.ToString()}");
        }

        #endregion
    }
}
