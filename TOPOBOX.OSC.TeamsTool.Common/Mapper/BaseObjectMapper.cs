using System;
using System.Globalization;

namespace TOPOBOX.OSC.TeamsTool.Common.Mapper
{
    /// <summary>
    /// Base Mapper for all data mapper
    /// </summary>
    public class BaseObjectMapper : IObjectMapper
    {
        /// <summary>
        /// Map from "Graph" Object to "Internal" Object
        /// </summary>
        /// <param name="graphObject">Object from Azure Graph NS</param>
        /// <returns>Object from DAL NS</returns>
        public virtual object MapFrom(object graphObject)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Map from "Internal" Object to "Graph" Object
        /// </summary>
        /// <param name="internalObject">Object from DAL NS</param>
        /// <returns>Object from Azure Graph NS</returns>
        public virtual object MapTo(object internalObject)
        {
            throw new NotImplementedException();
        }

        #region Value Mapper and Converters
        /// <summary>
        /// Convert string to long
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static long? GetValueLong(string input)
        {
            if (string.IsNullOrEmpty(input)) return null;

            try
            {
                return long.Parse(input, NumberStyles.AllowDecimalPoint);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Convert string to datetime
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static DateTime? GetValueDate(string input)
        {
            if (string.IsNullOrEmpty(input)) return null;

            try
            {
                return Convert.ToDateTime(input);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Convert string to decimal
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static decimal? GetValueDecimal(string input)
        {
            if (string.IsNullOrEmpty(input)) return null;

            try
            {
                return decimal.Parse(input, NumberStyles.AllowDecimalPoint);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Convert decimal to formated decimal
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static decimal? GetDecimalWithTwoDecimalPlaces(decimal? input)
        {
            if (!input.HasValue) return null;

            try
            {
                return decimal.Parse(string.Format("{0:0.00}", input), NumberStyles.AllowDecimalPoint);
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}