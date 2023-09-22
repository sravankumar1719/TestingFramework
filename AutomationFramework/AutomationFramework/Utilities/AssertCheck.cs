using AutomationFramework.Utilities.Enum;
using System.Text;

namespace AutomationFramework.Utilities
{
    /// <summary>
    /// Assert Check.
    /// </summary>
    public class AssertCheck
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssertCheck"/> class.
        /// </summary>
        /// <param name="expectedConditionMessage"> The expected condition message. </param>
        public AssertCheck(string expectedConditionMessage)
        {
            this.ExpectedConditionMessage = expectedConditionMessage;
            this.AssertOutcome = AssertOutcome.Inconclusive;
        }

        /// <summary>
        /// Gets or sets the assert outcome.
        /// </summary>
        public AssertOutcome AssertOutcome { get; set; }

        /// <summary>
        /// The expected condition message.
        /// </summary>
        public string ExpectedConditionMessage { get; set; }

        /// <summary>
        /// The failure message.
        /// </summary>
        public string FailureMessage { get; set; }

        /// <summary>
        /// To string method.
        /// </summary>
        /// <returns> The <see cref="string"/>. </returns>
        public override string ToString()
        {
            var finalMessage = new StringBuilder();
            finalMessage.AppendLine($"<Assert> {this.ExpectedConditionMessage} </Assert>");
            finalMessage.AppendLine($"<Outcome> {this.AssertOutcome} </Outcome>");
            if (!string.IsNullOrEmpty(this.FailureMessage))
            {
                finalMessage.AppendLine($"<Failure Message> {this.FailureMessage} </Failure Message>");
            }

            return finalMessage.ToString();
        }
    }
}
