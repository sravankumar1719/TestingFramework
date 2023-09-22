using System.Text;
using Xunit.Sdk;
using Xunit;
using AutomationFramework.Utilities.Enum;

namespace AutomationFramework.Utilities
{
    public class SoftAssertions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SoftAssertions"/> class.
        /// </summary>
        public SoftAssertions()
        {
            this.Asserts = new List<AssertCheck>();
        }

        /// <summary>
        /// Gets or sets the assertions.
        /// </summary>
        public List<AssertCheck> Asserts { get; set; }

        /// <summary>
        /// Adding all the asserts to a list.
        /// </summary>
        /// <param name="assertChecks"> The assert checks. </param>
        public void AddAssertions(params AssertCheck[] assertChecks)
        {
            foreach (var assert in assertChecks)
            {
                if (!this.Asserts.Any(assert => assert.ExpectedConditionMessage.Equals(assert.ExpectedConditionMessage)))
                {
                    this.Asserts.Add(assert);
                }
            }
        }

        /// <summary>
        /// Checks if expected result and actual result are equal
        /// </summary>
        /// <param name="assert"> The assert. </param>
        /// <param name="expected"> The expected result. </param>
        /// <param name="actual"> The actual result. </param>
        /// <param name="failureMessage"> The failure message. </param>
        /// <typeparam name="T">  Type of result </typeparam>
        public void AreEqual<T>(AssertCheck assert, T expected, T actual, string failureMessage)
        {
            AssertCheck assertCheck = this.GetAssertCheck(assert);
            var finalMessage = new StringBuilder();
            finalMessage.AppendLine(failureMessage);
            finalMessage.AppendLine($"Expected Result -> {expected} and Actual result -> {actual}");

            assertCheck.FailureMessage = finalMessage.ToString();
            try
            {
                Assert.Equal(expected, actual);
                assertCheck.AssertOutcome = AssertOutcome.Passed;
            }
            catch (AssertActualExpectedException)
            {
                assertCheck.AssertOutcome = AssertOutcome.Failed;
            }
        }

        /// <summary>
        /// Checks if expected result and actual result are not equal
        /// </summary>
        /// <param name="assert"> The assert. </param>
        /// <param name="expected"> The expected result. </param>
        /// <param name="actual"> The actual result. </param>
        /// <param name="failureMessage"> The failure message. </param>
        /// <typeparam name="T">  Type of result </typeparam>
        public void AreNotEqual<T>(AssertCheck assert, T expected, T actual, string failureMessage)
        {
            AssertCheck assertCheck = this.GetAssertCheck(assert);
            var finalMessage = new StringBuilder();
            finalMessage.AppendLine(failureMessage);
            finalMessage.AppendLine($"Expected Result -> {expected} and Actual result -> {actual}");

            assertCheck.FailureMessage = finalMessage.ToString();
            try
            {
                Assert.NotEqual(expected, actual);
                assertCheck.AssertOutcome = AssertOutcome.Passed;
            }
            catch (AssertActualExpectedException)
            {
                assertCheck.AssertOutcome = AssertOutcome.Failed;
            }
        }

        /// <summary>
        /// Checks if the expected result is true
        /// </summary>
        /// <param name="assert"> The assert. </param>
        /// <param name="expected"> The expected result. </param>
        /// <param name="failureMessage"> The failure message. </param>
        public void IsTrue(AssertCheck assert, bool expected, string failureMessage)
        {
            AssertCheck assertCheck = this.GetAssertCheck(assert);
            assertCheck.FailureMessage = failureMessage;
            try
            {
                Assert.True(expected, failureMessage);
                assertCheck.AssertOutcome = AssertOutcome.Passed;
            }
            catch (AssertActualExpectedException)
            {
                assertCheck.AssertOutcome = AssertOutcome.Failed;
            }
        }

        /// <summary>
        /// Checks if the expected result is false
        /// </summary>
        /// <param name="assert"> The assert. </param>
        /// <param name="expected"> The expected result. </param>
        /// <param name="failureMessage"> The failure message. </param>
        public void IsFalse(AssertCheck assert, bool expected, string failureMessage)
        {
            AssertCheck assertCheck = this.GetAssertCheck(assert);
            assertCheck.FailureMessage = failureMessage;
            try
            {
                Assert.False(expected, failureMessage);
                assertCheck.AssertOutcome = AssertOutcome.Passed;
            }
            catch (AssertActualExpectedException)
            {
                assertCheck.AssertOutcome = AssertOutcome.Failed;
            }
        }

        /// <summary>
        /// Returns the Assert check
        /// </summary>
        /// <param name="assert"> The assert. </param>
        /// <returns> Returns the Assert check. </returns>
        private AssertCheck GetAssertCheck(AssertCheck assert)
        {
            try
            {
                return this.Asserts.First(assertCheck => assertCheck.ExpectedConditionMessage.Equals(assert.ExpectedConditionMessage));
            }
            catch (Exception)
            {
                throw new Exception($"Assert -> '{assert.ExpectedConditionMessage}' is not added to the list.");
            }
        }
    }
}
