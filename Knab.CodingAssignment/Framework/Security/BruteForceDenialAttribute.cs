using System;
using System.Collections.Generic;
using Knab.CodingAssignment.Framework.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Knab.CodingAssignment.Framework.Security
{
    public class BruteForceDenialAttribute : ActionFilterAttribute
    {
        private static readonly Dictionary<string, ApiCallInfo> _dicCallCounts;
        private readonly double _blockPeriodGrowthFactor;
        private readonly int _resetAfterPeriodInSeconds;

        static BruteForceDenialAttribute()
        {
            _dicCallCounts = new Dictionary<string, ApiCallInfo>();
        }

        /// <summary>
        /// Blocks if the api is constantly called
        /// </summary>
        /// <param name="blockPeriodGrowthFactor">The growth factor by which the block time is increased</param>
        /// <param name="resetAfterPeriodInSeconds">The time, in seconds, in which if the api is not called by the user the next block period will be reset to 1 second.</param>
        public BruteForceDenialAttribute(double blockPeriodGrowthFactor, int resetAfterPeriodInSeconds)
        {
            _blockPeriodGrowthFactor = blockPeriodGrowthFactor;
            _resetAfterPeriodInSeconds = resetAfterPeriodInSeconds;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var ip = context.HttpContext.Connection.RemoteIpAddress?.ToString();

            var key = $"RequestCount-{ip}";

            if (_dicCallCounts.TryGetValue(key, out ApiCallInfo lastCallInfo))
            {
                if (!lastCallInfo.CanCall())
                {
                    _dicCallCounts[key].Next();

                    //*** Attention ***
                    //This error will be handled by the custom exception middleware. (In production env.)
                    //While debugging, the code will break here, but you can continue running to see the result on the client side.
                    throw new GeneralApplicationException("Too many requests.");
                }

                _dicCallCounts[key].Next();
            }
            else
            {
                _dicCallCounts.Add(key, new ApiCallInfo(_blockPeriodGrowthFactor, _resetAfterPeriodInSeconds));
            }
        }
    }
}