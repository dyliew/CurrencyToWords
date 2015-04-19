using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using CurrencyToWords.Models;
using CurrencyToWords.Services;
using Microsoft.Ajax.Utilities;

namespace CurrencyToWords.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ValuesController : ApiController
    {
        private readonly INumberService _numberService;

        public ValuesController(INumberService numberService)
        {
            _numberService = numberService;
        }

        // POST api/values
        [HttpPost]
        public async Task<IHttpActionResult >TranslateToWords([FromBody]NumberModel model)
        {
            var regex = new Regex(@"\d*(\.\d{0,2})?");

            if (!model.input.IsNullOrWhiteSpace() && regex.Match(model.input).Success)
            {
                var output = await Task.FromResult(_numberService.ConvertPrice(model.input));
                return Ok(new ResponseObject()
                {
                    Success = true,
                    Payload = output
                });
            }

            return Ok(new ResponseObject()
            {
                Success = false,
                ErrorMessage = "Only numbers and 2 digit decimals are accepted!",
            });
        }

//        public void BulkTranslateToWords([FromBody]string value)
//        {
//        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(new ResponseObject()
            {
                Success = true,
                Payload = "test~!",
            });
        }
    }
}
