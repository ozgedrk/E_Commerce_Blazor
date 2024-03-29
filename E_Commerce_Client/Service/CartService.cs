﻿using Blazored.LocalStorage;
using E_Commerce_Client.Service.IService;
using E_Commerce_Client.ViewModels;
using E_Commerce_Common;

namespace E_Commerce_Client.Service
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorageService;
        public CartService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public event Action OnChange;

        public async Task DecrementItem(ShoppingCart shoppingCart)
        {
            var cart = await _localStorageService.GetItemAsync<List<ShoppingCart>>(Keys.ShoppingCart);
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ProductId == shoppingCart.ProductId && cart[i].ProductPriceId == shoppingCart.ProductPriceId)
                {
                    if (cart[i].Count == 1 || shoppingCart.Count == 0)
                    {
                        cart.Remove(cart[i]);
                    }
                    else
                    {
                        cart[i].Count -= shoppingCart.Count;
                    }
                }
            }
            await _localStorageService.SetItemAsync(Keys.ShoppingCart, cart);
            OnChange?.Invoke();
        }

        public async Task IncrementItem(ShoppingCart shoppingCart)
        {
            var cart = await _localStorageService.GetItemAsync<List<ShoppingCart>>(Keys.ShoppingCart);
            bool itemInCart = false;
            if (cart == null)
            {
                cart = new List<ShoppingCart>();
            }
            foreach (var item in cart)
            {
                if (item.ProductId == shoppingCart.ProductId && item.ProductPriceId == shoppingCart.ProductPriceId)
                {
                    itemInCart = true;
                    item.Count += shoppingCart.Count;
                }
            }
            if (!itemInCart)
            {
                cart.Add(new ShoppingCart()
                {
                    ProductId = shoppingCart.ProductId,
                    ProductPriceId = shoppingCart.ProductPriceId,
                    Count = shoppingCart.Count
                });
            }
            await _localStorageService.SetItemAsync(Keys.ShoppingCart, cart);
            OnChange?.Invoke();

        }
    }
}
