using System;
using System.Windows;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.Maps;
using static GhostrunnerRNG.Game.GameUtils;
using GhostrunnerRNG.Windows;
using GhostrunnerRNG.MapGen;

namespace GhostrunnerRNG {
    public partial class MainWindow : Window {

		public readonly bool DEBUG_MODE = Debugger.IsAttached;

		// Hook
		globalKeyboardHook kbHook = new globalKeyboardHook();
		public static Process game { get; private set; }
		public bool hooked = false;
		Timer updateTimer;

		//////// Pointers ////////
		DeepPointer mapNameDP, hcDP, capsuleDP, LoadingDP, preciseTimeDP;
		public static IntPtr mapNamePtr, hcPtr, xPosPtr, yPosPtr, zPosPtr, angleSinPtr, angleCosPtr, LoadingPtr, preciseTimePtr; 

		// player pos&aim, timer, hcFlag
		public static bool IsHC;
		public static float xPos, yPos, zPos, angleSin, angleCos;
		public float oldPreciseTimer, preciseTimer;
		public static Angle angle;

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
		private MapType AccurateMapType;

		public static string GlobalLog;


        private void ButtonAbout_Click(object sender, RoutedEventArgs e) {
			Window aboutWindow = new About();
			aboutWindow.ShowDialog();
		}

        private void ButtonSettings_Click(object sender, RoutedEventArgs e) {
			Window settingsWindow = new Settings();
			settingsWindow.ShowDialog();
        }

