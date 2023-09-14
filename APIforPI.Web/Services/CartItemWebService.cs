﻿using APIforPI.Infrastracture.Dto;
using APIforPI.Web.Services.Contracts;
using Azure;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Json;

namespace APIforPI.Web.Services
{
    public class CartItemWebService : ICartItemWebService
    {
        private readonly HttpClient _httpClient;
        public CartItemWebService(HttpClient httpCline)
        {
            _httpClient = httpCline;
        }
        public async Task<CartItemDto> AddItem(CartItemAddDto cartItemAddDto)
        {
            var result = await _httpClient.PostAsJsonAsync<CartItemAddDto>("api/Cart", cartItemAddDto);

            if (result.IsSuccessStatusCode)
            {
                if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(CartItemDto);
                }
                return await result.Content.ReadFromJsonAsync<CartItemDto>();
            }

            return null;
        }

        public async Task<IEnumerable<CartItemDto>> GetItems(int userId)
        {
            var result = await _httpClient.GetAsync($"api/Cart/{userId}/GetItems");
            if (result.IsSuccessStatusCode)
            {
                if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return Enumerable.Empty<CartItemDto>();
                }
                return await result.Content.ReadFromJsonAsync<IEnumerable<CartItemDto>>();
            }
            return null;
        }
        public async Task<CartItemDto> DeleteItem(int id)
        {
            //реализовать удаление из корзины
            var result = await _httpClient.DeleteAsync($"api/Cart/{id}");
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<CartItemDto>();
            }
            return default(CartItemDto);
        }
    }
}