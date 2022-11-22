using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using BigDaddy_CharacterManager;
using System.Collections.Generic;
using MenuAPI;
using Newtonsoft.Json;
using BigDaddy_CharacterManager_Models;

namespace BigDaddy_CharacterManager.Client
{
    public class ClientMain : BaseScript
    {
		List<Character> characters = new List<Character>();
		bool firstRun = true;
		string CurrentName = "Unnamed Character";
		int currentId = -1;
		bool editing = false;
		int screenW = 0;
		int screenH = 0;
		Vector3 initpos;
		Menu menu = new Menu("Character Manager", "Manage Characters");
		Menu submenu = new Menu("Delete", "ARE YOU SURE?");

		public ClientMain()
        {
			EventHandlers["onResourceStart"] += new Action(StartCM);
			EventHandlers["playerSpawned"] += new Action(StartCM);
			EventHandlers["BigDaddy-CharacterManager:SetCharacters"] += new Action<string>(SetCharacters);
			EventHandlers["BigDaddy-CharacterManager:SaveComplete"] += new Action<int>(SaveComplete);
			EventHandlers["BigDaddy-CharacterManager:SetNewCharacter"] += new Action(SetNewCharacter);
		}

		private void StartCM()
		{
			GetActiveScreenResolution(ref screenW, ref screenH);

			MenuController.MenuToggleKey = (Control)(-1);
			MenuController.MenuAlignment = MenuController.MenuAlignmentOption.Right;
			MenuController.AddMenu(menu);
			menu.ClearMenuItems();

			TriggerServerEvent("BigDaddy-CharacterManager:GetCharacters");

			RegisterCommand("manageme", new Action(OpenMenu), false);

			menu.OnItemSelect += (_menu, _item, _index) =>
			{
				if (_item.ItemData.ToString() == "create")
				{
					Debug.WriteLine("Create Character");
					InputName(true, false);
					return;
				}
				else if (_item.ItemData.ToString() == "edit")
				{
					Debug.WriteLine("Edit Character");
					StartEditor(false, false, false);
					return;
				}
				else if (_item.ItemData.ToString() == "clone")
				{
					Debug.WriteLine("Clone Character");
					InputName(false, true);
					return;
				}
				else if (_item.ItemData.ToString() == "delete")
				{
					submenu.OpenMenu();
					Debug.WriteLine("Delete Character Check");
					return;
				}
				else if (_item.ItemData.ToString() == "rename")
				{
					Debug.WriteLine("Rename Character");
					Rename();
					return;
					//save new name to db
				}
				else
				{
					Debug.WriteLine("Spawning Character");
					//CharacterAppearance data = JsonConvert.DeserializeObject<CharacterAppearance>(_item.ItemData);
					var thisone = characters.Find(c => c.name == _item.Text.ToString().Replace("~b~", ""));
					CurrentName = thisone.name;
					changeCharacter(thisone.id);
					return;
				}
			};

			submenu.OnItemSelect += (_menu, _item, _index) =>
			{
				if (_item.ItemData.ToString() == "yes")
				{
					//delete character
					Debug.WriteLine("Deleting Character");
					TriggerServerEvent("BigDaddy-CharacterManager:DeleteCharacter", currentId);
					currentId = -1;
					CurrentName = "Unsaved Character";
					Exports["fivem-appearance"].setPlayerModel("mp_m_freemode_01");
					Exports["fivem-appearance"].setPlayerAppearance(null);
					submenu.CloseMenu();
					menu.OpenMenu();

				}
			};
		}

		private void SetNewCharacter()
		{
			int newId = -1;
			foreach(Character c in characters)
			{
				if (c.id > newId)
				{
					newId = c.id;
				}
			}
			currentId = newId;
			changeCharacter(currentId);
		}

		private async void SetCharacters(string data)
		{
			editing = false;
			characters = JsonConvert.DeserializeObject<List<Character>>(data);
			BuildMenu();
		}

