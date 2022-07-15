using System;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SWxCustomTab.Shared;

namespace SWxCustomTab.TestDLL
{
    public class Plugin : IUserDefinedDLL
    {

        public SldWorks swApp { get; set; }
        public void Start()
        {

            swApp.SendMsgToUser2(String.Format("Test von {0}", this.GetType().FullName), (int)swMessageBoxIcon_e.swMbInformation, (int)swMessageBoxBtn_e.swMbOk);

        }

    }

}
