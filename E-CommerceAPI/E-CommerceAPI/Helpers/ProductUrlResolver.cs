using AutoMapper;
using AutoMapper.Execution;
using Core.Entities;
using E_CommerceAPI.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace E_CommerceAPI.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration _configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Image))
            {
                return _configuration["ApiUrl"]+ "/Images/" + source.Image;
            }

            return null;
        }
    }
}
