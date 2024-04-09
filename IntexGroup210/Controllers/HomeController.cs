using IntexGroup210.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.ML.OnnxRuntime;

namespace IntexGroup210.Controllers
{
    public class HomeController : Controller
    {
        private readonly IntexContext _context;
        private readonly InferenceSession _session;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IntexContext context, InferenceSession session, ILogger<HomeController> logger)
        {
            _context = context;
            _session = session;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Predict(int time, int amount, int fraud, int day_of_week_Mon, int day_of_week_Sat, int day_of_week_Sun, int day_of_week_Thu, int day_of_week_Tue, int day_of_week_Wed, int entry_mode_PIN, int entry_mode_Tap, int type_of_transaction_Online, int type_of_transaction_POS, int country_of_transaction_India, int country_of_transaction_Russia, int country_of_transaction_USA, int country_of_transaction_UnitedKingdom, int shipping_address_India, int shipping_address_Russia, int shipping_address_USA, int shipping_address_UnitedKingdom, int shipping_address_unknown, int bank_HSBC, int bank_Halifax, int bank_Lloyds, int bank_Metro, int bank_Monzo, int bank_RBS, int type_of_card_Visa)
        {
            // Dictionary mapping the numeric prediction to an animal type
            var class_type_dict = new Dictionary<int, string>
            {
                { 0, "Not Fraudulent" },
                { 1, "Fraudulent" }
            };

            try
            {
                var input = new List<float> { time, amount, day_of_week_Mon, day_of_week_Sat, day_of_week_Sun, day_of_week_Thu, day_of_week_Tue, day_of_week_Wed, entry_mode_PIN, entry_mode_Tap, type_of_transaction_Online, type_of_transaction_POS, country_of_transaction_India, country_of_transaction_Russia, country_of_transaction_USA, country_of_transaction_UnitedKingdom, shipping_address_India, shipping_address_Russia, shipping_address_USA, shipping_address_UnitedKingdom, shipping_address_unknown, bank_HSBC, bank_Halifax, bank_Lloyds, bank_Metro, bank_Monzo, bank_RBS, type_of_card_Visa  };
                var inputTensor = new DenseTensor<float>(input.ToArray(), new[] { 1, input.Count });

                var inputs = new List<NamedOnnxValue>
                {
                    NamedOnnxValue.CreateFromTensor("float_input", inputTensor)
                };

                using (var results = _session.Run(inputs)) // makes the prediction with the inputs from the form (i.e. class_type 1-7)
                {
                    var prediction = results.FirstOrDefault(item => item.Name == "output_label")?.AsTensor<long>().ToArray();
                    if (prediction != null && prediction.Length > 0)
                    {
                        // Use the prediction to get the animal type from the dictionary
                        var fraudType = class_type_dict.GetValueOrDefault((int)prediction[0], "Unknown");
                        ViewBag.Prediction = fraudType;
                    }
                    else
                    {
                        ViewBag.Prediction = "Error: Unable to make a prediction.";
                    }
                }

                _logger.LogInformation("Prediction executed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during prediction: {ex.Message}");
                ViewBag.Prediction = "Error during prediction.";
            }

            return View("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
                public IActionResult ShowPredictions()
        {
            var records = _context.Customers.ToList();
            
            var predictions = new List<FraudPrediction>();  
            
            var class_type_dict = new Dictionary<int, string>
            {
                { 0, "Not Fraudulent" },
                { 1, "Fraudulent" }
            };

            foreach (var record in records)
            {
                var input = new List<float>
                {
                    record.time, record.amount, record.fraud, record.day_of_week_Mon, record.day_of_week_Sat,
                    record.day_of_week_Sun,
                    record.day_of_week_Thu, record.day_of_week_Tue, record.day_of_week_Wed, record.entry_mode_PIN,
                    record.entry_mode_Tap, record.type_of_transaction_Online, record.type_of_transaction_POS,
                    record.country_of_transaction_India, record.country_of_transaction_Russia,
                    record.country_of_transaction_USA,
                    record.country_of_transaction_UnitedKingdom, record.shipping_address_India,
                    record.shipping_address_Russia,
                    record.shipping_address_USA, record.shipping_address_UnitedKingdom, record.shipping_address_unknown,
                    record.bank_HSBC, record.bank_Halifax, record.bank_Lloyds, record.bank_Metro, record.bank_Monzo,
                    record.bank_RBS, record.type_of_card_Visa
                };
                var inputTensor = new DenseTensor<float>(input.ToArray(), new[] { 1, input.Count });

                var inputs = new List<NamedOnnxValue>
                {
                    NamedOnnxValue.CreateFromTensor("float_input", inputTensor)
                };

                int predictionResult;
                using (var results = _session.Run(inputs))
                {
                    var prediction = results.FirstOrDefault(item => item.Name == "output_label")?.AsTensor<long>()
                        .ToArray();
                    predictionResult =
                        prediction != null && prediction.Length > 0
                            ? (int)prediction[0]
                            : -1; // Default value in case of error
                }

                predictions.Add(new FraudPrediction { Customer = record, Prediction = predictionResult });
            }

            return View(predictions);
        }
    }
}
