using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeSipDashboard.Model
{
    public class CoasterCollection
    {
        //I have to use this voodoo thing to hand data to xaml
        public ObservableCollection<Coaster> Coasters { get; set; }
        public CoasterCollection DataContext { get; private set; }

        public CoasterCollection() {

            Coasters = new ObservableCollection<Coaster>();
            //not using a helper method here because I'm lazy
            //Coasters.Add(new Coaster(0, "Steve Bannon", "1234", "5678"));
            //Coasters.Add(new Coaster(1, "Karen Swanson", "0987", "6543"));
        }

    }

}