		private async void BuildMenu()
		{
			menu.ClearMenuItems();
			menu.AddMenuItem(
				new MenuItem(
				   "CREATE",
				   ("Create a new character")
				)
				{
					Enabled = true,
					ItemData = "create"
				}
			);
			menu.AddMenuItem(
				new MenuItem(
				   "CLONE",
				   ("Clone this character into a new one")
				)
				{
					Enabled = true,
					ItemData = "clone"
				}
			);
				if (currentId > 0)
				{
					menu.AddMenuItem(
						new MenuItem(
						   "EDIT",
						   ("Edit this character")
						)
						{
							Enabled = true,
							ItemData = "edit"
						}
					);
					menu.AddMenuItem(
						new MenuItem(
						   "RENAME",
						   ("Rename this character")
						)
						{
							Enabled = true,
							ItemData = "rename"
						}
					);
				submenu.ClearMenuItems();
				submenu.AddMenuItem(
					new MenuItem(
					   "YES, I am sure, delete it",
					   ("Delete this character")
					)
					{
						Enabled = true,
						ItemData = "yes"
					}
				);
				MenuController.AddSubmenu(menu, submenu);

				MenuItem menuButton = new MenuItem(
					"DELETE",
					"Delete this Character"
				)
				{
					Enabled = true,
					ItemData = "delete"
				};
				menu.AddMenuItem(menuButton);
				MenuController.BindMenuItem(menu, submenu, menuButton);
			}

			foreach (Character c in characters)
			{
				menu.AddMenuItem(
					new MenuItem(
						$"~b~{c.name}",
						$"Switch to ~b~ {c.name}"
					)
					{
						Enabled = true,
						ItemData = c.data,
						Label = (c.id == currentId ? "CURRENT" : "")
					}
				);
			}
		}



		private async void StartEditor(bool isNew, bool loadDefault, bool isClone) {
			Tick += EditingWatcher;
			editing = true;
			menu.CloseMenu();

			initpos = Game.PlayerPed.Position;
			DoScreenFadeOut(1000);
			await Delay(1000);
			//get directly over the lineup spot so it will cache in if necessary
			Game.PlayerPed.Position = new Vector3(402.88f, -996.45f, 30);
			await Delay(1000);
			//line up
			Game.PlayerPed.Position = new Vector3(402.88f, -996.45f, -98.5f);
			await Delay(500);
			Game.PlayerPed.Heading = 180;
			await Delay(500);

			if (isNew && loadDefault) {
				//--load default model
				Exports["fivem-appearance"].setPlayerModel("mp_m_freemode_01");
				await Delay(500);
			} else if (isClone)
			{
				var a = Exports["fivem-appearance"].getPedAppearance(Game.PlayerPed.Handle);
				string j = JsonConvert.SerializeObject(a, Formatting.None);
				Debug.WriteLine(j);
				Appearance data = JsonConvert.DeserializeObject<Appearance>(j);
				string model = data.model;
				data.tattoos = null;
				Exports["fivem-appearance"].setPlayerModel(model);
				Exports["fivem-appearance"].setPlayerAppearance(data);
			}

			Dictionary<string, bool> config = new Dictionary<string, bool>();
			config.Add("ped", true);
			config.Add("headBlend", true);
			config.Add("faceFeatures", true);
			config.Add("headOverlays", true);
			config.Add("components", true);
			config.Add("props", true);
			config.Add("tattoos", true);

			DoScreenFadeIn(1000);

			Exports["fivem-appearance"].startPlayerCustomization(new Action<Object>(async (appearance) =>
			{
				DoScreenFadeOut(1);
				Game.PlayerPed.Position = initpos;
				await Delay(1000);
				DoScreenFadeIn(1000);
				await Delay(1000);
				if (!string.IsNullOrEmpty(appearance.ToString()))
				{
					Appearance a = new Appearance();
					string j = JsonConvert.SerializeObject(appearance, Formatting.None);
					//Debug.WriteLine(j);
					a = JsonConvert.DeserializeObject<Appearance>(j);

					if(a.tattoos != null)
					{
						if (a.tattoos.ZONE_HEAD == null)
						{
							a.tattoos.ZONE_HEAD = new List<ZONEHEAD>();
						}
						if (a.tattoos.ZONE_TORSO == null)
						{
							a.tattoos.ZONE_TORSO = new List<ZONETORSO>();
						}
						if (a.tattoos.ZONE_LEFT_LEG == null)
						{
							a.tattoos.ZONE_LEFT_LEG = new List<ZONELEFTLEG>();
						}
						if (a.tattoos.ZONE_RIGHT_LEG == null)
						{
							a.tattoos.ZONE_RIGHT_LEG = new List<ZONERIGHTLEG>();
						}
						if (a.tattoos.ZONE_LEFT_ARM == null)
						{
							a.tattoos.ZONE_LEFT_ARM = new List<ZONELEFTARM>();
						}
						if (a.tattoos.ZONE_RIGHT_ARM == null)
						{
							a.tattoos.ZONE_RIGHT_ARM = new List<ZONERIGHTARM>();
						}
					}

					j = JsonConvert.SerializeObject(a, Formatting.None);
					//Debug.WriteLine(JsonConvert.SerializeObject(a));

					if (isNew)
					{
						//save new
						TriggerServerEvent("BigDaddy-CharacterManager:SaveNewCharacter", CurrentName, j);
					}
					else
					{
						//update
						TriggerServerEvent("BigDaddy-CharacterManager:SaveCharacter", currentId, j);
					}
				} else
				{
					//put back orig character
					changeCharacter(currentId);
				}
				editing = false;
				Tick -= EditingWatcher;

			}), config);

		}

