using PrintingAppRepository.SystemVariables.Models;
using System.Collections.Generic;

namespace PrintingAppTests.SystemVariables {
    internal class PrintingStyleEqualityComparer : IEqualityComparer<PrintingStyleClass> {
        public bool Equals(PrintingStyleClass x, PrintingStyleClass y) {
            if((x.Equals(null) && !y.Equals(null)) || (y.Equals(null) && !x.Equals(null))) {
                return false;
            }
            else if(x.Bleeds == y.Bleeds && x.Gripper == y.Gripper && x.TailMargin == y.TailMargin && x.SideMargin == y.SideMargin) {
                return true;
            }
            return false;
        }

        public int GetHashCode(PrintingStyleClass obj) {
            return obj.GetHashCode();
        }
    }
}
