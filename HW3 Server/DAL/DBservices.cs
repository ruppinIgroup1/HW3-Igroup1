using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Microsoft.AspNetCore.Server;
using System.Xml.Linq;
using STEAM.Models;
using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Mvc;



/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
public class DBservices
{
    

    public DBservices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString("myProjDB");
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

    //--------------------------------------------------------------------------------------------------
    // This method inserts a Game to the Games table 
    //--------------------------------------------------------------------------------------------------
    public int InitInsert(List<Game> listOfGames)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB");
        }
        catch (Exception ex)
        {

            throw ex;
        }
        int sumOfNumEff = 0;
        for (int i = 0; i < listOfGames.Count; i++)
        {
            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            Game game = listOfGames[i];
            paramDic.Add("@AppID", game.AppID);
            paramDic.Add("@Name", game.Name);
            paramDic.Add("@IsMac", game.Mac == "TRUE" ? true : false);
            paramDic.Add("@Price", game.Price);
            paramDic.Add("@Description", game.Description);
            paramDic.Add("@IsWindows", game.Windows == "TRUE" ? true : false);
            paramDic.Add("@Website", game.Website);
            paramDic.Add("@HeaderImage", game.HeaderImage);
            paramDic.Add("@IsLinux", game.Linux == "TRUE" ? true : false);
            paramDic.Add("@ScoreRank", game.ScoreRank);
            paramDic.Add("@Recommendations", game.Recommendations);
            paramDic.Add("@Publisher", game.Publisher);
            paramDic.Add("@ReleaseDate", game.releaseDate);



            cmd = CreateCommandWithStoredProcedureGeneral("SP_InsertGame", con, paramDic);        // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                sumOfNumEff += numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
                throw (ex);
            }


        }

        if (con != null)
        {
            con.Close();
        }
        return sumOfNumEff;



    }


    //SP_UpdateUser
    public int UpdateUser(int Id, string email, string password, string name)
    {

        SqlConnection con;
        SqlCommand cmd;
        int sumOfNumEff = 0;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@UserID", Id);
        paramDic.Add("@Email", email);
        paramDic.Add("@Password", password);
        paramDic.Add("@Name", name);

        cmd = CreateCommandWithStoredProcedureGeneral("SP_UpdateUser", con, paramDic);          // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            sumOfNumEff += numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
            throw (ex);
        }


        if (con != null)
        {
            con.Close();
        }
        return sumOfNumEff;
    }



    //SP_LoginUser
    public UserClass LoginUser(string Email, string Password)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@Email", Email);
        paramDic.Add("@Password", Password);


        cmd = CreateCommandWithStoredProcedureGeneral("SP_LoginUser", con, paramDic);          // create the command


        try
        {
            
            using (SqlDataReader dataReader = cmd.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    var userData = new UserClass
                    {
                        Id = dataReader.GetInt32(0),
                        Name = dataReader.IsDBNull(1) ? "Guest" : dataReader.GetString(1),
                        Email = dataReader.GetString(2), 
                        Password = dataReader.GetString(3) 

                    };

                    return userData; 
                }
                return null;
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }


    //SP_MyUsers
    public List<UserClass> MyUsers()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();

        cmd = CreateCommandWithStoredProcedureGeneral("SP_MyUsers", con, paramDic);          // create the command

        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        List<UserClass> result = new List<UserClass>();

        while (dataReader.Read())
        {
            UserClass u = new UserClass();
            u.id = Convert.ToInt32(dataReader["id"]);
            u.Name = dataReader["Name"].ToString();
            u.Email = dataReader["Email"].ToString();
            u.Password = dataReader["Password"].ToString();
            result.Add(u);
        }

        return result;

    }





    //SP_AddNewUser
    public UserClass AddNewUser(string Name, string Email, string Password)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@Name", Name);
        paramDic.Add("@Email", Email);
        paramDic.Add("@Password",Password);


        cmd = CreateCommandWithStoredProcedureGeneral("SP_AddNewUser", con, paramDic);          // create the command

        try
        {
            using (SqlDataReader dataReader = cmd.ExecuteReader())
            {
                if (dataReader.Read()) 
                {
                    var userData = new UserClass
                    {
                        Id = dataReader.GetInt32(0),
                        Name = dataReader.IsDBNull(1) ? "Guest" : dataReader.GetString(1),
                        Email = dataReader.GetString(2),
                        Password = dataReader.GetString(3)
                    };
                    return userData;
                }
                return null;
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }



    //SP_UserBuyGame
    public int UserBuyGame(int ID, int AppID)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@UserID", ID);
        paramDic.Add("@AppID", AppID);


        cmd = CreateCommandWithStoredProcedureGeneral("SP_UserBuyGame", con, paramDic);          // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //SP_UserDeleteGame
    public int DeleteById(int id, int AppID)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@UserID", id);
        paramDic.Add("@AppID", AppID);

        cmd = CreateCommandWithStoredProcedureGeneral("SP_UserDeleteGame", con, paramDic);          // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    //SP_MyGames
    public List<Game> MyGames(int UserID)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@UserID", UserID);

        cmd = CreateCommandWithStoredProcedureGeneral("SP_MyGames", con, paramDic); // create the command

        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        List<Game> result = new List<Game>();

        while (dataReader.Read())
        {
            Game g = new Game();
            g.AppID = Convert.ToInt32(dataReader["AppID"]);
            g.Name = dataReader["Name"].ToString();
            g.ReleaseDate = dataReader["ReleaseDate"].ToString();
            g.Price = Convert.ToDouble(dataReader["Price"]);
            g.Description = Convert.ToString(dataReader["Description"]);
            g.HeaderImage = Convert.ToString(dataReader["HeaderImage"]);
            g.Website = Convert.ToString(dataReader["Website"]);
            g.Windows = Convert.ToString(dataReader["IsWindows"]);
            g.Mac = Convert.ToString(dataReader["IsMac"]);
            g.Linux = Convert.ToString(dataReader["IsLinux"]);
            g.ScoreRank = Convert.ToInt32(dataReader["ScoreRank"]);
            g.Recommendations = Convert.ToString(dataReader["Recommendations"]);
            g.Publisher = Convert.ToString(dataReader["Publisher"]);
            g.NumberOfPurchases = Convert.ToInt32(dataReader["NumberOfPurchases"]);
            result.Add(g);
        }

        return result;
    }


    //SP_GetByRankScore
    public List<Game> GetByRankScore(string UserID,double RankScore)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        
        paramDic.Add("@UserID", UserID);
        paramDic.Add("@RankScore", RankScore);

        cmd = CreateCommandWithStoredProcedureGeneral("SP_GetByRankScore", con, paramDic); // create the command

        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        List<Game> result = new List<Game>(); 

        while (dataReader.Read())
        {
            Game g = new Game();
            g.AppID = Convert.ToInt32(dataReader["AppID"]);
            g.Name = dataReader["Name"].ToString();
            g.ReleaseDate = dataReader["ReleaseDate"].ToString();
            g.Price = Convert.ToDouble(dataReader["Price"]);
            g.Description = Convert.ToString(dataReader["Description"]);
            g.HeaderImage = Convert.ToString(dataReader["HeaderImage"]);
            g.Website = Convert.ToString(dataReader["Website"]);
            g.Windows = Convert.ToString(dataReader["IsWindows"]);
            g.Mac = Convert.ToString(dataReader["IsMac"]);
            g.Linux = Convert.ToString(dataReader["IsLinux"]);
            g.ScoreRank = Convert.ToInt32(dataReader["ScoreRank"]);
            g.Recommendations = Convert.ToString(dataReader["Recommendations"]);
            g.Publisher = Convert.ToString(dataReader["Publisher"]);
            g.NumberOfPurchases = Convert.ToInt32(dataReader["NumberOfPurchases"]);
            result.Add(g); 
        }

        return result;
    }


    //SP_GetByPrice
    public List<Game> GetByPrice(string UserID, double Price)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@UserID", UserID);
        paramDic.Add("@Price", Price);
       

        cmd = CreateCommandWithStoredProcedureGeneral("SP_Price", con, paramDic); // create the command

        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        List<Game> result = new List<Game>();

        while (dataReader.Read())
        {
            Game g = new Game();
            g.AppID = Convert.ToInt32(dataReader["AppID"]);
            g.Name = dataReader["Name"].ToString();
            g.ReleaseDate = dataReader["ReleaseDate"].ToString();
            g.Price = Convert.ToDouble(dataReader["Price"]);
            g.Description = Convert.ToString(dataReader["Description"]);
            g.HeaderImage = Convert.ToString(dataReader["HeaderImage"]);
            g.Website = Convert.ToString(dataReader["Website"]);
            g.Windows = Convert.ToString(dataReader["IsWindows"]);
            g.Mac = Convert.ToString(dataReader["IsMac"]);
            g.Linux = Convert.ToString(dataReader["IsLinux"]);
            g.ScoreRank = Convert.ToInt32(dataReader["ScoreRank"]);
            g.Recommendations = Convert.ToString(dataReader["Recommendations"]);
            g.Publisher = Convert.ToString(dataReader["Publisher"]);
            g.NumberOfPurchases = Convert.ToInt32(dataReader["NumberOfPurchases"]);
            result.Add(g);
        }

        return result;
    }


    //---------------------------------------------------------------------------------
    // Create the SqlCommand
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureGeneral(String spName, SqlConnection con, Dictionary<string, object> paramDic)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        if (paramDic != null)
            foreach (KeyValuePair<string, object> param in paramDic)
            {
                cmd.Parameters.AddWithValue(param.Key, param.Value);

            }


        return cmd;
    }


    //--------------------------------------------------------------------
    // TODO Build the FLight Delete  method
    // DeleteFlight(int id)
    //--------------------------------------------------------------------

}
