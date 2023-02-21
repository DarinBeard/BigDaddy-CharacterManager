using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using static CitizenFX.Core.Native.API;
using BigDaddy_CharacterManager_Models;


namespace BigDaddy_CharacterManager.Server
{
    public class ServerMain : BaseScript
    {
        public ServerMain()
        {

			EventHandlers["BigDaddy-CharacterManager:GetCharacters"] += new Action<Player>(GetCharacters);
			EventHandlers["BigDaddy-CharacterManager:SaveNewCharacter"] += new Action<Player, string, string>(SaveNewCharacter);
			EventHandlers["BigDaddy-CharacterManager:SaveCharacter"] += new Action<Player, int, string>(SaveCharacter);
			EventHandlers["BigDaddy-CharacterManager:SaveCharacterName"] += new Action<Player, int, string>(SaveCharacterName);
			EventHandlers["BigDaddy-CharacterManager:DeleteCharacter"] += new Action<Player, int>(DeleteCharacter);

		}

		public async void DeleteCharacter([FromSource] Player source, int id)
		{
			string connStr = GetConvar("mysql_connection_string", "");
			MySqlConnection conn = new MySqlConnection(connStr);
			try
			{
				await conn.OpenAsync();

				string q = "DELETE FROM `bd_character` WHERE `id` = @id";
				MySqlCommand command = new MySqlCommand(q, conn);
				command.Parameters.AddWithValue("@id", id);

				long rowsNo = (long)await command.ExecuteNonQueryAsync();

				GetCharacters(source);
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"BigDaddy-CharacterManager: >> {ex} ");
			}
			finally
			{
				conn.Close();
			}

		}
		public async void SaveNewCharacter([FromSource] Player source, string name, string appearance)
		{
			int id = 0;

			string connStr = GetConvar("mysql_connection_string", "");
			MySqlConnection conn = new MySqlConnection(connStr);
			string lic = GetPlayerIdentifier(source.Handle, 0);
			string discord = "";
			for (int i = 0; i < GetNumPlayerIdentifiers(source.Handle); i++)
			{
				string l = GetPlayerIdentifier(source.Handle, i);
				if (l.ToLower().Contains("discord:"))
				{
					discord = l.ToLower().Replace("discord:", "");
				} 
			}
			try
			{
				await conn.OpenAsync();

				//string q = "insert into message (player_name, message_text) values (@player_name, @message)";
				//string q = "SELECT * FROM bd_character WHERE steam = @steam";
				string q = "INSERT INTO `bd_character` (`steam`, `discord`,  `name`, `data`) VALUES (@steam, @discord, @name, @data)";
				MySqlCommand command = new MySqlCommand(q, conn);
				command.Parameters.AddWithValue("@steam", lic);
				command.Parameters.AddWithValue("@discord", discord);
				command.Parameters.AddWithValue("@name", name);
				command.Parameters.AddWithValue("@data", appearance);

				long rowsNo = (long)await command.ExecuteNonQueryAsync();

				GetCharacters(source);
				source.TriggerEvent("BigDaddy-CharacterManager:SetNewCharacter");
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"BigDaddy-CharacterManager: >> {ex} ");
			}
			finally
			{
				conn.Close();
			}
		}

		public async void SaveCharacter([FromSource] Player source, int id, string appearance)
		{
			string connStr = GetConvar("mysql_connection_string", "");
			MySqlConnection conn = new MySqlConnection(connStr);
			try
			{
				await conn.OpenAsync();

				string q = "UPDATE `bd_character` SET `data` = @data WHERE `id` = @id";
				MySqlCommand command = new MySqlCommand(q, conn);
				command.Parameters.AddWithValue("@id", id);
				command.Parameters.AddWithValue("@data", appearance);

				long rowsNo = (long)await command.ExecuteNonQueryAsync();

				GetCharacters(source);
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"BigDaddy-CharacterManager: >> {ex} ");
			}
			finally
			{
				conn.Close();
			}
		}

		public async void SaveCharacterName([FromSource] Player source, int id, string name)
		{
			string connStr = GetConvar("mysql_connection_string", "");
			MySqlConnection conn = new MySqlConnection(connStr);
			try
			{
				await conn.OpenAsync();

				string q = "UPDATE `bd_character` SET `name` = @name WHERE `id` = @id";
				MySqlCommand command = new MySqlCommand(q, conn);
				command.Parameters.AddWithValue("@id", id);
				command.Parameters.AddWithValue("@name", name);

				long rowsNo = (long)await command.ExecuteNonQueryAsync();

				GetCharacters(source);
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"BigDaddy-CharacterManager: >> {ex} ");
			}
			finally
			{
				conn.Close();
			}
		}

		public async void GetCharacters([FromSource] Player source)
		{
			string connStr = GetConvar("mysql_connection_string", "");
			MySqlConnection conn = new MySqlConnection(connStr);
			string lic = GetPlayerIdentifier(source.Handle, 0);

			List<Character> chars = new List<Character>();
			try
			{
				await conn.OpenAsync();
				string q = "SELECT * FROM `bd_character` WHERE `steam` = @steam ORDER BY name";
				MySqlCommand command = new MySqlCommand(q, conn);
				command.Parameters.AddWithValue("@steam", lic);

				//Debug.WriteLine($"BigDaddy-CharacterManager:Getting Characters for: {lic}");

				var reader = command.ExecuteReader();
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						var id = reader[0];
						
						if (id != null)
						{
							try
							{
								//Debug.WriteLine($"id: {reader[0]} name: {reader[3]}");
							} catch { }
							Character _char = new Character();
							try
							{
								_char.id = int.Parse(id.ToString());
							}
							catch (Exception ex) { Debug.WriteLine($"id: {ex.Message} {ex.StackTrace}"); }
							try
							{
								_char.name = reader[3].ToString();
							}
							catch (Exception ex) { Debug.WriteLine($"name: {ex.Message} {ex.StackTrace}"); }
							try
							{
								_char.steam = reader[1].ToString();
							}
							catch (Exception ex) { Debug.WriteLine($"steam: {ex.Message} {ex.StackTrace}"); }
							try
							{
								_char.discord = reader[2].ToString();
							}
							catch (Exception ex) { Debug.WriteLine($"discord: {ex.Message} {ex.StackTrace}"); }
							try
							{
								_char.data = reader[4].ToString();
							}
							catch (Exception ex) { Debug.WriteLine($"data: {ex.Message} {ex.StackTrace}"); }
							chars.Add(_char);
						}
					}
				}
				Debug.WriteLine($"BigDaddy-CharacterManager: Retrieved {chars.Count} characters.");
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"BigDaddy-CharacterManager: >> {ex.Message} {ex.StackTrace} ");
			} finally
			{
				conn.Close();
			}

			source.TriggerEvent("BigDaddy-CharacterManager:SetCharacters", JsonConvert.SerializeObject(chars));
	
		}

	}
}