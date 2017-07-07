using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using PrintingAppRepository.PrintingDesign.Models;

namespace PrintingAppRepository.PrintingDesign.Implementation {
    public class PrintingDesignManager : IPrintingDesignManager {
        private readonly IPrintingDesignRepository _printingDesignRepo;
        public PrintingDesignManager(IPrintingDesignRepository printingDesignRepo) {
            this._printingDesignRepo = printingDesignRepo;
        }

        public PagePrintingDesignParameters ReturnPagePrintingDesignParams(int maxClientViewHeight, int maxClientViewWidth) {
           return _printingDesignRepo.ReturnPagePrintingDesignParams(maxClientViewHeight, maxClientViewWidth);
        }

        public IList<Rectangle> ReturnPages(PagePrintingDesignParameters pageParameters) {
            List<Rectangle> rectList = new List<Rectangle>();
            float xSpacesAroundRectangles = 0;
            float ySpacesAroundRectangles = 0;
            float xSpaceInbetweenRectangles = 0;
            float ySpaceInbetweenRectangles = 0;

            float eachRectWidth = (pageParameters.PageSizeWidth * pageParameters.PointsPerInch) * pageParameters.ScalingRatio;
            float eachRectHeight = (pageParameters.PageSizeHeight * pageParameters.PointsPerInch) * pageParameters.ScalingRatio;

            pageParameters.Scale = $"Scale: 1 inch: {Math.Round(pageParameters.PointsPerInch * pageParameters.ScalingRatio)} points.";

            xSpacesAroundRectangles = (((pageParameters.OriginalSheetWidth - (pageParameters.PageSizeWidth * pageParameters.PagesAcross) -
                (pageParameters.PagesAcross - 1) * pageParameters.Bleeds) / 2) * pageParameters.PointsPerInch) * pageParameters.ScalingRatio;

            ySpacesAroundRectangles = (((pageParameters.OriginalSheetHeight - (pageParameters.PageSizeHeight * pageParameters.PagesUp) - (pageParameters.PagesUp - 1)
                * pageParameters.Bleeds) / 2) * pageParameters.PointsPerInch) * pageParameters.ScalingRatio;

            xSpaceInbetweenRectangles = ((pageParameters.UpScaledSheetWidth - (eachRectWidth * pageParameters.PagesAcross) - (xSpacesAroundRectangles * 2))
                / pageParameters.PagesAcross);
            ySpaceInbetweenRectangles = ((pageParameters.UpScaledSheetHeight) - (eachRectHeight * pageParameters.PagesUp) - (ySpacesAroundRectangles * 2))
                / pageParameters.PagesUp;


            int intVersionOfEachRectWidth = (int)Math.Round(eachRectWidth);
            int intVersionOfEachRectHeight = (int)Math.Round(eachRectHeight);

            if (xSpaceInbetweenRectangles <= 1) {
                xSpaceInbetweenRectangles = 1;
            }

            if (ySpaceInbetweenRectangles <= 1) {
                ySpaceInbetweenRectangles = 1;
            }

            //Will need to one for loop and once you get on the second line of rectangles, change stuff up to work it out
            for (int j = 0; j < pageParameters.PagesUp; j++) {
                for (int i = 0; i < pageParameters.PagesAcross; i++) {
                    if (j == 0) {
                        if (i == 0) {
                            rectList.Add(new Rectangle((int)Math.Round(pageParameters.SheetXPosition + xSpacesAroundRectangles),
                        (int)Math.Round(pageParameters.SheetYPosition + ySpacesAroundRectangles), intVersionOfEachRectWidth, intVersionOfEachRectHeight));
                        }
                        else {
                            rectList.Add(new Rectangle((int)Math.Round(rectList[i - 1].X + xSpaceInbetweenRectangles + eachRectWidth),
                       (int)Math.Round(pageParameters.SheetYPosition + ySpacesAroundRectangles), intVersionOfEachRectWidth, intVersionOfEachRectHeight));
                        }
                    }
                    else {
                        if (i == 0) {
                            rectList.Add(new Rectangle(rectList[i].X, (int)Math.Round(rectList.Last().Y + intVersionOfEachRectHeight + ySpaceInbetweenRectangles),
                            intVersionOfEachRectWidth, intVersionOfEachRectHeight));
                        }
                        else {
                            rectList.Add(new Rectangle(rectList[i].X, rectList.Last().Y
                                , intVersionOfEachRectWidth, intVersionOfEachRectHeight));
                        }
                    }
                }
            }

            return rectList;
        }

        public Rectangle ReturnSheet(SheetPrintingDesignParameters sheetParameters) {
            //If rectangle is larger than the client viewable size then scale the rectangle so it will fit.
            if (sheetParameters.UpScaledSheetHeight > sheetParameters.MaxClientViewHeight || sheetParameters.UpScaledSheetWidth > sheetParameters.MaxClientViewWidth) {
                float ratioX = sheetParameters.MaxClientViewHeight / sheetParameters.UpScaledSheetHeight;
                float ratioY = sheetParameters.MaxClientViewWidth / sheetParameters.UpScaledSheetWidth;

                sheetParameters.ScalingRatio = Math.Min(ratioX, ratioY);

                sheetParameters.UpScaledSheetHeight = sheetParameters.UpScaledSheetHeight * sheetParameters.ScalingRatio;
                sheetParameters.UpScaledSheetWidth = sheetParameters.UpScaledSheetWidth * sheetParameters.ScalingRatio;
         //       var x = sheetParameters.UpScaledSheetHeight
            }

            return new Rectangle(sheetParameters.SheetXPosition, sheetParameters.SheetYPosition, (int)Math.Round(sheetParameters.UpScaledSheetWidth),
                (int)Math.Round(sheetParameters.UpScaledSheetHeight));
        }

        public void SavePagePrintingDesignParams(PagePrintingDesignParameters pageParameters) {
            _printingDesignRepo.SavePagePrintingDesignParams(pageParameters);
        }
    }
}
