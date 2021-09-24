using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Dto
{
    [Serializable]
    public class Exam
    {
        public string LessonName { get; set; }
        public decimal ExamTime { get; set; }
        public string AcademicianNote { get; set; }
        public byte[] ExamDocument { get; set; }
        public string FileExtension { get; set; }
    }
}
