using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Common
{
    public static class DocumentUploadFilePath
    {
        public static string TempFilePath { get { return @"TempFiles\"; } }
        public static string ProjectFilePath { get { return @"documents\Project\"; } }
        public static string TemplatesLocationFilePath { get { return @"Templates\Location\"; } }
        public static string UserProfileImagePath { get { return @"assets\images\users\"; } }
        public static string PartnerFilePath { get { return @"documents\Partner\"; } }


    }
}
