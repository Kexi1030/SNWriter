using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNWrite.Models;

namespace SNWrite.Commands
{
    class AddOneSnToJson
    {

        public void SnToJson(string fileload,string oneSn)
        {
            CreatJsonFromInitalizationWindow creatJsonFromInitalizationWindow = new CreatJsonFromInitalizationWindow();
            SNinitalize sNinitalize = creatJsonFromInitalizationWindow.CreateSNFromJsonFile(fileload);

            var SNnumber = sNinitalize.SN.SNnumber;
        }
    }
}
