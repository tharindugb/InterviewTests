using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using GraduationTracker.Entities;
using GraduationTracker.Interfaces;
using GraduationTracker.Services;
using GraduationTracker.Moq;

namespace GraduationTracker.Tests.Unit
{
    [TestClass]
    public class GraduationTrackerTests
    {
        GraduationTracker _tracker;
        IStudentService _studentService;
        IDiplomaService _diplomaService;
        IStudentRepository _studentRepository;
        IDiplomaRepository _diplomaRepository;

        private void SetRequirements()
        {
            _diplomaService.AddRequirement(new Requirement { Id = 100, Name = "Math", MinimumMark = 50, Courses = new int[] { 1 }, Credits = 1 });
            _diplomaService.AddRequirement(new Requirement { Id = 102, Name = "Science", MinimumMark = 50, Courses = new int[] { 2 }, Credits = 1 });
            _diplomaService.AddRequirement(new Requirement { Id = 103, Name = "Literature", MinimumMark = 50, Courses = new int[] { 3 }, Credits = 1 });
            _diplomaService.AddRequirement(new Requirement { Id = 104, Name = "Physichal Education", MinimumMark = 50, Courses = new int[] { 4 }, Credits = 1 });            
        }

        private void SetDiploma()
        {
            _diplomaService.AddDiploma(new Diploma
            {
                Id = 1,
                Credits = 4,
                Requirements = new int[] { 100, 102, 103, 104 }
            });
        }

        private void SetStudents1()
        {
            _studentService.AddStudent(new Student
            {
                Id = 1,
                Courses = new Course[]
                    {
                        new Course{Id = 1, Name = "Math", Mark=95 },
                        new Course{Id = 2, Name = "Science", Mark=95 },
                        new Course{Id = 3, Name = "Literature", Mark=95 },
                        new Course{Id = 4, Name = "Physichal Education", Mark=95 }
                    }
            });
            _studentService.AddStudent(new Student
            {
                Id = 2,
                Courses = new Course[]
                   {
                        new Course{Id = 1, Name = "Math", Mark=80 },
                        new Course{Id = 2, Name = "Science", Mark=80 },
                        new Course{Id = 3, Name = "Literature", Mark=80 },
                        new Course{Id = 4, Name = "Physichal Education", Mark=80 }
                   }
            });
            _studentService.AddStudent(new Student
            {
                Id = 3,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=50 },
                    new Course{Id = 2, Name = "Science", Mark=50 },
                    new Course{Id = 3, Name = "Literature", Mark=50 },
                    new Course{Id = 4, Name = "Physichal Education", Mark=50 }
                }
            });
            _studentService.AddStudent(new Student
            {
                Id = 4,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=40 },
                    new Course{Id = 2, Name = "Science", Mark=40 },
                    new Course{Id = 3, Name = "Literature", Mark=40 },
                    new Course{Id = 4, Name = "Physichal Education", Mark=40 }
                }
            });
        }

        private void SetStudents2()
        {
            _studentService.AddStudent(new Student
            {
                Id = 1,
                Courses = new Course[]
                    {
                        new Course{Id = 1, Name = "Math", Mark=95 },
                        new Course{Id = 2, Name = "Science", Mark=95 },
                        new Course{Id = 3, Name = "Literature", Mark=95 },
                        new Course{Id = 4, Name = "Physichal Education", Mark=95 }
                    }
            });
            _studentService.AddStudent(new Student
            {
                Id = 2,
                Courses = new Course[]
                   {
                        new Course{Id = 1, Name = "Math", Mark=80 },
                        new Course{Id = 2, Name = "Science", Mark=80 },                        
                        new Course{Id = 4, Name = "Physichal Education", Mark=80 }
                   }
            });
            _studentService.AddStudent(new Student
            {
                Id = 3,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=50 },
                    new Course{Id = 2, Name = "Science", Mark=50 },
                    new Course{Id = 3, Name = "Literature", Mark=50 },
                    new Course{Id = 4, Name = "Physichal Education", Mark=50 }
                }
            });
        }

        [TestInitialize]
        public void Setup()
        {
            
            _diplomaRepository = DiplomaRepositoryMoq.Instance;            
           
            _diplomaService = new DiplomaService(_diplomaRepository);

            _tracker = new GraduationTracker(_diplomaService);

            SetDiploma();
            SetRequirements();           
        }

        [TestMethod]
        public void TestHasCredits()
        {
            _studentRepository = StudentRepositoryMoq.Instance;
            _studentService = new StudentService(_studentRepository);
           
            SetStudents1();

            var graduated = new List<Tuple<bool, STANDING>>();

            foreach(var student in _studentService.GetAllStudents())
            {
                graduated.Add(_tracker.HasGraduated(_diplomaService.GetDiploma(1), student));      
            }

            Assert.IsFalse(graduated.All(a => a.Item1));

            Assert.IsTrue(!graduated.Last().Item1);
        }

        [TestMethod]
        public void TestNotTakenRequirement()
        {
            _studentRepository = StudentRepositoryMoq.Instance;
            _studentService = new StudentService(_studentRepository);
          
            SetStudents2();

            var graduated = new List<Tuple<bool, STANDING>>();

            foreach (var student in _studentService.GetAllStudents())
            {
                graduated.Add(_tracker.HasGraduated(_diplomaService.GetDiploma(1), student));
            }

            Assert.IsFalse(graduated.All(a => a.Item1));

            Assert.IsTrue(!graduated[1].Item1);
        }
    }
}
