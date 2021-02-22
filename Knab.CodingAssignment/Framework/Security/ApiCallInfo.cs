using System;

namespace Knab.CodingAssignment.Framework.Security
{
    public class ApiCallInfo
    {
        public ApiCallInfo(double growthFactor, int resetAfterPeriodInSeconds)
        {
            GrowthFactor = growthFactor;
            ResetAfterPeriodInSeconds = resetAfterPeriodInSeconds;
            BlockPeriodInSeconds = 1;
            CanCallSince = DateTime.Now;
        }

        public DateTime CanCallSince { get; private set; }
        public double BlockPeriodInSeconds { get; private set; }
        public int ResetAfterPeriodInSeconds { get; set; }
        public double GrowthFactor { get; }

        public void Next()
        {
            if (DateTime.Now > CanCallSince.AddSeconds(ResetAfterPeriodInSeconds))
                BlockPeriodInSeconds = 1;
            else
                BlockPeriodInSeconds *= GrowthFactor;

            CanCallSince = DateTime.Now.AddSeconds(BlockPeriodInSeconds);
        }

        public bool CanCall()
        {
            return DateTime.Now > CanCallSince;
        }
    }
}