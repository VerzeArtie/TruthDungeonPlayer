using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using UnityEngine;

namespace DungeonPlayer
{
    public class ControlSQL : MonoBehaviour
    {
        public string connection = string.Empty;

        private string TABLE_OWNER_DATA = "owner_data";
        private string TABLE_ARCHIVEMENT = "archivement";
        private string TABLE_DUEL = "duel";
        public void SetupSql()
        {
            if (GroundOne.SupportLog == false) { return; }

            try
            {
                string connection = string.Empty;
                StringBuilder sb = new StringBuilder();
                sb.Append("Server=133.242.151.26;");
                sb.Append("Port=36712;");
                sb.Append("User Id=postgres;");
                sb.Append("Password=volgan3612;");
                sb.Append("Database=postgres;");
                sb.Append("Timeout=1;");

                connection = sb.ToString();
                this.connection = connection;

            }
            catch {
                Debug.Log("SetupSql error");
            } // ログ失敗時は、そのまま進む
        }

        public string SelectOwner(string name)
        {
            if (GroundOne.SupportLog == false) { return String.Empty; }

            string table = TABLE_OWNER_DATA;
            string jsonData = String.Empty;
            try
            {
                using (Npgsql.NpgsqlConnection con = new NpgsqlConnection(connection))
                {
                    //Debug.Log("SelectOwner timeout: " + con.ConnectionTimeout);
                    con.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand(@"select to_json(" + table + ") from " + table + " where name = '" + name + "'", con);
                    var dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        jsonData += dataReader[0].ToString();
                    }
                }
            }
            catch
            {
                Debug.Log("SelectOwner error");
            } // ログ失敗時は、そのまま進む

            return jsonData;
        }

        public void UpdateOwner(string main_event, string sub_event, string current_field)
        {
            if (GroundOne.SupportLog == false) { return; }
            if (GroundOne.SQL == null) { return; }

            try
            {
                //Debug.Log("UpdateOwner(S) " + DateTime.Now);
                string name = GroundOne.WE2.Account;
                string main_level = String.Empty;

                if (GroundOne.MC != null)
                {
                    main_level = GroundOne.MC.Level.ToString();
                }

                using (Npgsql.NpgsqlConnection con = new NpgsqlConnection(connection))
                {
                    //Debug.Log("UpdateOwner timeout: " + con.ConnectionTimeout);
                    con.Open();
                    DateTime update_time = DateTime.Now;
                    string updateCommand = @"update " + TABLE_OWNER_DATA + " set update_time = :update_time";
                    if (main_event != string.Empty)
                    {
                        updateCommand += ", main_event = :main_event";
                    }
                    if (sub_event != string.Empty)
                    {
                        updateCommand += ", sub_event = :sub_event";
                    }
                    if (current_field != string.Empty)
                    {
                        updateCommand += ", current_field = :current_field";
                    }
                    if (main_level != string.Empty)
                    {
                        updateCommand += ", main_level = :main_level";
                    }
                    updateCommand += " where name = :name";

                    NpgsqlCommand command = new NpgsqlCommand(updateCommand, con);
                    command.Parameters.Add(new NpgsqlParameter("name", DbType.String) { Value = name });
                    command.Parameters.Add(new NpgsqlParameter("update_time", DbType.DateTime) { Value = update_time });
                    if (main_event != string.Empty)
                    {
                        command.Parameters.Add(new NpgsqlParameter("main_event", DbType.String) { Value = main_event });
                    }
                    if (sub_event != string.Empty)
                    {
                        command.Parameters.Add(new NpgsqlParameter("sub_event", DbType.String) { Value = sub_event });
                    }
                    if (current_field != string.Empty)
                    {
                        command.Parameters.Add(new NpgsqlParameter("current_field", DbType.String) { Value = current_field });
                    }
                    if (main_level != string.Empty)
                    {
                        command.Parameters.Add(new NpgsqlParameter("main_level", DbType.String) { Value = main_level });
                    }
                    command.ExecuteNonQuery();
                }
                //Debug.Log("UpdateOwner(E) " + DateTime.Now);
            }
            catch
            {
                Debug.Log("UpdateOwner error... " + DateTime.Now);
            } // ログ失敗時は、そのまま進む
        }

