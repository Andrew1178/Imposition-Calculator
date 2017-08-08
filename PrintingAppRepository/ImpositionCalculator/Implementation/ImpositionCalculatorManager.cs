using System;
using PrintingAppRepository.SignatureSize.Model;
using System.Text.RegularExpressions;
using PrintingAppRepository.ImpositionCalculator.Model;

namespace PrintingAppRepository.ImpositionCalculator.Implementation {
    public class ImpositionCalculatorManager : IImpositionCalculatorManager {
        private readonly IImpositionCalculatorRepository _impositionCalculatorRepository;
        public ImpositionCalculatorManager(IImpositionCalculatorRepository impositionCalculatorRepository) {
            _impositionCalculatorRepository = impositionCalculatorRepository;
        }

        /// <summary>
        /// Calculate all printing design parameters for the first option which the client may choose
        /// </summary>
        /// <param name="props"></param>
        /// <returns></returns>
        public Option ReturnFinalOptionOne(PropertiesToCalculateOptionsWith props) {
            Option option = new Option();

            if (props.SignatureSizeWidth > 0) {
                option.Around = (int)Math.Floor((props.SheetSizeAround - (props.Gripper
                    + props.TailMargin)) / props.SignatureSizeWidth);
            }
            else {
                option.Around = (int)Math.Floor((props.SheetSizeAround - props.Gripper
                    - props.TailMargin) / (props.PageSizeWidth + props.Bleeds));
            }

            if (props.SignatureSizeLength > 0) {
                option.Across = (int)Math.Floor((props.SheetSizeAcross - props.SideMargin)
                    / props.SignatureSizeLength);
            }
            else {
                option.Across = (int)Math.Floor((props.SheetSizeAcross - props.SideMargin)
                    / (props.PageSizeLength + props.Bleeds));
            }

            option.Out = option.Around * option.Across;

            if (props.SignatureSizeWidth > 0) {
                option.Area = (float)Math.Round((props.SignatureSizeWidth * option.Around) 
                    * (props.SignatureSizeLength * option.Across), 2);
            }
            else {
                option.Area = (float)Math.Round(((props.PageSizeWidth + props.Bleeds) 
                    * option.Around) * ((props.PageSizeLength + props.Bleeds) * option.Across), 2);
            }

            if (option.Out == 0) {
                option.Wasteage = 0;
            }
            else {
                option.Wasteage = (int)Math.Round((1 - (option.Area / (props.SheetSizeAround * props.SheetSizeAcross))) * 100);
            }
            return option;
        }

        /// <summary>
        /// Calculate the printing design parameters for option 2 which the client may choose
        /// </summary>
        /// <param name="props"></param>
        /// <returns></returns>
        public Option ReturnFinalOptionTwo(PropertiesToCalculateOptionsWith props) {
            Option option = new Option();

            if (props.SignatureSizeWidth > 0) {
                option.Across = (int)Math.Floor(((props.SheetSizeAcross - props.SideMargin)
                    / props.SignatureSizeWidth));
            }
            else {
                option.Across = (int)Math.Floor((props.SheetSizeAcross - props.SideMargin)
                    / (props.PageSizeWidth + props.Bleeds));
            }

            if (props.SignatureSizeLength > 0) {
                option.Around = (int)Math.Floor((props.SheetSizeAround - props.Gripper
                    - props.TailMargin) / props.SignatureSizeLength);
            }
            else {
                option.Around = (int)Math.Floor((props.SheetSizeAround - props.Gripper
                    - props.TailMargin) / (props.PageSizeLength + props.Bleeds));
            }

            option.Out = option.Across * option.Around;

            if (props.SignatureSizeWidth > 0) {
                option.Area = (float)Math.Round((props.SignatureSizeWidth * option.Across)
                    * (props.SignatureSizeLength * option.Around), 2);
            }
            else {
                option.Area = (float)Math.Round(((props.PageSizeWidth + props.Bleeds)
                    * option.Across) * ((props.PageSizeLength + props.Bleeds) * option.Around), 2);
            }

            if (option.Out == 0) {
                option.Wasteage = 0;
            }
            else {
                option.Wasteage = (int)Math.Round((1 - (option.Area / (props.SheetSizeAround
                    * props.SheetSizeAcross))) * 100);
            }

            return option;
        }

        public ComboBoxItem[] ReturnInkDataSource() {
            return _impositionCalculatorRepository.ReturnInkDataSource();
        }

        public ComboBoxItem[] ReturnCoatingDataSource() {
            return _impositionCalculatorRepository.ReturnCoatingDataSource();
        }

