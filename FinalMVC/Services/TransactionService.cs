using Dapper;
using FinalMVC.Areas.Identity.Data;
using FinalMVC.Models;
using MVCWithDapper.DBContext;
using System.Web.Mvc;
using System.Data;
using System.Net.Http;
using System.Text.Json;
using FinalMVC.DTO;

namespace FinalMVC.Services
{
    public interface ITransactionService
    {
        public Transaction InsertTransaction(Transaction transaction);
        public string GeneratetransactionFormFillLink(TransactionInfo transaction);
        public void UpdateTransaction(UpdateTransaction transaction);
        public Transaction getTransactionById(int Id);

    }
    public class TransactionService:ITransactionService
    {


        public readonly DapperContext? _context;
        public TransactionService(DapperContext context)
        {
            _context = context;


        }
        public string GeneratetransactionFormFillLink(TransactionInfo transaction)
        {
            string start = "";
            if (transaction.PaymentType == "Deposit") start = "https://localhost:44339/User/Deposit";
            else if (transaction.PaymentType == "Withdraw") start = "https://localhost:44339/User/Withdraw";
            string bankFormFillLink = start+"?amount=" + transaction.Amount.ToString() + 
                "&id=" + transaction.Id.ToString() + "&status=" + transaction.Status.ToString()+ "&paymentType=" + transaction.PaymentType.ToString();
            // string bankFormFillLink = "https://localhost:44365/Card/CardInformation";
            return bankFormFillLink;
        }


        public Transaction InsertTransaction(Transaction transaction)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", transaction.UserId);
            parameters.Add("PaymentType", transaction.PaymentType);
            parameters.Add("Amount", transaction.Amount);
            parameters.Add("Currency", transaction.Currency);
            parameters.Add("CreateDate", transaction.CreateDate);
            parameters.Add("Status", transaction.Status);

            using (IDbConnection dbConnection = _context.Connection)
            {
                dbConnection.Open();
                var newTransaction=dbConnection.QuerySingleOrDefault<Transaction>("InsertTransactions", parameters, commandType: CommandType.StoredProcedure);
                
                dbConnection.Close();
                return newTransaction;
            }
        }

        public void UpdateTransaction(UpdateTransaction transaction)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", transaction.UserId);
            parameters.Add("Id", transaction.Id);
            parameters.Add("Amount", transaction.Amount);
            parameters.Add("Status", transaction.Status);

            using (IDbConnection dbConnection = _context.Connection)
            {
                dbConnection.Open();
               dbConnection.Execute("UpdateTransaction", parameters, commandType: CommandType.StoredProcedure);

                dbConnection.Close();
                
            }
        }


        public Transaction getTransactionById(int Id)
        {
            var sql = @"SELECT *
                          FROM Transactions WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", Id);

            using (IDbConnection dbConnection = _context.Connection)
            {

                dbConnection.Open();

                var result = dbConnection.Query<Transaction>(sql, parameters).FirstOrDefault();
                return result;


            }
        }
    }
}
