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

namespace GhostrunnerRNG {
	public partial class MainWindow : Window {

		public readonly bool DEBUG_MODE = Debugger.IsAttached;

		// Hook
		globalKeyboardHook kbHook = new globalKeyboardHook();
		Process game;
		public bool hooked = false;
		Timer updateTimer;

		//////// Pointers ////////
		DeepPointer mapNameDP, hcDP, preciseTimeDP, capsuleDP;
		IntPtr mapNamePtr, hcPtr, preciseTimePtr, xPosPtr, yPosPtr, zPosPtr, angleSinPtr, angleCosPtr;

		// player pos&aim, timer, hcFlag
		public static bool IsHC;
		float oldPreciseTimer = -1, preciseTimer = -1;
		float xPos, yPos, zPos, angleSin, angleCos;

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

		private void MapChanged(string from, string to) {
			MapType mapTo = GetMapName(to);
			MapType mapFrom = GetMapName(from);

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

			// UI
			ButtonNewRng.Visibility = Visibility.Hidden;

			// HotKeys
			kbHook.KeyDown += InputKeyDown;
			kbHook.HookedKeys.Add(Keys.F7);

			// DEBUG 
			if(DEBUG_MODE) {
				kbHook.HookedKeys.Add(Keys.NumPad1);
				kbHook.HookedKeys.Add(Keys.NumPad2);
				kbHook.HookedKeys.Add(Keys.NumPad3);
				kbHook.HookedKeys.Add(Keys.NumPad4);
				kbHook.HookedKeys.Add(Keys.NumPad5);
				kbHook.HookedKeys.Add(Keys.NumPad6);
			} else {
				outputBox.Visibility = Visibility.Collapsed;
				copyButton.Visibility = Visibility.Collapsed;
			}
			// Update Timer
			updateTimer = new Timer {
				Interval = (100) // 0.1sec
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

			// Timer
			oldPreciseTimer = preciseTimer;
			game.ReadValue<float>(preciseTimePtr, out preciseTimer);
			TimerTrackerUpdate();

			////// Read Memory /////
			game.ReadValue<float>(xPosPtr, out xPos);
			game.ReadValue<float>(yPosPtr, out yPos);
			game.ReadValue<float>(zPosPtr, out zPos);

			/// DebugMode ///
			if(DEBUG_MODE) {
				game.ReadValue<float>(angleSinPtr, out angleSin);
				game.ReadValue<float>(angleCosPtr, out angleCos);
				angle.angleSin = angleSin;
				angle.angleCos = angleCos;
			}
		}


		private void TimerTrackerUpdate() {
			// we don't need main menu
			if(AccurateMapType == MapType.MainMenu) return;

			// New timer started?
			if(oldPreciseTimer == 0 && preciseTimer > 0) {
				//TODO: ADD HC SUPPORT
				checkHCMode();
				if(IsHC) {
					currentMap = null;
					ButtonNewRng.Visibility = Visibility.Hidden;
					AccurateMapType = MapType.Unknown;
					LogStatus("[!] Hardcore mode is not supported for any map.");
					return;
				}

				// awakening/look inside?
				if(MapLevels.FirstOrDefault(x => x.Value == MapType.AwakeningLookInside).Key == MapName) { // Lookinside or Awakening?
					ButtonNewRng.Visibility = Visibility.Visible;
					if(xPos < 50000) {
						// awakening
						currentMap = new Awakening(IsHC);
						NewRNG();
						AccurateMapType = MapType.Awakening;
						return;
					} else {
						// Look Inside
						currentMap = new LookInside(IsHC);
						NewRNG();
						AccurateMapType = MapType.LookInside;
						return;
					}
				} else {
					currentMap = null;
					AccurateMapType = GetMapName(MapName);
					
				}
				// Not mainMenu and not Awakening? - not supported!
				if(AccurateMapType != MapType.MainMenu
					&& AccurateMapType != MapType.AwakeningLookInside
					&& AccurateMapType != MapType.Awakening
					&& AccurateMapType != MapType.LookInside) {
					LogStatus("Level not supported.");
					return;
                } else {
					LogStatus("Level loaded.");
                }
			}
		}


		// FOR DEBUG ONLY: copy generated code to clipboard
		private void copyButton_Click(object sender, RoutedEventArgs e) {
			System.Windows.Clipboard.SetText(outputBox.Text);
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
			} catch(Win32Exception ex) {
				Console.WriteLine(ex.ErrorCode);
				return false;
			}
		}