        private void UpdateArchiveData(string table, string archive_name)
        {
            if (GroundOne.SupportLog == false) { return; }
            if (GroundOne.SQL == null) { return; }

            try
            {
                string guid = String.Empty;

                using (Npgsql.NpgsqlConnection con = new NpgsqlConnection(connection))
                {
                    Debug.Log("UpdateArchiveData timeout: " + con.ConnectionTimeout);
                    con.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand(@"select guid from " + TABLE_OWNER_DATA + " where name = '" + GroundOne.WE2.Account + "'", con);
                    var dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        guid += dataReader[0].ToString();
                    }
                }

                if (guid == String.Empty)
                {
                    return;
                }

                string count = String.Empty;
                using (Npgsql.NpgsqlConnection con = new NpgsqlConnection(connection))
                {
                    con.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand(@"select count(*) from " + table + " where guid = '" + guid + "'", con);
                    var dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        count += dataReader[0].ToString();
                    }
                }

                DateTime update_time = DateTime.Now;
                if (count.ToString() == "0")
                {
                    using (Npgsql.NpgsqlConnection con = new NpgsqlConnection(connection))
                    {
                        con.Open();
                        string sqlCmd = "INSERT INTO " + table + " ( guid, " + archive_name + " ) VALUES ( :guid, :" + archive_name + " )";
                        var cmd = new NpgsqlCommand(sqlCmd, con);
                        //cmd.Prepare();
                        cmd.Parameters.Add(new NpgsqlParameter("guid", NpgsqlDbType.Varchar) { Value = guid });
                        cmd.Parameters.Add(new NpgsqlParameter(archive_name, NpgsqlDbType.Timestamp) { Value = update_time });
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    string currentValue = String.Empty;
                    using (Npgsql.NpgsqlConnection con = new NpgsqlConnection(connection))
                    {
                        con.Open();
                        NpgsqlCommand cmd = new NpgsqlCommand(@"select " + archive_name + " from " + table + " where guid = '" + guid + "'", con);
                        var dataReader = cmd.ExecuteReader();
                        while (dataReader.Read())
                        {
                            currentValue += dataReader[0].ToString();
                        }
                    }

                    if (currentValue == String.Empty)
                    {
                        using (Npgsql.NpgsqlConnection con = new NpgsqlConnection(connection))
                        {
                            con.Open();
                            string updateCommand = @"update " + table + " set " + archive_name + " = :" + archive_name + " where guid = :guid";
                            NpgsqlCommand command = new NpgsqlCommand(updateCommand, con);
                            command.Parameters.Add(new NpgsqlParameter(archive_name, DbType.DateTime) { Value = update_time });
                            command.Parameters.Add(new NpgsqlParameter("guid", DbType.String) { Value = guid });
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch
            {
                Debug.Log("UpdateArchiveData error");
            } // ログ失敗時は、そのまま進む
        }

        public void UpdateDuel(string archive_name)
        {
            UpdateArchiveData(TABLE_DUEL, archive_name);
        }
        public void UpdateArchivement(string archive_name)
        {
            UpdateArchiveData(TABLE_ARCHIVEMENT, archive_name);
        }

        public void CreateOwner(string name)
        {
            if (GroundOne.SupportLog == false) { return; }

            try
            {
                System.Guid guid = System.Guid.NewGuid();
                DateTime create_time = DateTime.Now;
                using (Npgsql.NpgsqlConnection con = new NpgsqlConnection(connection))
                {
                    Debug.Log("CreateOwner timeout: " + con.ConnectionTimeout);
                    con.Open();
                    string sqlCmd = "INSERT INTO " + TABLE_OWNER_DATA + " ( name, guid, create_time ) VALUES ( :name, :guid, :create_time )";
                    var cmd = new NpgsqlCommand(sqlCmd, con);
                    //cmd.Prepare();
                    cmd.Parameters.Add(new NpgsqlParameter("name", NpgsqlDbType.Varchar) { Value = name });
                    cmd.Parameters.Add(new NpgsqlParameter("guid", NpgsqlDbType.Varchar) { Value = guid });
                    cmd.Parameters.Add(new NpgsqlParameter("create_time", NpgsqlDbType.Timestamp) { Value = create_time });
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                Debug.Log("CreateOwner error");
            } // ログ失敗時は、そのまま進む
        }

        public bool ExistOwnerName(string name)
        {
            // if (GroundOne.SupportLog == false) { return false; }

            try
            {
                string existName = String.Empty;

                using (Npgsql.NpgsqlConnection con = new NpgsqlConnection(connection))
                {
                    con.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand(@"select name from " + TABLE_OWNER_DATA + " where name = '" + name + "'", con);
                    var dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        existName += dataReader[0].ToString();
                    }
                }

                if (existName == name)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                return false;
            } // 取得失敗時は名前がぶつかっている可能性があるが、ひとまず通しとする。
        }
    }
}
