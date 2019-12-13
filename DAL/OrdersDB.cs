﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DataTransferObject;

namespace DAL
{
    public class OrdersDB : IOrdersDB
    {
        private string connectionString = null;
        public OrdersDB(IConfiguration configuration)
        {
            var config = configuration;
            connectionString = config.GetConnectionString("DefaultConnection");
        }




        public List<Order> GetOrders()
        {
            List<Order> results = null;
            string connectionString = "Data Source=153.109.124.35;Initial Catalog=VsEatPiguetBerthouzoz;Integrated Security=False;User Id=6231db;Password=Pwd46231.;MultipleActiveResultSets=True";

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [Order]";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Order>();

                            Order order = new Order();

                            order.IdOrder = (int)dr["IdOrder"];
                            order.status = (string)dr["status"];
                            order.created_at = (DateTime)dr["created_at"];
                            order.timeToDeliver = (DateTime)dr["timeToDeliver"];
                            order.totalprice = (decimal)dr["totalprice"];
                            order.IdCustomer = (int)dr["IdCustomer"];
                            order.IdCourier = (int)dr["IdCourier"];

                            results.Add(order);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return results;
        }


        public List<Order> GetCustomerOrders()
        {
            List<Order> lOrder = null;

            string connectionString = "Data Source=153.109.124.35;Initial Catalog=VsEatPiguetBerthouzoz;Integrated Security=False;User Id=6231db;Password=Pwd46231.;MultipleActiveResultSets=True";
            try
            { 
            using (SqlConnection cn = new SqlConnection(connectionString))
            {

                string query = "SELECT * FROM [Order] inner join Customer on [Order].IdCustomer=Customer.IdCustomer";
                SqlCommand cmd = new SqlCommand(query, cn);
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if (lOrder == null)
                                lOrder = new List<Order>();

                        Order orderTemp = new Order();

                            orderTemp.IdOrder = (int)dr["IdOrder"];
                            orderTemp.status = (string)dr["status"];
                            orderTemp.created_at = (DateTime)dr["created_at"];
                            orderTemp.timeToDeliver = (DateTime)dr["timeToDeliver"];
                            orderTemp.totalprice = (decimal)dr["totalprice"];
                            orderTemp.IdCustomer = (int)dr["IdCustomer"];
                            orderTemp.IdCourier = (int)dr["IdCourier"];
                            orderTemp.name = (string)dr["name"];
                            orderTemp.lastname = (string)dr["lastname"];
                            orderTemp.streetname = (string)dr["streetname"];
                            orderTemp.IdCity = (int)dr["IdCity"];

                            lOrder.Add(orderTemp);

                    }
                }
            }
            }
            catch (Exception e)
            {
                throw e;
            }

