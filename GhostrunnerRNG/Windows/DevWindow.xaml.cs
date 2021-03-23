using System;
using System.Windows;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Controls;
using GhostrunnerRNG.Game;
using static GhostrunnerRNG.Game.GameUtils;
using GhostrunnerRNG.MapGen;

namespace GhostrunnerRNG.Windows {

    public partial class DevWindow : Window {

		public static WorldObject worldObject;

		// Hook
		globalKeyboardHook kbHook = new globalKeyboardHook();

		// creation
		private Vector3f pos1, pos2;

		// testing
		private Vector3f test_pos1, test_pos2;

		public DevWindow() {
            InitializeComponent();
			SpawnPlaneTab.IsSelected = true;
			test_pos1 = Vector3f.Empty;
			test_pos2 = Vector3f.Empty;

			// HotKeys
			kbHook.KeyDown += InputKeyDown;
			HookKeys(true);
		}

		private void HookKeys(bool flag) {
            if(flag && kbHook.HookedKeys.Count == 0) {
				kbHook.HookedKeys.Add(Keys.NumPad1);
				kbHook.HookedKeys.Add(Keys.NumPad2);
				kbHook.HookedKeys.Add(Keys.NumPad3);
				kbHook.HookedKeys.Add(Keys.NumPad4);
				kbHook.HookedKeys.Add(Keys.NumPad5);
				kbHook.HookedKeys.Add(Keys.NumPad7);
				kbHook.HookedKeys.Add(Keys.NumPad8);
            } else if(!flag) {
				kbHook.HookedKeys.Clear();
            }
		}



        private void textbox_commands_TextChanged(object sender, TextChangedEventArgs e) {
			string[] commands = textbox_commands.Text.Split('\n');
			if(commands.Length > 0 && commands[0].Trim().StartsWith("layout.AddSpawnPlane(new SpawnPlane(new Vector3f(")) {
				try {
					// parse point a
					string c = commands[0].Remove(0, ("layout.AddSpawnPlane(new SpawnPlane(new Vector3f(").Length);
					float x1 = float.Parse(c.Substring(0, c.IndexOf(",")));
					c = c.Remove(0, c.IndexOf(",") + 1);
					float y1 = float.Parse(c.Substring(0, c.IndexOf(",")));
					c = c.Remove(0, c.IndexOf(",") + 1);
					float z1 = float.Parse(c.Substring(0, c.IndexOf(")")));
					test_pos1 = new Vector3f(x1, y1, z1);
					c = c.Remove(0, c.IndexOf(")") + 1);

					// second point?
					if(c.StartsWith(", new Vector3f(")) {
						c = c.Remove(0, (", new Vector3f(").Length);
						float x2 = float.Parse(c.Substring(0, c.IndexOf(",")));
						c = c.Remove(0, c.IndexOf(",") + 1);
						float y2 = float.Parse(c.Substring(0, c.IndexOf(",")));
						c = c.Remove(0, c.IndexOf(",") + 1);
						float z2 = float.Parse(c.Substring(0, c.IndexOf(")")));
						test_pos2 = new Vector3f(x2, y2, z2);
                       
					} else {
						test_pos2 = Vector3f.Empty;
                    }
				} catch(Exception) {
					test_pos1 = Vector3f.Empty;
					test_pos2 = Vector3f.Empty;
				}
            }
        }

        private void copyButton_Click(object sender, RoutedEventArgs e) {
			System.Windows.Clipboard.SetText(outputBox.Text);
		}

