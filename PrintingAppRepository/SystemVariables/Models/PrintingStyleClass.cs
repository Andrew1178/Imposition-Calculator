namespace PrintingAppRepository.SystemVariables.Models {
    public class PrintingStyleClass {
        public PrintingStyleClass(float gripper = 0, float sideMargin = 0, float tailMargin = 0,
            float bleeds = 0) {
            Gripper = gripper;
            SideMargin = sideMargin;
            TailMargin = tailMargin;
            Bleeds = bleeds;
        }
        //Empty constructor for JSON deserialise
        public PrintingStyleClass() {

        }
        public float Gripper { get; set; }
        public float SideMargin { get; set; }
        public float TailMargin { get; set; }
        public float Bleeds { get; set; }
    }
}
