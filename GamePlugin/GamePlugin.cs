using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using Game_PluginAPI;
using KartKraft;
using FlatBuffers;

namespace KartKraft_GamePlugin
{
    public class GamePlugin : IPlugin_Game
    {
        /***************************************************************************************
         * SimTools Plugin - Edit the Setting below to provide support for your favorite game! *
         ***************************************************************************************/

        /********************************************
         * Per Game Settings - Change for Each Game *
         ********************************************/
        private const string _gameName = "KartKraft";       // GameName (Must Be Unique!) - the displayed Name.
        private const string _processName = "project_k";     // Process_Name without the (".exe") for this game
        private const string _pluginAuthorsName = "Keir";   // Authors
        private const string _port = "5000";                // Your Sending/Recieving UDP Port for this game
        private const string _pluginOptions = "";           // Reserved For Future Use - No Change Needed 
        private const bool _requiresPatchingPath = false;   // Do we need to make any alterations to the game files? (must be True if _RequiresSecondCheck = True)
        private const bool _requiresSecondCheck = false;    // Used when games have the same _ProcessName. (all plugins with the same _ProcessName must use this!)
        private const bool _enable_DashBoard = true;        // Enable the DashBoard Output System?
        private const bool _enable_GameVibe = false;        // Enable the GameVibe Output System?

        /*************************************
        * DOFs Used for Output for this Game *
        **************************************/
        private const bool _DOF_Support_Roll = true;
        private const bool _DOF_Support_Pitch = true;
        private const bool _DOF_Support_Heave = true;
        private const bool _DOF_Support_Yaw = true;
        private const bool _DOF_Support_Sway = true;
        private const bool _DOF_Support_Surge = true;
        private const string _DOF_Support_Extra1 = "Traction Loss";
        private const string _DOF_Support_Extra2 = "";
        private const string _DOF_Support_Extra3 = "";

        /************************
         * MemoryHook Variables *
        *************************/
        private const bool _enable_MemoryHook = false;      // Is a Memory Hook Required for this game? 

        /***********************
        * Memory Map Variables *
        ************************/
        private const bool _enable_MemoryMap = false;       // Is a MemoryMap file Required for this game?

        /****************************************************************************
        *** INIT
        ****************************************************************************/
        public void GameStart()
        {
            // DO SOMETHING AT GAME START
        }

        public void GameEnd()
        {
            // DO SOMETHING AT GAME END
        }