        private void InputKeyDown(object sender, KeyEventArgs e) {
            switch(e.KeyCode) {
				case Keys.NumPad1:
					// 1 pos save
					pos1 = new Vector3f(MainWindow.xPos, MainWindow.yPos, MainWindow.zPos);
					if(checkbox_onlyVector.IsChecked == true) {
						outputBox.Text = $"new Vector3f({(int)pos1.X}, {(int)pos1.Y}, {(int)pos1.Z})";
					}
					break;
				case Keys.NumPad2:
					// 2'nd pos save
					pos2 = new Vector3f(MainWindow.xPos, MainWindow.yPos, MainWindow.zPos);
					break;
				case Keys.NumPad3:
					// generate code: 2 pos, fixed current angle
					outputBox.Text = $"layout.AddSpawnPlane(new SpawnPlane(new Vector3f({(int)pos1.X}, {(int)pos1.Y}, {(int)pos1.Z}), new Vector3f({(int)pos2.X}, {(int)pos2.Y}, {(int)pos2.Z}), new Angle({MainWindow.angle.angleSin:0.00}f, {MainWindow.angle.angleCos:0.00}f)));";
					break;
				case Keys.NumPad4:
					// generate code: 1 pos, fixed current angle
					outputBox.Text = $"layout.AddSpawnPlane(new SpawnPlane(new Vector3f({(int)pos1.X}, {(int)pos1.Y}, {(int)pos1.Z}), new Angle({MainWindow.angle.angleSin:0.00}f, {MainWindow.angle.angleCos:0.00}f)));";
					break;
				case Keys.NumPad5:
					// generate code: 1 pos
					outputBox.Text = $"layout.AddSpawnPlane(new SpawnPlane(new Vector3f({(int)pos1.X}, {(int)pos1.Y}, {(int)pos1.Z})));";
					break;
				case Keys.NumPad7:
					// tp to testpos1
					if(!test_pos1.IsEmpty())
						Teleport(MainWindow.game, test_pos1);
					break;
				case Keys.NumPad8:
					// tp to testpos2
					if(!test_pos2.IsEmpty())
						Teleport(MainWindow.game, test_pos2);
					break;
				default:
					break;
			}
			e.Handled = true;
		}

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if(SpawnPlaneTab.IsSelected) {
				//Console.WriteLine("plane tab");
				HookKeys(true);
			} else if(RotationTab.IsSelected) {
				HookKeys(false);
				ButtonApply.Visibility = worldObject != null ? Visibility.Visible : Visibility.Collapsed;
			}
		}

        private void ButtonYPR_Click(object sender, RoutedEventArgs e) {
			if(GameUtils.IsNumeric(InputYaw.Text) && GameUtils.IsNumeric(InputPitch.Text) && GameUtils.IsNumeric(InputRoll.Text)) {
				OutputboxRotation.Text = GameUtils.CreateQuaternion(float.Parse(InputYaw.Text), float.Parse(InputPitch.Text), float.Parse(InputRoll.Text)).ToString(true);
			}
        }


		private void ButtonAngle_Click(object sender, RoutedEventArgs e) {
			if(GameUtils.IsNumeric(AngleSin.Text) && GameUtils.IsNumeric(AngleCos.Text)) {
				Enemies.EnemyTurret.TurretOrientation mode = Enemies.EnemyTurret.TurretOrientation.Normal;
				if(TurretMode.SelectedIndex == 1)
					mode = Enemies.EnemyTurret.TurretOrientation.Ceiling;
				if(TurretMode.SelectedIndex == 2)
					mode = Enemies.EnemyTurret.TurretOrientation.WallLeft;
				if(TurretMode.SelectedIndex == 3)
					mode = Enemies.EnemyTurret.TurretOrientation.WallRight;

				OutputboxRotation.Text = GameUtils.CreateTurretQuaternion(mode, float.Parse(AngleSin.Text), float.Parse(AngleCos.Text)).ToString(true);
			}
		}

        private void CopyRotationCode_Click(object sender, RoutedEventArgs e) {
			System.Windows.Clipboard.SetText(OutputboxRotation.Text);
		}

        private void ButtonApply_Click(object sender, RoutedEventArgs e) {
            if(worldObject != null && GameUtils.IsNumeric(InputYaw.Text) && GameUtils.IsNumeric(InputPitch.Text) && GameUtils.IsNumeric(InputRoll.Text)) {
				worldObject.SetMemoryPos(MainWindow.game, new SpawnData(worldObject.Pos, new QuaternionAngle(float.Parse(InputYaw.Text), float.Parse(InputPitch.Text), float.Parse(InputRoll.Text))));
            }
        }

        private void Teleport(Process game, Vector3f pos) {
			game.WriteBytes(MainWindow.xPosPtr, BitConverter.GetBytes(pos.X));
			game.WriteBytes(MainWindow.yPosPtr, BitConverter.GetBytes(pos.Y));
			game.WriteBytes(MainWindow.zPosPtr, BitConverter.GetBytes(pos.Z));
		}

		protected override void OnClosing(CancelEventArgs e) {
			// unhook keys
			kbHook.HookedKeys.Remove(Keys.NumPad1);
			kbHook.HookedKeys.Remove(Keys.NumPad2);
			kbHook.HookedKeys.Remove(Keys.NumPad3);
			kbHook.HookedKeys.Remove(Keys.NumPad4);
			kbHook.HookedKeys.Remove(Keys.NumPad5);

			kbHook.HookedKeys.Remove(Keys.NumPad7);
			kbHook.HookedKeys.Remove(Keys.NumPad8);
			kbHook.KeyDown -= InputKeyDown;
			base.OnClosing(e);
        }
    }
}
