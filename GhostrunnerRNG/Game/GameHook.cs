using System;
using System.Windows;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using GhostrunnerRNG.Maps;
using static GhostrunnerRNG.Game.GameUtils;
using GhostrunnerRNG.MapGen;
using System.Text;

namespace GhostrunnerRNG.Game {
    class GameHook {

		// Hook
		public static Process game { get; private set; }
		public bool hooked = false;
		Timer updateTimer;

		//////// Pointers ////////
		DeepPointer mapNameDP, hcDP, capsuleDP, LoadingDP, preciseTimeDP, reloadCounterDP;
		public static IntPtr mapNamePtr, hcPtr, xPosPtr, yPosPtr, zPosPtr, angleSinPtr, angleCosPtr, LoadingPtr, preciseTimePtr, reloadCounterPtr;

		// player pos&aim, timer, hcFlag
		public static bool IsHC;
		public static float xPos, yPos, zPos, angleSin, angleCos;
		public float oldPreciseTimer, preciseTimer;
		public static Angle angle = new Angle();

		private int _cpCounter;
		public int cpCounter {
            get { return _cpCounter; }
            set { if(value != _cpCounter) {_cpCounter = value; cpCounterChanged(value); } }
        }

		// MAP OBJECT
		MapCore currentMap;
		private string _mapName;

		public string MapName {
			get { return _mapName; }
			set {
				if(_mapName == value) return;
				MapChanged(_mapName, value);
				_mapName = value;
			}
		}
		public static MapType AccurateMapType { get; private set; }


		private MainWindow main;

        public GameHook(MainWindow main) {
			this.main = main;

			// Update Timer
			updateTimer = new Timer {
				Interval = (200) // 0.2sec
			};
			updateTimer.Tick += new EventHandler(Update);
			updateTimer.Start();

			oldPreciseTimer = preciseTimer = -1;
		}

		// Timer Tick
		private void Update(object sender, EventArgs e) {
			// Check if game is running/hooked
			if(game == null || game.HasExited) {
				game = null;
				hooked = false;

				main.errorGrid.Visibility = Visibility.Visible;
				main.errorMsg.Content = "Ghostrunner not found";
				currentMap = null;
				AccurateMapType = MapType.Unknown;
				main.ToggleRngControls(false);
				main.ClearTexts();
			} else {
				main.errorGrid.Visibility = Visibility.Hidden;
				main.errorMsg.Content = "";
			}
			if(!hooked)
				hooked = Hook();

			if(!hooked)
				return;
			try {
				DerefPointers();
			} catch(Exception) {
				return;
			}

			// Map name
			string map = "";
			game.ReadString(mapNamePtr, 250, out map);
			if(!string.IsNullOrEmpty(map)) {
				if(currentMap != null) {
					main.label_levelName.Text = $"Current Level: {currentMap.mapType}";  // load map type from object
				} else {
					main.label_levelName.Text = $"Current Level: {AccurateMapType}";     // last updated
				}
				MapName = map;
			}

			// Timer + LoadFlag
			oldPreciseTimer = preciseTimer;
			game.ReadValue<float>(preciseTimePtr, out preciseTimer);
			TimerTrackerUpdate();

			////// Read Memory /////  - PLAYER POS
			game.ReadValue<float>(xPosPtr, out xPos);
			game.ReadValue<float>(yPosPtr, out yPos);
			game.ReadValue<float>(zPosPtr, out zPos);

			// Update Map
			if(currentMap != null) {
				currentMap.UpdateMap(new Vector3f(xPos, yPos, zPos));
			}

			//// Death/CP counter
			int cp = 0;
			game.ReadValue<int>(reloadCounterPtr, out cp);
			cpCounter = cp;

			/// DebugMode ///
			if(main.DEBUG_MODE) {
				// Player Angle (for dev)
				game.ReadValue<float>(angleSinPtr, out angleSin);
				game.ReadValue<float>(angleCosPtr, out angleCos);
				angle.angleSin = angleSin;
				angle.angleCos = angleCos;
			}

			// global Log check (if logs are sent from outside MainWindow)
			main.CheckOutsideLog();
		}

		bool levelStarted, rngLoaded;

