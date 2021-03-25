using System;
using DevanshAgrawalCS365;
using System.Data.SqlClient;

namespace DevanshAgrawalCS365
{
    public class Mainmenu
    {
        public string ConnectionString = "";
         public Mainmenu(string _databaseConnectionString)
        {
            ConnectionString = _databaseConnectionString;
        }
        public void showCommands()
        {
            string[] menuStrings = { 
                "(1) Query the order frequency of products",
                "(2) Query a customer",
                "(3) Query all the orders of a customer",
                "(4) Query all the order items of an order",
                "(5) Query all products",
                "(6) Insert a new product",
                "(7) Update a product price",
                "(8) Delete a product",
                "(9) Save(Commit)",
                "(10) Exit" 
            };
            foreach (string i in menuStrings)
            {
                Console.WriteLine($"{i}");
            }
        }

        public void switchAndSelectCommand(int _input)
        {
            switch (_input)
            {
                case 1:

                    string command = "SELECT p.ProductID, p.ProductDescription, p.productFinish, COUNT(o.ORDEREDQUANTITY) AS ORDERFREQUENCY FROM Product_T p JOIN OrderLine_T o on p.ProductID = o.ProductID GROUP BY p.PRODUCTID, p.PRODUCTDESCRIPTION, p.PRODUCTFINISH ORDER BY ORDERFREQUENCY DESC";

                    try
                    {
                        using (SqlConnection client = new SqlConnection(ConnectionString))
                        {
                            SqlCommand cmd = new SqlCommand(command, client);

                            client.Open();

                            using (SqlDataReader rd = cmd.ExecuteReader())
                            {
                                PrintLine();
                                PrintRow("Product ID", "Product Description", "Product Finish", "Order Frequency");
                                PrintLine();
                                
                                static void PrintLine()
                                {
                                    Console.WriteLine(new string('-', 100));
                                }

                                static void PrintRow(params string[] columns)
                                {
                                    int width = 25;
                                    string row = "|";

                                    foreach (string column in columns)
                                    {
                                        row += AlignCentre(column, width) + "|";
                                    }

                                    Console.WriteLine(row);
                                }

                                static string AlignCentre(string text, int width)
                                {
                                    text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

                                    if (string.IsNullOrEmpty(text))
                                    {
                                        return new string(' ', width);
                                    }
                                    else
                                    {
                                        return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
                                    }
                                }



                 
                                while (rd.Read())
                                {
                                    int i = 0;
                                   
                                    while (i < rd.FieldCount)
                                    {
                                       

                                        Console.Write($"{rd[i]} " + "                           ");
                                        i++;
                                    }
                                    Console.Write("  \n");
                                }
                            }
                        }
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine("Error Message " + e.Message);
                    }

                
                    break;
                case 2:


                    Console.Write("Customer Id number: ");
                    int customerId = Convert.ToInt32(Console.ReadLine());
                    string command1 = $"SELECT * FROM Customer_T WHERE CustomerID = {customerId}";
                    try
                    {
                        using (SqlConnection client = new SqlConnection(ConnectionString))
                        {
                            SqlCommand cmd = new SqlCommand(command1, client);

                            client.Open();

                            using (SqlDataReader rd = cmd.ExecuteReader())
                            {
                                Console.Write("  \n");

                                while (rd.Read())
                                {
                                    int i = 0;
                                    while (i < rd.FieldCount)
                                    {
                                        Console.Write($"{rd[i]} ");
                                        i++;
                                    }
                                    Console.Write("  \n");
                                }
                            }
                        }
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine("Error Message " + e.Message);
                    }

                   
                    break;
                case 3:

                    Console.Write("Please Enter the Customer Id number: ");
                    int customerId1 = Convert.ToInt32(Console.ReadLine());

                    string command2 = $"SELECT OrderID  FROM Order_T OT JOIN Customer_T CT on OT.CustomerID = CT.CustomerID WHERE CT.CustomerID = {customerId1}";
                    try
                    {
                        using (SqlConnection client = new SqlConnection(ConnectionString))
                        {
                            SqlCommand cmd = new SqlCommand(command2, client);

                            client.Open();

                            using (SqlDataReader rd = cmd.ExecuteReader())
                            {
                                while (rd.Read())
                                {
                                    int i = 0;
                                    while (i < rd.FieldCount)
                                    {
                                        Console.Write($"{rd[i]} ");
                                        i++;
                                    }
                                    Console.Write("  \n");
                                }
                            }
                        }
                    }
                    catch (SqlException e)
                    {

                        Console.WriteLine("Error Message " + e.Message);
                    }


                    //  readDB.queryCustomerOrderInfo();
                    break;
                case 4:
                    Console.Write("Please Enter the Order Id number: ");
                    int orderId = Convert.ToInt32(Console.ReadLine());

                    string command3 = $"SELECT p.ProductDescription FROM Product_T p join OrderLine_T o on p.ProductID = o.ProductID where OrderID = {orderId}";
                    try
                    {
                        using (SqlConnection client = new SqlConnection(ConnectionString))
                        {
                            SqlCommand cmd = new SqlCommand(command3, client);

                            client.Open();

                            using (SqlDataReader rd = cmd.ExecuteReader())
                            {
                                while (rd.Read())
                                {
                                    int i = 0;
                                    while (i < rd.FieldCount)
                                    {
                                        Console.Write($"{rd[i]} ");
                                        i++;
                                    }
                                    Console.Write("  \n");
                                }
                            }
                        }
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine("Error Message " + e.Message);
                    }
                    break;
                case 5:

                    string command4 = "SELECT * FROM Product_T";
                    try
                    {
                        using (SqlConnection client = new SqlConnection(ConnectionString))
                        {
                            SqlCommand cmd = new SqlCommand(command4, client);

                            client.Open();

                            using (SqlDataReader rd = cmd.ExecuteReader())
                            {
                                while (rd.Read())
                                {
                                    int i = 0;
                                    while (i < rd.FieldCount)
                                    {
                                        Console.Write($"{rd[i]} ");
                                        i++;
                                    }
                                    Console.Write("  \n");
                                }
                            }
                        }
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine("Error Message " + e.Message);
                    }
                    break;
                case 6:

                    int productId = 0;
                    int productLineId = 0;
                    string productDesc = "";
                    string productFinish = "";
                    double productStandardPrice = 0.0;

                    Console.Write("Please Enter the Product ID: ");
                    productId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Please Enter the Product Line ID: ");
                    productLineId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Please Enter the Product Description: ");
                    productDesc = Console.ReadLine();
                    Console.Write("Please Enter the Product Finish: ");
                    productFinish =   Console.ReadLine();
                    Console.Write("Please Enter the Product Standard Price: ");
                    productStandardPrice = Convert.ToDouble(Console.ReadLine());
                    
                    try
                    {
                        using (SqlConnection client = new SqlConnection(ConnectionString))
                        {
                            SqlCommand cmd = new SqlCommand("INSERT INTO PRODUCT_T(ProductID, ProductLineID, ProductDescription, ProductFinish, ProductStandardPRice) VALUES(@productId,@productLineId,@productDesc,@productFinish,@productStandardPrice)", client);
                            cmd.Parameters.AddWithValue("@productId",productId);
                            cmd.Parameters.AddWithValue("@productLineId", productLineId);
                            cmd.Parameters.AddWithValue("@productDesc", productDesc);
                            cmd.Parameters.AddWithValue("@productFinish", productFinish);
                            cmd.Parameters.AddWithValue("@productStandardPrice", productStandardPrice);

                            client.Open();
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                Console.WriteLine("Insertion was successfull!");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error Message " + e.Message);
                    }

                    break;
                case 7:

                    int productId3 = 0;
                    double productStandardPrice1 = 0.0;

                    Console.Write("Input Product ID: ");
                    productId3 = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Input Product Standard Price: ");
                    productStandardPrice1 = Convert.ToDouble(Console.ReadLine());

                    

                    try
                    {
                        using (SqlConnection client = new SqlConnection(ConnectionString))
                        {
                            SqlCommand cmd = new SqlCommand("UPDATE PRODUCT_T SET  ProductStandardPrice  = @ProductStandardPrice WHERE PRODUCTID = @productID", client);
                            cmd.Parameters.AddWithValue("@productId", productId3);
                            cmd.Parameters.AddWithValue("@ProductStandardPrice", productStandardPrice1);
                            client.Open();
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                Console.WriteLine("Update was successfull!");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error Message " + e.Message);
                    }


                    break;
                case 8:

                    int productId2 = 0;

                    Console.Write("Input Product ID: ");
                    productId2 = Convert.ToInt32(Console.ReadLine());

                    string command6 = $"DELETE FROM PRODUCT_T WHERE PRODUCTID = {productId2}";

                    try
                    {
                        using (SqlConnection client = new SqlConnection(ConnectionString))
                        {
                            SqlCommand cmd = new SqlCommand(command6, client);
                            client.Open();
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                Console.WriteLine("Delete was successfull!");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error Message " + e.Message);
                    }
                    break;
                case 9:
                    try
                    {
                        using (SqlConnection client = new SqlConnection(ConnectionString))
                        {
                            client.Open();
                            SqlCommand cmd = client.CreateCommand();
                            SqlTransaction transaction = client.BeginTransaction();
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                Console.WriteLine("Commit was successfull!");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error Message " + e.Message);
                    }
                    break;
                case 10:
                    break;
                default:
                    Console.WriteLine("Error: Please select a number between [1, 10]");
                    Console.Write("  \n");
                    break;
            }

        }

    }
}
