using System;

namespace PrintingApp.Models.CustomEventArgs {
    public class ErrorEventArgs : EventArgs {
        public ErrorEventArgs(string errorMessage) {
            ErrorMessage = errorMessage;
        }
        public string ErrorMessage { get; set; }
    }
}
