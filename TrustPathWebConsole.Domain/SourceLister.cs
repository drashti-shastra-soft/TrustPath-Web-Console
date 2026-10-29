using System.Collections.Generic;

namespace TrustPathWebConsole.Domain
{
    public class SourceLister
    {
        public List<Source> List { get; set; } = new List<Source>();

        public Source SearchCriteria { get; set; } = new Source();

        public Pagination Pagination { get; set; } = new Pagination();
    }
}