        public bool PatchGame(string myPath, string myIp)
        {
            // PATCH THE GAME

            try
            {
                // Patch game routine.
                // Specific to your game

                MessageBox.Show("Game patched successfully!", "Patching info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("You must first install and run the game once prior to patching.\nGame Not Patched!", "Patch failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void PatchPathInfo()
        {
            // Tell them to complete a race first before patching.
            MessageBox.Show("Before patching, please complete one race in the game.", "Patching info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            // See if they are on a 64 bit or 32 bit machine.
            if (System.Environment.Is64BitOperatingSystem)
            {
                MessageBox.Show("Please select game save directory.", "Patching info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else
            {
                MessageBox.Show("Please select installation directory.", "Patching info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void UnPatchGame(string myPath)
        {
            // UNDO patching routine

            MessageBox.Show("Game patch removed!", "Patching info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool ValidatePatchPath(string myPath)
        {
            // Insert a simple validation of the patching path - let the user know he got it right
            return true;
        }

        /****************************************************************************
        *** PROCCESS DATA
        ****************************************************************************/

        public void Process_PacketRecieved(string text)
        {
            //Get KartKraft frame
            byte[] rawBytes = System.Text.Encoding.Default.GetBytes(text);
            ByteBuffer b = new ByteBuffer(rawBytes);
            KartKraft.Frame frame = KartKraft.Frame.GetRootAsFrame(b);

            if (frame.Motion.HasValue)
            {
                // Output
                Yaw_Output = frame.Motion.Value.Yaw;
                Roll_Output = frame.Motion.Value.Roll;
                Pitch_Output = frame.Motion.Value.Pitch;
                Surge_Output = frame.Motion.Value.AccelerationX;
                Sway_Output = frame.Motion.Value.AccelerationY;
                Heave_Output = frame.Motion.Value.AccelerationZ;
                Extra1_Output = frame.Motion.Value.Traction;
            }

            if (frame.Dash.HasValue)
            {
                // Dash
                Dash_1_Output = "Speed," + frame.Dash.Value.Speed;
                Dash_2_Output = "Gear," + frame.Dash.Value.Gear;
                Dash_3_Output = "RPM," + frame.Dash.Value.Rpm;
                Dash_4_Output = "Position," + frame.Dash.Value.Pos;
                Dash_5_Output = "Lap," + 0; //#todo
                Dash_6_Output = "Lap Time," + frame.Dash.Value.BestLap;
                Dash_7_Output = "Total Laps," + 0; //#todo
            }

            // Vibe
            Vibe_1_Output = "RPM," + 0;
            Vibe_2_Output = "Gear Shift," + 0;
            Vibe_3_Output = "Collision L/R," + 0;
            Vibe_4_Output = "Collision F/B," + 0;
            Vibe_5_Output = "Road Detail," + 0;
            Vibe_6_Output = "Chassis," + 0;


        }

        public void Process_MemoryHook()
        {
            // CALCULATE YOUR OUTPUT VALUES, AS PER THE GAME
        }

        public void Process_MemoryMap()
        {
            // CALCULATE YOUR OUTPUT VALUES, AS PER THE GAME
        }

        /****************************************************************************
        *** VARIABLES
        ****************************************************************************/
        private string Dash_1_Output = "";
        private string Dash_2_Output = "";
        private string Dash_3_Output = "";
        private string Dash_4_Output = "";
        private string Dash_5_Output = "";
        private string Dash_6_Output = "";
        private string Dash_7_Output = "";
        private string Dash_8_Output = "";
        private string Dash_9_Output = "";
        private string Dash_10_Output = "";
        private string Dash_11_Output = "";
        private string Dash_12_Output = "";
        private string Dash_13_Output = "";
        private string Dash_14_Output = "";
        private string Dash_15_Output = "";
        private string Dash_16_Output = "";
        private string Dash_17_Output = "";
        private string Dash_18_Output = "";
        private string Dash_19_Output = "";
        private string Dash_20_Output = "";

        // Output Vars
        private double Roll_Output = 0;
        private double Pitch_Output = 0;
        private double Heave_Output = 0;
        private double Yaw_Output = 0;
        private double Sway_Output = 0;
        private double Surge_Output = 0;
        private double Extra1_Output = 0;
        private double Extra2_Output = 0;
        private double Extra3_Output = 0;

        // MemHook Vars
        private double Roll_MemHook = 0;
        private double Pitch_MemHook = 0;
        private double Heave_MemHook = 0;
        private double Yaw_MemHook = 0;
        private double Sway_MemHook = 0;
        private double Surge_MemHook = 0;
        private double Extra1_MemHook = 0;
        private double Extra2_MemHook = 0;
        private double Extra3_MemHook = 0;

        // MemMap Vars
        private double Roll_MemMap = 0;
        private double Pitch_MemMap = 0;
        private double Heave_MemMap = 0;
        private double Yaw_MemMap = 0;
        private double Sway_MemMap = 0;
        private double Surge_MemMap = 0;
        private double Extra1_MemMap = 0;
        private double Extra2_MemMap = 0;
        private double Extra3_MemMap = 0;

        // GameVibe Vars
        private string Vibe_1_Output = "";
        private string Vibe_2_Output = "";
        private string Vibe_3_Output = "";
        private string Vibe_4_Output = "";
        private string Vibe_5_Output = "";
        private string Vibe_6_Output = "";
        private string Vibe_7_Output = "";
        private string Vibe_8_Output = "";
        private string Vibe_9_Output = "";

        /****************************************************************************
        *** RESETS
        ****************************************************************************/
        public void ResetDashVars()
        {
            Dash_1_Output = "";
            Dash_2_Output = "";
            Dash_3_Output = "";
            Dash_4_Output = "";
            Dash_5_Output = "";
            Dash_6_Output = "";
            Dash_7_Output = "";
            Dash_8_Output = "";
            Dash_9_Output = "";
            Dash_10_Output = "";
            Dash_11_Output = "";
            Dash_12_Output = "";
            Dash_13_Output = "";
            Dash_14_Output = "";
            Dash_15_Output = "";
            Dash_16_Output = "";
            Dash_17_Output = "";
            Dash_18_Output = "";
            Dash_19_Output = "";
            Dash_20_Output = "";
        }

        public void ResetDOFVars()
        {
            Roll_Output = 0;
            Pitch_Output = 0;
            Heave_Output = 0;
            Yaw_Output = 0;
            Sway_Output = 0;
            Surge_Output = 0;
            Extra1_Output = 0;
            Extra2_Output = 0;
            Extra3_Output = 0;
        }

        public void ResetHookVars()
        {
            Roll_MemHook = 0;
            Pitch_MemHook = 0;
            Heave_MemHook = 0;
            Yaw_MemHook = 0;
            Sway_MemHook = 0;
            Surge_MemHook = 0;
            Extra1_MemHook = 0;
            Extra2_MemHook = 0;
            Extra3_MemHook = 0;
        }

        public void ResetMapVars()
        {
            Roll_MemMap = 0;
            Pitch_MemMap = 0;
            Heave_MemMap = 0;
            Yaw_MemMap = 0;
            Sway_MemMap = 0;
            Surge_MemMap = 0;
            Extra1_MemMap = 0;
            Extra2_MemMap = 0;
            Extra3_MemMap = 0;
        }

        public void ResetVibeVars()
        {
            Vibe_1_Output = "";
            Vibe_2_Output = "";
            Vibe_3_Output = "";
            Vibe_4_Output = "";
            Vibe_5_Output = "";
            Vibe_6_Output = "";
            Vibe_7_Output = "";
            Vibe_8_Output = "";
            Vibe_9_Output = "";
        }

        /****************************************************************************
        *** PROPERTIES
        ****************************************************************************/
        public string GameName => _gameName;
        public string ProcessName => _processName;
        public string PluginAuthorsName => _pluginAuthorsName;
        public string Port => _port;
        public string PluginOptions => _pluginOptions;
        public bool Enable_MemoryMap => _enable_MemoryMap;
        public bool Enable_DashBoard => _enable_DashBoard;
        public bool Enable_GameVibe => _enable_GameVibe;
        public bool Enable_MemoryHook => _enable_MemoryHook;
        public bool RequiresPatchingPath => _requiresPatchingPath;
        public bool RequiresSecondCheck => _requiresSecondCheck;

        /****************************************************************************
        *** GETTERS
        ****************************************************************************/
        public string Get_PluginVersion() => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public string GetDOFsUsed() => _DOF_Support_Roll.ToString() + ","
                + _DOF_Support_Pitch.ToString() + ","
                + _DOF_Support_Heave.ToString() + ","
                + _DOF_Support_Yaw.ToString() + ","
                + _DOF_Support_Sway.ToString() + ","
                + _DOF_Support_Surge.ToString() + ","
                + _DOF_Support_Extra1.ToString() + ","
                + _DOF_Support_Extra2.ToString() + ","
                + _DOF_Support_Extra3.ToString();

        public string Get_Dash1_Output() => Dash_1_Output;
        public string Get_Dash2_Output() => Dash_2_Output;
        public string Get_Dash3_Output() => Dash_3_Output;
        public string Get_Dash4_Output() => Dash_4_Output;
        public string Get_Dash5_Output() => Dash_5_Output;
        public string Get_Dash6_Output() => Dash_6_Output;
        public string Get_Dash7_Output() => Dash_7_Output;
        public string Get_Dash8_Output() => Dash_8_Output;
        public string Get_Dash9_Output() => Dash_9_Output;
        public string Get_Dash10_Output() => Dash_10_Output;
        public string Get_Dash11_Output() => Dash_11_Output;
        public string Get_Dash12_Output() => Dash_12_Output;
        public string Get_Dash13_Output() => Dash_13_Output;
        public string Get_Dash14_Output() => Dash_14_Output;
        public string Get_Dash15_Output() => Dash_15_Output;
        public string Get_Dash16_Output() => Dash_16_Output;
        public string Get_Dash17_Output() => Dash_17_Output;
        public string Get_Dash18_Output() => Dash_18_Output;
        public string Get_Dash19_Output() => Dash_19_Output;
        public string Get_Dash20_Output() => Dash_20_Output;
        public double Get_Extra1MemHook() => Extra1_MemHook;
        public double Get_Extra1MemMap() => Extra1_MemMap;
        public double Get_Extra1Output() => Extra1_Output;
        public double Get_Extra2MemHook() => Extra2_MemHook;
        public double Get_Extra2MemMap() => Extra2_MemMap;
        public double Get_Extra2Output() => Extra2_Output;
        public double Get_Extra3MemHook() => Extra3_MemHook;
        public double Get_Extra3MemMap() => Extra3_MemMap;
        public double Get_Extra3Output() => Extra3_Output;
        public double Get_HeaveMemHook() => Heave_MemHook;
        public double Get_HeaveMemMap() => Heave_MemMap;
        public double Get_HeaveOutput() => Heave_Output;
        public double Get_PitchMemHook() => Pitch_MemHook;
        public double Get_PitchMemMap() => Pitch_MemMap;
        public double Get_PitchOutput() => Pitch_Output;
        public double Get_RollMemHook() => Roll_MemHook;
        public double Get_RollMemMap() => Roll_MemMap;
        public double Get_RollOutput() => Roll_Output;
        public double Get_SurgeMemHook() => Surge_MemHook;
        public double Get_SurgeMemMap() => Surge_MemMap;
        public double Get_SurgeOutput() => Surge_Output;
        public double Get_SwayMemHook() => Sway_MemHook;
        public double Get_SwayMemMap() => Sway_MemMap;
        public double Get_SwayOutput() => Sway_Output;
        public string Get_Vibe1_Output() => Vibe_1_Output;
        public string Get_Vibe2_Output() => Vibe_2_Output;
        public string Get_Vibe3_Output() => Vibe_3_Output;
        public string Get_Vibe4_Output() => Vibe_4_Output;
        public string Get_Vibe5_Output() => Vibe_5_Output;
        public string Get_Vibe6_Output() => Vibe_6_Output;
        public string Get_Vibe7_Output() => Vibe_7_Output;
        public string Get_Vibe8_Output() => Vibe_8_Output;
        public string Get_Vibe9_Output() => Vibe_9_Output;
        public double Get_YawMemHook() => Yaw_MemHook;
        public double Get_YawMemMap() => Yaw_MemMap;
        public double Get_YawOutput() => Yaw_Output;
    }
}
