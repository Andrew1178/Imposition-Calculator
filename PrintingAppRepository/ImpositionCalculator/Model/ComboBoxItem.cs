namespace PrintingAppRepository.ImpositionCalculator.Model {
    public class ComboBoxItem {
        public ComboBoxItem(string text, int value) {
            Text = text;
            Value = value;
        }

        public string Text { get; set; }
        public int Value { get; set; }
    }
}
