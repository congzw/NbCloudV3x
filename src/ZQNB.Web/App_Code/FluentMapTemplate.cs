using System.Collections.Generic;

namespace ZQNB.Web
{
    public class FluentMapTemplateModel
    {
        public FluentMapTemplateModel()
        {
            Members = new List<string>();
        }

        public string ClassName { get; set; }
        public string TableName { get; set; }
        public IList<string> Members { get; set; }
    }
}
