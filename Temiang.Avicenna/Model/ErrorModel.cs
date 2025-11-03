using System.ComponentModel.DataAnnotations;

namespace Temiang.Avicenna.Model
{
    public class ErrorModel
    {
        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
    }
}