using System.Collections.Generic;

namespace PrintingAppRepository.SystemVariables.Models {
    public class SystemVariables {
        //Default values
        public SystemVariables(float? bindingLip = 0.25F, float? headTrim = 0.1875F, float? footTrim = 0.125F) {
            BindingLip = bindingLip.Value;
            HeadTrim = headTrim.Value;
            FootTrim = footTrim.Value;
            CutOff = new List<float>() {
                17,
                17.75F,
                22,
                22.75F,
                23.5F,
                28
            };
            RollSize = new List<float>() {
                17.5F,
                18,
                19,
                23.5F,
                26.5F,
                35,
                35.5F,
                38
            };
            SheetSize = new List<string>() {
                "17.5 x 22.5",
                "18 x 24",
                "19 x 25",
                "20 x 26",
                "20 x 28",
                "23 x 29",
                "23 x 35",
                "23.5 x 34",
                "24 x 29",
                "24 x 36",
                "25 x 38",
                "25.5 x 38",
                "26 x 40",
                "28 x 40",
                "22.5 x 28.5",
                "22.5 x 35",
                "25.5 x 30.5"
            };
            PrintingStyles = new Dictionary<string, PrintingStyleClass>() {
                {"Sheet Fed", new PrintingStyleClass(0.5F, 1, 0.5F, 0.125F) },
                {"Heat Set", new PrintingStyleClass(0.125F, 0.125F, 0.125F, 0.125F) },
                { "UV", new PrintingStyleClass() },
                {"Digital", new PrintingStyleClass() }
            };
        }

        public float BindingLip { get; set; }
        public float HeadTrim { get; set; }
        public float FootTrim { get; set; }
        public List<float> CutOff { get; set; }
        public List<float> RollSize { get; set; }
        public List<string> SheetSize { get; set; }
        public Dictionary<string, PrintingStyleClass> PrintingStyles { get; set; }
    }
}
