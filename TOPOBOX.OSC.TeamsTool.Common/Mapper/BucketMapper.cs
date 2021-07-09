using TOPOBOX.OSC.TeamsTool.Common.DAL;
using Graph = Microsoft.Graph;

namespace TOPOBOX.OSC.TeamsTool.Common.Mapper
{
    /// <summary>
    /// PlannerMapper
    /// </summary>
    public sealed class BucketMapper : BaseObjectMapper
    {
        public static Bucket MapFrom(Graph.PlannerBucket plannerBucket)
        {
            var bucket = new Bucket();
            bucket.Id = plannerBucket.Id;
            bucket.Name = plannerBucket.Name;

            return bucket;
        }

    }
}
