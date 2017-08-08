using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using PrintingAppRepository.PrintingDesign.Models;

namespace PrintingAppRepository.PrintingDesign.Implementation {
    public class PrintingDesignManager : IPrintingDesignManager {
        private readonly IPrintingDesignRepository _printingDesignRepo;

        /// <summary>
        /// Inject repository
        /// </summary>
        /// <param name="printingDesignRepo"></param>
        public PrintingDesignManager(IPrintingDesignRepository printingDesignRepo) {
            _printingDesignRepo = printingDesignRepo;
        }

        public PagePrintingDesignParameters ReturnPagePrintingDesignParams(int maxClientViewHeight,
            int maxClientViewWidth) {
            return _printingDesignRepo.ReturnPagePrintingDesignParams(maxClientViewHeight,
                maxClientViewWidth);
        }

        /// <summary>
        /// Returned scaled printing design pages based on the passed in screen size and rectangle size
        /// You must first calculate the width and height of each page based on the scaling ratio
        /// You must then calculate the y space around each page, this would be the length
        /// You must then calculate the x space between each page, this would be the height between each page
        /// You must then add each page into a list of the pages based on how many pages up and across there are
        /// This will help set the x and y co-ordinates for each rectangle
        /// </summary>
        /// <param name="pageParameters"></param>
        /// <returns></returns>
        public IList<Rectangle> ReturnPages(PagePrintingDesignParameters pageParameters) {
            List<Rectangle> rectList = new List<Rectangle>();

            float xSpacesAroundRectangles, ySpacesAroundRectangles, xSpaceInbetweenRectangles,
                ySpaceInbetweenRectangles = 0;

            float eachPageWidth = (pageParameters.PageSizeWidth * pageParameters.PointsPerInch)
                * pageParameters.ScalingRatio;
            float eachPageHeight = (pageParameters.PageSizeHeight * pageParameters.PointsPerInch)
                * pageParameters.ScalingRatio;

            pageParameters.Scale =
                $@"Scale: 1 inch: {Math.Round(pageParameters.PointsPerInch *
                pageParameters.ScalingRatio)} points.";

            xSpacesAroundRectangles = (((pageParameters.OriginalSheetWidth -
                (pageParameters.PageSizeWidth * pageParameters.PagesAcross) -
                (pageParameters.PagesAcross - 1) * pageParameters.Bleeds) / 2)
                * pageParameters.PointsPerInch) * pageParameters.ScalingRatio;

            ySpacesAroundRectangles = (((pageParameters.OriginalSheetHeight -
                (pageParameters.PageSizeHeight * pageParameters.PagesUp)
                - (pageParameters.PagesUp - 1)
                * pageParameters.Bleeds) / 2) * pageParameters.PointsPerInch)
                * pageParameters.ScalingRatio;

            xSpaceInbetweenRectangles = ((pageParameters.UpScaledSheetWidth
                - (eachPageWidth * pageParameters.PagesAcross)
                - (xSpacesAroundRectangles * 2))
                / pageParameters.PagesAcross);

            ySpaceInbetweenRectangles = ((pageParameters.UpScaledSheetHeight)
                - (eachPageHeight * pageParameters.PagesUp)
                - (ySpacesAroundRectangles * 2))
                / pageParameters.PagesUp;


            int intVersionOfEachRectWidth = (int)Math.Round(eachPageWidth);
            int intVersionOfEachRectHeight = (int)Math.Round(eachPageHeight);

            if (xSpaceInbetweenRectangles <= 1) {
                xSpaceInbetweenRectangles = 1;
            }

            if (ySpaceInbetweenRectangles <= 1) {
                ySpaceInbetweenRectangles = 1;
            }

            for (int j = 0; j < pageParameters.PagesUp; j++) {
                for (int i = 0; i < pageParameters.PagesAcross; i++) {
                    if (j == 0) {
                        if (i == 0) {
                            rectList.Add(new Rectangle((int)Math.Round(pageParameters.SheetXPosition
                                + xSpacesAroundRectangles),
                                (int)Math.Round(pageParameters.SheetYPosition
                                + ySpacesAroundRectangles), intVersionOfEachRectWidth,
                                intVersionOfEachRectHeight));
                        }
                        else {
                            rectList.Add(new Rectangle((int)Math.Round(rectList[i - 1].X +
                                xSpaceInbetweenRectangles + eachPageWidth),
                                (int)Math.Round(pageParameters.SheetYPosition + ySpacesAroundRectangles),
                                intVersionOfEachRectWidth, intVersionOfEachRectHeight));
                        }
                    }
                    else {
                        if (i == 0) {
                            rectList.Add(new Rectangle(rectList[i].X,
                                (int)Math.Round(rectList.Last().Y + intVersionOfEachRectHeight
                                + ySpaceInbetweenRectangles),
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

        /// <summary>
        /// Create the scaled sheet design which each page will be printed onto.
        /// It first checks whether the planned sheet design is larger than the screen size and then
        /// decided whether it needs to scale it or not.
        /// If it does need to scale, it will calculate the scale needed to fit the sheet onto the screen
        /// and then calculate the new width and height.
        /// </summary>
        /// <param name="sheetParameters"></param>
        /// <returns></returns>
        public Rectangle ReturnSheet(SheetPrintingDesignParameters sheetParameters) {
            //If rectangle is larger than the client viewable size then scale the rectangle so it will fit.
            if (sheetParameters.UpScaledSheetHeight > sheetParameters.MaxClientViewHeight
                || sheetParameters.UpScaledSheetWidth > sheetParameters.MaxClientViewWidth) {

                float ratioX = sheetParameters.MaxClientViewHeight
                    / sheetParameters.UpScaledSheetHeight;

                float ratioY = sheetParameters.MaxClientViewWidth 
                    / sheetParameters.UpScaledSheetWidth;

                sheetParameters.ScalingRatio = Math.Min(ratioX, ratioY);

                sheetParameters.UpScaledSheetHeight = sheetParameters.UpScaledSheetHeight
                    * sheetParameters.ScalingRatio;
                sheetParameters.UpScaledSheetWidth = sheetParameters.UpScaledSheetWidth 
                    * sheetParameters.ScalingRatio;
            }

            return new Rectangle(sheetParameters.SheetXPosition, sheetParameters.SheetYPosition, (int)Math.Round(sheetParameters.UpScaledSheetWidth),
                (int)Math.Round(sheetParameters.UpScaledSheetHeight));
        }

        public void SavePagePrintingDesignParams(PagePrintingDesignParameters pageParameters) {
            _printingDesignRepo.SavePagePrintingDesignParams(pageParameters);
        }
    }
}
