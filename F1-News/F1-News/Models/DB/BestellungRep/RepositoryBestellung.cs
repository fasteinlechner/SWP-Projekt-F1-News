using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace F1_News.Models.DB.BestellungRep {
    public class RepositoryBestellung : IRepositoryBestellung {

        private string connectionString = DB_Connect.connectionStr;
        private DbConnection conn;

        public async Task ConnectAsync() {
            if(this.conn == null) {
                this.conn = new MySqlConnection(this.connectionString);
            }
            if(this.conn.State != ConnectionState.Open) {
                await this.conn.OpenAsync();
            }
        }

        public async Task DisconnectAsync() {
            if(this.conn !=null && this.conn.State == ConnectionState.Open) {
                await this.conn.CloseAsync();
            }
        }

        public async Task<List<Bestellung>> GetAllBestellungAsync() {
            throw new NotImplementedException();
        }

        public async Task<List<Bestellung>> GetBestellungbyBNrAsync(int bNr,int uId) {
            List<Bestellung> bestellungen = new List<Bestellung>();
            if(this.conn?.State == ConnectionState.Open) {
                DbCommand com = this.conn.CreateCommand();
                com.CommandText = "select * from bestellungen where bestellNr = @bestellNr and FK_user_id = @userId";

                DbParameter paramBestell = com.CreateParameter();
                paramBestell.ParameterName = "bestellNr";
                paramBestell.DbType = DbType.Int32;
                paramBestell.Value = bNr;

                DbParameter paramUId = com.CreateParameter();
                paramUId.ParameterName = "userId";
                paramUId.DbType = DbType.Int32;
                paramUId.Value = uId;

                com.Parameters.Add(paramBestell);
                com.Parameters.Add(paramUId);

                using (DbDataReader reader = await com.ExecuteReaderAsync()) {
                    while (await reader.ReadAsync()) {
                        bestellungen.Add(new Bestellung() {
                            BestellID = Convert.ToInt32(reader["idbestellungen"]),
                            ArticleID = Convert.ToInt32(reader["FK_idarticle"]),
                            UserID = Convert.ToInt32(reader["FK_user_id"]),
                            Anzahl = Convert.ToInt32(reader["anz"]),
                            BestellNummer = Convert.ToInt32(reader["bestellNr"]),
                        });
                    }
                }
            }
            return bestellungen;
        }

        public async Task<List<Bestellung>> GetBestellungbyUIdAsync(int uId) {
            List<Bestellung> bestellungen = new List<Bestellung>();
            if (this.conn?.State == ConnectionState.Open) {
                DbCommand com = this.conn.CreateCommand();
                com.CommandText = "select * from bestellungen where FK_user_id = @userId";

                DbParameter paramUser = com.CreateParameter();
                paramUser.ParameterName = "userId";
                paramUser.DbType = DbType.Int32;
                paramUser.Value = uId;

                com.Parameters.Add(paramUser);

                using (DbDataReader reader = await com.ExecuteReaderAsync()) {
                    while (await reader.ReadAsync()) {
                        bestellungen.Add(new Bestellung() {
                            BestellID = Convert.ToInt32(reader["idbestellungen"]),
                            ArticleID = Convert.ToInt32(reader["FK_idarticle"]),
                            UserID = Convert.ToInt32(reader["FK_user_id"]),
                            Anzahl = Convert.ToInt32(reader["anz"]),
                            BestellNummer = Convert.ToInt32(reader["bestellNr"]),
                        });
                    }
                }
            }
            return bestellungen;
        }

        public async Task<bool> InsertAsync(Bestellung bestellung) {
            if(this.conn?.State == ConnectionState.Open) {
                DbCommand com = this.conn.CreateCommand();
                com.CommandText = "insert into bestellungen values(null, @aId, @uId, @anz, @bNr);";

                DbParameter paramAId = com.CreateParameter();
                paramAId.ParameterName = "aId";
                paramAId.DbType = DbType.Int32;
                paramAId.Value = bestellung.ArticleID;

                DbParameter paramUId = com.CreateParameter();
                paramUId.ParameterName = "uId";
                paramUId.DbType = DbType.Int32;
                paramUId.Value = bestellung.UserID;

                DbParameter paramAnz = com.CreateParameter();
                paramAnz.ParameterName = "anz";
                paramAnz.DbType = DbType.Int32;
                paramAnz.Value = bestellung.Anzahl;

                DbParameter paramBNr = com.CreateParameter();
                paramBNr.ParameterName = "bNr";
                paramBNr.DbType = DbType.Int32;
                paramBNr.Value = bestellung.BestellNummer;

                com.Parameters.Add(paramAId);
                com.Parameters.Add(paramUId);
                com.Parameters.Add(paramAnz);
                com.Parameters.Add(paramBNr);

                return await com.ExecuteNonQueryAsync() == 1;
            }
            return false;
        }
    }
}
