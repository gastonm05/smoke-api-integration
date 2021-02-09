﻿using System;
using TechTalk.SpecFlow;

namespace Prod_Integration.Steps
{
    [Binding]
    public class TimeTransforms
    {
        /// <summary>
        /// Transforms Today into a DateTime, matching the provided Regex and adds the appropriate days
        /// </summary>
        /// <param name="days">Number of days to add</param>
        /// <returns>DateTime object</returns>
        [StepArgumentTransformation(@"Today plus (\d+) day(?:s)?")]
        public DateTime DateTimeTransformPlus(int days)
        {
            var today = DateTime.Today;
            return today.AddDays(days).Date;
        }

        /// <summary>
        /// Transforms Today into a DateTime, matching the provided Regex and subtracts the appropriate days
        /// </summary>
        /// <param name="days">Number of days to subtract</param>
        /// <returns>DateTime object</returns>
        [StepArgumentTransformation(@"Today minus (\d+) day(?:s)?")]
        public DateTime DateTimeTransformMinus(int days)
        {
            var today = DateTime.Today;
            return today.AddDays(-days).Date;
        }

        /// <summary>
        /// Transforms Today into a DateTime, matching the provided Regex
        /// </summary>
        /// <returns>DateTime object</returns>
        [StepArgumentTransformation(@"Today")]
        public DateTime DateTimeTransformToday()
        {
            var today = DateTime.Today;
            return today.Date;
        }
    }
}
