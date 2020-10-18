﻿using FlyPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLFlyPack
{
    public class BlShop
    {
        public int Id { get; }
        public string ShopManagerId { get; set; }
        public string ShopName { get; set; }
        public  Point Location { get; }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="shopManagerId"></param>
        /// <param name="shopName"></param>
        public BlShop( string shopManagerId, string shopName)
        {
            try
            {
                Id = DalShop.AddShop(shopManagerId,shopName);
                Location =this.GetPosition();
            }
            catch
            {
                Id = -1;
            }
            ShopManagerId = shopManagerId;
           
            ShopName = shopName;
          
        }
        /// <summary>
        /// constructor by data row
        /// </summary>
        /// <param name="row"></param>
        public BlShop(DataRow row)
        {
            Id = int.Parse(row["ID"].ToString());
            ShopName = row["ShopName"].ToString();
            ShopManagerId= row["ShopManagerID"].ToString();
            Location = GetPosition();
        }
        /// <summary>
        /// get all the shop in DB
        /// </summary>
        /// <returns> shop list</returns>
        public static List<BlShop> GetShops()
        {
            DataTable shops = DalShop.GetShopTable();
            return (from DataRow row in shops.Rows select new BlShop(row)).ToList();
        }
        /// <summary>
        /// a new shop by shop id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static BlShop GetShopById(int id)
        {
            DataRow row = null;
            try
            {
                 row = DalShop.GetShop(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
                return null;
            }
            return new BlShop(row);
        }
        /// <summary>
        /// get the shop location
        /// </summary>
        /// <returns>point object that represent the shop location</returns>
        public Point GetPosition()
        {
            DataRow row = DalShop.GetLocation(this.Id);
            return new Point(double.Parse(row["Lat"].ToString()), double.Parse(row["Lng"].ToString()));
        }
    }
}
