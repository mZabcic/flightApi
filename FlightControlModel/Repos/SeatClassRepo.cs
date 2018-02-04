using FlightControlApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FlightControlModel.Repos
{
    public class SeatClassRepo : CRUD
    {
        public SeatClassRepo(IDbConnection conn, IDbCommand comm) : base(conn, comm)
        {
        }

        public SeatClass Get(int id)
        {
            SeatClass a;

            _comm.CommandText = "SELECT * " +
                                "FROM SeatClass a " +
                                "WHERE a.Id = @id;";

            _comm.AddParameter("@id", id);

            _conn.Open();

            try
            {
                using (IDataReader rdr = _comm.ExecuteReader())
                {
                    rdr.Read();

                    a = new SeatClass()
                    {
                        Id = Convert.ToInt32(rdr["id"]),
                        Name = Convert.ToString(rdr["Name"]),
                        PriceMultipler = Convert.ToInt32(rdr["PriceMultipler"])
                    };

                }

                _conn.Close();

                return a;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _comm.Parameters.Clear();
                _conn.Close();
            }


        }
        public List<SeatClass> GetAll()
        {
            List<int> allSeatClasssIDs = new List<int>();
            List<SeatClass> allSeatClasss = new List<SeatClass>();

            _comm.CommandText = "SELECT * " +
                                "FROM SeatClass ";

            _conn.Open();

            try
            {
                using (IDataReader rdr = _comm.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        allSeatClasssIDs.Add(Convert.ToInt32(rdr["Id"]));

                        allSeatClasss.Add(new SeatClass()
                        {
                            Id = Convert.ToInt32(rdr["id"]),
                            Name = Convert.ToString(rdr["Name"]),
                            PriceMultipler = Convert.ToInt32(rdr["PriceMultipler"])
                        });
                    }
                }

                _conn.Close();

                //foreach (int id in allSeatClasssIDs)
                //{
                //    allSeatClasss.Add(this.Get(id));
                //}

                return allSeatClasss;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conn.Close();
            }
        }

    }
}
