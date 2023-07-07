using Dapper;
using FinalMVC.DTO;
using FinalMVC.Models;
using MVCWithDapper.DBContext;
using System.Data;

namespace FinalMVC.Services
{


    public interface ITokenService
    {
        public void GenerateToken(Token token);
    }
    public class TokenService :ITokenService
    {

        public readonly DapperContext? _context;
        public TokenService(DapperContext context)
        {
            _context = context;


        }


        public void GenerateToken(Token token)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", token.UserId);
           
            parameters.Add("PrivateToken", token.PrivateToken);
            parameters.Add("PrivateTokenStatus", token.PrivateTokenStatus);
            parameters.Add("PublicToken", token.PublicToken);
            parameters.Add("PublicTokenStatus", token.PublicTokenStatus);
            parameters.Add("CreateDate", token.CreateDate);

            using (IDbConnection dbConnection = _context.Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("InsertToken", parameters, commandType: CommandType.StoredProcedure);

                dbConnection.Close();

            }
        }
    }
}
