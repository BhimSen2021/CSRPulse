using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Common
{
    public static class DocumentUploadFilePath
    {
        public static string TempFilePath { get { return @"TempFiles\"; } }
        public static string ProjectFilePath { get { return @"documents\project\"; } }
        public static string TemplatesLocationFilePath { get { return @"Templates\location\"; } }
        public static string UserProfileImagePath { get { return @"assets\images\users\"; } }
        public static string AuditorFilePath { get { return @"documents\auditor\"; } }


    }
}