		private void SaveComplete(int id)
		{
			currentId = id;
		}


		private async Task EditingWatcher()
		{
			for (int i = 1; i >= 256; i++) {
				if (NetworkIsPlayerActive(i)) {

					SetEntityVisible(GetPlayerPed(i), false, false);
					SetEntityVisible(PlayerPedId(), true, true);
					SetEntityNoCollisionEntity(GetPlayerPed(i), PlayerPedId(), false);
				}
			}
			HideHudComponentThisFrame(19);
		}


		private void OpenMenu()
		{
			BuildMenu();
			menu.OpenMenu();
		}

		private async void Rename()
		{
			string result = "";
			DisplayOnscreenKeyboard(1, "FMMC_MPM_NA", "", "", "", "", "", 30);
			while (UpdateOnscreenKeyboard() == 0)
			{
				DisableAllControlActions(0);
				await Delay(0);
			}
			if (!string.IsNullOrEmpty(GetOnscreenKeyboardResult()))
			{
				result = GetOnscreenKeyboardResult();
				CurrentName = result;
				TriggerServerEvent("BigDaddy-CharacterManager:SaveCharacterName", currentId, CurrentName);
			}
		}

		private async void InputName(bool loadDefault, bool isClone)
		{
			string result = "";
			DisplayOnscreenKeyboard(1, "FMMC_MPM_NA", "", "", "", "", "", 30);
			while (UpdateOnscreenKeyboard() == 0) {
				DisableAllControlActions(0);
				await Delay(0);
			}
			if (!string.IsNullOrEmpty(GetOnscreenKeyboardResult())) {
				result = GetOnscreenKeyboardResult();
				CurrentName = result;
				BuildMenu();
			}
			if (loadDefault)
			{
				StartEditor(true, true, false);
			}
			else
			{
				StartEditor(true, false, isClone);
			}
		}
		

		private void changeCharacter(int id) {
			currentId = id;
			var thisone = characters.Find(c => c.id == id);
			CurrentName = thisone.name;
			Appearance data = JsonConvert.DeserializeObject<Appearance>(thisone.data);
			string model = data.model;
			Exports["fivem-appearance"].setPlayerModel(model);
			Exports["fivem-appearance"].setPlayerAppearance(data);
			OpenMenu();
		}


	}
}

