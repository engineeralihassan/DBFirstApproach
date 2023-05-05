using System;
using System.Collections.Generic;
using System.Linq;

namespace DBFirstApproach
{
    internal class Program
    {
        public void AddStudent()
        {
            var newStudent = new Student()
            {
                FirstName = "Basit ",
                LastName = "Ali ",
                StandardId = 2

            };
            using (EF_Demo_DBEntities context = new EF_Demo_DBEntities())
            {
                context.Students.Add(newStudent);
                context.Database.Log = Console.Write;
                Console.WriteLine($"Before SaveChanges Entity State: {context.Entry(newStudent).State}");
                context.SaveChanges();
                Console.WriteLine($"After SaveChanges Entity State: {context.Entry(newStudent).State}");
            }
            Console.ReadKey();
        }
        public void UpdateStudent()
        {
            using (EF_Demo_DBEntities context = new EF_Demo_DBEntities())
            {
                var student = context.Students.Find(3);
                student.FirstName = "Asim";
                student.LastName = "Hameed";
                context.SaveChanges();
            }
        }
        public void DeleteStudent()
        {
            //Create DBContext object
            using (EF_Demo_DBEntities context = new EF_Demo_DBEntities())
            {
                var studentadd = context.StudentAddresses.Find(4);
                //At this point the Entity State will be Unchanged
                //Console.WriteLine($"Entity State Before Removing: {context.Entry(student).State}");
                //The following statement mark the Entity State as Deleted
                context.StudentAddresses.Remove(studentadd);
                var stdCourse = context.vwStudentCourses.Find(4);
                context.vwStudentCourses.Remove(stdCourse);
                var student = context.Students.Find(4);
                context.Students.Remove(student);
                //At this point, the Entity State will be in Deleted state
                //Console.WriteLine($"Entity State After Removing: {context.Entry(student).State}");
                //If you want to Capture SQL Statements generates by the context object use the following statement 
                //it will log the SQL Statements in the console window
                //context.Database.Log = Console.Write;
                //SaveChanges method will delete the Entity from the database
                context.SaveChanges();
                //Once the SaveChanges Method executed successfully, 
                //the Entity State will be in Detached state
                //Console.WriteLine($"Entity State After Removing: {context.Entry(student).State}");
            }
            Console.ReadKey();
        }
        public void PrintRecords()
        {
            using (EF_Demo_DBEntities DBEntities = new EF_Demo_DBEntities())
            {
                List<Student> listStudents = DBEntities.Students.ToList();
                Console.WriteLine();
                foreach (Student student in listStudents)
                {
                    Console.WriteLine($" Name = {student.FirstName} {student.LastName}, Email {student.StudentAddress?.Email}, Mobile {student.StudentAddress?.Mobile}");
                }
                Console.ReadKey();
            }
        }


        static void Main(string[] args)
        {

            try
            {
                Program program = new Program();
                //int a = Convert.ToInt32("asdfa");
                program.DeleteStudent();
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                throw;
            }
            //program.UpdateStudent();
            // program.PrintRecords();
            // program.DeleteStudent();
        }
    }

}
