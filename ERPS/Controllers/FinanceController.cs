using ERPS.DataModel.Entities.Finance;
using ERPS.ServiceLayer.Functionals;
using ERPS.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ERPS.ServiceLayer.Controllers
{
    public class FinanceController : Controller
    {
        // GET: Finance
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetEmployees()
        {
            EmployeesFinance employees = Manager.Instance().GetEmployeesFinance();
            List<EmployeeFinanceModel> Salaries = Manager.Instance().CastToEmployeesFinanceModel(employees);
            List<EmployeeFinanceModel> Employees = new List<EmployeeFinanceModel>();
            foreach (EmployeeFinanceModel emp in Salaries)
            {
                bool exists = false;
                foreach (var item in Employees)
                {
                    if (item.Id == emp.Id)
                    {
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                    Employees.Add(emp);
            }
            List<EmployeeFinanceModel> SortedList = Employees.OrderBy(e => e.Name).ToList();
            return View(SortedList);
        }
        public ActionResult GetEmployeeById(int id)
        {
            string urlBase = Utils.Functionals.RouteHandler.GetUrlBase(Request, Url);
            ViewBag.urlBase = urlBase;
            ViewBag.GetEmployeeById = "Finance/GetEmployeeById";
            EmployeeFinanceId empl = new EmployeeFinanceId();
            empl.Id = id;
            EmployeeFinance employee = Manager.Instance().GetEmployeeFinanceById(empl);
            EmployeeFinanceModel Employee = Manager.Instance().CastToEmployeeFinance(employee);
            return View("GetEmployeeById", Employee);
        }
        public ActionResult DeleteEmployee(EmployeeFinanceId employee)
        {
            string urlBase = Utils.Functionals.RouteHandler.GetUrlBase(Request, Url);
            ViewBag.urlBase = urlBase;
            ViewBag.DeleteEmployee = "Finance/DeleteEmployee";
            Manager.Instance().DeleteEmployeeFinance(employee);
            return View(employee);
        }
        public ActionResult UpdateEmployee(EmployeeFinanceModel employee)
        {
            string urlBase = Utils.Functionals.RouteHandler.GetUrlBase(Request, Url);
            ViewBag.urlBase = urlBase;
            ViewBag.UpdateEmployee = "Finance/AddEmployee";
            return View("UpdateEmployee", employee);
        }
        
        public ActionResult AddEmployee(EmployeeFinanceModel employee)
        {
            string urlBase = Utils.Functionals.RouteHandler.GetUrlBase(Request, Url);
            ViewBag.urlBase = urlBase;
            ViewBag.UpdateEmployee = "Finance/AddEmployee";
            Manager.Instance().UpdateEmployeeFinance(employee);
            return View("UpdateEmployee", employee);
        }
        public ActionResult SalaryChart(int Id)
        {
            EmployeesFinance Employees = Manager.Instance().GetEmployeesFinance();
            List<EmployeeFinanceModel> Salaries = Manager.Instance().CastToEmployeesFinanceModel(Employees);
            List<SalaryChartModel> empl = new List<SalaryChartModel>();
            for (int i = Salaries.Count() - 1; i >= 0; --i)
            {
                if (Salaries[i].Id == Id)
                {
                    SalaryChartModel e = new SalaryChartModel();
                    e.Salary = Salaries[i].Salary;
                    e.date = Salaries[i].SalaryDate;
                    empl.Add(e);
                }
            }
            return View(empl);
        }
        
    }
}