		private void TimerTrackerUpdate() {
			// we don't need main menu
			if(AccurateMapType == MapType.MainMenu) {
				rngLoaded = false;
				return;
			}
			// level loaded but that's no menu? 
			if(AccurateMapType == MapType.Unknown) {
				AccurateMapType = GetMapType(MapName);
			}

			// check if level started
			if(oldPreciseTimer == 0 && preciseTimer > 0) {
				levelStarted = true;
			} else if(preciseTimer == 0) {
				levelStarted = false;
				rngLoaded = false;

			}

			// done loading?
			if(!rngLoaded && levelStarted && (xPos != 0 && yPos != 0)) {
				rngLoaded = true;

				//HC not supported
				checkHCMode();
				if(IsHC) {
					currentMap = null;
					main.ToggleRngControls(false);
					AccurateMapType = MapType.Unknown;
					LogStatus("[!] Hardcore mode is not supported for any map.");
					return;
				}
				// cv map and cv rng is disabled by user?
				if(!Config.GetInstance().Gen_RngCV && IsCVMap(AccurateMapType)) {
					currentMap = null;
					LogStatus("CV RNG is disabled by user.");
					return;
				}

				// Create Map Object
				bool mapCreated = CreateMapObject(AccurateMapType);
				main.ToggleRngControls(mapCreated && currentMap.HasRng);
                if(mapCreated) {
					NewRNG();
					return;
                }

				// Maps without RNG
				if(!MapHasRng(AccurateMapType)) {
					currentMap = null;
					LogStatus("No RNG for this level.");
					return;
				}

				// Supported maps
				if(!MapSupported(AccurateMapType)) {
					currentMap = null;
					LogStatus("Level not supported.");
					return;
				} else {
					LogStatus("Level loaded.");
				}
			}
		}

		private bool CreateMapObject(MapType type) {
            switch(type) {
				case MapType.AwakeningLookInside:
					if(xPos < 50000) {
						// Awakening
						AccurateMapType = MapType.Awakening;
						currentMap = new Awakening();
						return true;
                    } else {
						// Look Inside
						AccurateMapType = MapType.LookInside;
						currentMap = new LookInside();
						return true;
                    }

				case MapType.LookInsideCV:
					currentMap = new LookInsideCV();
					return true;

				case MapType.TheClimb:
					currentMap = new TheClimb();
					return true;

				case MapType.TheClimbCV:
					currentMap = new TheClimbCV();
					return true;

				case MapType.JackedUp:
					currentMap = new JackedUp();
					return true;

				case MapType.BlinkCV:
					currentMap = new BlinkCV();
					return true;

				case MapType.BreatheIn:
					currentMap = new BreatheIn();
					return true;

				case MapType.RoadToAmida:
					currentMap = new RoadToAmida();
					return true;

				case MapType.RoadToAmidaCV:
					currentMap = new RoadToAmidaCV();
					return true;

				case MapType.TempestCV:
					currentMap = new TempestCV();
					return true;

				case MapType.RunUpGatekeeper:
					if(yPos < 14000) {
						// RunUp
						AccurateMapType = MapType.RunUp;
						currentMap = new RunUp();
						return true;
                    } else {
						// TOM
						AccurateMapType = MapType.Gatekeeper;
						currentMap = new GateKeeper();
						return true;
                    }

				case MapType.DharmaCity:
					currentMap = new DharmaCity();
					return true;

				case MapType.Echoes:
					currentMap = new Echoes();
					return true;

				case MapType.EchoesCV:
					currentMap = new EchoesCV();
					return true;

				case MapType.FasterInHerOwnImage:
					if(yPos < -1) {
						// Faster
						AccurateMapType = MapType.Faster;
						currentMap = new Faster();
						return true;
                    } else {
						// Hell
						AccurateMapType = MapType.InHerOwnImage;
						return false;
                    }

				case MapType.ForbiddenZone:
					currentMap = new ForbiddenZone();
					return true;

				case MapType.ReignInHell:
					currentMap = new ReignInHell();
					return true;

				case MapType.ReignInHellCV:
					currentMap = new ReignInHellCV();
					return true;

				case MapType.TYWB:
					currentMap = new TYWB();
					return true;
			}

			AccurateMapType = GetMapType(MapName);
			return false;
        }


		public void NewRNG(bool force = false) {
			if(currentMap == null) return;

			// Can RNG?
			if(!Config.GetInstance().Gen_RngOnRestart) {
				currentMap = null;
				LogStatus("RNG disabled by user.");
				return;
			}

			// valid map but no rng?
            if(!currentMap.HasRng) {
				LogStatus("No RNG for this section, relax...");
				return;
			}


			if(currentMap != null && (Config.GetInstance().Gen_RngOnRestart || force)) {
				Config.GetInstance().NewSeed();
				currentMap.RandomizeEnemies(game);
				if(currentMap.CPRequired)
					LogStatus("RNG Generated! \nDie or load cp to see changes.");
				else
					main.LogStatus("Rng Loaded! good luck!");
			}
		}

