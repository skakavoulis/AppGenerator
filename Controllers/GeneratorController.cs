using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Management.Automation;

namespace DotNetAppGenerator.Controllers
{
    public class GeneratorController : Controller
    {
        private IWebHostEnvironment _environment;
        public GeneratorController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        // GET: GeneratorController
        public ActionResult Index()
        {
            return View();
        }

        // GET: GeneratorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GeneratorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GeneratorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                var directory = _environment.WebRootPath;
                var filePath = $"{directory}\\generate.ps1";
                using (PowerShell ps = PowerShell.Create())
                using (var sr = new StreamReader(filePath))
                {
                    var scriptContents = await sr.ReadToEndAsync().ConfigureAwait(false);

                    // specify the script code to run.
                    // specify the parameters to pass into the script.
                    collection.TryGetValue("appName", out var appName);
                    ps.AddScript(scriptContents)
                        .AddParameter("AppName", appName.ToString());

                    // execute the script and await the result.
                    var results = ps.Invoke(); // .InvokeAsync().ConfigureAwait(false);

                    // print the resulting pipeline objects to the console.
                    foreach (var item in results)
                    {
                        Console.WriteLine(item.BaseObject.ToString());
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
                //return RedirectToAction("Index");
            }
        }

        // GET: GeneratorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GeneratorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GeneratorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GeneratorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