        private void MapChanged(string from, string to) {
			MapType mapTo = GetMapType(to);
			MapType mapFrom = GetMapType(from);
			

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
					ButtonNewRng.Visibility = Visibility.Hidden;
				}
				LogStatus("Idle");
				return;
			}
		}

		public MainWindow() {
			InitializeComponent();
			Config.GetInstance();
			label_Title.Content += $" v{Config.VERSION}";

			oldPreciseTimer = preciseTimer = -1;

			// UI
			ToggleButton(ButtonNewRng, false);
			ButtonDev.Visibility = DEBUG_MODE ? Visibility.Visible : Visibility.Collapsed;
			ButtonDev.IsEnabled = DEBUG_MODE;

			// HotKeys
			kbHook.KeyDown += InputKeyDown;
			kbHook.HookedKeys.Add(Keys.F7);

			// Update Timer
			updateTimer = new Timer {
				Interval = (200) // 0.2sec
			};
			updateTimer.Tick += new EventHandler(Update);
			updateTimer.Start();

			LogStatus("Idle");
		}

		// Timer Tick
		private void Update(object sender, EventArgs e) {
			// Check if game is running/hooked
			if(game == null || game.HasExited) {
				game = null;
				hooked = false;

				errorGrid.Visibility = Visibility.Visible;
				errorMsg.Content = "Ghostrunner not found";
			} else {
				errorGrid.Visibility = Visibility.Hidden;
				errorMsg.Content = "";
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
					label_levelName.Text = $"Current Level: {currentMap.mapType}";  // load map type from object
				} else {
					label_levelName.Text = $"Current Level: {AccurateMapType}";     // last updated
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

			/// DebugMode ///
			if(DEBUG_MODE) {
				// Player Angle (for dev)
				game.ReadValue<float>(angleSinPtr, out angleSin);
				game.ReadValue<float>(angleCosPtr, out angleCos);
				angle.angleSin = angleSin;
				angle.angleCos = angleCos;
			}

			// global Log check (if logs are sent from outside MainWindow)
			CheckOutsideLog();
		}

		private void TimerTrackerUpdate() {
			// we don't need main menu
			if(AccurateMapType == MapType.MainMenu) return;
			// level loaded but that's no menu? 
			if(AccurateMapType == MapType.Unknown) {
				AccurateMapType = GetMapType(MapName);
            }

			// done loading?
			if(oldPreciseTimer == 0 && preciseTimer > 0) {
				//HC not supported
				checkHCMode();
				if(IsHC) {
					currentMap = null;
					ToggleButton(ButtonNewRng, false);
					AccurateMapType = MapType.Unknown;
					LogStatus("[!] Hardcore mode is not supported for any map.");
					return;
				}

				// awakening/look inside?
				if(MapLevels.FirstOrDefault(x => x.Value == MapType.AwakeningLookInside).Key == MapName) { // Lookinside or Awakening?
					ToggleButton(ButtonNewRng, true);
					if(xPos < 50000) {
						// awakening
						AccurateMapType = MapType.Awakening;
						currentMap = new Awakening(IsHC);
						NewRNG();
						return;
					} else {
						// Look Inside
						AccurateMapType = MapType.LookInside;
						currentMap = new LookInside(IsHC);
						NewRNG();
						return;
					}

				} else if(AccurateMapType == MapType.TheClimb) {
					// TheClimb
					currentMap = new TheClimb(IsHC);
					NewRNG();
					ToggleButton(ButtonNewRng, true);
					return;

				} else if(AccurateMapType == MapType.JackedUp) {
					// JackedUp
					currentMap = new JackedUp(IsHC);
					NewRNG();
					ToggleButton(ButtonNewRng, true);
					return;

				} else if(AccurateMapType == MapType.BlinkCV) {
					// BlinkCV
					if(!Config.GetInstance().Gen_RngCV) {
						currentMap = null;
						LogStatus("CV-RNG is disabled by user.");
						return;
					}
					currentMap = new BlinkCV();
					NewRNG();
					ToggleButton(ButtonNewRng, true);
					return;

				} else if(AccurateMapType == MapType.BreatheIn) {
					// BreatheIn
					currentMap = new BreatheIn(IsHC);
					NewRNG();
					ToggleButton(ButtonNewRng, true);
					return;
				} else if(AccurateMapType == MapType.RoadToAmida) {
					// RoadToAmida
					currentMap = new RoadToAmida(IsHC);
					NewRNG();
					ToggleButton(ButtonNewRng, true);
					return;
				}else if(MapLevels.FirstOrDefault(x => x.Value == MapType.RunUpGatekeeper).Key == MapName) { // RunUp or Tom?
					ToggleButton(ButtonNewRng, true);
					if(yPos < 14000) {
						// RunUp
						AccurateMapType = MapType.RunUp;
						currentMap = new RunUp(IsHC);
						NewRNG();
						return;
					} else {
						// Tom
						AccurateMapType = MapType.Gatekeeper;
					}
				} else if(AccurateMapType == MapType.DharmaCity) {
					// DharmaCity
					currentMap = new DharmaCity(IsHC);
					NewRNG();
					ToggleButton(ButtonNewRng, true);
					return;
				} else {
					currentMap = null;
					AccurateMapType = GetMapType(MapName);
					
				}

				// Maps without RNG
				if(!MapHasRng(AccurateMapType)) {
					currentMap = null;
					LogStatus("No RNG for this level.");
					ToggleButton(ButtonNewRng, false);
					return;
				}

				// Supported maps
				if(!MapSupported(AccurateMapType)) {
					currentMap = null;
					LogStatus("Level not supported.");
					ToggleButton(ButtonNewRng, false);
					return;
                } else {
					LogStatus("Level loaded.");
                }
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

        // New RNG
        private void NewRNG(bool force = false) {
			// Can RNG?
			if(!Config.GetInstance().Gen_RngOnRestart) {
				currentMap = null;
				LogStatus("RNG is disabled by user.");
				return;
			}

			if(currentMap != null && (Config.GetInstance().Gen_RngOnRestart || force)) {
				SpawnPlane.r = new Random();
				currentMap.RandomizeEnemies(game);
				LogStatus("RNG Generated! \nDie or load cp to see changes.");
            }
        }

		private void InputKeyDown(object sender, KeyEventArgs e) {
			switch(e.KeyCode) {
				case Keys.F7:
					NewRNG(true);
					break;
				default:
					break;
			}
			e.Handled = true;
		}

		private void SetPointersByModuleSize(int moduleSize) {
			switch(moduleSize) {
				case 78376960:
					Debug.WriteLine("found steam5");
					capsuleDP = new DeepPointer(0x04328538, 0x30, 0x130, 0x0);
					mapNameDP = new DeepPointer(0x04328548, 0x30, 0xF8, 0x0);
					//preciseTimeDP = new DeepPointer(0x045A3C20, 0x52C);
					preciseTimeDP = new DeepPointer(0x045A3C20, 0x138, 0xB0, 0x128);
					hcDP = new DeepPointer(0x04328548, 0x328, 0x30);
					LoadingDP = new DeepPointer(0x0445ED38, 0x1E8);
					break;

				case 78168064:
					Debug.WriteLine("found gog5");
					capsuleDP = new DeepPointer(0x04328538, 0x30, 0x130, 0x0);
					mapNameDP = new DeepPointer(0x04328548, 0x30, 0xF8, 0x0);
					//preciseTimeDP = new DeepPointer(0x045A3C20, 0x52C);
					preciseTimeDP = new DeepPointer(0x045A3C20, 0x138, 0xB0, 0x128);
					hcDP = new DeepPointer(0x04328548, 0x328, 0x30);
					LoadingDP = new DeepPointer(0x0445ED38, 0x1E8);
					break;

				case 77910016:
					Debug.WriteLine("found egs3");
					capsuleDP = new DeepPointer(0x042F0310, 0x30, 0x130, 0x0);
					mapNameDP = new DeepPointer(0x042F02E8, 0x30, 0xF8, 0x0);
					//preciseTimeDP = new DeepPointer(0x045A3C20, 0x52C);
					preciseTimeDP = new DeepPointer(0x045A3C20, 0x138, 0xB0, 0x128);
					hcDP = new DeepPointer(0x04328548, 0x328, 0x30);
					LoadingDP = new DeepPointer(0x0445ED38, 0x1E8);
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

			IntPtr capsulePtr;
			capsuleDP.DerefOffsets(game, out capsulePtr);
			xPosPtr = capsulePtr + 0x1D0;
			yPosPtr = capsulePtr + 0x1D4;
			zPosPtr = capsulePtr + 0x1D8;
			angleSinPtr = capsulePtr + 0x1A8;
			angleCosPtr = capsulePtr + 0x1AC;
		}

		// Log
		private void LogStatus(string str) {
			str = str.Replace("\n", "\n ");
			label_RNGStatus.Text = $"RNG Status:\n {str}";
		}


		private string globalLogTemp = GlobalLog;
		private void CheckOutsideLog() {
			if(globalLogTemp != GlobalLog) {
				globalLogTemp = GlobalLog;
				label_GlobalLog.Text = "Debug: " + GlobalLog;
            }
        }

		private void checkHCMode() {
			game.ReadValue<bool>(hcPtr, out IsHC);
		}

		// NewRng Button Click
        private void ButtonNewRng_Click(object sender, RoutedEventArgs e) {
			if(ButtonNewRng.Visibility == Visibility.Visible)
				NewRNG(true);
        }

		public void ToggleButton(System.Windows.Controls.Control control, bool flag) {
			control.Visibility = flag ? Visibility.Visible : Visibility.Hidden;
			control.IsEnabled = flag;
        }

		private void ButtonDev_Click(object sender, RoutedEventArgs e) {
			Window devWindow = new DevWindow();
			devWindow.ShowDialog();
		}
	}
}
