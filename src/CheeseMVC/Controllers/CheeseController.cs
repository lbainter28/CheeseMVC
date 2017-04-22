using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CheeseMVC.Controllers
{
    // view location: /Views/Cheese*
    public class CheeseController : Controller
    {
        // static property is available to *any* instance
        // of the CheeseController class
        static private Dictionary<string, string> Cheeses = new Dictionary<string, string>();

        // Display the list of cheeses
        // GET: /cheese
        public IActionResult Index()
        {
            // data for the view
            ViewBag.cheeses = Cheeses;

            // view defaults to action name: Index.cshtml
            // /Views/Cheese/Index.cshtml
            return View();

            // OR render particular view *by name*
            // return View("Index");
        }

        public IActionResult Add()
        {
            ViewBag.error = "";
            // /Views/Cheese/Add.cshtml
            return View();
        }

        // by default, the route will be: /Cheese/NewCheese
        // but we want to post to the form at the same route
        // from which we loaded it: /Cheese/Add
        [HttpPost]
        [Route("/Cheese/Add")]
        public IActionResult NewCheese(string name = "", string description = "")
        {
            ViewBag.error = "";
            // valid cheese names: letters and spaces
            Regex validCheeseNamePattern = new Regex(@"[a-zA-Z\s]+");

            if (name == null || !validCheeseNamePattern.IsMatch(name))
            {
                // cheese name is not valid, re-render the form and
                // send an error message
                ViewBag.error = "Please enter a valid cheese name (letters and spaces only)";
                return View("Add");
            }

            // add new cheese to static class list Cheeses
            Cheeses.Add(name.First().ToString().ToUpper()+name.Substring(1), description);

            // go back to the list of cheeses
            return Redirect("/Cheese");
        }

        [HttpGet]
        [Route("/Cheese/Remove")]
        public IActionResult Remove()
        {
            // data for the view
            ViewBag.cheeses = Cheeses;

            // view defaults to action name: Remove.cshtml
            // /Views/Cheese/Remove.cshtml
            return View();
        }

        [HttpPost]
        [Route("/Cheese/Remove")]
        public IActionResult RemoveManyCheeses(string[] selectedCheeses)
        {
            foreach(string cheeseName in selectedCheeses)
            {
                if (Cheeses.ContainsKey(cheeseName))
                {
                    Cheeses.Remove(cheeseName);
                }
            }

            return Redirect("/Cheese");
        }

        [Route("cheese/remove/{cheeseName}")]
        [HttpGet]
        public IActionResult RemoveSingleCheese(string cheeseName="")
        {
            if (Cheeses.ContainsKey(cheeseName))
            {
                Cheeses.Remove(cheeseName);
            }

            return Redirect("/Cheese");
        }
    }
}

