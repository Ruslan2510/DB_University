using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    public class Layer
    {
        Brokerage db = new Brokerage();
        public List<CLIENT> GetClients()
        {
            List<CLIENT> result = new List<CLIENT>();
            try
            {
                var clients = db.Database.SqlQuery<CLIENT>("SELECT * FROM Client");


                foreach (var item in clients)
                {
                    result.Add(new CLIENT
                    {
                        client_id = item.client_id,
                        client_surname = item.client_surname,
                        client_patronymic = item.client_patronymic,
                        client_name = item.client_name,
                        client_email = item.client_email,
                        client_fortune = item.client_fortune,
                        client_tel = item.client_tel
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public List<BROKER> GetBrokers()
        {
            List<BROKER> result = new List<BROKER>();
            try
            {
                var brokers = db.Database.SqlQuery<BROKER>("SELECT * FROM Broker");

                foreach (var item in brokers)
                {
                    result.Add(new BROKER
                    {
                        broker_id = item.broker_id,
                        broker_surname = item.broker_surname,
                        broker_patronymic = item.broker_patronymic,
                        broker_name = item.broker_name,
                        broker_email = item.broker_email,
                        broker_experience = item.broker_experience,
                        broker_tel = item.broker_tel
                    });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }


        public List<ORDERR> GetOrders()
        {
            List<ORDERR> result = new List<ORDERR>();
            try
            {
                var orders = db.Database.SqlQuery<ORDERR>("SELECT * FROM Orderr");

                foreach (var item in orders)
                {
                    result.Add(new ORDERR
                    {
                        order_id = item.order_id,
                        order_comment = item.order_comment,
                        order_status = item.order_status,
                        broker_id = item.broker_id,
                        client_id = item.client_id
                    });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public List<BIDDING> GetBidding()
        {
            List<BIDDING> result = new List<BIDDING>();
            try
            {
                var biddings = db.Database.SqlQuery<BIDDING>("SELECT * FROM Bidding");

                foreach (var item in biddings)
                {
                    result.Add(new BIDDING
                    {
                        bidding_id = item.bidding_id,
                        client_id = item.client_id,
                        broker_id = item.broker_id,
                        order_id = item.order_id,
                        bidding_date = item.bidding_date
                    });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public List<DEAL> GetDeal()
        {
            List<DEAL> result = new List<DEAL>();
            try
            {
                var deals = db.Database.SqlQuery<DEAL>("SELECT * FROM Deal");

                foreach (var item in deals)
                {
                    result.Add(new DEAL
                    {
                        deal_id = item.deal_id,
                        order_id = item.order_id,
                        bidding_id = item.bidding_id,
                        broker_id = item.broker_id,
                        client_id = item.client_id,
                        deal_customer = item.deal_customer,
                        deal_date = item.deal_date,
                        deal_type = item.deal_type,
                        securities_amount = item.securities_amount,
                        securities_id = item.securities_id,
                    });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public List<SECURITy> GetSecurities()
        {
            List<SECURITy> result = new List<SECURITy>();
            try
            {
                var orders = db.Database.SqlQuery<SECURITy>("SELECT * FROM SECURITies");

                foreach (var item in orders)
                {
                    result.Add(new SECURITy
                    {
                        securities_id = item.securities_id,
                        securities_amount = item.securities_amount,
                        securities_name = item.securities_name,
                        securities_owner = item.securities_owner,
                        securities_purchase_rate = item.securities_purchase_rate,
                        securities_sales_rate = item.securities_sales_rate
                    });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public List<PROFIT> GetProfit()
        {
            List<PROFIT> result = new List<PROFIT>();
            try
            {
                var profit = db.Database.SqlQuery<PROFIT>("SELECT * FROM PROFIT");

                foreach (var item in profit)
                {
                    result.Add(new PROFIT
                    {
                        profit_id = item.profit_id,
                        securities_id = item.securities_id,
                        profit_amount = item.profit_amount,
                        bidding_id = item.bidding_id,
                        broker_id = item.broker_id,
                        client_id = item.client_id,
                        deal_id = item.deal_id,
                        order_id = item.order_id
                    });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }


        public string GetBrokerageIncome(string lowLimit, string upperLimit)
        {
            string result = "";
            try 
            {
                result = db.Database.SqlQuery<string>(
                    string.Format($"SELECT dbo.GetBrokerageCompanyIncome('{lowLimit}', '{upperLimit}')")).Single();

                if (string.IsNullOrEmpty(result))
                {
                    return "Прибыли за данный период нет.";
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return result;
        }

        public List<Income> GetBurseIncome(string lowLimit, string upperLimit)
        {
            List<Income> burseIncomes = null;
            try
            {
                burseIncomes = new List<Income>();
                var result = db.Database.SqlQuery<Income>(
                    string.Format($"SELECT * FROM dbo.GetBurseIncome('{lowLimit}', '{upperLimit}')"));

                foreach (var item in result)
                {
                    burseIncomes.Add(new Income
                    {
                        SecuritiesName = item.SecuritiesName,
                        Profit = item.Profit
                    });
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return burseIncomes;
        }

        public List<Securities> GetExchangeSecurities(int brokerId)
        {
            List<Securities> securities = null;
            try
            {
                securities = new List<Securities>();
                var result = db.Database.SqlQuery<Securities>(
                    string.Format($"SELECT * FROM GetExchangeSecurities({brokerId})"));

                foreach (var item in result)
                {
                    securities.Add(new Securities
                    {
                        resultTb_securities_name = item.resultTb_securities_name
                    });
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return securities;
        }

        public List<Income> GetMostProfitableSecurities()
        {
            List<Income> mostProfitable = null;
            try
            {
                mostProfitable = new List<Income>();
                var result = db.Database.SqlQuery<Income>(
                    string.Format($"SELECT * FROM dbo.GetMostProfitableSecurities()"));

                foreach (var item in result)
                {
                    mostProfitable.Add(new Income
                    {
                        SecuritiesName = item.SecuritiesName,
                        Profit = item.Profit
                    });
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return mostProfitable;
        }

        public List<Paper> GetSecuritiesAmountOnBurse(string date)
        {
            List<Paper> securitiesOnBurse = null;
            try
            {
                securitiesOnBurse = new List<Paper>();
                var result = db.Database.SqlQuery<Paper>(
                    string.Format($"SELECT * FROM GetSecuritiesAmountOnBurse('{date}')"));

                foreach (var item in result)
                {
                    securitiesOnBurse.Add(new Paper
                    {
                        securitiesName = item.securitiesName,
                        securitiesAmount = item.securitiesAmount
                    });
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return securitiesOnBurse;
        }

        public void BuyOrSellPaper(int biddingId, int securitiesId, 
            int securitiesAmount, string dealDate, string dealCustomer, string dealType)
        {
            try
            {
                db.Database.SqlQuery<NewDeal>(
                    string.Format($"EXEC SecuritiesOperations {biddingId}, {securitiesId}, {securitiesAmount}," +
                    $" '{dealDate}', '{dealCustomer}', '{dealType}'"));
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
