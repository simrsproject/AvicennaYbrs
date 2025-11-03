using System;

namespace Temiang.Avicenna.Common
{
    public class ValidateArgs : EventArgs
    {
        public ValidateArgs()
        {
            IsCancel = false;
            MessageText = string.Empty;
        }

        public bool IsCancel { get; set; }

        public string MessageText { get; set; }

        public string ReasonText { get; set; }
    }
}