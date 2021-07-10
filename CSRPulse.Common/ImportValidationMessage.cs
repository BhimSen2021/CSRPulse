using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Common
{
    public class ImportValidationMessage
    {
        public static Dictionary<string, string> ImportErrMsg()
        {
            Dictionary<string, string> dicErrMessage = new Dictionary<string, string>();

            dicErrMessage.Add("HDR", "'{0}' Header(s) are missing in source sheet, please correct the source sheet and upload again.");
            dicErrMessage.Add("NF", "{0} Invalid.");
            dicErrMessage.Add("EX", "{0} Already exist.");
            dicErrMessage.Add("B", "{0} should not be blank.");
            dicErrMessage.Add("DI", "Found duplicate.");
            dicErrMessage.Add("SZ", "Should be Zero.");
            dicErrMessage.Add("SGZ", "Should be greater then Zero.");
            dicErrMessage.Add("NOREC", "No record found.");
            dicErrMessage.Add("NBPODNAME", "{0} can not be blank.");
            dicErrMessage.Add("BPODNAME", "{0} should be blank.");
            dicErrMessage.Add("UIIS", "{0} Imported item(s)  updated successfully");
            dicErrMessage.Add("IP", "Should be postive integer");
            dicErrMessage.Add("DUPE", "Found duplicate.");
            dicErrMessage.Add("MAXLEN50", "Value should not be more than 50 characters including spaces.");
            dicErrMessage.Add("MAXLEN150", "Value should not be more than 150 characters including spaces.");
            dicErrMessage.Add("MAXLEN250", "Value should not be more than 250 characters including spaces.");
            dicErrMessage.Add("MAXMIN3", "Value should not be less than 3 and more than 3 characters including spaces.");
            dicErrMessage.Add("MAXMIN200", "Value should not be less than 4 and more than 200 characters including spaces.");
            dicErrMessage.Add("MAXMIN4", "Value should not be less than 3 and more than 4 characters including spaces.");
            dicErrMessage.Add("IIS", "{0} Item(s) imported successfully.");
            dicErrMessage.Add("IE", "Invalid email format.");
            return dicErrMessage;

        }
    }
}
