using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Management.gRPC.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Context;
using Microsoft.Extensions.Caching.Distributed;
using System;
using Newtonsoft.Json;

namespace Management.gRPC.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly IDistributedCache _redisCache;
        public ProductRepository(OrderContext dbContext, IMapper mapper, IDistributedCache redisCache) : base(dbContext)
        {
            _mapper = mapper;
            _redisCache = redisCache;
        }

        public async Task<bool> CreateProduct(Entities.Product product)
        {
            var productEntity = _mapper.Map<Domain.Entities.Product>(product);
            await _dbContext.Products.AddAsync(productEntity);
            var productsRedis = await _redisCache.GetStringAsync(typeof(Domain.Entities.Product).Name);
            
            if(!(String.IsNullOrEmpty(productsRedis) || productsRedis == "[]"))
            {
                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    product.Id = productEntity.Id;
                    var oldProductsRedis = JsonConvert.DeserializeObject<List<Entities.Product>>(productsRedis);
                    oldProductsRedis.Add(product);
                    var newProductsRedis = oldProductsRedis;
                    await _redisCache.RemoveAsync(typeof(Domain.Entities.Product).Name);
                    await _redisCache.SetStringAsync(typeof(Domain.Entities.Product).Name, JsonConvert.SerializeObject(newProductsRedis));
                    return true;
                }
            } else
            {
                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    product.Id = productEntity.Id;
                    var products = new List<Entities.Product>() { product };
                    await _redisCache.SetStringAsync(typeof(Domain.Entities.Product).Name, JsonConvert.SerializeObject(products));
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = _dbContext.Products.Find(id);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    var productsRedis = await _redisCache.GetStringAsync(typeof(Domain.Entities.Product).Name);

                    var oldProductsRedis = JsonConvert.DeserializeObject<List<Entities.Product>>(productsRedis);
                    var productToRemove = oldProductsRedis.SingleOrDefault(p => p.Id == id);
                    oldProductsRedis.Remove(productToRemove);
                    await _redisCache.RemoveAsync(typeof(Domain.Entities.Product).Name);
                    await _redisCache.SetStringAsync(typeof(Domain.Entities.Product).Name, JsonConvert.SerializeObject(oldProductsRedis));
                    return true;
                }
            }
            return false;
        }

        public async Task<Entities.Product> GetProductByIdAsync(int id)
        {
            var productsRedis = JsonConvert.DeserializeObject<List<Entities.Product>>(await _redisCache.GetStringAsync(typeof(Domain.Entities.Product).Name));
            return productsRedis.SingleOrDefault(p => p.Id == id);
        }

        public async Task<List<Entities.Product>> GetProducts()
        {
            var productsRedis = await _redisCache.GetStringAsync(typeof(Domain.Entities.Product).Name);
            if (String.IsNullOrEmpty(productsRedis))
            {
                var products = await _dbContext.Products.ToListAsync();
                var productsResult = _mapper.Map<List<Management.gRPC.Entities.Product>>(products);
                if (products != null)
                {
                    await _redisCache.SetStringAsync(typeof(Domain.Entities.Product).Name, JsonConvert.SerializeObject(productsResult));
                }
                return productsResult;
            }
            var productsRedisDes = JsonConvert.DeserializeObject<List<Entities.Product>>(productsRedis);
            var productsResponseFromRedis = _mapper.Map<List<Entities.Product>>(productsRedisDes);
            return productsResponseFromRedis;
        }

        public async Task<List<Entities.Product>> GetProductsByName(string productName)
        {
            var productsRedis = await _redisCache.GetStringAsync(typeof(Domain.Entities.Product).Name);
            if (String.IsNullOrEmpty(productsRedis))
            {
                var products = await _dbContext.Products.Where(p => p.Name == productName).ToListAsync();
                var productsResult = _mapper.Map<List<Entities.Product>>(products);
                return productsResult;
            }
            var productsRedisConvert = JsonConvert.DeserializeObject<List<Entities.Product>>(productsRedis);
            return productsRedisConvert.Where(p => p.Name == productName).ToList();
        }

        public async Task<bool> UpdateProduct(Entities.Product product)
        {
            var productEntity = _mapper.Map<Domain.Entities.Product>(product);
            _dbContext.Products.Update(productEntity);

            var productsRedis = await _redisCache.GetStringAsync(typeof(Domain.Entities.Product).Name);

            var oldProductsRedis = JsonConvert.DeserializeObject<List<Entities.Product>>(productsRedis);
            var productToUpdate = oldProductsRedis.SingleOrDefault(p => p.Id == product.Id);
            if (productToUpdate != null)
            {
                var index = oldProductsRedis.IndexOf(productToUpdate);
                oldProductsRedis.RemoveAt(index);
                oldProductsRedis.Insert(index, product);
                await _redisCache.RemoveAsync(typeof(Domain.Entities.Product).Name);
                await _redisCache.SetStringAsync(typeof(Domain.Entities.Product).Name, JsonConvert.SerializeObject(oldProductsRedis));
            }

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
