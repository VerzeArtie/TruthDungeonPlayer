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
            UpdateArchiveData(TABLE_DUEL, archive_name);
        }
        public void UpdateArchivement(string archive_name)
        {
            UpdateArchiveData(TABLE_ARCHIVEMENT, archive_name);
        }

        public void CreateOwner(string name)
        {
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

        private string SelectCharacter(string table, string name)
        {
            string jsonData = string.Empty;
            using (Npgsql.NpgsqlConnection con = new NpgsqlConnection(connection))
            {
                con.Open();
                var cmd = new NpgsqlCommand(@"select to_json(" + table + ") from " + table + " where name = '" + name + "'", con);
                var dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    jsonData += dataReader[0].ToString();
                }
            }
            var dict = Json.Deserialize(jsonData) as Dictionary<string, object>;
            var str = Json.Serialize(dict);
            return str;
        }
        public string LoadCharacter(string name)
        {
            return SelectCharacter("character_data", name);
        }
        public string LoadMonster(string name)
        {
            return SelectCharacter("battlefield", name);
        }

        public void UpdateLevelUp(string name)
        {
            using (Npgsql.NpgsqlConnection con = new NpgsqlConnection(connection))
            {
                con.Open();
                var str = SelectCharacter("character_data", name);
                var dict = Json.Deserialize(str) as Dictionary<string, object>;
                byte[] bb = System.Text.Encoding.UTF8.GetBytes(str);
                int currentLevel = Convert.ToInt32(dict["level"]);
                int currentRemain = Convert.ToInt32(dict["remain_parameter"]);
                currentLevel += 1;
                currentRemain += 5;

                var command = new NpgsqlCommand(@"update character_data set level = :level, remain_parameter = :remain_parameter where name = :name", con);
                command.Parameters.Add(new NpgsqlParameter("name", DbType.String) { Value = name });
                command.Parameters.Add(new NpgsqlParameter("level", DbType.Int32) { Value = currentLevel });
                command.Parameters.Add(new NpgsqlParameter("remain_parameter", DbType.Int32) { Value = currentRemain });
                command.ExecuteNonQuery();
            }
        }
        public void UpdateGold(string name, int gold)
        {
            UpdateCharacter(name, gold, 0);
        }
        public void UpdateExp(string name, int exp)
        {
            UpdateCharacter(name, 0, exp);
        }
        public void UpdateGoldExp(string name, int gold, int exp)
        {
            UpdateCharacter(name, gold, exp);
        }
        public void UpdateCharacter(string name, int gold, int exp)
        {
            using (Npgsql.NpgsqlConnection con = new NpgsqlConnection(connection))
            {
                con.Open();
                var command = new NpgsqlCommand(@"update character_data set gold = :gold, experience = :experience where name = :name", con);
                command.Parameters.Add(new NpgsqlParameter("name", DbType.String) { Value = name });
                command.Parameters.Add(new NpgsqlParameter("gold", DbType.Int32) { Value = gold });
                command.Parameters.Add(new NpgsqlParameter("experience", DbType.Int32) { Value = exp });
                command.ExecuteNonQuery();
            }
        }


        public void CreateCharacter(string name)
        {
            List<string> nameList = new List<string>();
            List<NpgsqlDbType> typeList = new List<NpgsqlDbType>();
            List<string> valueList = new List<string>();
            nameList.Add("name"); typeList.Add(NpgsqlDbType.Varchar); valueList.Add(name);
            nameList.Add("level"); typeList.Add(NpgsqlDbType.Integer); valueList.Add("1");
            nameList.Add("strength"); typeList.Add(NpgsqlDbType.Integer); valueList.Add("10");
            nameList.Add("agility"); typeList.Add(NpgsqlDbType.Integer); valueList.Add("10");
            nameList.Add("intelligence"); typeList.Add(NpgsqlDbType.Integer); valueList.Add("10");
            nameList.Add("stamina"); typeList.Add(NpgsqlDbType.Integer); valueList.Add("10");
            nameList.Add("mind"); typeList.Add(NpgsqlDbType.Integer); valueList.Add("10");
            nameList.Add("remain_parameter"); typeList.Add(NpgsqlDbType.Integer); valueList.Add("5");
            nameList.Add("experience"); typeList.Add(NpgsqlDbType.Integer); valueList.Add("0");
            nameList.Add("gold"); typeList.Add(NpgsqlDbType.Integer); valueList.Add("1000");
            nameList.Add("current_life"); typeList.Add(NpgsqlDbType.Integer); valueList.Add("180"); // 80 + stamina(10) * 10
            nameList.Add("current_mana"); typeList.Add(NpgsqlDbType.Integer); valueList.Add("120"); // 20 + intelligence(10) * 10
            nameList.Add("current_skillpoint"); typeList.Add(NpgsqlDbType.Integer); valueList.Add("100");
            using (Npgsql.NpgsqlConnection con = new NpgsqlConnection(connection))
            {
                con.Open();
                string sqlCmd = "INSERT INTO character_data (";
                for (int ii = 0; ii < nameList.Count; ii++)
                {
                    if (ii != 0) { sqlCmd += ","; }
                    sqlCmd += nameList[ii];
                }
                sqlCmd += ") VALUES (";
                for (int ii = 0; ii < nameList.Count; ii++)
                {
                    if (ii == 0) { sqlCmd += ":" + nameList[ii]; }
                    else { sqlCmd += ", :" + nameList[ii]; }
                }
                sqlCmd += ")";
                var cmd = new NpgsqlCommand(sqlCmd, con);
                //cmd.Prepare();
                for (int ii = 0; ii < nameList.Count; ii++)
                {
                    cmd.Parameters.Add(new NpgsqlParameter(nameList[ii], typeList[ii]) { Value = valueList[ii] });
                }
                cmd.ExecuteNonQuery();
            }
        }

        public void ConstructField(List<string> list)
        {
            using (Npgsql.NpgsqlConnection con = new NpgsqlConnection(connection))
            {
                con.Open();
                var command = new NpgsqlCommand(@"CREATE TABLE battlefield (  name character varying(256),  level integer,  strength integer,  agility integer,  intelligence integer,  stamina integer,  mind integer,  current_life integer,  current_mana integer,  current_skillpoint integer);", con);
                command.ExecuteNonQuery();

                for (int jj = 0; jj < list.Count; jj++)
                {
                    List<string> nameList = new List<string>();
                    List<NpgsqlDbType> typeList = new List<NpgsqlDbType>();
                    List<string> valueList = new List<string>();
                    nameList.Add("name"); typeList.Add(NpgsqlDbType.Varchar); valueList.Add(list[jj]);
                    nameList.Add("level"); typeList.Add(NpgsqlDbType.Integer); valueList.Add("1");
                    nameList.Add("strength"); typeList.Add(NpgsqlDbType.Integer); valueList.Add("3");
                    nameList.Add("agility"); typeList.Add(NpgsqlDbType.Integer); valueList.Add("3");
                    nameList.Add("intelligence"); typeList.Add(NpgsqlDbType.Integer); valueList.Add("3");
                    nameList.Add("stamina"); typeList.Add(NpgsqlDbType.Integer); valueList.Add("3");
                    nameList.Add("mind"); typeList.Add(NpgsqlDbType.Integer); valueList.Add("3");
                    nameList.Add("current_life"); typeList.Add(NpgsqlDbType.Integer); valueList.Add("30");
                    nameList.Add("current_mana"); typeList.Add(NpgsqlDbType.Integer); valueList.Add("30");
                    nameList.Add("current_skillpoint"); typeList.Add(NpgsqlDbType.Integer); valueList.Add("100");
                    string sqlCmd = "INSERT INTO battlefield (";
                    for (int ii = 0; ii < nameList.Count; ii++)
                    {
                        if (ii != 0) { sqlCmd += ","; }
                        sqlCmd += nameList[ii];
                    }
                    sqlCmd += ") VALUES (";
                    for (int ii = 0; ii < nameList.Count; ii++)
                    {
                        if (ii == 0) { sqlCmd += ":" + nameList[ii]; }
                        else { sqlCmd += ", :" + nameList[ii]; }
                    }
                    sqlCmd += ")";
                    var cmd = new NpgsqlCommand(sqlCmd, con);
                    //cmd.Prepare();
                    for (int ii = 0; ii < nameList.Count; ii++)
                    {
                        cmd.Parameters.Add(new NpgsqlParameter(nameList[ii], typeList[ii]) { Value = valueList[ii] });
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateLife(string name, int life, bool plus)
        {
            using (Npgsql.NpgsqlConnection con = new NpgsqlConnection(connection))
            {
                con.Open();
                var str = SelectCharacter("battlefield", name);
                var dict = Json.Deserialize(str) as Dictionary<string, object>;
                byte[] bb = System.Text.Encoding.UTF8.GetBytes(str);
                int currentLife = Convert.ToInt32(dict["currentLife"]);
                currentLife -= life;
                if (currentLife < 0) { currentLife = 0; }

                var command = new NpgsqlCommand(@"update battlefield set current_life = :current_life where name = :name", con);
                command.Parameters.Add(new NpgsqlParameter("name", DbType.String) { Value = name });
                command.Parameters.Add(new NpgsqlParameter("current_life", DbType.Int32) { Value = currentLife });
                command.ExecuteNonQuery();
            }
        }

        public void DeleteAll()
        {
            using (Npgsql.NpgsqlConnection con = new NpgsqlConnection(connection))
            {
                con.Open();
                var command = new NpgsqlCommand(@"delete from character_data", con);
                command.ExecuteNonQuery();
            }
        }
    }
}
