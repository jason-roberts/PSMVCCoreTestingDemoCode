using System.Threading.Tasks;
using CreditCards.Core.Interfaces;
using CreditCards.Core.Model;
using CreditCards.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CreditCards.Controllers
{
    public class ApplyController : Controller
    {
        private readonly ICreditCardApplicationRepository _applicationRepository;

        public ApplyController(ICreditCardApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(NewCreditCardApplicationDetails applicationDetails)
        {
            if (!ModelState.IsValid)
            {
                return View(applicationDetails);
            }

            var creditCardApplication = new CreditCardApplication
            {
                FirstName = applicationDetails.FirstName,
                LastName = applicationDetails.LastName,
                FrequentFlyerNumber = applicationDetails.FrequentFlyerNumber,
                Age = applicationDetails.Age.Value,
                GrossAnnualIncome = applicationDetails.GrossAnnualIncome.Value
            };

            // Not mock-able
            var evaluator = new CreditCardApplicationEvaluator(new FrequentFlyerNumberValidator());
            CreditCardApplicationDecision decision = evaluator.Evaluate(creditCardApplication);
            creditCardApplication.Decision = decision;

            await _applicationRepository.AddAsync(creditCardApplication);

            return View("ApplicationComplete", creditCardApplication);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
