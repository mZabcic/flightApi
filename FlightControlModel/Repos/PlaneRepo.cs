using FlightControlApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FlightControlModel.Repos
{
    public class PlaneRepo : CRUD
    {
        public PlaneRepo(IDbConnection conn, IDbCommand comm) : base(conn, comm)
        {
        }

        public Plane Get(int id)
        {
            Plane p;

            _comm.CommandText = "SELECT * " +
                                "FROM Plane p " +
                                "WHERE p.Id = @id;";

            _comm.AddParameter("@id", id);

            _conn.Open();

            try
            {
                using (IDataReader rdr = _comm.ExecuteReader())
                {
                    rdr.Read();

                    p = new Plane()
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        Model = Convert.ToString(rdr["Model"]),
                        SerialNumber = Convert.ToString(rdr["SerialNumber"]),
                        EconomyCapacity = Convert.ToInt32(rdr["EconomyCapacity"]),
                        BusinessCapacity = Convert.ToInt32(rdr["BusinessCapacity"]),
                        FirstClassCapacity = Convert.ToInt32(rdr["FirstClassCapacity"]),
                        Active = Convert.ToInt32(rdr["Active"])

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
        public List<Plane> GetAll()
        {
            List<int> allPlanesIDs = new List<int>();
            List<Plane> allPlanes = new List<Plane>();

            _comm.CommandText = "SELECT * " +
                                "FROM Plane ";

            _conn.Open();

            try
            {
                using (IDataReader rdr = _comm.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        allPlanesIDs.Add(Convert.ToInt32(rdr["Id"]));

                        allPlanes.Add(new Plane()
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            Model = Convert.ToString(rdr["Model"]),
                            SerialNumber = Convert.ToString(rdr["SerialNumber"]),
                            EconomyCapacity = Convert.ToInt32(rdr["EconomyCapacity"]),
                            BusinessCapacity = Convert.ToInt32(rdr["BusinessCapacity"]),
                            FirstClassCapacity = Convert.ToInt32(rdr["FirstClassCapacity"]),
                            Active = Convert.ToInt32(rdr["Active"])

                        });
                    }
                }

                _conn.Close();

                //foreach (int id in allPlanesIDs)
                //{
                //    allPlanes.Add(this.Get(id));
                //}

                return allPlanes;
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
        public int Insert(Plane p)
        {
            _comm.CommandText = "INSERT INTO Plane " +
                                "(Model, SerialNumber, EconomyCapacity, BusinessCapacity, FirstClassCapacity) " +
                                "VALUES " +
                                "(@model, @serialnumber, @economy, @business, @firstclass);" +
                                "SELECT SCOPE_IDENTITY();";

            _comm.AddParameter("@id", p.Id);
            _comm.AddParameter("@serialnumber", p.SerialNumber);
            _comm.AddParameter("@economy", p.EconomyCapacity);
            _comm.AddParameter("@business", p.BusinessCapacity);
            _comm.AddParameter("@firstclass", p.FirstClassCapacity);
            _comm.AddParameter("@model", p.Model);

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
        public bool Update(int id, Plane p)
        {
            _comm.CommandText = "UPDATE Plane " +
                                "SET Model = @model, SerialNumber = @serialnumber, EconomyCapacity = @economy, BusinessCapacity = @business, FirstClassCapacity = @firstclass, Active = @active " +
                                "WHERE Id = @id ";

            _comm.AddParameter("@id", id);
            _comm.AddParameter("@model", p.Model);
            _comm.AddParameter("@serialnumber", p.SerialNumber);
            _comm.AddParameter("@economy", p.EconomyCapacity);
            _comm.AddParameter("@business", p.BusinessCapacity);
            _comm.AddParameter("@firstclass", p.FirstClassCapacity);
            _comm.AddParameter("@active", p.Active);

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
            _comm.CommandText = "UPDATE Plane " +
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