		private void LogStatus(string log) {
			main.LogStatus(log);
        }

		// Is it Hardcore mode?
		private void checkHCMode() {
			game.ReadValue<bool>(hcPtr, out IsHC);
		}

		private void cpCounterChanged(int value) {
			if(currentMap == null) return;

			// no rng, return...
			if(!currentMap.HasRng) return;

			// first cp/death? or cv map?
			if(currentMap.CPRequired && (value == 1 || value == 2)) {
				// first restart/death
				main.LogStatus("Rng Loaded! good luck!");
				return;
            }

			// silly death/cp load messges
			if(value == 100) {
				main.LogStatus("Try harder!");
			}else if(value == 300) {
				main.LogStatus("You like losing, eh?");
			} else if(value == 1000) {
				main.LogStatus("Worst Player Achievement Unlocked!");
			}
		}

		// Hook Game
		private bool Hook() {
			List<Process> processList = Process.GetProcesses().ToList().FindAll(x => x.ProcessName.Contains("Ghostrunner-Win64-Shipping"));
			if(processList.Count == 0) {
				game = null;
				return false;
			}
			game = processList[0];

			if(game.HasExited)
				return false;

			try {
				int mainModuleSize = game.MainModule.ModuleMemorySize;
				SetPointersByModuleSize(mainModuleSize);
				return true;
			} catch(Exception ex) {
				Console.WriteLine(ex.Message);
				return false;
			}
		}

		private void SetPointersByModuleSize(int moduleSize) {
			switch(moduleSize) {
				case 78376960:
					Debug.WriteLine("found steam5");
					capsuleDP = new DeepPointer(0x04328538, 0x30, 0x130, 0x0);
					mapNameDP = new DeepPointer(0x04328548, 0x30, 0xF8, 0x0);
					preciseTimeDP = new DeepPointer(0x045A3C20, 0x138, 0xB0, 0x128);
					hcDP = new DeepPointer(0x04328548, 0x328, 0x30);
					LoadingDP = new DeepPointer(0x0445ED38, 0x1E8);
					reloadCounterDP = new DeepPointer(0x045A3C20, 0x128, 0x388);
					break;

				case 78856192:
					Debug.WriteLine("found steam6");
					capsuleDP = new DeepPointer(0x0438BB50, 0x30, 0x130, 0x0);
					mapNameDP = new DeepPointer(0x0438BB40, 0x30, 0xF8, 0x0);
					preciseTimeDP = new DeepPointer(0x04609420, 0x138, 0xB0, 0x128);
					hcDP = new DeepPointer(0x0438BB40, 0x330, 0x30);
					LoadingDP = new DeepPointer(0x044C4478, 0x1E8);
					reloadCounterDP = new DeepPointer(0x04609420, 0x128, 0x388);
					break;

				case 78168064:
					Debug.WriteLine("found gog5");
					capsuleDP = new DeepPointer(0x04328538, 0x30, 0x130, 0x0);
					mapNameDP = new DeepPointer(0x04328548, 0x30, 0xF8, 0x0);
					preciseTimeDP = new DeepPointer(0x045A3C20, 0x138, 0xB0, 0x128);
					hcDP = new DeepPointer(0x04328548, 0x328, 0x30);
					LoadingDP = new DeepPointer(0x0445ED38, 0x1E8);
					reloadCounterDP = new DeepPointer(0x045A3C20, 0x128, 0x388);
					break;


				case 77910016:
					Debug.WriteLine("found egs3");
					capsuleDP = new DeepPointer(0x042F0310, 0x30, 0x130, 0x0);
					mapNameDP = new DeepPointer(0x042F02E8, 0x30, 0xF8, 0x0);
					preciseTimeDP = new DeepPointer(0x045A3C20, 0x138, 0xB0, 0x128);
					hcDP = new DeepPointer(0x04328548, 0x328, 0x30);
					LoadingDP = new DeepPointer(0x0445ED38, 0x1E8);
					reloadCounterDP = new DeepPointer(0x045A3C20, 0x128, 0x388);
					break;
				default:
					updateTimer.Stop();
					Console.WriteLine(moduleSize.ToString());
					System.Windows.MessageBox.Show("This game version (" + moduleSize.ToString() + ") is not supported.\nPlease Contact the developers.", "Unsupported Game Version", MessageBoxButton.OK, MessageBoxImage.Error);
					Environment.Exit(0);
					break;
			}
		}

