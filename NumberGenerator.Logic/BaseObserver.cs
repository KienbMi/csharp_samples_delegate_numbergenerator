using System;
using System.ComponentModel;
using System.Text;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Beobachter, welcher die Zahlen auf der Konsole ausgibt.
    /// Diese Klasse dient als Basisklasse für komplexere Beobachter.
    /// </summary>
    public class BaseObserver
    {
        #region Fields

        private readonly IObservable _numberGenerator;

        #endregion

        #region Properties

        public int CountOfNumbersReceived { get; private set; }
        public int CountOfNumbersToWaitFor { get; private set; }

        #endregion

        #region Constructors

        public BaseObserver(IObservable numberGenerator, int countOfNumbersToWaitFor)
        {
            if (countOfNumbersToWaitFor <= 0)
            {
                throw new ArgumentException($"Argument {nameof(countOfNumbersToWaitFor)} ist kleiner <= 0!");
            }

            _numberGenerator = numberGenerator;
            CountOfNumbersToWaitFor = countOfNumbersToWaitFor;

            _numberGenerator.NumberChanged += this.OnNextNumber;
        }

        #endregion

        #region Methods

        #region IObserver Members

        /// <summary>
        /// Wird aufgerufen wenn der NumberGenerator eine neue Zahl generiert hat.
        /// </summary>
        /// <param name="number"></param>
        public virtual void OnNextNumber(int number)
        {
            CountOfNumbersReceived++;

            // Sobald die Anzahl der max. Beobachtungen (_countOfNumbersToWaitFor) erreicht ist -> Detach()
            if (CountOfNumbersReceived >= CountOfNumbersToWaitFor)
            {
                DetachFromNumberGenerator();
            }
        }

        #endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(BaseObserver)} ");
            sb.Append($"[{nameof(CountOfNumbersReceived)}='{CountOfNumbersReceived}', ");
            sb.Append($"{nameof(CountOfNumbersToWaitFor)}='{CountOfNumbersToWaitFor}']");

            return sb.ToString();
        }

        protected void DetachFromNumberGenerator()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"   >> {this.GetType().Name}: {GetTypeSpecificDetachText()} => I am not interested in new numbers anymore => Detach().");
            Console.ResetColor();
            _numberGenerator.NumberChanged -= this.OnNextNumber;
        }

        protected string GetBaseInfo()
        {
            return ($" >> {this.GetType().Name + ':',-20} Received {CountOfNumbersReceived:d5} numbers");
        }

        protected virtual string GetTypeSpecificDetachText()
        {
            return $"Received '{CountOfNumbersReceived}' of '{CountOfNumbersToWaitFor}'";
        }
        
        public virtual string GetInfo()
        {
            return GetBaseInfo();
        }

        #endregion
    }
}
