using TOPOBOX.OSC.TeamsTool.Common.DAL;
using Graph = Microsoft.Graph;

namespace TOPOBOX.OSC.TeamsTool.Common.Mapper
{
    /// <summary>
    /// BucketMapper
    /// </summary>
    public sealed class BucketMapper : BaseObjectMapper
    {
# pragma warning disable CS1591
        public static Bucket MapFrom(Graph.PlannerBucket plannerBucket)
        {
            var bucket = new Bucket();
            bucket.Id = plannerBucket.Id;
            bucket.Name = plannerBucket.Name;

            return bucket;
        }
# pragma warning restore CS1591

    }
}
