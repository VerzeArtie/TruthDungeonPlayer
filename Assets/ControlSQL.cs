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
        // debug
        private bool DebugDisable = false;

        public string connection = string.Empty;

        private string TABLE_OWNER_DATA = "owner_data";
        private string TABLE_ARCHIVEMENT = "archivement";
        private string TABLE_DUEL = "duel";
        public void SetupSql()
        {
            if (DebugDisable) return;

            try
            {
                string connection = string.Empty;
                StringBuilder sb = new StringBuilder();
                sb.Append("Server=133.242.151.26;");
                sb.Append("Port=5432;");
                sb.Append("User Id=postgres;");
                sb.Append("Password=postgres;");
                sb.Append("Database=postgres;");

                connection = sb.ToString();
                this.connection = connection;
            }
            catch { } // ログ失敗時は、そのまま進む
        }

        public string SelectOwner(string name)
        {
            if (DebugDisable) { return String.Empty; }

            string table = TABLE_OWNER_DATA;
            string jsonData = String.Empty;
            try
            {
                using (Npgsql.NpgsqlConnection con = new NpgsqlConnection(connection))
                {
                    con.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand(@"select to_json(" + table + ") from " + table + " where name = '" + name + "'", con);
                    var dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        jsonData += dataReader[0].ToString();
                    }
                }
            }
            catch { } // ログ失敗時は、そのまま進む

            return jsonData;
        }

        public void UpdateOwner(string main_event, string sub_event, string current_field)
        {
            if (DebugDisable) return;
            if (GroundOne.SQL == null) { return; }

            try
            {
                string name = GroundOne.WE2.Account;
                using (Npgsql.NpgsqlConnection con = new NpgsqlConnection(connection))
                {
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
                    command.ExecuteNonQuery();
                }
            }
            catch { } // ログ失敗時は、そのまま進む
        }

        private void UpdateArchiveData(string table, string archive_name)
        {
            if (DebugDisable) return;
            try
            {
                if (GroundOne.SQL == null) { return; }

                string guid = String.Empty;

                using (Npgsql.NpgsqlConnection con = new NpgsqlConnection(connection))
                {
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
            catch { } // ログ失敗時は、そのまま進む
        }

        public void UpdateDuel(string archive_name)
        {
            if (DebugDisable) return;
            UpdateArchiveData(TABLE_DUEL, archive_name);
        }
        public void UpdateArchivement(string archive_name)
        {
            if (DebugDisable) return;
            UpdateArchiveData(TABLE_ARCHIVEMENT, archive_name);
        }

        public void CreateOwner(string name)
        {
            if (DebugDisable) return;
            try
            {
                System.Guid guid = System.Guid.NewGuid();
                DateTime create_time = DateTime.Now;
                using (Npgsql.NpgsqlConnection con = new NpgsqlConnection(connection))
                {
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
            catch { } // ログ失敗時は、そのまま進む
        }
    }
}
