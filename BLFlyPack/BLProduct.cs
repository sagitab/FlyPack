﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyPack;

namespace BLFlyPack
{
    public class BLProduct
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int ShopID { get; set; }
        public int OrderID { get; set; }
        public int ShopProductCode { get; set; }
        public string ImageUrl { get; set; } /*name in DB Image*/

        public BLProduct(int id, double price, string description, int shopId, int orderId, int shopProductCode, string imageUrl)
        {
            Id = id;//update DB?
            Price = price;
            Description = description;
            ShopID = shopId;
            OrderID = orderId;
            ShopProductCode = shopProductCode;
            ImageUrl = imageUrl;
        }

        public BLProduct(DataRow row)
        {
            Id = int.Parse(row["ID"].ToString());
            Price = double.Parse(row["Price"].ToString());
            Description = row["Description"].ToString();
            ShopID = int.Parse(row["ShopID"].ToString());
            OrderID = int.Parse(row["OrderID"].ToString());
            ShopProductCode = int.Parse(row["ShopProductCode"].ToString());
            ImageUrl = row["Image"].ToString();
        }
        public static List<BLProduct> GetAllProducts(string condition)
        {
            DataTable products = ProductDal.GetAllProducts(condition);
            return (from object rowProduct in products.Rows select new BLProduct((DataRow)rowProduct)).ToList();
        }

        public static List<BLProduct> GetAllProductsByShopId(int shopId,string condition)
        {
            DataTable products = null;
            products = ProductDal.GetAllProductsOfShop(shopId,condition);
            return (from object rowProduct in products.Rows select new BLProduct((DataRow)rowProduct)).ToList();
        }
        public static List<BLProduct> GetAllProductsByPrice(bool isUp, string condition)
        {
            DataTable products = null;
            products = ProductDal.GetAllProductsOrderByPrice(isUp ? "DESC" : "ASC");
            return (from object rowProduct in products.Rows select new BLProduct((DataRow)rowProduct)).ToList();
        }
        public static List<BLProduct> GetAllProductsByName(bool isUp, string condition)
        {
            DataTable products = null;
            products = ProductDal.GetAllProductsOrderByName(isUp ? "DESC" : "ASC");
            return (from object rowProduct in products.Rows select new BLProduct((DataRow)rowProduct)).ToList();
        }
        public static List<BLProduct> ProductsSearch(bool IsByPrice, string searchVal, string condition)
        {
            DataTable products = null;
            products = IsByPrice ? ProductDal.SearchProductsByPrice(double.Parse(searchVal)) : ProductDal.SearchProductsByName(searchVal);
            return (from object rowProduct in products.Rows select new BLProduct((DataRow)rowProduct)).ToList();
        }
        public static string setOrderBy(int orderBy)
        {
            string[] OrderByArr = { "Price DESC", "Price ASC", "Description DESC", "Description ASC", "ID" };
            return " ORDER BY " + OrderByArr[orderBy];
        }
        public static List<BLProduct> Search(string SearchVal, int shopId, bool isSearchName, int orderBy)
        {


            string condition = "";
            List<BLProduct> products = null;
            if (shopId == -1)
            {
                if (isSearchName)
                {
                    condition = $"WHERE Description='{SearchVal}'";
                }
                else
                {
                    condition = "WHERE Price=" + SearchVal;
                }
                condition += setOrderBy(orderBy);
                products = BLProduct.GetAllProducts(condition);
            }
            else
            {
                if (isSearchName)
                {
                    condition = $"AND Description='{SearchVal}'";
                }
                else
                {
                    condition = "AND Price=" + SearchVal;
                }
                condition += setOrderBy(orderBy);
                products = BLProduct.GetAllProductsByShopId(shopId, condition);
            }

            if (products == null || products.Count == 0)
            {
                return null;
            }

            return products;
        }
        public static int SumArr(int[] arr)
        {
            int sum = 0;
            foreach (var t in arr)
            {
                sum += t;
            }
            return sum;
        }

        public static void Delete(int[] amounts,int index)
        {
            //int temp;
            for (int i = index; i < amounts.Length-1; i++)
            {
                amounts[i] = amounts[i + 1];
            }
        }
        public static int IndexOfProduct(List<BLProduct> products,BLProduct product)
        {
            for (var index = 0; index < products.Count; index++)
            {
                var p = products[index];
                if (p.Id == product.Id)
                {
                    return index;
                }
            }
            return -1;
        }
    }
}
