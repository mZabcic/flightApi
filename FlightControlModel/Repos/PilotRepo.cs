using FlightControlApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FlightControlModel.Repos
{
    public class PilotRepo : CRUD
    {
        public PilotRepo(IDbConnection conn, IDbCommand comm) : base(conn, comm)
        {
        }

        public  Pilot Get(int id)
        {
            Pilot p;

            _comm.CommandText = "SELECT * " +
                                "FROM Pilot p " +
                                "WHERE p.Id = @id;";

            _comm.AddParameter("@id", id);

            _conn.Open();

            try
            {
                using (IDataReader rdr = _comm.ExecuteReader())
                {
                    rdr.Read();

                    p = new Pilot()
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        FirstName = Convert.ToString(rdr["FirstName"]),
                        LastName = Convert.ToString(rdr["LastName"]),
                        BirthDay = Convert.ToDateTime(rdr["BirthDay"])
                   
                    };

                }

                _conn.Close();

                return p;
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
        public List<Pilot> GetAll()
        {
            List<int> allPilotsIDs = new List<int>();
            List<Pilot> allPilots = new List<Pilot>();

            _comm.CommandText = "SELECT * " +
                                "FROM Pilot ";

            _conn.Open();

            try
            {
                using (IDataReader rdr = _comm.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        allPilotsIDs.Add(Convert.ToInt32(rdr["Id"]));

                        allPilots.Add(new Pilot()
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            FirstName = Convert.ToString(rdr["FirstName"]),
                            LastName = Convert.ToString(rdr["LastName"]),
                            BirthDay = Convert.ToDateTime(rdr["BirthDay"])

                        });
                    }
                }

                _conn.Close();

                //foreach (int id in allPilotsIDs)
                //{
                //    allPilots.Add(this.Get(id));
                //}

                return allPilots;
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
        public int Insert(Pilot p)
        {
            _comm.CommandText = "INSERT INTO Pilot " +
                                "(FirstName, LastName, BirthDay) " +
                                "VALUES " +
                                "(@firstname, @lastname, @birthday);";

            _comm.AddParameter("@id", p.Id);
            _comm.AddParameter("@firstname", p.FirstName);
            _comm.AddParameter("@lastname", p.LastName);
            _comm.AddParameter("@birthday", p.BirthDay);

            _conn.Open();

            try
            {
                int recordID = Convert.ToInt32(_comm.ExecuteScalar());

                _comm.Parameters.Clear();
                _conn.Close();

                return recordID;
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
        public bool Update(int id, Pilot p)
        {
            _comm.CommandText = "UPDATE Pilot " +
                                "SET FirstName = @firstname, LastName = @lastname " +
                                "WHERE Id = @id ";

            _comm.AddParameter("@id", p.Id);
            _comm.AddParameter("@firstname", p.FirstName);
            _comm.AddParameter("@lastname", p.LastName);
            _comm.AddParameter("@birthday", p.BirthDay);

            _conn.Open();

            try
            {
                _comm.ExecuteNonQuery();
                
                _comm.Parameters.Clear();
                _conn.Close();

                return true;
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
        public bool Delete(int id)
        {
            _comm.CommandText = "UPDATE Pilot " +
                                 "SET Active = 0 " +
                                 "WHERE Id = @id ";

            _comm.AddParameter("@id", id);

            _conn.Open();

            try
            {
                _comm.ExecuteNonQuery();

                _comm.Parameters.Clear();
                _conn.Close();

                return true;
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
    }
}
