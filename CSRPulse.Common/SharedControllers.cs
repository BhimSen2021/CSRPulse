using System.Collections.Generic;

namespace CSRPulse.Common
{
    public class SharedControllers
    {
        private SharedControllers()
        {
        }

        public static readonly List<string> controllers = new List<string>() {
            "base","account","registration","dashboard","quickemail","maintenance","customer","user","profile","role","state","district","block","village","uom","theme","subtheme","activity","subactivity","indicator","department","designation","process","partner", "processworkflow","auditreviewmodule","auditreviewparamter"

        };
    }
}
