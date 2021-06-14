using System.Collections.Generic;

namespace CSRPulse.Common
{
    public class SharedControllers
    {
        private SharedControllers()
        {
        }

        public static readonly List<string> controllers = new List<string>() {
            "base","account","registration","dashboard","quickemail","maintenance","customer","user","profile","role"
        };
    }
}
