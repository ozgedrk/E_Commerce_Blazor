﻿using Blazored.LocalStorage;
using E_Commerce_Client.Service.IService;
using E_Commerce_Common;
using E_Commerce_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace E_Commerce_Client.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider _authStateProvider;
        public AuthenticationService(HttpClient httpClient, ILocalStorageService localStorageService, AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
            _authStateProvider = authStateProvider;
        }

        public async Task<LoginResponseDTO> LoginUser(LoginRequestDTO requestDTO)
        {
            var content = JsonConvert.SerializeObject(requestDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/account/login", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LoginResponseDTO>(contentTemp);
            if (response.IsSuccessStatusCode)
            {
                await _localStorageService.SetItemAsync(Keys.Local_Token, result.Token);
                await _localStorageService.SetItemAsync(Keys.Local_UserDetails, result.UserDTO);
                ((CustomStateProvider)_authStateProvider).NotifyUserLoggedIn(result.Token);
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", result.Token);

                return new LoginResponseDTO() { IsAuthSuccess = true, };
            }
            else
            {
                return result;
            }
        }

        public async Task LogOut()
        {
            await _localStorageService.RemoveItemAsync(Keys.Local_Token);
            await _localStorageService.RemoveItemAsync(Keys.Local_UserDetails);

            ((CustomStateProvider)_authStateProvider).NotifyUserLogout();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<RegisterResponseDTO> RegisterUser(RegisterRequestDTO requestDTO)
        {
            var content = JsonConvert.SerializeObject(requestDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/account/register", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RegisterResponseDTO>(contentTemp);
            if (response.IsSuccessStatusCode)
            {
                return new RegisterResponseDTO { IsRegisterationSuccess = true };
            }
            else
            {
                return new RegisterResponseDTO
                {
                    IsRegisterationSuccess = false,
                    Errors = result.Errors,
                };
            }
        }
    }
}
