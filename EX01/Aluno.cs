using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX01
{

    public class Aluno
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double Grade { get; set; }

        public Aluno()
        {

        }
        public Aluno(string name, int age, double grade)
        {
            Name = name;
            Age = age;
            Grade = grade;
        }
    }
}
