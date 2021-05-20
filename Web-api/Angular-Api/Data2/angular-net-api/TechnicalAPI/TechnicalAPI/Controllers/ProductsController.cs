using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalAPI.Controllers.Base;
using TechnicalWinForms.DTO;
using TechnicalWinForms.Services;

namespace TechnicalAPI.Controllers
{
    public class ProductsController : BaseAPIController
    {
        private readonly string _connectionString;

        public ProductsController()
        {
            _connectionString = @"Data Source=212.101.89.7,55321;initial catalog=DDW04Pdb;user id=DDW04Pusu;password=T4buAzt2QcPYS7b"; ;
        }

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {  
            var dataResult =  GeneralData.GetProductsFromConnection(_connectionString); 
            return Ok(dataResult);
        }
        [HttpGet("GetProductById")]
        public IActionResult GetProductById(int id)
        {
            var dataResult = GeneralData.GetProductFromId(id, _connectionString);
            return Ok(dataResult);
        }
        [HttpPut("UpdateProduct")]
        public IActionResult UpdateProduct(ProductDTO dto)
        {
            GeneralData.UpdateProduct(dto, _connectionString);
            return Ok();
        }
    }
}
