using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        
        private readonly IProductService _productService;        
        public ProductsController(IProductService productService)
        {
            this._productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> dataLogger()
        {
            string filePath = @"C:\Users\ugurcan.bas\Desktop\MyDocuments\Development\qqq.txt";
            string allTextsOfFile = "";
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon/dittoqq");
                string dataToString = await response.Content.ReadAsStringAsync();                
                if (System.IO.File.Exists(filePath)){
                    allTextsOfFile = await System.IO.File.ReadAllTextAsync(filePath);
                }
                else
                {
                    await Task.Run(() => {
                    var newFile = System.IO.File.Create(filePath);
                    newFile.Close();
                    });
                }
                using StreamWriter file = new StreamWriter(filePath,append:true);                
                if (response.IsSuccessStatusCode)
                {
                    await _productService.SendEmail();
                    await _productService.AddSingleRequest(new RequestLog() { Id = Guid.NewGuid(), CreatedDate = DateTime.Now, IsSuccessfull = true });
                    await _productService.SendSms();
                    await file.WriteLineAsync("*****   Success!   *****" + dataToString);
                    file.Close();
                    
                }
                else
                {
                    await _productService.SendEmail();
                    await _productService.AddSingleRequest(new RequestLog() { Id = Guid.NewGuid(), CreatedDate = DateTime.Now, IsSuccessfull = false });
                    await _productService.SendSms();
                    await file.WriteLineAsync("*****   Error!   *****\n" + "Failed to send request!");
                    file.Close();
                }
            }
            return Ok();
        }
    }
}
