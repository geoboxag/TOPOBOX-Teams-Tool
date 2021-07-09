namespace TOPOBOX.OSC.TeamsTool.Common.Mapper
{
    /// <summary>
    /// Interface for all Object Mapper
    /// </summary>
    public interface IObjectMapper
    {
        // ToDo C#8 make Map static

        /// <summary>
        /// Map from "Graph" Object to "Internal" Object
        /// </summary>
        /// <param name="graphObject">Object from Azure Graph NS</param>
        /// <returns>Object from DAL NS</returns>
        object MapFrom(object graphObject);

        /// <summary>
        /// Map from "Internal" Object to "Graph" Object
        /// </summary>
        /// <param name="internalObject">Object from DAL NS</param>
        /// <returns>Object from Azure Graph NS</returns>
        object MapTo(object internalObject);
    }
}
