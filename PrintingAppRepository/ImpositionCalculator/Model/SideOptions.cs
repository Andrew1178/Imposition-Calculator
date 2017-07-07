using System.Collections.Generic;

namespace PrintingAppRepository.ImpositionCalculator.Model {
    public class SideOptions {
        public SideOptions(ComboBoxItem[] ink, ComboBoxItem[] coating) {
            Ink = ink;
            Coating = coating;
        }

        public ComboBoxItem[] Ink { get; private set; }
        public ComboBoxItem[] Coating { get; private set; }
    }
}
