using System;
using System.Text;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Beobachter, welcher einfache Statistiken bereit stellt (Min, Max, Sum, Avg).
    /// </summary>
    public class StatisticsObserver : BaseObserver
    {
        #region Fields

        #endregion

        #region Properties

        /// <summary>
        /// Enthält das Minimum der generierten Zahlen.
        /// </summary>
        public int Min { get; private set; }

        /// <summary>
        /// Enthält das Maximum der generierten Zahlen.
        /// </summary>
        public int Max { get; private set; }

        /// <summary>
        /// Enthält die Summe der generierten Zahlen.
        /// </summary>
        public int Sum { get; private set; }

        /// <summary>
        /// Enthält den Durchschnitt der generierten Zahlen.
        /// </summary>
        public int Avg { get; private set; }

        #endregion

        #region Constructors

        public StatisticsObserver(IObservable numberGenerator, int countOfNumbersToWaitFor) : base(numberGenerator, countOfNumbersToWaitFor)
        {

        }

        #endregion

        #region Methods

        public override string ToString()
        {
            //  => StatisticsObserver [Min='1', Max='999', Sum='2486436', Avg='497']
            StringBuilder sb = new StringBuilder();
            sb.Append($"{base.ToString()} => ");
            sb.Append($"{nameof(StatisticsObserver)} ");
            sb.Append($"[{nameof(Min)}='{Min}'");
            sb.Append($", {nameof(Max)}='{Max}'");
            sb.Append($", {nameof(Sum)}='{Sum}'");
            sb.Append($", {nameof(Avg)}='{Avg}']");

            return sb.ToString();
        }

        public override void OnNextNumber(int number)
        {
            if (base.CountOfNumbersReceived == 0)
            {
                Min = number;
                Max = number;
            }

            if (number < Min)
                Min = number;
            else if (number > Max)
                Max = number;

            Sum += number;
            Avg = Sum / (base.CountOfNumbersReceived + 1);

            base.OnNextNumber(number);
        }

        public override string GetInfo()
        {
            return ($"{base.GetInfo()} ====> {nameof(Min)}='{Min}', {nameof(Max)}='{Max}, {nameof(Sum)}='{Sum}, {nameof(Avg)}='{Avg}");
        }


        #endregion
    }
}
