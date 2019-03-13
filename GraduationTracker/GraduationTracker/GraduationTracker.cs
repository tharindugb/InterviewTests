using GraduationTracker.Entities;
using GraduationTracker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationTracker
{
    public partial class GraduationTracker
    {
        public struct Result{
            public int Credit { get; set; }
            public double Average { get; set; }
            public bool IsValid { get; set; }
        }
        private IDiplomaService _service;

        public GraduationTracker(IDiplomaService diplomaService)
        {
            _service = diplomaService;
        }

        private Result Evaluateresult(IDiploma diploma, IStudent student)
        {
            Result result = new Result{ IsValid = true };
            bool courseTaken = false;

            for (int i = 0; i < diploma.Requirements.Length; i++)
            {
                var requirement = _service.GetRequirement(diploma.Requirements[i]);
                for (int k = 0; k < requirement.Courses.Length; k++)
                {
                    courseTaken = false;
                    for (int j = 0; j < student.Courses.Length; j++)
                    {
                        if (requirement.Courses[k] == student.Courses[j].Id)
                        {
                            result.Average += student.Courses[j].Mark;
                            if (student.Courses[j].Mark >= requirement.MinimumMark)
                            {
                                result.Credit += requirement.Credits;
                            }
                            courseTaken = true;
                            break;
                        }
                    }
                    if(!courseTaken)
                    {
                        result.IsValid = false;
                        return result;
                    }
                }
            }

            result.Average = result.Average / student.Courses.Length;

            return result;
        }

        public Tuple<bool, STANDING>  HasGraduated(IDiploma diploma, IStudent student)
        {
            var standing = STANDING.None;
            var result = Evaluateresult(diploma, student);

            if(!result.IsValid)
            {
                return new Tuple<bool, STANDING>(false, standing);
            }
            
            if (result.Average < 50)
                standing = STANDING.Remedial;
            else if (result.Average < 80)
                standing = STANDING.Average;
            else if (result.Average < 95)
                standing = STANDING.MagnaCumLaude;
            else
                standing = STANDING.SumaCumLaude;

            student.Standing = standing;

            switch (standing)
            {
                case STANDING.Remedial:
                    return new Tuple<bool, STANDING>(false, standing);
                case STANDING.Average:
                    return new Tuple<bool, STANDING>(true, standing);
                case STANDING.SumaCumLaude:
                    return new Tuple<bool, STANDING>(true, standing);
                case STANDING.MagnaCumLaude:
                    return new Tuple<bool, STANDING>(true, standing);

                default:
                    return new Tuple<bool, STANDING>(false, standing);
            } 
        }
    }
}
