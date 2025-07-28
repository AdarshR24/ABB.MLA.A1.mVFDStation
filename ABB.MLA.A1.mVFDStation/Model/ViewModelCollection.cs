using ABB.MLA.A1.mVFDStation.HelperClasses;
using ABB.MLA.A1.mVFDStation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ABB.MLA.A1.mVFDStation.Model
{
    public class ViewModelCollection
    {

        public HomeViewModel HomeViewModel { get; set; }

        public ModbusClass mb { get; set; }
        public ViewModelCollection(ModbusClass mb)
        {
            HomeViewModel = new HomeViewModel(mb);
        }
    }
}
