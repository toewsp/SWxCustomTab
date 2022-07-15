using SolidWorks.Interop.sldworks;

namespace SWxCustomTab.Shared
{
    public interface IUserDefinedDLL
    {

        SldWorks swApp { get; set; }
        void Start();

    }

}
