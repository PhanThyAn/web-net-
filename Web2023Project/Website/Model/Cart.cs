using System;
using System.Collections.Generic;
using Web2023Project.Models;

namespace Web2023Project.Website.Model
{
    public class Cart
    {
        private int cartID;
        private Nguoidung member;
        private List<Item> item;
        private int cartStatus;

        public Cart(int cartId, Nguoidung member, List<Item> item, int cartStatus)
        {
            cartID = cartId;
            this.member = member;
            this.item = item;
            this.cartStatus = cartStatus;
        }
        public Cart()
        {
             
        }

        public int CartId
        {
            get => cartID;
            set => cartID = value;
        }

        public Nguoidung Member
        {
            get => member;
            set => member = value;
        }

        public List<Item> Item
        {
            get => item;
            set => item = value;
        }

        public int CartStatus
        {
            get => cartStatus;
            set => cartStatus = value;
        }

        public int TotalItem()
        {
            int count = 0;
            if (item != null && item.Count != 0)
            {
                foreach (Item i in item)
                {
                    count += i.Amount;
                }
            }

            return count;
        }

        public double TotalPrice()
        {
            double price = 0;
            if (item != null && item.Count != 0)
            {
                foreach (Item i in item)
                {
                    price += i.Price * i.Amount;
                }
            }

            return price;
        }
    }
}