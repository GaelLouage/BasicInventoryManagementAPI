using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Infra.Dtos;
using Infra.Extensions;
using Infra.Helpers;
using Infra.Mappers;
using Infra.Models;
using Infra.Services.Interfaces;
using Newtonsoft.Json;


namespace Infra.Services.Classes
{
    public class ProductService : IProductService
    {
        private List<ProductEntity>  _productList;
        private readonly string _filePath;
        public ProductService(string filePath)
        {
            _filePath = filePath;
        }
        public async Task<bool> Add(ProductEntity product)
        {
            _productList = await FileHelper.ReaderAsync(_filePath);

            var beforeAddingCount = _productList.Count;


            // check if product already in database
            // if yes then add quantity
            var productDuplicateCheck = _productList.DuplicateCheck(product);


            // add id
            _productList.SetProductId(product, productDuplicateCheck);


            //converter
            var data = JsonConvert.SerializeObject(_productList);
            //update the file
            await File.WriteAllTextAsync(_filePath, data);

            if (productDuplicateCheck is not null) 
                return true;

            var afterAddingCount = _productList.Count;
            return afterAddingCount > beforeAddingCount;
        }



        public async Task<List<ProductDto>> GetAllAsync()
        {
            if(_productList is null)
            {
                _productList = await FileHelper.ReaderAsync(_filePath);
            }
            
            return _productList
                .Select(x => x
                .MapToProductDto())
                .ToList();
        }

        public async Task<List<ProductDto>> GetLowStockAsync(Func<ProductEntity, bool> predicate)
        {
            if (_productList is null)
            {
                _productList = await FileHelper.ReaderAsync(_filePath);
            }
            return _productList
                .Where(predicate)
                .Select(x => x.MapToProductDto())
                .ToList(); ;
        }
    }
}
