using Askify.Models;
using System.Reflection.Metadata.Ecma335;

namespace Askify.ViewModels
{
    public class QuestionVM
    {
        public EndUser User { get; set; }
        public Question Question { get; set; }
    }
}
