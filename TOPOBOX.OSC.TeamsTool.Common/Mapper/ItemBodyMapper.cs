using TOPOBOX.OSC.TeamsTool.Common.DAL;
using Graph = Microsoft.Graph;

namespace TOPOBOX.OSC.TeamsTool.Common.Mapper
{
    /// <summary>
    /// ItemBodyMapper
    /// </summary>
    public sealed class ItemBodyMapper : BaseObjectMapper
    {
# pragma warning disable CS1591
        public static ItemBody MapFrom(Graph.ItemBody graphItemBody)
        {
            return new ItemBody()
            {
                ContentType = graphItemBody.ContentType,
                Content = graphItemBody.Content
            };
        }

        public static Graph.ItemBody MapTo(ItemBody itemBody)
        {
            return new Graph.ItemBody()
            {
                ContentType = itemBody.ContentType,
                Content = itemBody.Content
            };
        }
# pragma warning restore CS1591

    }
}
