using ManagementTeam = ERPS.DataModel.Entities.Management;
using ERPS.DataModel.Entities.Common;
using ERPS.ServiceLayer.Functionals;
using ERPS.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPS.ServiceLayer.Controllers
{
    public class ManagementController : Controller
    {
        #region GetEmployees
        /// <summary>
        /// Gets Employees and shows the list in the view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetEmployees()
        {
            IEnumerable<EmployeeManagementModel> employeesModel = this.GetEmployeesCollection();
            return View("GetEmployees", employeesModel);
        }

        /// <summary>
        /// Gets all employees
        /// </summary>
        /// <returns></returns>
        private ICollection<EmployeeManagementModel> GetEmployeesCollection()
        {
            //var serverUrl = System.Configuration.ConfigurationManager.ConnectionStrings["ManagementGetEmployees"].ConnectionString
            var serverUrl = System.Configuration.ConfigurationManager.ConnectionStrings["ManagementServerName"].ConnectionString
                + DestinationNames.ManagementGetEmployees;

            var employees = Manager.Instance().ManagementGetEntities<ManagementTeam.Employee>(serverUrl);
            //return Manager.Instance().CastToManagementEmployeesModel(employees);
            return Manager.Instance().CastToModelsCollection<ManagementTeam.Employee, EmployeeManagementModel>(employees, Manager.Instance().CastToManagementEmployeeModel);
        }
        #endregion

        #region UpdateEmployee
        /// <summary>
        /// Updates an employee's fields
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult UpdateEmployee(Models.EmployeeManagementModel model)
        {
            string urlBase = Utils.Functionals.RouteHandler.GetUrlBase(Request, Url);
            ViewBag.urlBase = urlBase;
            ViewBag.updateEmployeeAction = "Management/ParseJSONEmployee";
            return View(model);
        }

        /// <summary>
        /// Sends request to the server to update the employee's fields
        /// </summary>
        /// <param name="employeeModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ParseJSONEmployee(Models.EmployeeManagementModel employeeModel)
        {
            var employee = Functionals.Manager.Instance().CastToManagementEmployeeEntity(employeeModel);
            var serverUrl = System.Configuration.ConfigurationManager.ConnectionStrings["ManagementServerName"].ConnectionString
                + DestinationNames.ManagementUpdateEmployee;
            var responseEntityBase = Manager.Instance().ManagementUpdateEntity<ManagementTeam.Employee>(employee, serverUrl);
            ViewBag.statusInfo = responseEntityBase.StatusInfo;
            return View("StartPage");
        }
        #endregion

        #region LeaveFeedback
        /// <summary>
        /// Leaves a feedback to an employee
        /// </summary>
        /// <param name="employeeModel"></param>
        /// <returns></returns>
        public ActionResult LeaveFeedback(Models.EmployeeManagementModel employeeModel)
        {
            // Filling employees list to whom a feedback can be left
            var feedbackModel = new FeedbackModel()
            {
                Employees = this.GetEmployeesCollection()
            };

            // Sorry, but you can't leave feedback to yourself :)
            this.RemoveCurrentEmployeeFromCollection(employeeModel, feedbackModel.Employees);

            // Have to clean up this and the View
            // Passing needed values to the view
            //ViewBag.employeeId = employeeModel.Id;
            //ViewBag.employeeName = employeeModel.Name;
            //ViewBag.employeeSurname = employeeModel.Surname;
            //ViewBag.employeeTeam = employeeModel.Team;

            // Instead:
            feedbackModel.Employee = employeeModel;
            //feedbackModel.EmployeeId = employeeModel.Id;

            // Getting URL to send request
            string urlBase = Utils.Functionals.RouteHandler.GetUrlBase(Request, Url);
            ViewBag.urlBase = urlBase;
            ViewBag.leaveFeedback = "Management/ParseFeedback";

            return View("LeaveFeedback", feedbackModel);
        }

        /// <summary>
        /// Sends request to the server to submit a new feedback
        /// </summary>
        /// <param name="feedbackModel"></param>
        /// <returns></returns>
        public ActionResult ParseFeedback(Models.FeedbackModel feedbackModel)
        {
            var feedback = Functionals.Manager.Instance().CastToFeedbackEntity(feedbackModel);
            //var serverUrl = System.Configuration.ConfigurationManager.ConnectionStrings["ManagementLeaveFeedback"].ConnectionString;
            var serverUrl = System.Configuration.ConfigurationManager.ConnectionStrings["ManagementServerName"].ConnectionString
                + DestinationNames.ManagementLeaveFeedback;
            var responseEntityBase = Manager.Instance().ManagementLeaveFeedback(feedback, serverUrl);
            ViewBag.statusInfo = responseEntityBase.StatusInfo;
            IEnumerable<EmployeeManagementModel> employeesModel = this.GetEmployeesCollection();
            return View("GetEmployees", employeesModel);
        }

        /// <summary>
        /// Removes provided employee from the provided collection
        /// </summary>
        /// <param name="employeeModel"></param>
        /// <param name="employeesModel"></param>
        private void RemoveCurrentEmployeeFromCollection(EmployeeManagementModel employeeModel, ICollection<EmployeeManagementModel> employeesModel)
        {
            // Sorry, but you can't leave feedback to yourself :)
            try
            {
                employeesModel.Remove(employeesModel.First(e => e.Id == employeeModel.Id));
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.ToString() + "\nThe collection doesn't containt the element.");
            }
        }
        #endregion

        #region GetEmployee'sFeedbacks
        /// <summary>
        /// Gets all the feedback left to provided employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetEmployeeFeedbacks(EmployeeManagementModel employee)
        {
            var employeeEntity = new EntityBase();
            employeeEntity.Id = employee.Id;

            ViewBag.employeeName = employee.Name;
            ViewBag.employeeSurname = employee.Surname;
           
            IEnumerable<FeedbackModel> feedbackModels = Manager.Instance().GetEmployeeFeedbacks(employeeEntity);
            //IEnumerable<FeedbackModel> feedbackModels = this.GetEmployeeFeedbacksCollection();
            return View("GetFeedbacks", feedbackModels);
        }
        #endregion

        #region GetAllFeedbacks
        /// <summary>
        /// Gets all the feedback left so far
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFeedbacks()
        {
            IEnumerable<FeedbackModel> feedbackModels = Manager.Instance().GetFeedbacks();
            return View("GetFeedbacks", feedbackModels);
        }
        #endregion

        #region Performance Chart
        public ActionResult DrawPerformanceChart(int Id)
        {
            List<TaskModel> tasks = Manager.Instance().GetTasks();
            List<TaskModel> tasksByAssignee = new List<TaskModel>();
            foreach (var item in tasks)
            {
                if (item.AssigneeId == Id)
                    tasksByAssignee.Add(item);
            }
            int[] performance = new int[tasksByAssignee.Count()];
            for (int i = 0; i < performance.Length; ++i)
            {
                performance[i] = ((tasksByAssignee[i].PlannedEnd.Value - tasksByAssignee[i].PlannedStart.Value) -
                    (tasksByAssignee[i].ActualEnd.Value - tasksByAssignee[i].ActualStart.Value)).Days;
            }
            return View(performance);
        }
        #endregion

    }
}