            return lOrder;
        }




        public Order GetOrder(int id)
        {
            string connectionString = "Data Source=153.109.124.35;Initial Catalog=VsEatPiguetBerthouzoz;Integrated Security=False;User Id=6231db;Password=Pwd46231.;MultipleActiveResultSets=True";
            Order order = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [Order] WHERE IdOrder = @id"; 
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            order = new Order();

                            order.IdOrder = (int)dr["IdOrder"];
                            order.status = (string)dr["status"];
                            order.created_at = (DateTime)dr["created_at"];
                            order.timeToDeliver = (DateTime)dr["timeToDeliver"];
                            order.totalprice = (decimal)dr["totalprice"];
                            order.IdCustomer = (int)dr["IdCustomer"];
                            order.IdCourier = (int)dr["IdCourier"];


                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return order;
        }



        public Order GetCustomerOrder(int id)
        {
            string connectionString = "Data Source=153.109.124.35;Initial Catalog=VsEatPiguetBerthouzoz;Integrated Security=False;User Id=6231db;Password=Pwd46231.;MultipleActiveResultSets=True";
            Order order = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [Order] inner join Customer on [Order].IdCustomer=Customer.IdCustomer WHERE IdOrder = @id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            order = new Order();

                            order.IdOrder = (int)dr["IdOrder"];
                            order.status = (string)dr["status"];
                            order.created_at = (DateTime)dr["created_at"];
                            order.timeToDeliver = (DateTime)dr["timeToDeliver"];
                            order.totalprice = (decimal)dr["totalprice"];
                            order.IdCustomer = (int)dr["IdCustomer"];
                            order.IdCourier = (int)dr["IdCourier"];
                            order.name = (string)dr["name"];
                            order.lastname = (string)dr["lastname"];
                            order.streetname = (string)dr["streetname"];
                            order.IdCity = (int)dr["IdCity"];


                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return order;
        }


        public Order CreateOrder(Order order)
        {

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Insert into Order(status, created_at, timeToDeliver, totalprice, IdCustomer, IdCourier) values(@status, NOW(), @timeToDeliver, @totalprice, @IdCustomer, @IdCourier); SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    //cmd.Parameters.AddWithValue("@IdOrder", order.IdOrder);
                    cmd.Parameters.AddWithValue("@status", order.status);
                    cmd.Parameters.AddWithValue("@created_at", order.created_at);
                    cmd.Parameters.AddWithValue("@timeToDeliver", order.timeToDeliver);
                    cmd.Parameters.AddWithValue("@totalprice", order.totalprice);
                    cmd.Parameters.AddWithValue("@IdCustomer", order.IdCustomer);
                    cmd.Parameters.AddWithValue("@IdCourier", order.IdCourier);
                    cn.Open();

                    order.IdOrder = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return order;
        }


        public Order AddOrder(Order order)
        {

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Insert into Order(IdOrder, status, created_at, IdCustomer, IdCourier) values(@IdOrder, @status, @created_at, @IdCustomer, @IdCourier); SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@IdOrder", order.IdOrder);
                    cmd.Parameters.AddWithValue("@status", order.status);
                    cmd.Parameters.AddWithValue("@created_at", order.created_at);
                    cmd.Parameters.AddWithValue("@IdCustomer", order.IdCustomer);
                    cmd.Parameters.AddWithValue("@IdCourier", order.IdCourier);
                    cn.Open();

                    order.IdOrder = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return order;
        }

        public int UpdateOrder(Order order)
        {
            int result = 0;
            string connectionString = "Data Source=153.109.124.35;Initial Catalog=VsEatPiguetBerthouzoz;Integrated Security=False;User Id=6231db;Password=Pwd46231.;MultipleActiveResultSets=True";
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE [Order] SET status = @status, created_at = @created_at, timeToDeliver = @timeToDeliver, totalprice = @totalprice, IdCustomer = @IdCustomer, IdCourier = @IdCourier WHERE IdOrder=@id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", order.IdOrder);
                    cmd.Parameters.AddWithValue("@status", order.status);
                    cmd.Parameters.AddWithValue("@created_at", order.created_at);
                    cmd.Parameters.AddWithValue("@timeToDeliver", order.timeToDeliver);
                    cmd.Parameters.AddWithValue("@totalprice", order.totalprice);
                    cmd.Parameters.AddWithValue("@IdCustomer", order.IdCustomer);
                    cmd.Parameters.AddWithValue("@IdCourier", order.IdCourier);
                    cn.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }


        public int SetDelivered(Order order)
        {
            int result = 0;
            string connectionString = "Data Source=153.109.124.35;Initial Catalog=VsEatPiguetBerthouzoz;Integrated Security=False;User Id=6231db;Password=Pwd46231.;MultipleActiveResultSets=True";
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE [Order] SET status = @status WHERE IdOrder=@id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", order.IdOrder);
                    cmd.Parameters.AddWithValue("@status", order.status);

                    cn.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }




        public int DeleteOrder(int id)
        {
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Order WHERE IdOrder=@id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

    }
}