		/// <summary>		
		/// Deref Pointers	
		/// </summary>		
		private void DerefPointers() {
			mapNameDP.DerefOffsets(game, out mapNamePtr);
			hcDP.DerefOffsets(game, out hcPtr);
			LoadingDP.DerefOffsets(game, out LoadingPtr);
			preciseTimeDP.DerefOffsets(game, out preciseTimePtr);
			reloadCounterDP.DerefOffsets(game, out reloadCounterPtr);

			IntPtr capsulePtr;
			capsuleDP.DerefOffsets(game, out capsulePtr);
			xPosPtr = capsulePtr + 0x1D0;
			yPosPtr = capsulePtr + 0x1D4;
			zPosPtr = capsulePtr + 0x1D8;
			angleSinPtr = capsulePtr + 0x1A8;
			angleCosPtr = capsulePtr + 0x1AC;
		}


		private void MapChanged(string from, string to) {
			MapType mapTo = GetMapType(to);
			MapType mapFrom = GetMapType(from);

			if(mapTo == MapType.MainMenu)
				MenuLoaded();

			// rng started in middle of level, request to restart or menu
			if(mapFrom == MapType.Unknown && mapTo != MapType.MainMenu) {
                AccurateMapType = MapType.Unknown;
				LogStatus("[!] Level is already running,\nreturn to MainMenu or restart level!");
				return;
			}

			// entered a level, from menu
			if(mapFrom == MapType.MainMenu && mapTo != MapType.MainMenu) {
				LogStatus("Loading level/cutscene...");
			}
			AccurateMapType = mapTo;    // display temp map name

			// Entered Menu
			if(mapTo == MapType.MainMenu) {
				// reset currMap if any and hide rng button
				if(currentMap != null) {
					currentMap = null;
					main.ToggleRngControls(false);
				}
				LogStatus("Idle");
				return;
			}
		}

		private void MenuLoaded() {
			string Title = "Randomizer Mode";
			string Description = "Randomizes enemies and objects in a challenging and unexpected way.";
			// title
			DeepPointer titleDP = new DeepPointer(0x044629B0, 0x3E8, 0x70, 0x2F0, 0x20, 0x0);
			DeepPointer titleLengthDP = new DeepPointer(0x044629B0, 0x3E8, 0x70, 0x2F0, 0x28);
			//description
			DeepPointer descDP = new DeepPointer(0x044629B0, 0x3E8, 0x70, 0x2F0, 0x0, 0x0);
			DeepPointer descLengthDP = new DeepPointer(0x044629B0, 0x3E8, 0x70, 0x2F0, 0x8);
			// pointers
			IntPtr titlePtr, titleLengthPtr, descPtr, descLengthPtr;
			// deref
			titleDP.DerefOffsets(game, out titlePtr);
			descDP.DerefOffsets(game, out descPtr);
			titleLengthDP.DerefOffsets(game, out titleLengthPtr);
			descLengthDP.DerefOffsets(game, out descLengthPtr);

			// set title and length + 1
			game.WriteBytes(titleLengthPtr, BitConverter.GetBytes((int)(Title.Length + 1)));
			game.WriteBytes(descLengthPtr, BitConverter.GetBytes((int)(Description.Length + 1)));
			game.WriteBytes(titlePtr, StringToMemoryBytes(Title));
			game.WriteBytes(descPtr, StringToMemoryBytes(Description));
		}

		/// <summary>
		/// For future updates (localization needed)
		/// </summary>
		private void EditProTips() {
            for(int i = 1; i < 12; i++) {
				DeepPointer tipDP = new DeepPointer(0x043FD270, 0x368, 0x80, 0x28 * (i - 1), 0x0, 0x0);//badpointer
				DeepPointer tipLengthDP = new DeepPointer(0x043FD270, 0x368, 0x80, 0x28 * (i - 1), 0x8);//badpointer
				IntPtr tipPtr, tipLengthPtr;
				tipDP.DerefOffsets(game, out tipPtr);
				tipLengthDP.DerefOffsets(game, out tipLengthPtr);
				string tip = "You're too slow, go faster! ";
				game.WriteBytes(tipPtr, StringToMemoryBytes(tip));
				game.WriteBytes(tipLengthPtr, BitConverter.GetBytes((int)(tip.Length + 1)));
			}
		}

		private byte[] StringToMemoryBytes(string str) {
			var titleBytes = Encoding.ASCII.GetBytes(str).ToList();
			List<byte> memoryBytes = new List<byte>();
			for(int i = 0; i < titleBytes.Count; i++) {
				memoryBytes.Add(titleBytes[i]);
				memoryBytes.Add(00);
			}
			memoryBytes.Add(00);
			memoryBytes.Add(00);

			return memoryBytes.ToArray();
		}
	}
}
