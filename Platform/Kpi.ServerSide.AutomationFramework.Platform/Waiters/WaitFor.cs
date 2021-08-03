using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Kpi.ServerSide.AutomationFramework.Platform.Waiters
{
    public static class WaitFor
    {
        /// <summary>
        /// Wait for the passed function to return true. Throws exception when passed function has returned false on every call.
        /// Default timeout is 30 seconds.
        /// </summary>
        /// <param name="waitCondition">Function that is expected to return true before timeout. Upon retrieving true the waiter returns.</param>
        /// <param name="timeoutMessage">Verbose error message that explains why the function never returned true.</param>
        public static void Condition(Func<bool> waitCondition, string timeoutMessage)
        {
            Condition(waitCondition, timeoutMessage, ExceptionsDuringWait.Ignore);
        }

        /// <summary>
        /// Wait for the passed function to return true. Throws exception when passed function has returned false on every call.
        /// Default timeout is 30 seconds
        /// </summary>
        /// <param name="waitCondition">Function that is expected to return true before timeout. Upon retrieving true the waiter returns.</param>
        /// <param name="timeoutMessage">Verbose error message that explains why the function never returned true.</param>
        /// <param name="ignoreExceptionsDuringWait">If true it will ignore all exceptions during waiting</param>
        public static void Condition(Func<bool> waitCondition, string timeoutMessage,
            ExceptionsDuringWait ignoreExceptionsDuringWait)
        {
            Condition(waitCondition, timeoutMessage, TimeSpan.FromSeconds(30), ignoreExceptionsDuringWait);
        }

        /// <summary>
        /// Wait for the passed function to return true. Throws exception when passed function has returned false on every call
        /// </summary>
        /// <param name="waitCondition">Function that is expected to return true before timeout. Upon retrieving true the waiter returns.</param>
        /// <param name="timeoutMessage">Verbose error message that explains why the function never returned true.</param>
        /// <param name="maxWaitTime">Maximum timeout. When it's reached, an error with the timeout message is thrown.</param>
        /// <param name="timeStep">Timeout between retries.</param>
        public static void Condition(Func<bool> waitCondition, string timeoutMessage, TimeSpan maxWaitTime,
            TimeSpan timeStep = default(TimeSpan))
        {
            Condition(waitCondition, timeoutMessage, maxWaitTime, ExceptionsDuringWait.Ignore, timeStep);
        }

        /// <summary>
        /// Wait for the passed function to return true. Throws exception when passed function has returned false on every call
        /// </summary>
        /// <param name="waitCondition">Function that is expected to return true before timeout. Upon retrieving true the waiter returns.</param>
        /// <param name="timeoutMessage">Verbose error message that explains why the function never returned true.</param>
        /// <param name="maxWaitTime">Maximum timeout. When it's reached, an error with the timeout message is thrown.</param>
        /// <param name="ignoreExceptionsDuringWait">If true it will ignore all exceptions during waiting</param>
        /// <param name="timeStep">Timeout between retries.</param>
        public static void Condition(Func<bool> waitCondition, string timeoutMessage, TimeSpan maxWaitTime,
            ExceptionsDuringWait ignoreExceptionsDuringWait, TimeSpan timeStep = default)
        {
            StringBuilder exceptionsDuringWait = new StringBuilder();
            Stopwatch stopwatch = Stopwatch.StartNew();
            TimeSpan step;
            if (timeStep == default)
            {
                step = TimeSpan.FromMilliseconds(maxWaitTime.TotalMilliseconds / 20);
                step = step > TimeSpan.FromSeconds(10) ? TimeSpan.FromSeconds(10) : step;
            }
            else
            {
                step = timeStep;
            }

            int checksDone = 0;

            while (stopwatch.Elapsed < maxWaitTime || checksDone == 0)
            {
                try
                {
                    if (waitCondition())
                    {
                        return;
                    }
                }
                catch (Exception e)
                {
                    if (ignoreExceptionsDuringWait == ExceptionsDuringWait.Collect)
                    {
                        exceptionsDuringWait.AppendLine(e.Message);
                    }

                    if (ignoreExceptionsDuringWait == ExceptionsDuringWait.CollectWithStackTrace)
                    {
                        exceptionsDuringWait.AppendLine(e.ToString());
                    }
                }

                checksDone++;
                Thread.Sleep(step);
            }

            string exceptionMsg = $"Timeout after {maxWaitTime.TotalSeconds} seconds: {timeoutMessage}";
            if ((ignoreExceptionsDuringWait == ExceptionsDuringWait.Collect
                 || ignoreExceptionsDuringWait == ExceptionsDuringWait.CollectWithStackTrace)
                && !string.IsNullOrEmpty(exceptionsDuringWait.ToString()))
            {
                throw new Exception($"{exceptionMsg} . Exceptions During Wait:( {exceptionsDuringWait} )");
            }

            throw new Exception(exceptionMsg);
        }
    }
}
