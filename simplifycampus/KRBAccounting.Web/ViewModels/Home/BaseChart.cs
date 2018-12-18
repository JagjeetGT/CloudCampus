using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.Home
{
    public class BaseChart<T>
    {
        /// <summary>
        /// This is the base chart for all simple charts.
        /// </summary>
        /// <typeparam name="T">The type constraint for the type of data contained in the chart.</typeparam>
        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public List<string> Categories { get; set; }

        ///// <summary>
        ///// Gets or sets the data.
        ///// </summary>
        ///// <value>
        ///// The chart data.
        ///// </value>
        //public List<T> Data { get; set; }

        /// <summary>
        /// Gets or sets the begin date.
        /// </summary>
        /// <value>
        /// The begin date.
        /// </value>
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate { get; set; }
    }
}