using FlightControlApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FlightControlModel.Repos
{
    public class StoreRepo : CRUD
    {
        public StoreRepo(IDbConnection conn, IDbCommand comm) : base(conn, comm)
        {
        }

        public Store Get(int id)
        {
            Store a;

            _comm.CommandText = "SELECT * " +
                                "FROM Store a " +
                                "WHERE a.Id = @id;";

            _comm.AddParameter("@id", id);

            _conn.Open();

            try
            {
                using (IDataReader rdr = _comm.ExecuteReader())
                {
                    rdr.Read();

                    a = new Store()
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        Name = Convert.ToString(rdr["Name"]),
                        Address = Convert.ToString(rdr["Address"]),
                        ZipCode = Convert.ToString(rdr["ZipCode"]),
                        CountryId = Convert.ToInt32(rdr["CountryId"])

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
        public List<Store> GetAll()
        {
            List<int> allStoresIDs = new List<int>();
            List<Store> allStores = new List<Store>();

            _comm.CommandText = "SELECT * " +
                                "FROM Store ";

            _conn.Open();

            try
            {
                using (IDataReader rdr = _comm.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        allStoresIDs.Add(Convert.ToInt32(rdr["Id"]));

                        allStores.Add(new Store()
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            Name = Convert.ToString(rdr["Name"]),
                            Address = Convert.ToString(rdr["Address"]),
                            ZipCode = Convert.ToString(rdr["ZipCode"]),
                            CountryId = Convert.ToInt32(rdr["CountryId"])

                        });
                    }
                }

                _conn.Close();

                //foreach (int id in allStoresIDs)
                //{
                //    allStores.Add(this.Get(id));
                //}

                return allStores;
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
        public int Insert(Store p)
        {
            _comm.CommandText = "INSERT INTO Store " +
                                "(Name, Address, ZipCode, CountryId) " +
                                "VALUES " +
                                "(@name, @address, @zip, @countryid);" +
                                "SELECT SCOPE_IDENTITY();";

            _comm.AddParameter("@id", p.Id);
            _comm.AddParameter("@name", p.Name);
            _comm.AddParameter("@address", p.Address);
            _comm.AddParameter("@zip", p.ZipCode);
            _comm.AddParameter("@countryid", p.CountryId);

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
        public bool Update(int id, Store p)
        {
            _comm.CommandText = "UPDATE Store " +
                                "SET Name = @name, Address = @address, ZipCode = @zip, CountryId = @countryid " +
                                "WHERE Id = @id ";

            _comm.AddParameter("@id", p.Id);
            _comm.AddParameter("@name", p.Name);
            _comm.AddParameter("@address", p.Address);
            _comm.AddParameter("@zip", p.ZipCode);
            _comm.AddParameter("@countryid", p.CountryId);
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
            _comm.CommandText = "DELETE FROM Store " +
                                "WHERE Store.Id = @id;";

            _comm.AddParameter("@id", id);

            _conn.Open();

            _trans = _conn.BeginTransaction();

            _comm.Transaction = _trans;

            try
            {
                _comm.ExecuteNonQuery();
                _trans.Commit();
            }
            catch (Exception ex)
            {
                _trans.Rollback();
                throw ex;
            }
            finally
            {
                _comm.Transaction = null;
                _comm.Parameters.Clear();
                _conn.Close();
            }

            return true;
        }
    }
}

