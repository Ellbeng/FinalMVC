using Dapper;
using FinalMVC.Areas.Identity.Data;
using FinalMVC.DTO;
using FinalMVC.Models;
using MVCWithDapper.DBContext;
using System.Data;
using System.Data.Common;

namespace FinalMVC.Services
{
    public interface IWalletService
    {
        public Users GetUserById(string userId);
        public WalletUserDto GetUserWithWallet(string userId);
    }
    public class WalletService : IWalletService
    {
        public readonly DapperContext? _context;
       


        public WalletService(DapperContext context)
        {
            _context = context;
            
           
        }



        public WalletUserDto GetUserWithWallet(string userId)
        {    
            
            
            
            
            var sql = @"SELECT w.walletId, w.UserId, w.CurrentBalance, u.Id AS UserId, u.Email, u.FirstName, u.LastName
                          FROM Wallet w 
                         INNER JOIN AspNetUsers u ON w.UserId = u.Id
                            WHERE u.Id = @UserId";
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);

            using (IDbConnection dbConnection = _context.Connection)
            { 
                
                dbConnection.Open();

                var result = dbConnection.Query<WalletUserDto>(sql,parameters).FirstOrDefault();
                return result;


            }




        }



        public Users GetUserById(string userId)
        {
            IDbConnection dbConnection = _context.Connection;
            string sql = "SELECT * FROM AspNetUsers WHERE Id = @UserId";
            return dbConnection.QueryFirstOrDefault<Users>(sql, new { UserId = userId });
        }


    }

}
