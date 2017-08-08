using PrintingApp.Models.CustomEventArgs;
using PrintingApp.Views;
using PrintingAppRepository.PrintingDesign;
using PrintingAppRepository.PrintingDesign.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PrintingApp.Presenters {
    internal class PrintingDesignPresenter {
        private readonly IPrintingDesignManager _printingDesignManager;
        private readonly IPrintingDesignView _view;

        /// <summary>
        /// Inject all interfaces, assign in constructor and initialise events. 
        /// </summary>
        /// <param name="printingDesignManager"></param>
        /// <param name="view"></param>
        public PrintingDesignPresenter(IPrintingDesignManager printingDesignManager, IPrintingDesignView view) {
            _printingDesignManager = printingDesignManager;
            _view = view;
            InitaliseEvents();
        }

        /// <summary>
        /// Subscribe methods to events
        /// </summary>
        private void InitaliseEvents() {
            _view.LogErrorToView += ClearPaint;
            _view.LogErrorToView += LogErrorToView;
            _view.ClearPaintedItems += ClearPaint;
            _view.ResizeForm += ResizeDesign;
            _view.OnFormLoad += OnFormLoad;
            _view.SetImpositionFormAsActive += SetImpositionFormAsActive;
        }

        /// <summary>
        /// Make form active
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetImpositionFormAsActive(object sender, EventArgs e) {
            ImpositionForm form = Application.OpenForms["ImpositionForm"] as ImpositionForm;
            if (form != null) {
                form.Show();
                form.Focus();
            }
        }

        private void LogErrorToView(object sender, ErrorEventArgs e) {
            _view.ErrorMessage += $"{Environment.NewLine} {e.ErrorMessage}";
        }

        private void OnFormLoad(object sender, EventArgs e) {
            try {
                _view.Form.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex) {
                ClearPaint(this, e);
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        /// <summary>
        /// Clear the form and set the background colour to the default one. You have to do this
        /// because when Invalidate gets called, it redraws the existing on paint methods. 
        /// So once this happens and you resize the form. It flickers and keeps exisitng rectangles.
        /// So I have set the background colour to cover these existing rectangles. It's not the most
        /// elegant solution but I have tried other ways such as overriding the OnPaint method and
        /// only painting when Invalidate isnt  called but this doesn't work due to the way the
        /// OnPaint event queues up methods.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearPaint(object sender, EventArgs e) {
            try {
                _view.Form.Invalidate();
                _view.Form.Paint += (se, pe) => {
                    var brush = new SolidBrush(_view.Form.BackColor);
                    var sheet = new Rectangle(0, 0, _view.Form.ClientRectangle.Width,
                        _view.Form.ClientRectangle.Height);
                    pe.Graphics.FillRectangle(brush, sheet);
                    using (var pen = new Pen(brush.Color, 2))
                        pe.Graphics.DrawRectangle(pen, sheet);
                };
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        /// <summary>
        /// Resize the design whenever te page gets redrawn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResizeDesign(object sender, EventArgs e) {
            try {
                //Return the saved printing parameters
                PagePrintingDesignParameters printingDesignParams = 
                    _printingDesignManager.ReturnPagePrintingDesignParams(
                        _view.Form.ClientRectangle.Height, _view.Form.ClientRectangle.Width);

                //Return the sheet which rectangles will get printed on
                Rectangle sheet = _printingDesignManager.ReturnSheet(printingDesignParams);

                //Draw the main sheet
                _view.Form.Paint += (se, pe) => {
                    var brush = new SolidBrush(Color.FromArgb(255, 238, 114));

                    pe.Graphics.FillRectangle(brush, sheet);
                    using (var pen = new Pen(brush.Color, 2))
                        pe.Graphics.DrawRectangle(pen, sheet);
                };

                //Return all pages which will be drawn on the sheet
                List<Rectangle> rectList = _printingDesignManager.ReturnPages(printingDesignParams)
                    .ToList();

                //Loop around pages list and draw the rectangles
                for (int i = 0; i < rectList.Count; i++) {
                    int x = i;
                    _view.Form.Paint += (se, pe) => {
                        var rectangle = rectList[x];

                        var brush = new SolidBrush(Color.White);

                        pe.Graphics.FillRectangle(brush, rectangle);
                        using (var pen = new Pen(brush.Color, 2)) {
                            pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                            pe.Graphics.DrawRectangle(pen, rectangle);
                        }
                    };
                }

                //Show the current scale to the user
                _view.Scale = printingDesignParams.Scale;
            }
            catch (Exception ex) {
                ClearPaint(this, e);
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }
    }
}