        /// <summary>
        /// Calculate the SignatureSizeLength. Depending on the PagesPerSignature variables, it can
        /// either be calculated via an automatic multiplier or it can be manually set. Please see
        /// below for more information
        /// 
        /// The default is set to'2pg', this is set in the model when null is passed through
        /// as the multiplier in lengthParameters.
        /// If the PagesPerSignature is either a 'right angle' or 'right angle album', then it must
        /// be set manually as there are no correlation between the lengths.
        /// If the PagesPerSignature is equal to 'tear apart one', the multipliers can be calculated
        /// as it is the page / 4
        /// If PagerPerSignature is either 'parallel' or 'standard', then the default multipliers
        /// will work fine.
        /// </summary>
        /// <param name="lengthParamaters"></param>
        /// <returns></returns>
        public float ReturnSignatureSizeLength(SignatureSizeLength lengthParamaters) {
            if (lengthParamaters.PagesPerSignature.Contains("right angle")) {
                //No need for the 8pg right angle as the default will work
                switch (lengthParamaters.PagesPerSignature.ToLower()) {
                    case "12pg right angle":
                    case "32pg right angle":
                        lengthParamaters.ValueOneMultiplier = 3;
                        lengthParamaters.ValueTwoMultiplier = 3;
                        lengthParamaters.ValueThreeMultipler = 3;
                        break;
                    case "16pg right angle":
                    case "24pg right angle":
                        lengthParamaters.ValueOneMultiplier = 2;
                        lengthParamaters.ValueTwoMultiplier = 2;
                        lengthParamaters.ValueThreeMultipler = 2;
                        break;
                    case "16pg right angle album":
                        lengthParamaters.ValueOneMultiplier = 4;
                        lengthParamaters.ValueTwoMultiplier = 4;
                        lengthParamaters.ValueThreeMultipler = 4;
                        break;
                }
            }
            else if (lengthParamaters.PagesPerSignature.Contains("tear apart")) {
                //Regex only pulls ints
                int amountOfPages = int.Parse(Regex.Match(lengthParamaters.PagesPerSignature, "\\d+")
                    .Value);

                int allMultipliers = amountOfPages / 4;

                lengthParamaters.ValueOneMultiplier = allMultipliers;
                lengthParamaters.ValueTwoMultiplier = allMultipliers;
                lengthParamaters.ValueThreeMultipler = allMultipliers;
            }

            return (lengthParamaters.Length * lengthParamaters.ValueOneMultiplier)
                + (lengthParamaters.HeadTrim * lengthParamaters.ValueTwoMultiplier)
                + (lengthParamaters.FootTrim * lengthParamaters.ValueThreeMultipler);
        }

        public float ReturnSignatureSizeWidth(SignatureSizeWidth widthParameters) {
            //Regex to only pull ints
            int amountOfPages = int.Parse(Regex.Match(widthParameters.PagesPerSignature, "\\d+")
                .Value);

            if (widthParameters.PagesPerSignature.Contains("right angle")) {
                if (amountOfPages <= 12) {
                    widthParameters.ValueOneMultiplier = 2;
                    widthParameters.ValueTwoMultiplier = 2;
                    widthParameters.ValueThreeMultipler = 1;
                }
                else {
                    widthParameters.ValueOneMultiplier = 4;
                    widthParameters.ValueTwoMultiplier = 4;
                    widthParameters.ValueThreeMultipler = 2;
                }
            }
            else if (widthParameters.PagesPerSignature.Contains("parallel")) {
                widthParameters.ValueOneMultiplier = amountOfPages / 2;
                widthParameters.ValueTwoMultiplier = amountOfPages / 2;
                widthParameters.ValueThreeMultipler = (int)Math.Floor(amountOfPages / 4D);
            }
            else if (widthParameters.PagesPerSignature.Contains("2up tear apart")) {
                widthParameters.ValueOneMultiplier = 4;
                widthParameters.ValueTwoMultiplier = 4;
                widthParameters.ValueThreeMultipler = 2;
            }
            else if (widthParameters.PagesPerSignature.Contains("4pg")) {
                widthParameters.ValueOneMultiplier = widthParameters.ValueOneMultiplier * 2;
            }

            return (widthParameters.Width * widthParameters.ValueOneMultiplier)
                + (widthParameters.Bleeds * widthParameters.ValueTwoMultiplier)
                + (widthParameters.BindingLip * widthParameters.ValueThreeMultipler);
        }
    }
}