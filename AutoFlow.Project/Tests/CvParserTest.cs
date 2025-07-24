//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Tests
//{
//    public class CvParserTest
//    {
//        [Fact]
//        public void ExtractName_ShouldReturnCorrectName()
//        {
//            var cv = "Jean Dupont\n5 years experience\nSkills: C#, Python";
//            var name = CVParser.ExtractName(cv);
//            Assert.Equal("Jean Dupont", name);
//        }

//        [Fact]
//        public void ExtractYearsExperience_ShouldReturnCorrectYears()
//        {
//            var cv = "Jean Dupont\n5 years experience\nSkills: C#, Python";
//            var years = CVParser.ExtractYearsExperience(cv);
//            Assert.Equal(5, years);
//        }

//        [Fact]
//        public void ExtractSkills_ShouldReturnCorrectSkills()
//        {
//            var cv = "Jean Dupont\n5 years experience\nSkills: C#, Python";
//            var skills = CVParser.ExtractSkills(cv);
//            Assert.Contains("C#", skills);
//            Assert.Contains("Python", skills);
//        }
//    }
//}
