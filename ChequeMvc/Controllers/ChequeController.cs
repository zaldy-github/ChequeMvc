using ChequeMvc.Entities;
using ChequeMvc.Services.Shared.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace ChequeMvc.Controllers
{
    public class ChequeController : Controller
    {
        private IChequeUtilities _chequeUtilities;

        /// <summary>
        /// Public constructor.
        /// Inject IChequeUtilities.
        /// IChequeUtilities is registered as a service in Startup.cs
        /// </summary>
        /// <param name="chequeUtilities"></param>
        public ChequeController(IChequeUtilities chequeUtilities)
        {
            _chequeUtilities = chequeUtilities;
        }

        /// <summary>
        /// GET: Cheque/Create
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: Cheque/Create
        /// </summary>
        /// <param name="cheque"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChequeEntity cheque)
        {
            if (ModelState.IsValid)
            {
                // Serialize object to a string and store in TempData
                var serializedObject = JsonConvert.SerializeObject(cheque);
                TempData["cheque"] = serializedObject;

                return RedirectToAction("Show", "cheque");
            }

            return View();
        }

        /// <summary>
        /// Show cheque issued
        /// </summary>
        /// <returns></returns>
        public ActionResult Show()
        {
            if (TempData["cheque"] is string serializedObject)
            {
                // Deserialize object and store as ChequeEntity
                var cheque = JsonConvert.DeserializeObject<ChequeEntity>(serializedObject);
                ViewData["chequePayee"] = cheque.chequePayee;
                ViewData["chequeAmount"] = ((decimal)cheque.chequeAmount).ToString("#,##0.00");
                ViewData["amountInWords"] = _chequeUtilities.TranslateDollarAmountToWords(cheque.chequeAmount);
                ViewData["chequeDate"] = ((DateTime)cheque.chequeDate).ToString("dd/MM/yyyy");
            }

            return View();
        }
    }
}