        // New RNG
        private void NewRNG(bool force = false) {
			if(currentMap != null && (checkbox_RngOnRestart.IsChecked == true || force)) {
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

#region Debug
				case Keys.NumPad1:
					// 1 pos save
					pos1 = new Vector3f(xPos, yPos, zPos);
					break;

				case Keys.NumPad2:
					// 2'nd pos save
					pos2 = new Vector3f(xPos, yPos, zPos);
					break;

				case Keys.NumPad3:
					// generate code: 2 pos, fixed current angle
					outputBox.Text = $"layout.AddSpawnPlane(new SpawnPlane(new Vector3f({(int)pos1.X}, {(int)pos1.Y}, {(int)pos1.Z}), new Vector3f({(int)pos2.X}, {(int)pos2.Y}, {(int)pos2.Z}), new Angle({angle.angleSin:0.00}f, {angle.angleCos:0.00}f)));";
					break;

				case Keys.NumPad4:
					// generate code: 1 pos, fixed current angle
					outputBox.Text = $"layout.AddSpawnPlane(new SpawnPlane(new Vector3f({(int)pos1.X}, {(int)pos1.Y}, {(int)pos1.Z}), new Angle({angle.angleSin:0.00}f, {angle.angleCos:0.00}f)));";
					break;
				case Keys.NumPad5:
					// generate code: 2 pos, random angle
					outputBox.Text = $"layout.AddSpawnPlane(new SpawnPlane(new Vector3f({(int)pos1.X}, {(int)pos1.Y}, {(int)pos1.Z})).RandomAngle());";
					break;
				case Keys.NumPad6:
					// generate code: 2 pos, random angle
					outputBox.Text = $"layout.AddSpawnPlane(new SpawnPlane(new Vector3f({(int)pos1.X}, {(int)pos1.Y}, {(int)pos1.Z}), new Vector3f({(int)pos2.X}, {(int)pos2.Y}, {(int)pos2.Z})).RandomAngle());";
					break;
#endregion
				default:
					break;
			}
			e.Handled = true;
		}

		// for debug ^^^
		private Vector3f pos1, pos2;
		private Angle angle;

		private void SetPointersByModuleSize(int moduleSize) {
			switch(moduleSize) {

				case 78376960:
					Debug.WriteLine("found steam5");
					capsuleDP = new DeepPointer(0x04328538, 0x30, 0x130, 0x0);
					mapNameDP = new DeepPointer(0x04328548, 0x30, 0xF8, 0x0);
					preciseTimeDP = new DeepPointer(0x045A3C20, 0x52C);
					hcDP = new DeepPointer(0x04328548, 0x328, 0x30);
					break;

				case 78168064:
					Debug.WriteLine("found gog5");
					capsuleDP = new DeepPointer(0x04328538, 0x30, 0x130, 0x0);
					mapNameDP = new DeepPointer(0x04328548, 0x30, 0xF8, 0x0);
					preciseTimeDP = new DeepPointer(0x045A3C20, 0x52C);
					hcDP = new DeepPointer(0x04328548, 0x328, 0x30);
					break;

				case 77910016:
					Debug.WriteLine("found egs3");
					capsuleDP = new DeepPointer(0x042F0310, 0x30, 0x130, 0x0);
					mapNameDP = new DeepPointer(0x042F02E8, 0x30, 0xF8, 0x0);
					preciseTimeDP = new DeepPointer(0x045A3C20, 0x52C);
					hcDP = new DeepPointer(0x04328548, 0x328, 0x30);
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
			preciseTimeDP.DerefOffsets(game, out preciseTimePtr);
			hcDP.DerefOffsets(game, out hcPtr);

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

		private void checkHCMode() {
			game.ReadValue<bool>(hcPtr, out IsHC);
		}

		// NewRng Button Click
        private void ButtonNewRng_Click(object sender, RoutedEventArgs e) {
			NewRNG(true);
        }
    }
}
