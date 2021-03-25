using System;
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
        public int ShopProductCode { get; set; }
        public string ImageUrl { get; set; } /*name in DB Image*/

        public static string GetProductNameById(int productId)
        {
            try
            {
                return GetProductNameById(productId);
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// constructor that not update DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="price"></param>
        /// <param name="description"></param>
        /// <param name="shopId"></param>
        /// <param name="orderId"></param>
        /// <param name="shopProductCode"></param>
        /// <param name="imageUrl"></param>
        public BLProduct(int id, double price, string description, int shopId, int shopProductCode, string imageUrl)
        {
            Id = id;//update DB?
            Price = price;
            Description = description;
            ShopID = shopId;
            ShopProductCode = shopProductCode;
            ImageUrl = imageUrl;
        }


        public static BLProduct GetProductById(int productId)
        {
            DataRow row = ProductDal.GetProductById(productId);
            return new BLProduct(row);
        }
        //
        /// <summary>
        /// constructor that add product to DB
        /// </summary>
        /// <param name="price"></param>
        /// <param name="description"></param>
        /// <param name="shopId"></param>
        /// <param name="orderId"></param>
        /// <param name="shopProductCode"></param>
        /// <param name="imageUrl"></param>
        public BLProduct(double price, string description, int shopId, int shopProductCode, string imageUrl)
        {
            Id = ProductDal.AddProduct(price, description, shopId, shopProductCode, imageUrl);
            Price = price;
            Description = description;
            ShopID = shopId;
            ShopProductCode = shopProductCode;
            ImageUrl = imageUrl;
        }
        /// <summary>
        /// add product by row and shop id
        /// </summary>
        /// <param name="row"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public static BLProduct AddProductByDataRow(DataRow row, int shopId)
        {
            double Price = double.Parse(row["Price"].ToString());
            string Description = row["Description"].ToString();
            int ShopProductCode = int.Parse(row["ShopProductCode"].ToString());
            string ImageUrl = row["Image"].ToString();
            int Id = ProductDal.AddProduct(Price, Description, shopId, ShopProductCode, ImageUrl);
            return new BLProduct(Id, Price, Description, shopId, ShopProductCode, ImageUrl);
        }
        /// <summary>
        /// update the product from shop DB to my DB
        /// </summary>
        /// <param name="products"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public static bool UpdateProduct(DataTable products, int shopId)
        {
            List<BLProduct> productsList = (from DataRow row in products.Rows select AddProductByDataRow(row, shopId)).ToList();
            return productsList.All(product => product?.Id != 1);
        }
        /// <summary>
        /// constructor by data row
        /// </summary>
        /// <param name="row"></param>
        public BLProduct(DataRow row)
        {
            Id = int.Parse(row["ID"].ToString());
            Price = double.Parse(row["Price"].ToString());
            Description = row["Description"].ToString();
            ShopID = int.Parse(row["ShopID"].ToString());
            ShopProductCode = int.Parse(row["ShopProductCode"].ToString());
            ImageUrl = row["Image"].ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns>List of all BLProduct in DB</returns>
        public static List<BLProduct> GetAllProducts(string condition)
        {
            DataTable products = ProductDal.GetAllProducts(condition);
            return (from object rowProduct in products.Rows select new BLProduct((DataRow)rowProduct)).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="condition"></param>
        /// <returns>List of all BLProduct with a specific shop id in DB</returns>
        public static List<BLProduct> GetAllProductsByShopId(int shopId, string condition)
        {
            DataTable products = null;
            products = ProductDal.GetAllProductsOfShop(shopId, condition);
            return (from object rowProduct in products.Rows select new BLProduct((DataRow)rowProduct)).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderBy"></param>
        /// <returns>the match string to order by int</returns>
        public static string setOrderBy(int orderBy)
        {
            string[] OrderByArr = { "Price DESC", "Price ASC", "Description DESC", "Description ASC", "ID" };
            return " ORDER BY " + OrderByArr[orderBy];
        }
        /// <summary>
        /// search product
        /// </summary>
        /// <param name="SearchVal"></param>
        /// <param name="shopId"></param>
        /// <param name="isSearchName"></param>
        /// <param name="orderBy"></param>
        /// <returns>a list of BLProduct that match the searched value</returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <returns>sum of the values in arr</returns>
        public static int SumArr(int[] arr)
        {
            if (arr == null)
            {
                return 0;
            }
            int sum = 0;
            foreach (var t in arr)
            {
                sum += t;
            }
            return sum;
        }
        /// <summary>
        /// delete val at index from amounts
        /// </summary>
        /// <param name="amounts"></param>
        /// <param name="index"></param>
        public static void Delete(int[] amounts, int index)
        {
            //int temp;
            for (int i = index; i < amounts.Length - 1; i++)
            {
                amounts[i] = amounts[i + 1];
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="products"></param>
        /// <param name="product"></param>
        /// <returns>the Index Of Product</returns>
        public static int IndexOfProduct(List<BLProduct> products, BLProduct product)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>ShopId By Product Id</returns>
        public static int GetShopIdByProductId(int productId)
        {
            return int.Parse(ProductDal.GetShopIdByProductId(productId).Rows[0]["ShopID"].ToString());
        }
        /// <summary>
        /// calculate total price
        /// </summary>
        /// <param name="products"></param>
        /// <param name="amounts"></param>
        /// <returns> total price</returns>
        public static double TotalPrice(List<BLProduct> products, int[] amounts)
        {
            if (products != null)
            {
                return products.Select((product, i) => product.Price * amounts[i]).Sum();
            }
            else
            {
                return 0;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="products"></param>
        /// <param name="amounts"></param>
        /// <returns> string that describe the list</returns>
        public static string GetProductString(List<BLOrderDetailsDB> orderDetails)
        {
            string ProductString = "";
            for (int i = 0; i < orderDetails.Count; i++)
            {
                BLOrderDetailsDB orderDetail = orderDetails[i];
                BLProduct product = BLProduct.GetProductById(orderDetail.productId);
                ProductString += "<br/>" + product.ToString() + " amount-" + orderDetail.amount;
            }
            return ProductString;
        }
        public override string ToString()
        {
            return this.Description + " $" + this.Price;
        }
        //public static List<BLProduct> GetAllProductsByPrice(bool isUp)
        //{
        //    DataTable products = null;
        //    products = ProductDal.GetAllProductsOrderByPrice(isUp ? "DESC" : "ASC");
        //    return (from object rowProduct in products.Rows select new BLProduct((DataRow)rowProduct)).ToList();
        //}
        //public static List<BLProduct> GetAllProductsByName(bool isUp)
        //{
        //    DataTable products = null;
        //    products = ProductDal.GetAllProductsOrderByName(isUp ? "DESC" : "ASC");
        //    return (from object rowProduct in products.Rows select new BLProduct((DataRow)rowProduct)).ToList();
        //}
        //public static List<BLProduct> ProductsSearch(bool IsByPrice, string searchVal, string condition)
        //{
        //    DataTable products = null;
        //    products = IsByPrice ? ProductDal.SearchProductsByPrice(double.Parse(searchVal)) : ProductDal.SearchProductsByName(searchVal);
        //    return (from object rowProduct in products.Rows select new BLProduct((DataRow)rowProduct)).ToList();
        //}
        public bool RemoveProduct()
        {
            try
            {
                return ProductDal.RemoveProduct(this.Id);
            }
            catch
            {
                return false;
            }
        }
    }
}
